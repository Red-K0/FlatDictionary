using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Collections;

namespace FlatDictionary;

[Serializable]
public class FlatDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IReadOnlyDictionary<TKey, TValue>, ISerializable, IDeserializationCallback
where TKey : unmanaged
{
	#region Fields

	private readonly List<KeyValuePair<ulong, TValue>> _pairs = new(50);

	private readonly Type _keyType = typeof(TKey);

	private readonly Type _valueType = typeof(TValue);

	private readonly List<TKey> _nestedKeys = new(5);

	public static readonly bool IsReadOnly;

	#endregion Fields

	#region Properties
	public int Count => _pairs.Count;
	public int Capacity => _pairs.Capacity;
	private ICollection<TKey> Keys => throw new NotSupportedException("FlatDictionary objects do not support acquiring key collections.");
	private ICollection<TValue> Values
	{
		get
		{
			TValue[] Values = new TValue[_pairs.Count];

			for (int i = 0; i < Values.Length; i++) Values[i] = _pairs[i].Value;

			return Values;
		}
	}

	#endregion Properties

	#region Key Handling

	private readonly UInt128 _seed = new((ulong)DateTime.Now.Ticks, (ulong)DateTime.UtcNow.Ticks);
	private ulong GetHash(ReadOnlySpan<TKey> keys) => GxHash.GxHash.HashU64(MemoryMarshal.AsBytes(keys), _seed);
	private ulong GetHash(TKey key) => GxHash.GxHash.HashU64(MemoryMarshal.AsBytes(new ReadOnlySpan<TKey>(ref key)), _seed);
	private bool TryGetKeyIndex(ulong hash, out int index)
	{
		for (int i = 0; i < _pairs.Count; i++)
		{
			if (_pairs[i].Key == hash)
			{
				index = i;
				return true;
			}
		}

		index = -1;
		return false;
	}
	private bool TryGetKeyIndex(TKey key, out int index)
	{
		ulong hash = GetHash(key);

		for (int i = 0; i < _pairs.Count; i++)
		{
			if (_pairs[i].Key == hash)
			{
				index = i;
				return true;
			}
		}

		index = -1;
		return false;
	}
	private bool TryGetKeyIndex(ReadOnlySpan<TKey> keys, out int index)
	{
		ulong hash = GetHash(keys);

		for (int i = 0; i < _pairs.Count; i++)
		{
			if (_pairs[i].Key == hash)
			{
				index = i;
				return true;
			}
		}

		index = -1;
		return false;
	}

	#endregion Key Handling

	#region Exceptions

	private static readonly ArgumentException _keyException = new("The value being assigned must be of type 'TValue', and not null.");
	private static readonly ArgumentException _valueException = new("The keys passed to an IDictionary object cast from FlatDictionary must be a hash of type ulong.");
	private static readonly NotSupportedException _keyValuePairException = new("FlatDictionary objects do not support the use of KeyValuePair methods.");

	#endregion Exceptions

	#region IDictionary<TKey, TValue>

	[EditorBrowsable(EditorBrowsableState.Never)]
	ICollection<TKey> IDictionary<TKey, TValue>.Keys => Keys;
	ICollection<TValue> IDictionary<TKey, TValue>.Values => Values;

	#endregion IDictionary IDictionary<TKey, TValue>

	#region IDictionary

	bool IDictionary.IsReadOnly => IsReadOnly;
	bool IDictionary.IsFixedSize => false;
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
	IDictionaryEnumerator IDictionary.GetEnumerator() => new FlatDictionaryEnumerator(_pairs);
	void IDictionary.Remove(object key)
	{
		if (key is not ulong hash) throw _keyException;

		TryGetKeyIndex(hash, out int index);

		_pairs.RemoveAt(index);
	}

	#endregion IDictionary

	#region IReadOnlyDictionary<TKey, TValue>

	[EditorBrowsable(EditorBrowsableState.Never)]
	IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => Keys;
	IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => Values;
	TValue IReadOnlyDictionary<TKey, TValue>.this[TKey key] => this[key];
	bool IReadOnlyDictionary<TKey, TValue>.ContainsKey(TKey key) => ContainsKey(key);
	bool IReadOnlyDictionary<TKey, TValue>.TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) => TryGetValue(key, out value);

	#endregion IReadOnlyDictionary<TKey, TValue>

	#region ICollection

	int ICollection.Count => Count;
	bool ICollection.IsSynchronized => false;
	object ICollection.SyncRoot => this;
	void ICollection.CopyTo(Array array, int index)
	{
		if (array is not KeyValuePair<ulong, TValue>[] targetArray) throw new ArgumentException("The target array is not of the correct type.");

		_pairs.CopyTo(targetArray, 0);
	}


	#endregion ICollection

	#region ICollection<KeyValuePair<TKey, TValue>>

	int ICollection<KeyValuePair<TKey, TValue>>.Count => throw _keyValuePairException;
	bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => throw _keyValuePairException;
	void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => throw _keyValuePairException;
	void ICollection<KeyValuePair<TKey, TValue>>.Clear() => throw _keyValuePairException;
	bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => throw _keyValuePairException;
	void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => throw _keyValuePairException;
	IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => throw _keyValuePairException;
	bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => throw _keyValuePairException;
	int IReadOnlyCollection<KeyValuePair<TKey, TValue>>.Count => throw _keyValuePairException;

	#endregion ICollection<KeyValuePair<TKey, TValue>>

	#region Serialization

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.")]
	public FlatDictionary(SerializationInfo info, StreamingContext context)
	{
		_seed = new(info.GetUInt64("_seedH"), info.GetUInt64("_seedL"));
		_pairs = (List<KeyValuePair<ulong, TValue>>)info.GetValue("_pairs", typeof(List<KeyValuePair<ulong, TValue>>))!;
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.")]
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("_seedH", _seed >> 64); info.AddValue("_seedL", _seed);

		info.AddValue("_pairs", _pairs, typeof(List<KeyValuePair<ulong, TValue>>));
	}
	public void OnDeserialization(object? sender) { }

	#endregion Serialization

	#region Constructors

	public FlatDictionary() { }
	public FlatDictionary(IDictionary dictionary)
	{
		ArgumentNullException.ThrowIfNull(dictionary);

		AddDictionary(dictionary);
	}

	#endregion Constructors

	#region Methods
	public TValue this[TKey key]
	{
		get
		{
			if (TryGetKeyIndex(key, out int index)) return _pairs[index].Value;
			throw new KeyNotFoundException($"The given key '{key}' was not found in the dictionary.");
		}
		set
		{
			if (TryGetKeyIndex(key, out int index))
			{
				_pairs[index] = new(_pairs[index].Key, value);
			}
			else
			{
				_pairs.Add(new(GetHash(key), value));
			}
		}
	}
	public TValue this[ReadOnlySpan<TKey> keys]
	{
		get
		{
			if (TryGetKeyIndex(keys, out int index)) return _pairs[index].Value;
			throw new KeyNotFoundException("The given key set was not found in the dictionary.");
		}
		set
		{
			if (TryGetKeyIndex(keys, out int index))
			{
				_pairs[index] = new(_pairs[index].Key, value);
			}
			else
			{
				_pairs.Add(new(GetHash(keys), value));
			}
		}
	}
	public void Add(TKey key, TValue value) => _pairs.Add(new(GetHash(key), value));
	public void Add(ReadOnlySpan<TKey> keys, TValue value) => _pairs.Add(new(GetHash(keys), value));
	public void AddDictionary(IDictionary dictionary, ReadOnlySpan<TKey> baseKeys)
	{
		_nestedKeys.AddRange(baseKeys);

		AddDictionary(dictionary);

		_nestedKeys.Clear();
	}
	private void AddDictionary(IDictionary dictionary)
	{
		IEnumerator Enumerator = dictionary.Keys.GetEnumerator();
		Enumerator.MoveNext();

		Enumerator = dictionary.Values.GetEnumerator();
		Enumerator.MoveNext();

		int EntryCount;

		switch (FindObjectType(Enumerator.Current, true))
		{
			case TypeStatus.Value:
				EntryCount = dictionary.Count;

				_pairs.EnsureCapacity(_pairs.Count + EntryCount);

				foreach (DictionaryEntry entry in dictionary) Add(entry.Key, entry.Value!);

				break;

			case TypeStatus.Dictionary:
				foreach (DictionaryEntry entry in dictionary)
				{
					IDictionary EntryDictionary = (IDictionary)entry.Value!;

					EntryCount = EntryDictionary.Count;

					_nestedKeys.Add((TKey)entry.Key);

					_pairs.EnsureCapacity(_pairs.Count + EntryCount);

					foreach (DictionaryEntry subEntry in EntryDictionary) Add(subEntry.Key, subEntry.Value!);

					_nestedKeys.RemoveAt(_nestedKeys.Count - 1);
				}
				break;

			case TypeStatus.NestedDictionary:
				foreach (DictionaryEntry entry in dictionary)
				{
					_nestedKeys.Add((TKey)entry.Key);

					AddDictionary((IDictionary)entry.Value!);

					_nestedKeys.RemoveAt(_nestedKeys.Count - 1);
				}
				break;

			case TypeStatus.Invalid:
			default: throw new ArgumentException($"A value or key in the provided dictionary was invalid. Please ensure all keys are of the provided key type: {_keyType.Name}, and all values are either dictionaries matching the appropriate types, or of provided value type: {_valueType.Name}");
		}

		void Add(object key, object value) => _pairs.Add(new(GetHash([.. _nestedKeys.Append((TKey)key)]), (TValue)value));
	}
	public void Clear() => _pairs.Clear();
	public bool ContainsKey(TKey key)
	{
		ulong hash = GetHash(key);

		foreach (KeyValuePair<ulong, TValue> pair in _pairs)
		{
			if (pair.Key == hash) return true;
		}

		return false;
	}
	public bool ContainsKey(ReadOnlySpan<TKey> keys)
	{
		ulong hash = GetHash(keys);

		foreach (KeyValuePair<ulong, TValue> pair in _pairs)
		{
			if (pair.Key == hash) return true;
		}

		return false;
	}
	public bool ContainsValue(TValue value)
	{
		foreach (KeyValuePair<ulong, TValue> pair in _pairs)
		{
			if (EqualityComparer<TValue>.Default.Equals(pair.Value, value)) return true;
		}
		return false;
	}
	public int EnsureCapacity(int capacity) => _pairs.EnsureCapacity(capacity);
	public IEnumerator GetEnumerator() => _pairs.GetEnumerator();
	private TypeStatus FindObjectType(object o, bool isType)
	{
		Type Type = isType ? o.GetType() : (Type)o;

		if (Type == _valueType) return TypeStatus.Value;

		if (Type.GenericTypeArguments.Length == 0 || Type.GenericTypeArguments[0] != _keyType) return TypeStatus.Invalid;

		return FindObjectType(Type.GenericTypeArguments[1], false) switch
		{
			TypeStatus.Dictionary or TypeStatus.NestedDictionary => TypeStatus.NestedDictionary,
			TypeStatus.Value => TypeStatus.Dictionary,
			TypeStatus.Invalid or _ => TypeStatus.Invalid,
		};
	}
	public bool Remove(TKey key)
	{
		if (TryGetKeyIndex(key, out int index))
		{
			_pairs.RemoveAt(index);
			return true;
		}
		return false;
	}
	public bool Remove(ReadOnlySpan<TKey> keys)
	{
		if (TryGetKeyIndex(keys, out int index))
		{
			_pairs.RemoveAt(index);
			return true;
		}
		return false;
	}
	public void TrimExcess() => _pairs.TrimExcess();
	public void TrimExcess(int capacity) => _pairs.Capacity = capacity;
	public bool TryAdd(ReadOnlySpan<TKey> keys, TValue value)
	{
		ulong hash = GetHash(keys);

		if (TryGetKeyIndex(hash, out int _)) return false;

		_pairs.Add(new(hash, value));

		return true;
	}
	public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
	{
		if (TryGetKeyIndex(key, out int index))
		{
			value = _pairs[index].Value;
			return true;
		}
		value = default;
		return false;
	}
	public bool TryGetValue(ReadOnlySpan<TKey> keys, [MaybeNullWhen(false)] out TValue value)
	{
		if (TryGetKeyIndex(keys, out int index))
		{
			value = _pairs[index].Value;
			return true;
		}
		value = default;
		return false;
	}

	#endregion Methods
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
	private enum TypeStatus
	{
		Value = 0,
		Dictionary = 1,
		NestedDictionary = 2,
		Invalid = -1,
	}
}
