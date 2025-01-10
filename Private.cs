using System.Collections;
using System.Runtime.InteropServices;

namespace FlatDictionary;
public partial class FlatDictionary<TKey, TValue>
{
	private readonly UInt128 _seed = new((ulong)DateTime.Now.Ticks, (ulong)DateTime.UtcNow.Ticks);

	private readonly Type _keyType = typeof(TKey), _valueType = typeof(TValue);

	private readonly List<KeyValuePair<ulong, TValue>> _pairs = new(50);

	private readonly List<TKey> _nestedKeys = new(5);

	private void AddDictionary(IDictionary dictionary)
	{
		IEnumerator Enumerator = dictionary.Keys.GetEnumerator();
		Enumerator.MoveNext();

		Enumerator = dictionary.Values.GetEnumerator();
		Enumerator.MoveNext();

		int EntryCount;

		switch (GetObjectType(Enumerator.Current, true))
		{
			case TypeStatus.Value:
				EntryCount = dictionary.Count;

				_pairs.EnsureCapacity(_pairs.Count + EntryCount);

				foreach (DictionaryEntry entry in dictionary) Add(entry.Key, entry.Value!);

				break;

			case TypeStatus.Dictionary:
				foreach (DictionaryEntry entry in dictionary)
				{
					IDictionary EntryDictionary = (IDictionary)entry.Value!;

					EntryCount = EntryDictionary.Count;

					_nestedKeys.Add((TKey)entry.Key);

					_pairs.EnsureCapacity(_pairs.Count + EntryCount);

					foreach (DictionaryEntry subEntry in EntryDictionary) Add(subEntry.Key, subEntry.Value!);

					_nestedKeys.RemoveAt(_nestedKeys.Count - 1);
				}
				break;

			case TypeStatus.NestedDictionary:
				foreach (DictionaryEntry entry in dictionary)
				{
					_nestedKeys.Add((TKey)entry.Key);

					AddDictionary((IDictionary)entry.Value!);

					_nestedKeys.RemoveAt(_nestedKeys.Count - 1);
				}
				break;

			case TypeStatus.Invalid:
			default: throw new ArgumentException($"A value or key in the provided dictionary was invalid. Please ensure all keys are of the provided key type: {_keyType.Name}, and all values are either dictionaries matching the appropriate types, or of provided value type: {_valueType.Name}");
		}

		void Add(object key, object value) => _pairs.Add(new(GetHash([.. _nestedKeys.Append((TKey)key)]), (TValue)value));
	}

	private ulong GetHash(ReadOnlySpan<TKey> keys) => GxHash.GxHash.HashU64(MemoryMarshal.AsBytes(keys), _seed);
	private ulong GetHash(TKey key) => GxHash.GxHash.HashU64(MemoryMarshal.AsBytes(new ReadOnlySpan<TKey>(ref key)), _seed);

	private TypeStatus GetObjectType(object o, bool isType)
	{
		Type Type = isType ? o.GetType() : (Type)o;

		if (Type == _valueType) return TypeStatus.Value;

		if (Type.GenericTypeArguments.Length == 0 || Type.GenericTypeArguments[0] != _keyType) return TypeStatus.Invalid;

		return GetObjectType(Type.GenericTypeArguments[1], false) switch
		{
			TypeStatus.Dictionary or TypeStatus.NestedDictionary => TypeStatus.NestedDictionary,
			TypeStatus.Value => TypeStatus.Dictionary,
			TypeStatus.Invalid or _ => TypeStatus.Invalid,
		};
	}

	private bool TryGetKeyIndex(ulong hash, out int index)
	{
		for (int i = 0; i < _pairs.Count; i++)
		{
			if (_pairs[i].Key == hash)
			{
				index = i;
				return true;
			}
		}

		index = -1;
		return false;
	}
	private bool TryGetKeyIndex(TKey key, out int index)
	{
		ulong hash = GetHash(key);

		for (int i = 0; i < _pairs.Count; i++)
		{
			if (_pairs[i].Key == hash)
			{
				index = i;
				return true;
			}
		}

		index = -1;
		return false;
	}
	private bool TryGetKeyIndex(ReadOnlySpan<TKey> keys, out int index)
	{
		ulong hash = GetHash(keys);

		for (int i = 0; i < _pairs.Count; i++)
		{
			if (_pairs[i].Key == hash)
			{
				index = i;
				return true;
			}
		}

		index = -1;
		return false;
	}

	private enum TypeStatus
	{
		Value = 0,
		Dictionary = 1,
		NestedDictionary = 2,
		Invalid = -1,
	}
}
