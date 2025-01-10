namespace FlatDictionary;
public partial class FlatDictionary<TKey, TValue>
{
	private static readonly NotSupportedException _keyValuePairException = new("FlatDictionary objects do not support the use of KeyValuePair methods.");

	[EditorBrowsable(EditorBrowsableState.Never)]
	void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => throw _keyValuePairException;

	[EditorBrowsable(EditorBrowsableState.Never)]
	void ICollection<KeyValuePair<TKey, TValue>>.Clear() => throw _keyValuePairException;

	[EditorBrowsable(EditorBrowsableState.Never)]
	bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => throw _keyValuePairException;

	[EditorBrowsable(EditorBrowsableState.Never)]
	void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => throw _keyValuePairException;

	[EditorBrowsable(EditorBrowsableState.Never)]
	IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => throw _keyValuePairException;

	[EditorBrowsable(EditorBrowsableState.Never)]
	bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => throw _keyValuePairException;
}
