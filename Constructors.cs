namespace RedK0; using System.Numerics;
public partial class Flat<TKey> where TKey : unmanaged, IAdditionOperators<TKey, TKey, TKey>
{
	readonly Dictionary<(TKey, TKey, TKey, TKey, TKey, TKey, TKey, TKey), object> _flattenedDictionary = [];

	public Flat(Dictionary<TKey, object> dict)
	{
		ArgumentNullException.ThrowIfNull(dict);

		foreach (KeyValuePair<TKey, object> pair in dict)
		{
			FlattenDictionary(pair);
		}
	}
}