using System.Collections;

namespace FlatDictionary;
public partial class FlatDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TKey : unmanaged
{
	public FlatDictionary() { }
	public FlatDictionary(IDictionary dictionary) => AddDictionary(dictionary);

	public int Count => _pairs.Count;
	public int Capacity => _pairs.Capacity;
	public bool IsReadOnly => false;

	[EditorBrowsable(EditorBrowsableState.Never)]
	public ICollection<TKey> Keys => throw new NotSupportedException("FlatDictionary objects do not support acquiring key collections.");
	public ICollection<TValue> Values
	{
		get
		{
			TValue[] Values = new TValue[_pairs.Count];

			for (int i = 0; i < Values.Length; i++) Values[i] = _pairs[i].Value;

			return Values;
		}
	}

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
}
