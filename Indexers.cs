namespace RedK0;
public partial class Flat<TKey>
{
	public object this[TKey k1] => _flattenedDictionary[(k1, default, default, default, default, default, default, default)];
	public object this[TKey k1, TKey k2] => _flattenedDictionary[(k1, k2, default, default, default, default, default, default)];
	public object this[TKey k1, TKey k2, TKey k3] => _flattenedDictionary[(k1, k2, k3, default, default, default, default, default)];
	public object this[TKey k1, TKey k2, TKey k3, TKey k4] => _flattenedDictionary[(k1, k2, k3, k4, default, default, default, default)];
	public object this[TKey k1, TKey k2, TKey k3, TKey k4, TKey k5] => _flattenedDictionary[(k1, k2, k3, k4, k5, default, default, default)];
	public object this[TKey k1, TKey k2, TKey k3, TKey k4, TKey k5, TKey k6] => _flattenedDictionary[(k1, k2, k3, k4, k5, k6, default, default)];
	public object this[TKey k1, TKey k2, TKey k3, TKey k4, TKey k5, TKey k6, TKey k7] => _flattenedDictionary[(k1, k2, k3, k4, k5, k6, k7, default)];
	public object this[TKey k1, TKey k2, TKey k3, TKey k4, TKey k5, TKey k6, TKey k7, TKey k8] => _flattenedDictionary[(k1, k2, k3, k4, k5, k6, k7, k8)];
}