namespace RedK0;
public partial class FlatDictionary<K>
{
	#region this[K..]

	/// <summary>Gets or sets the element with the specified base layer key.</summary>
	/// <returns>The element at the specified base layer key.</returns>
	///<inheritdoc cref="this[K,K,K,K,K,K,K,K]"/>
	public object this[K k1]
	{
		get => _dictionary[(k1, default, default, default, default, default, default, default)];
		set => _dictionary[(k1, default, default, default, default, default, default, default)] = value;
	}

	/// <inheritdoc cref="this[K,K,K,K,K,K,K,K]"/>
	public object this[K k1, K k2]
	{
		get => _dictionary[(k1, k2, default, default, default, default, default, default)];
		set => _dictionary[(k1, k2, default, default, default, default, default, default)] = value;
	}

	/// <inheritdoc cref="this[K,K,K,K,K,K,K,K]"/>
	public object this[K k1, K k2, K k3]
	{
		get => _dictionary[(k1, k2, k3, default, default, default, default, default)];
		set => _dictionary[(k1, k2, k3, default, default, default, default, default)] = value;
	///<inheritdoc cref="this[K,K,K,K,K,K,K,K]"/>
	}

	/// <inheritdoc cref="this[K,K,K,K,K,K,K,K]"/>
	public object this[K k1, K k2, K k3, K k4]
	{
		get => _dictionary[(k1, k2, k3, k4, default, default, default, default)];
		set => _dictionary[(k1, k2, k3, k4, default, default, default, default)] = value;
	}

	/// <inheritdoc cref="this[K,K,K,K,K,K,K,K]"/>
	public object this[K k1, K k2, K k3, K k4, K k5]
	{
		get => _dictionary[(k1, k2, k3, k4, k5, default, default, default)];
		set => _dictionary[(k1, k2, k3, k4, k5, default, default, default)] = value;
	}

	/// <inheritdoc cref="this[K,K,K,K,K,K,K,K]"/>
	public object this[K k1, K k2, K k3, K k4, K k5, K k6]
	{
		get => _dictionary[(k1, k2, k3, k4, k5, k6, default, default)];
		set => _dictionary[(k1, k2, k3, k4, k5, k6, default, default)] = value;
	}

	/// <inheritdoc cref="this[K,K,K,K,K,K,K,K]"/>
	public object this[K k1, K k2, K k3, K k4, K k5, K k6, K k7]
	{
		get => _dictionary[(k1, k2, k3, k4, k5, k6, k7, default)];
		set => _dictionary[(k1, k2, k3, k4, k5, k6, k7, default)] = value;
	}

	/// <summary>Gets or sets the element with the specified set of keys.</summary>
	/// <param name="k1">The key of the element to get or set, relative to the base layer.</param>
	/// <param name="k2">The key of the element to get or set, relative to the second layer.</param>
	/// <param name="k3">The key of the element to get or set, relative to the third layer.</param>
	/// <param name="k4">The key of the element to get or set, relative to the fourth layer.</param>
	/// <param name="k5">The key of the element to get or set, relative to the fifth layer.</param>
	/// <param name="k6">The key of the element to get or set, relative to the sixth layer.</param>
	/// <param name="k7">The key of the element to get or set, relative to the seventh layer.</param>
	/// <param name="k8">The key of the element to get or set, relative to the eighth layer.</param>
	/// <returns>The element at the specified set of keys.</returns>
	public object this[K k1, K k2, K k3, K k4, K k5, K k6, K k7, K k8]
	{
		get => _dictionary[(k1, k2, k3, k4, k5, k6, k7, k8)];
		set => _dictionary[(k1, k2, k3, k4, k5, k6, k7, k8)] = value;
	}

	#endregion this[]

	#region Add(K.., object)

	/// <summary>Adds an element with the specified base layer key and value to the <see cref="FlatDictionary{K}"/>.</summary>
	/// <inheritdoc cref="Add(K, K, K, K, K, K, K, K, object)"/>
	public void Add(K k1, object value) => _dictionary.Add((k1, default, default, default, default, default, default, default), value);

	/// <inheritdoc cref="Add(K, K, K, K, K, K, K, K, object)"/>
	public void Add(K k1, K k2, object value) => _dictionary.Add((k1, k2, default, default, default, default, default, default), value);

	/// <inheritdoc cref="Add(K, K, K, K, K, K, K, K, object)"/>
	public void Add(K k1, K k2, K k3, object value) => _dictionary.Add((k1, k2, k3, default, default, default, default, default), value);

	/// <inheritdoc cref="Add(K, K, K, K, K, K, K, K, object)"/>
	public void Add(K k1, K k2, K k3, K k4, object value) => _dictionary.Add((k1, k2, k3, k4, default, default, default, default), value);

	/// <inheritdoc cref="Add(K, K, K, K, K, K, K, K, object)"/>
	public void Add(K k1, K k2, K k3, K k4, K k5, object value) => _dictionary.Add((k1, k2, k3, k4, k5, default, default, default), value);

	/// <inheritdoc cref="Add(K, K, K, K, K, K, K, K, object)"/>
	public void Add(K k1, K k2, K k3, K k4, K k5, K k6, object value) => _dictionary.Add((k1, k2, k3, k4, k5, k6, default, default), value);

	/// <inheritdoc cref="Add(K, K, K, K, K, K, K, K, object)"/>
	public void Add(K k1, K k2, K k3, K k4, K k5, K k6, K k7, object value) => _dictionary.Add((k1, k2, k3, k4, k5, k6, k7, default), value);

	/// <summary>Adds an element with the specified set of keys and value to the <see cref="FlatDictionary{K}"/>.</summary>
	/// <param name="k1">The key of the element to add, relative to the base layer.</param>
	/// <param name="k2">The key of the element to add, relative to the second layer.</param>
	/// <param name="k3">The key of the element to add, relative to the third layer.</param>
	/// <param name="k4">The key of the element to add, relative to the fourth layer.</param>
	/// <param name="k5">The key of the element to add, relative to the fifth layer.</param>
	/// <param name="k6">The key of the element to add, relative to the sixth layer.</param>
	/// <param name="k7">The key of the element to add, relative to the seventh layer.</param>
	/// <param name="k8">The key of the element to add, relative to the eighth layer.</param>
	/// <param name="value">The value of the element to add.</param>
	public void Add(K k1, K k2, K k3, K k4, K k5, K k6, K k7, K k8, object value) => _dictionary.Add((k1, k2, k3, k4, k5, k6, k7, k8), value);

	#endregion Add(K.., object)

	#region Contains(k.., object)

	/// <summary>Determines whether the <see cref="FlatDictionary{K}"/> contains an entry with the specified key and value. </summary>
	/// <inheritdoc cref="Contains(K, K, K, K, K, K, K, K, object)"/>
	public bool Contains(K k1, object value) => Contains(new((k1, default, default, default, default, default, default, default), value));

	/// <inheritdoc cref="Contains(K, K, K, K, K, K, K, K, object)"/>
	public bool Contains(K k1, K k2, object value) => Contains(new((k1, k2, default, default, default, default, default, default), value));

	/// <inheritdoc cref="Contains(K, K, K, K, K, K, K, K, object)"/>
	public bool Contains(K k1, K k2, K k3, object value) => Contains(new((k1, k2, k3, default, default, default, default, default), value));

	/// <inheritdoc cref="Contains(K, K, K, K, K, K, K, K, object)"/>
	public bool Contains(K k1, K k2, K k3, K k4, object value) => Contains(new((k1, k2, k3, k4, default, default, default, default), value));

	/// <inheritdoc cref="Contains(K, K, K, K, K, K, K, K, object)"/>
	public bool Contains(K k1, K k2, K k3, K k4, K k5, object value) => Contains(new((k1, k2, k3, k4, k5, default, default, default), value));

	/// <inheritdoc cref="Contains(K, K, K, K, K, K, K, K, object)"/>
	public bool Contains(K k1, K k2, K k3, K k4, K k5, K k6, object value) => Contains(new((k1, k2, k3, k4, k5, k6, default, default), value));

	/// <inheritdoc cref="Contains(K, K, K, K, K, K, K, K, object)"/>
	public bool Contains(K k1, K k2, K k3, K k4, K k5, K k6, K k7, object value) => Contains(new((k1, k2, k3, k4, k5, k6, k7, default), value));

	/// <summary>Determines whether the <see cref="FlatDictionary{K}"/> contains an entry with the specified set of keys and value. </summary>
	/// <param name="k1">The key of the element to determine the presence of, relative to the base layer.</param>
	/// <param name="k2">The key of the element to determine the presence of, relative to the second layer.</param>
	/// <param name="k3">The key of the element to determine the presence of, relative to the third layer.</param>
	/// <param name="k4">The key of the element to determine the presence of, relative to the fourth layer.</param>
	/// <param name="k5">The key of the element to determine the presence of, relative to the fifth layer.</param>
	/// <param name="k6">The key of the element to determine the presence of, relative to the sixth layer.</param>
	/// <param name="k7">The key of the element to determine the presence of, relative to the seventh layer.</param>
	/// <param name="k8">The key of the element to determine the presence of, relative to the eighth layer.</param>
	/// <param name="value">The value of the element to determine the presence of.</param>
	/// <returns> <see langword="true"/> if the entry is found in the <see cref="FlatDictionary{K}"/>; otherwise <see langword="false"/>. </returns>
	public bool Contains(K k1, K k2, K k3, K k4, K k5, K k6, K k7, K k8, object value) => Contains(new((k1, k2, k3, k4, k5, k6, k7, k8), value));

	#endregion Contains(k.., object)

	#region ContainsKey(K..)

	/// <summary>Determines whether the <see cref="FlatDictionary{K}"/> contains an element with the specified base layer key.</summary>
	/// <inheritdoc cref="ContainsKey(K, K, K, K, K, K, K, K)"/>
	public bool ContainsKey(K k1) => _dictionary.ContainsKey((k1, default, default, default, default, default, default, default));

	/// <inheritdoc cref="ContainsKey(K, K, K, K, K, K, K, K)"/>
	public bool ContainsKey(K k1, K k2) => _dictionary.ContainsKey((k1, k2, default, default, default, default, default, default));

	/// <inheritdoc cref="ContainsKey(K, K, K, K, K, K, K, K)"/>
	public bool ContainsKey(K k1, K k2, K k3) => _dictionary.ContainsKey((k1, k2, k3, default, default, default, default, default));

	/// <inheritdoc cref="ContainsKey(K, K, K, K, K, K, K, K)"/>
	public bool ContainsKey(K k1, K k2, K k3, K k4) => _dictionary.ContainsKey((k1, k2, k3, k4, default, default, default, default));

	/// <inheritdoc cref="ContainsKey(K, K, K, K, K, K, K, K)"/>
	public bool ContainsKey(K k1, K k2, K k3, K k4, K k5) => _dictionary.ContainsKey((k1, k2, k3, k4, k5, default, default, default));

	/// <inheritdoc cref="ContainsKey(K, K, K, K, K, K, K, K)"/>
	public bool ContainsKey(K k1, K k2, K k3, K k4, K k5, K k6) => _dictionary.ContainsKey((k1, k2, k3, k4, k5, k6, default, default));

	/// <inheritdoc cref="ContainsKey(K, K, K, K, K, K, K, K)"/>
	public bool ContainsKey(K k1, K k2, K k3, K k4, K k5, K k6, K k7) => _dictionary.ContainsKey((k1, k2, k3, k4, k5, k6, k7, default));

	/// <summary>Determines whether the <see cref="FlatDictionary{K}"/> contains an element with the specified set of keys.</summary>
	/// <param name="k1">The key of the element to locate, relative to the base layer.</param>
	/// <param name="k2">The key of the element to locate, relative to the second layer.</param>
	/// <param name="k3">The key of the element to locate, relative to the third layer.</param>
	/// <param name="k4">The key of the element to locate, relative to the fourth layer.</param>
	/// <param name="k5">The key of the element to locate, relative to the fifth layer.</param>
	/// <param name="k6">The key of the element to locate, relative to the sixth layer.</param>
	/// <param name="k7">The key of the element to locate, relative to the seventh layer.</param>
	/// <param name="k8">The key of the element to locate, relative to the eighth layer.</param>
	/// <returns><see langword="true"/> if the <see cref="FlatDictionary{K}"/> contains an element with the specified set of keys; otherwise, <see langword="false"/>.</returns>
	public bool ContainsKey(K k1, K k2, K k3, K k4, K k5, K k6, K k7, K k8) => _dictionary.ContainsKey((k1, k2, k3, k4, k5, k6, k7, k8));

	#endregion ContainsKey(K..)

	#region Remove(K..)

	/// <summary>Removes the element with the specified base layer key from the <see cref="FlatDictionary{K}"/>.</summary>
	/// <inheritdoc cref="Remove(K, K, K, K, K, K, K, K)"/>
	public bool Remove(K k1) => _dictionary.Remove((k1, default, default, default, default, default, default, default));

	/// <inheritdoc cref="Remove(K, K, K, K, K, K, K, K)"/>
	public bool Remove(K k1, K k2) => _dictionary.Remove((k1, k2, default, default, default, default, default, default));

	/// <inheritdoc cref="Remove(K, K, K, K, K, K, K, K)"/>
	public bool Remove(K k1, K k2, K k3) => _dictionary.Remove((k1, k2, k3, default, default, default, default, default));

	/// <inheritdoc cref="Remove(K, K, K, K, K, K, K, K)"/>
	public bool Remove(K k1, K k2, K k3, K k4) => _dictionary.Remove((k1, k2, k3, k4, default, default, default, default));

	/// <inheritdoc cref="Remove(K, K, K, K, K, K, K, K)"/>
	public bool Remove(K k1, K k2, K k3, K k4, K k5) => _dictionary.Remove((k1, k2, k3, k4, k5, default, default, default));

	/// <inheritdoc cref="Remove(K, K, K, K, K, K, K, K)"/>
	public bool Remove(K k1, K k2, K k3, K k4, K k5, K k6) => _dictionary.Remove((k1, k2, k3, k4, k5, k6, default, default));

	/// <inheritdoc cref="Remove(K, K, K, K, K, K, K, K)"/>
	public bool Remove(K k1, K k2, K k3, K k4, K k5, K k6, K k7) => _dictionary.Remove((k1, k2, k3, k4, k5, k6, k7, default));

	/// <summary>Removes the element with the specified set of keys from the <see cref="FlatDictionary{K}"/>.</summary>
	/// <param name="k1">The key of the element to remove, relative to the first layer.</param>
	/// <param name="k2">The key of the element to remove, relative to the second layer.</param>
	/// <param name="k3">The key of the element to remove, relative to the third layer.</param>
	/// <param name="k4">The key of the element to remove, relative to the fourth layer.</param>
	/// <param name="k5">The key of the element to remove, relative to the fifth layer.</param>
	/// <param name="k6">The key of the element to remove, relative to the sixth layer.</param>
	/// <param name="k7">The key of the element to remove, relative to the seventh layer.</param>
	/// <param name="k8">The key of the element to remove, relative to the eighth layer.</param>
	/// <returns><see langword="true"/> if the element is successfully removed; otherwise, <see langword="false"/> This method also returns <see langword="false"/> if the set of keys does not point to an element in the <see cref="FlatDictionary{K}"/>.</returns>
	public bool Remove(K k1, K k2, K k3, K k4, K k5, K k6, K k7, K k8) => _dictionary.Remove((k1, k2, k3, k4, k5, k6, k7, k8));

	/// <inheritdoc cref="Remove(K, K, K, K, K, K, K, K, object)"/>
	public bool Remove(K k1, object value) => Remove(item: new((k1, default, default, default, default, default, default, default), value));

	/// <inheritdoc cref="Remove(K, K, K, K, K, K, K, K, object)"/>
	public bool Remove(K k1, K k2, object value) => Remove(item: new((k1, k2, default, default, default, default, default, default), value));

	/// <inheritdoc cref="Remove(K, K, K, K, K, K, K, K, object)"/>
	public bool Remove(K k1, K k2, K k3, object value) => Remove(item: new((k1, k2, k3, default, default, default, default, default), value));

	/// <inheritdoc cref="Remove(K, K, K, K, K, K, K, K, object)"/>
	public bool Remove(K k1, K k2, K k3, K k4, object value) => Remove(item: new((k1, k2, k3, k4, default, default, default, default), value));

	/// <inheritdoc cref="Remove(K, K, K, K, K, K, K, K, object)"/>
	public bool Remove(K k1, K k2, K k3, K k4, K k5, object value) => Remove(item: new((k1, k2, k3, k4, k5, default, default, default), value));

	/// <inheritdoc cref="Remove(K, K, K, K, K, K, K, K, object)"/>
	public bool Remove(K k1, K k2, K k3, K k4, K k5, K k6, object value) => Remove(item: new((k1, k2, k3, k4, k5, k6, default, default), value));

	/// <inheritdoc cref="Remove(K, K, K, K, K, K, K, K, object)"/>
	public bool Remove(K k1, K k2, K k3, K k4, K k5, K k6, K k7, object value) => Remove(item: new((k1, k2, k3, k4, k5, k6, k7, default), value));

	/// <param name="k1">The key of the element to remove, relative to the first layer.</param>
	/// <param name="k2">The key of the element to remove, relative to the second layer.</param>
	/// <param name="k3">The key of the element to remove, relative to the third layer.</param>
	/// <param name="k4">The key of the element to remove, relative to the fourth layer.</param>
	/// <param name="k5">The key of the element to remove, relative to the fifth layer.</param>
	/// <param name="k6">The key of the element to remove, relative to the sixth layer.</param>
	/// <param name="k7">The key of the element to remove, relative to the seventh layer.</param>
	/// <param name="k8">The key of the element to remove, relative to the eighth layer.</param>
	/// <param name="value">The value of the element to remove.</param>
	/// <inheritdoc cref="ICollection{T}.Remove(T)"/>
	public bool Remove(K k1, K k2, K k3, K k4, K k5, K k6, K k7, K k8, object value) => Remove(item: new((k1, k2, k3, k4, k5, k6, k7, k8), value));

	#endregion Remove(K..)

	#region TryAdd(K.., object)

	/// <summary>Attempts to add the specified base layer key and value to the <see cref="FlatDictionary{K}"/>.</summary>
	/// <returns><see langword="true"/> if the base layer key and value were added to the dictionary successfully; otherwise <see langword="false"/>.</returns>
	/// <inheritdoc cref="TryAdd(K, K, K, K, K, K, K, K, object)"/>
	public void TryAdd(K k1, object value) => _dictionary.TryAdd((k1, default, default, default, default, default, default, default), value);

	/// <inheritdoc cref="TryAdd(K, K, K, K, K, K, K, K, object)"/>
	public void TryAdd(K k1, K k2, object value) => _dictionary.TryAdd((k1, k2, default, default, default, default, default, default), value);

	/// <inheritdoc cref="TryAdd(K, K, K, K, K, K, K, K, object)"/>
	public void TryAdd(K k1, K k2, K k3, object value) => _dictionary.TryAdd((k1, k2, k3, default, default, default, default, default), value);

	/// <inheritdoc cref="TryAdd(K, K, K, K, K, K, K, K, object)"/>
	public void TryAdd(K k1, K k2, K k3, K k4, object value) => _dictionary.TryAdd((k1, k2, k3, k4, default, default, default, default), value);

	/// <inheritdoc cref="TryAdd(K, K, K, K, K, K, K, K, object)"/>
	public void TryAdd(K k1, K k2, K k3, K k4, K k5, object value) => _dictionary.TryAdd((k1, k2, k3, k4, k5, default, default, default), value);

	/// <inheritdoc cref="TryAdd(K, K, K, K, K, K, K, K, object)"/>
	public void TryAdd(K k1, K k2, K k3, K k4, K k5, K k6, object value) => _dictionary.TryAdd((k1, k2, k3, k4, k5, k6, default, default), value);

	/// <inheritdoc cref="TryAdd(K, K, K, K, K, K, K, K, object)"/>
	public void TryAdd(K k1, K k2, K k3, K k4, K k5, K k6, K k7, object value) => _dictionary.TryAdd((k1, k2, k3, k4, k5, k6, k7, default), value);

	/// <summary>Attempts to add the specified set of keys and value to the <see cref="FlatDictionary{K}"/>.</summary>
	/// <param name="k1">The key of the element to add, relative to the base layer.</param>
	/// <param name="k2">The key of the element to add, relative to the second layer.</param>
	/// <param name="k3">The key of the element to add, relative to the third layer.</param>
	/// <param name="k4">The key of the element to add, relative to the fourth layer.</param>
	/// <param name="k5">The key of the element to add, relative to the fifth layer.</param>
	/// <param name="k6">The key of the element to add, relative to the sixth layer.</param>
	/// <param name="k7">The key of the element to add, relative to the seventh layer.</param>
	/// <param name="k8">The key of the element to add, relative to the eighth layer.</param>
	/// <param name="value">The value of the element to add.</param>
	/// <returns><see langword="true"/> if the key set and value were added to the dictionary successfully; otherwise <see langword="false"/>.</returns>
	public void TryAdd(K k1, K k2, K k3, K k4, K k5, K k6, K k7, K k8, object value) => _dictionary.TryAdd((k1, k2, k3, k4, k5, k6, k7, k8), value);

	#endregion TryAdd(K.., object)

	#region TryGetValue(K.., out object)

	/// <summary>Gets the value associated with the specified base layer key.</summary>
	/// <inheritdoc cref="TryGetValue(K, K, K, K, K, K, K, K, out object)"/>
	public bool TryGetValue(K k1, [MaybeNullWhen(false)] out object value) =>
		_dictionary.TryGetValue((k1, default, default, default, default, default, default, default), out value);

	/// <inheritdoc cref="TryGetValue(K, K, K, K, K, K, K, K, out object)"/>
	public bool TryGetValue(K k1, K k2, [MaybeNullWhen(false)] out object value) =>
		_dictionary.TryGetValue((k1, k2, default, default, default, default, default, default), out value);

	/// <inheritdoc cref="TryGetValue(K, K, K, K, K, K, K, K, out object)"/>
	public bool TryGetValue(K k1, K k2, K k3, [MaybeNullWhen(false)] out object value) =>
		_dictionary.TryGetValue((k1, k2, k3, default, default, default, default, default), out value);

	/// <inheritdoc cref="TryGetValue(K, K, K, K, K, K, K, K, out object)"/>
	public bool TryGetValue(K k1, K k2, K k3, K k4, [MaybeNullWhen(false)] out object value) =>
		_dictionary.TryGetValue((k1, k2, k3, k4, default, default, default, default), out value);

	/// <inheritdoc cref="TryGetValue(K, K, K, K, K, K, K, K, out object)"/>
	public bool TryGetValue(K k1, K k2, K k3, K k4, K k5, [MaybeNullWhen(false)] out object value) =>
		_dictionary.TryGetValue((k1, k2, k3, k4, k5, default, default, default), out value);

	/// <inheritdoc cref="TryGetValue(K, K, K, K, K, K, K, K, out object)"/>
	public bool TryGetValue(K k1, K k2, K k3, K k4, K k5, K k6, [MaybeNullWhen(false)] out object value) =>
		_dictionary.TryGetValue((k1, k2, k3, k4, k5, k6, default, default), out value);

	/// <inheritdoc cref="TryGetValue(K, K, K, K, K, K, K, K, out object)"/>
	public bool TryGetValue(K k1, K k2, K k3, K k4, K k5, K k6, K k7, [MaybeNullWhen(false)] out object value) =>
		_dictionary.TryGetValue((k1, k2, k3, k4, k5, k6, k7, default), out value);

	/// <summary>Gets the value associated with the specified set of keys.</summary>
	/// <param name="k1">The key of the value to get, relative to the base layer.</param>
	/// <param name="k2">The key of the value to get, relative to the second layer.</param>
	/// <param name="k3">The key of the value to get, relative to the third layer.</param>
	/// <param name="k4">The key of the value to get, relative to the fourth layer.</param>
	/// <param name="k5">The key of the value to get, relative to the fifth layer.</param>
	/// <param name="k6">The key of the value to get, relative to the sixth layer.</param>
	/// <param name="k7">The key of the value to get, relative to the seventh layer.</param>
	/// <param name="k8">The key of the value to get, relative to the eighth layer.</param>
	/// <param name="value">When this method returns, the value associated with the specified set of keys.</param>
	/// <returns><see langword="true"/> if the <see cref="FlatDictionary{K}"/> contains an element with the specified set of keys; otherwise <see langword="false"/>.</returns>
	public bool TryGetValue(K k1, K k2, K k3, K k4, K k5, K k6, K k7, K k8, [MaybeNullWhen(false)] out object value) =>
		_dictionary.TryGetValue((k1, k2, k3, k4, k5, k6, k7, k8), out value);

	#endregion TryGetValue(K.., out object)
}