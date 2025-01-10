using System.Collections;

namespace FlatDictionary;
public partial class FlatDictionary<TKey, TValue> : IDictionary
{
	private static readonly ArgumentException _keyException = new("The value being assigned must be of type 'TValue', and not null.");

	private static readonly ArgumentException _valueException = new("The keys passed to an IDictionary object cast from FlatDictionary must be a hash of type ulong.");

	bool IDictionary.IsFixedSize => false;
	bool ICollection.IsSynchronized => false;
	object ICollection.SyncRoot => this;

	[EditorBrowsable(EditorBrowsableState.Never)]
	ICollection IDictionary.Keys => (ICollection)Keys;
	ICollection IDictionary.Values => (ICollection)Values;

	object? IDictionary.this[object key]
	{
		get
		{
			if (key is not ulong hash) throw _keyException;

			TryGetKeyIndex(hash, out int index);

			return _pairs[index].Value;
		}
		set
		{
			if (key is not ulong hash) throw _keyException;

			if (!TryGetKeyIndex(hash, out int index) || !(value is TValue tValue and not null)) throw _valueException;

			_pairs[index] = new(_pairs[index].Key, tValue);
		}
	}

	void IDictionary.Add(object key, object? value)
	{
		if (key is not ulong hash) throw _keyException;

		if (value is not TValue tValue || value is null) throw _valueException;

		_pairs.Add(new(hash, tValue));
	}
	bool IDictionary.Contains(object key)
	{
		if (key is not ulong hash) throw _keyException;

		return TryGetKeyIndex(hash, out _);
	}
	void ICollection.CopyTo(Array array, int index)
	{
		if (array is not KeyValuePair<ulong, TValue>[] targetArray) throw new ArgumentException("The target array is not of the correct type.");

		_pairs.CopyTo(targetArray, 0);
	}
	IDictionaryEnumerator IDictionary.GetEnumerator() => new FlatDictionaryEnumerator(_pairs);
	void IDictionary.Remove(object key)
	{
		if (key is not ulong hash) throw _keyException;

		TryGetKeyIndex(hash, out int index);

		_pairs.RemoveAt(index);
	}

	private class FlatDictionaryEnumerator(IEnumerable<KeyValuePair<ulong, TValue>> pairs) : IDictionaryEnumerator
	{
		private readonly IEnumerator<KeyValuePair<ulong, TValue>> _enumerator = pairs.GetEnumerator();
		public object Key => _enumerator.Current.Key;
		public object? Value => _enumerator.Current.Value;
		public DictionaryEntry Entry => new(Key, Value);
		public bool MoveNext() => _enumerator.MoveNext();
		public void Reset() => _enumerator.Reset();
		public object Current => Entry;
	}
}
