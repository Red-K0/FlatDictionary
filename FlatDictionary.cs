using System.Collections;
using System.Runtime.Serialization;
using System.Text;

namespace FlatDictionary;
public partial class FlatDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IReadOnlyDictionary<TKey, TValue>, ISerializable, IDeserializationCallback where TKey : notnull
{
	#region Private Fields

	private readonly Type _keyType = typeof(TKey), _valueType = typeof(TValue);
	private readonly Dictionary<string, int> _keys = new(50);
	private readonly List<TValue> _values = new(50);

	private readonly Func<TKey, string> _keyConverter = (TKey key) => key.ToString()!;
	private readonly StringBuilder _keyBuilder = new();

	#endregion Private Fields

	#region Constructors

	public FlatDictionary() { }
	public FlatDictionary(Func<TKey, string> keyConverter) => _keyConverter = keyConverter;
	public FlatDictionary(IDictionary dictionary, Func<TKey, string>? keyConverter = null)
	{
		ArgumentNullException.ThrowIfNull(dictionary);

		if (keyConverter != null) _keyConverter = keyConverter;

		WalkDictionary(dictionary, true);

		_keyBuilder.Clear();
	}

	#endregion Constructors

	#region Properties

	public int Count { get; private set; }

	TValue IReadOnlyDictionary<TKey, TValue>.this[TKey key] => throw new NotImplementedException();
	ICollection<TKey> IDictionary<TKey, TValue>.Keys => throw new NotImplementedException();
	ICollection IDictionary.Keys => throw new NotImplementedException();
	IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => throw new NotImplementedException();
	ICollection<TValue> IDictionary<TKey, TValue>.Values => throw new NotImplementedException();
	ICollection IDictionary.Values => throw new NotImplementedException();
	IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => throw new NotImplementedException();
	int ICollection<KeyValuePair<TKey, TValue>>.Count => throw new NotImplementedException();
	int IReadOnlyCollection<KeyValuePair<TKey, TValue>>.Count => throw new NotImplementedException();
	bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => throw new NotImplementedException();
	bool IDictionary.IsReadOnly => throw new NotImplementedException();
	bool IDictionary.IsFixedSize => throw new NotImplementedException();
	bool ICollection.IsSynchronized => throw new NotImplementedException();
	object ICollection.SyncRoot => throw new NotImplementedException();

	#endregion Properties

	public bool ContainsKey(params ReadOnlySpan<TKey> keys)
	{
		_keyBuilder.Clear();

		foreach (TKey key in keys) _keyBuilder.Append(_keyConverter.Invoke(key));

		return _keys.ContainsKey(_keyBuilder.ToString());
	}

	TValue IDictionary<TKey, TValue>.this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	object? IDictionary.this[object key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

	void IDictionary<TKey, TValue>.Add(TKey key, TValue value) => throw new NotImplementedException();
	void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => throw new NotImplementedException();
	void IDictionary.Add(object key, object? value) => throw new NotImplementedException();
	void ICollection<KeyValuePair<TKey, TValue>>.Clear() => throw new NotImplementedException();
	void IDictionary.Clear() => throw new NotImplementedException();
	bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => throw new NotImplementedException();
	bool IDictionary.Contains(object key) => throw new NotImplementedException();
	bool IDictionary<TKey, TValue>.ContainsKey(TKey key) => throw new NotImplementedException();
	bool IReadOnlyDictionary<TKey, TValue>.ContainsKey(TKey key) => throw new NotImplementedException();
	void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => throw new NotImplementedException();
	void ICollection.CopyTo(Array array, int index) => throw new NotImplementedException();
	IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => throw new NotImplementedException();
	IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
	IDictionaryEnumerator IDictionary.GetEnumerator() => throw new NotImplementedException();
	void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) => throw new NotImplementedException();
	void IDeserializationCallback.OnDeserialization(object? sender) => throw new NotImplementedException();
	bool IDictionary<TKey, TValue>.Remove(TKey key) => throw new NotImplementedException();
	bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => throw new NotImplementedException();
	void IDictionary.Remove(object key) => throw new NotImplementedException();
	bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value) => throw new NotImplementedException();
	bool IReadOnlyDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value) => throw new NotImplementedException();
}
