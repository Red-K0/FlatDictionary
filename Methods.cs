namespace RedK0;
public partial class FlatDictionary<TKey> : IDictionary<TKey, object>
{
	/// <summary>
	/// Recursively traverses downwards through nested dictionaries, pulling up values as necessary.
	/// </summary>
	private void FlattenDictionary(KeyValuePair<TKey, object> pair)
	{
		TKey[] Keys = [default, default, default, default, default, default, default];
		Type DictionaryType = typeof(Dictionary<,>);
		int CurrentLayer = -1;

		PullUpValues(pair);

		void PullUpValues(KeyValuePair<TKey, object> pair)
		{
			Type valueType = pair.Value.GetType();

			if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == DictionaryType)
			{
				CurrentLayer++; Keys[CurrentLayer] = pair.Key;
				foreach (KeyValuePair<TKey, object> FoundPair in Unsafe.As<Dictionary<TKey, object>>(pair.Value)) PullUpValues(FoundPair);

#pragma warning disable IDE0059 // This is in fact, not unnecessary.
				CurrentLayer--;
#pragma warning restore IDE0059
			}
			else
			{
				_flattenedDictionary[(CurrentLayer + 1) switch
				{
					0 => (pair.Key, default, default, default, default, default, default, default),
					1 => (Keys[0], pair.Key, default, default, default, default, default, default),
					2 => (Keys[0], Keys[1], pair.Key, default, default, default, default, default),
					3 => (Keys[0], Keys[1], Keys[2], pair.Key, default, default, default, default),
					4 => (Keys[0], Keys[1], Keys[2], Keys[3], pair.Key, default, default, default),
					5 => (Keys[0], Keys[1], Keys[2], Keys[3], Keys[4], pair.Key, default, default),
					6 => (Keys[0], Keys[1], Keys[2], Keys[3], Keys[4], Keys[5], pair.Key, default),
					7 => (Keys[0], Keys[1], Keys[2], Keys[3], Keys[4], Keys[5], Keys[6], pair.Key),
					_ => throw new NotImplementedException("The nesting layer required is not supported.")
				}] = pair.Value;
			}
		}
	}

	#region Add(TKey[..], object)

	public void Add(TKey k1, object value) =>
		_flattenedDictionary.Add((k1, default, default, default, default, default, default, default), value);
	public void Add(TKey k1, TKey k2, object value) =>
		_flattenedDictionary.Add((k1, k2, default, default, default, default, default, default), value);
	public void Add(TKey k1, TKey k2, TKey k3, object value) =>
		_flattenedDictionary.Add((k1, k2, k3, default, default, default, default, default), value);
	public void Add(TKey k1, TKey k2, TKey k3, TKey k4, object value) =>
		_flattenedDictionary.Add((k1, k2, k3, k4, default, default, default, default), value);
	public void Add(TKey k1, TKey k2, TKey k3, TKey k4, TKey k5, object value) =>
		_flattenedDictionary.Add((k1, k2, k3, k4, k5, default, default, default), value);
	public void Add(TKey k1, TKey k2, TKey k3, TKey k4, TKey k5, TKey k6, object value) =>
		_flattenedDictionary.Add((k1, k2, k3, k4, k5, k6, default, default), value);
	public void Add(TKey k1, TKey k2, TKey k3, TKey k4, TKey k5, TKey k6, TKey k7, object value) =>
		_flattenedDictionary.Add((k1, k2, k3, k4, k5, k6, k7, default), value);
	public void Add(TKey k1, TKey k2, TKey k3, TKey k4, TKey k5, TKey k6, TKey k7, TKey k8, object value) =>
		_flattenedDictionary.Add((k1, k2, k3, k4, k5, k6, k7, k8), value);

	#endregion Add(TKey[..], object)

	#region this[]

	public object this[TKey k1]
	{
		get => _flattenedDictionary[(k1, default, default, default, default, default, default, default)];
		set => _flattenedDictionary[(k1, default, default, default, default, default, default, default)] = value;
	}
	public object this[TKey k1, TKey k2]
	{
		get => _flattenedDictionary[(k1, k2, default, default, default, default, default, default)];
		set => _flattenedDictionary[(k1, k2, default, default, default, default, default, default)] = value;
	}
	public object this[TKey k1, TKey k2, TKey k3]
	{
		get => _flattenedDictionary[(k1, k2, k3, default, default, default, default, default)];
		set => _flattenedDictionary[(k1, k2, k3, default, default, default, default, default)] = value;
	}
	public object this[TKey k1, TKey k2, TKey k3, TKey k4]
	{
		get => _flattenedDictionary[(k1, k2, k3, k4, default, default, default, default)];
		set => _flattenedDictionary[(k1, k2, k3, k4, default, default, default, default)] = value;
	}
	public object this[TKey k1, TKey k2, TKey k3, TKey k4, TKey k5]
	{
		get => _flattenedDictionary[(k1, k2, k3, k4, k5, default, default, default)];
		set => _flattenedDictionary[(k1, k2, k3, k4, k5, default, default, default)] = value;
	}
	public object this[TKey k1, TKey k2, TKey k3, TKey k4, TKey k5, TKey k6]
	{
		get => _flattenedDictionary[(k1, k2, k3, k4, k5, k6, default, default)];
		set => _flattenedDictionary[(k1, k2, k3, k4, k5, k6, default, default)] = value;
	}
	public object this[TKey k1, TKey k2, TKey k3, TKey k4, TKey k5, TKey k6, TKey k7]
	{
		get => _flattenedDictionary[(k1, k2, k3, k4, k5, k6, k7, default)];
		set => _flattenedDictionary[(k1, k2, k3, k4, k5, k6, k7, default)] = value;
	}
	public object this[TKey k1, TKey k2, TKey k3, TKey k4, TKey k5, TKey k6, TKey k7, TKey k8]
	{
		get => _flattenedDictionary[(k1, k2, k3, k4, k5, k6, k7, k8)];
		set => _flattenedDictionary[(k1, k2, k3, k4, k5, k6, k7, k8)] = value;
	}

	#endregion
}