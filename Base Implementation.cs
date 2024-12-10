using System.Collections;
using System.Runtime.Serialization;

namespace RedK0;
public partial class FlatDictionary<K> : IDictionary<(K, K, K, K, K, K, K, K), object> where K : unmanaged
{
	public FlatDictionary(Dictionary<K, object> dict)
	{
		ArgumentNullException.ThrowIfNull(dict);

		foreach (KeyValuePair<K, object> pair in dict)
		{
			FlattenDictionary(pair);
		}
	}

	private Dictionary<(K, K, K, K, K, K, K, K), object> _dictionary = [];

	/// <inheritdoc cref="IDictionary{TKey, TValue}.Keys"/>
	public ICollection<(K, K, K, K, K, K, K, K)> Keys => _dictionary.Keys;

	/// <inheritdoc cref="IDictionary{TKey, TValue}.Values"/>
	public ICollection<object> Values => _dictionary.Values;

	/// <inheritdoc cref="ICollection{T}.Count"/>
	public int Count => _dictionary.Count;

	/// <inheritdoc cref="ICollection{T}.IsReadOnly"/>
	public bool IsReadOnly => false;


	[SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "Incorrect Analysis")]
	private void FlattenDictionary(KeyValuePair<K, object> pair)
	{
		K[] Slot = [default, default, default, default, default, default, default];
		Type DictionaryType = typeof(Dictionary<,>);
		int CurrentLayer = -1;

		PullUpValues(pair);

		void PullUpValues(KeyValuePair<K, object> pair)
		{
			Type valueType = pair.Value.GetType();

			if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == DictionaryType)
			{
				CurrentLayer++; Slot[CurrentLayer] = pair.Key;
				foreach (KeyValuePair<K, object> FoundPair in CastToObjectDictionary(pair.Value))
				{
					PullUpValues(FoundPair);
				}

				CurrentLayer--;
			}
			else
			{
				_dictionary[(CurrentLayer + 1) switch
				{
					0 => (pair.Key, default, default, default, default, default, default, default),
					1 => (Slot[0], pair.Key, default, default, default, default, default, default),
					2 => (Slot[0], Slot[1], pair.Key, default, default, default, default, default),
					3 => (Slot[0], Slot[1], Slot[2], pair.Key, default, default, default, default),
					4 => (Slot[0], Slot[1], Slot[2], Slot[3], pair.Key, default, default, default),
					5 => (Slot[0], Slot[1], Slot[2], Slot[3], Slot[4], pair.Key, default, default),
					6 => (Slot[0], Slot[1], Slot[2], Slot[3], Slot[4], Slot[5], pair.Key, default),
					7 => (Slot[0], Slot[1], Slot[2], Slot[3], Slot[4], Slot[5], Slot[6], pair.Key),
					_ => throw new RankException("The dictionary's layer depth is higher than 8.")
				}] = pair.Value;
			}
		}
	}


	/// <inheritdoc cref="IDictionary{TKey, TValue}.this"/>
	public object this[(K, K, K, K, K, K, K, K) key] { get => _dictionary[key]; set => _dictionary[key] = value; }


	/// <inheritdoc cref="IDictionary{TKey, TValue}.Add(TKey, TValue)"/>
	public void Add((K, K, K, K, K, K, K, K) key, object value) => _dictionary.Add(key, value);

	/// <inheritdoc cref="ICollection{T}.Add(T)"/>
	public void Add(KeyValuePair<(K, K, K, K, K, K, K, K), object> item) => ((ICollection<KeyValuePair<(K, K, K, K, K, K, K, K), object>>)_dictionary).Add(item);


	public void ClearF() => _dictionary = []; // Clear is an O(n) operation, creating a new dict is faster.
	/// <inheritdoc cref="ICollection{T}.Clear"/>
	public void Clear() => _dictionary.Clear();


	/// <inheritdoc cref="ICollection{T}.Contains(T)"/>
	public bool Contains(KeyValuePair<(K, K, K, K, K, K, K, K), object> item) => ((ICollection<KeyValuePair<(K, K, K, K, K, K, K, K), object>>)_dictionary).Contains(item);

	/// <inheritdoc cref="IDictionary{TKey, TValue}.ContainsKey(TKey)"/>
	public bool ContainsKey((K, K, K, K, K, K, K, K) key) => _dictionary.ContainsKey(key);

	/// <inheritdoc cref="Dictionary{TKey, TValue}.ContainsValue(TValue)"/>
	public bool ContainsValue(object key) => _dictionary.ContainsValue(key);

	/// <inheritdoc cref="Dictionary{TKey, TValue}.EnsureCapacity(int)"/>
	public int EnsureCapacity(int capacity) => _dictionary.EnsureCapacity(capacity);


	/// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
	IEnumerator IEnumerable.GetEnumerator() => _dictionary.GetEnumerator();

	/// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
	public IEnumerator<KeyValuePair<(K, K, K, K, K, K, K, K), object>> GetEnumerator() => ((IEnumerable<KeyValuePair<(K, K, K, K, K, K, K, K), object>>)_dictionary).GetEnumerator();


	/// <inheritdoc cref="ICollection{T}.CopyTo(T[], int)"/>
	public void CopyTo(KeyValuePair<(K, K, K, K, K, K, K, K), object>[] array, int arrayIndex) => ((ICollection<KeyValuePair<(K, K, K, K, K, K, K, K), object>>)_dictionary).CopyTo(array, arrayIndex);

	/// <inheritdoc cref="Dictionary{TKey, TValue}.GetObjectData(SerializationInfo, StreamingContext)"/>
	[Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId = "SYSLIB0051", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void GetObjectData(SerializationInfo info, StreamingContext context) => _dictionary.GetObjectData(info, context);

	/// <inheritdoc cref="Dictionary{TKey, TValue}.OnDeserialization(object?)"/>
	public void OnDeserialization(object? sender) => _dictionary.OnDeserialization(sender);


	/// <inheritdoc cref="Dictionary{TKey, TValue}.Remove(TKey)"/>
	public bool Remove((K, K, K, K, K, K, K, K) key) => _dictionary.Remove(key);

	/// <inheritdoc cref="ICollection{T}.Remove(T)"/>
	public bool Remove(KeyValuePair<(K, K, K, K, K, K, K, K), object> item) => ((ICollection<KeyValuePair<(K, K, K, K, K, K, K, K), object>>)_dictionary).Remove(item);


	/// <inheritdoc cref="Dictionary{TKey, TValue}.TrimExcess()"/>
	public void TrimExcess(int capacity) => _dictionary.TrimExcess(capacity);

	/// <inheritdoc cref="Dictionary{TKey, TValue}.TrimExcess(int)"/>
	public void TrimExcess() => _dictionary.TrimExcess();


	/// <inheritdoc cref="Dictionary{TKey, TValue}.TryAdd(TKey, TValue)"/>
	public bool TryAdd((K, K, K, K, K, K, K, K) key, object value) => _dictionary.TryAdd(key, value);

	/// <inheritdoc cref="Dictionary{TKey, TValue}.TryGetValue(TKey, out TValue)"/>
	public bool TryGetValue((K, K, K, K, K, K, K, K) key, [MaybeNullWhen(false)] out object value) => _dictionary.TryGetValue(key, out value);
}