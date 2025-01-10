namespace FlatDictionary;
public partial class FlatDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => Keys;
	IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => Values;
}
