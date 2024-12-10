namespace RedK0;
public partial class FlatDictionary<K>
{
	public static Dictionary<K, object> CastToObjectDictionary(object dictionary) => Unsafe.As<Dictionary<K, object>>(dictionary);
}