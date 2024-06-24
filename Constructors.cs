namespace RedK0;
public partial class FlatDictionary<TKey> where TKey : unmanaged, IAdditionOperators<TKey, TKey, TKey>
{
	readonly Dictionary<(TKey, TKey, TKey, TKey, TKey, TKey, TKey, TKey), object> _flattenedDictionary = [];

	public FlatDictionary(Dictionary<TKey, object> dict)
	{
		ArgumentNullException.ThrowIfNull(dict);

		foreach (KeyValuePair<TKey, object> pair in dict)
		{
			FlattenDictionary(pair);
		}
	}
}