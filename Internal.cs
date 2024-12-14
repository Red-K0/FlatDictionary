using System.Collections;
using static FlatDictionary.UnsafeHelpers;

namespace FlatDictionary;
public partial class FlatDictionary<TKey, TValue>
{
	private void Add(string key, object value)
	{
		_keys.Add(key, Count++);

		_values.Add(ReadAs<TValue>(value));
	}

	private unsafe void WalkDictionary(IDictionary dictionary, bool checkConverter = false)
	{
		IEnumerator Enumerator = dictionary.Keys.GetEnumerator();
		Enumerator.MoveNext();

		if (checkConverter && _keyConverter.Invoke(ReadAs<TKey>(Enumerator.Current)) == _keyType.ToString())
		{
			throw new ArgumentException($"Invalid key type: {_keyType.Name}. Override ToString or use a custom converter.");
		}

		Enumerator = dictionary.Values.GetEnumerator();
		Enumerator.MoveNext();

		switch (FindObjectType(ReadAs<object>(Enumerator.Current), true))
		{
			case TypeStatus.Value:
				foreach (DictionaryEntry entry in dictionary) Add(_keyBuilder + _keyConverter.Invoke(ReadAs<TKey>(entry.Key)), entry.Value!);
				break;

			case TypeStatus.Dictionary:
				foreach (DictionaryEntry entry in ReadAs<IDictionary>(dictionary))
				{
					string keyString = _keyConverter.Invoke(ReadAs<TKey>(entry.Key));

					_keyBuilder.Append(keyString);

					foreach (DictionaryEntry subEntry in ReadAs<IDictionary>(entry.Value)) Add(_keyBuilder + _keyConverter.Invoke(ReadAs<TKey>(subEntry.Key)), subEntry.Value!);

					_keyBuilder.Length -= keyString.Length;
				}
				break;

			case TypeStatus.NestedDictionary:
				foreach (DictionaryEntry entry in ReadAs<IDictionary>(dictionary))
				{
					string keyString = _keyConverter(ReadAs<TKey>(entry.Key));

					_keyBuilder.Append(keyString);

					WalkDictionary(ReadAs<IDictionary>(entry.Value));

					_keyBuilder.Length -= keyString.Length;
				}
				break;

			case TypeStatus.Invalid:
			default: throw new ArgumentException($"A value or key in the provided dictionary was invalid. Please ensure all keys are of the provided key type: {_keyType.Name}, and all values are either dictionaries matching the appropriate types, or of provided value type: {_valueType.Name}");
		}
	}

	private TypeStatus FindObjectType(object o, bool isType)
	{
		Type Type = isType ? o.GetType() : ReadAs<Type>(o);

		if (Type == _valueType) return TypeStatus.Value;

		if (Type.GenericTypeArguments.Length == 0 || Type.GenericTypeArguments[0] != _keyType) return TypeStatus.Invalid;

		return FindObjectType(Type.GenericTypeArguments[1], false) switch
		{
			TypeStatus.Dictionary or TypeStatus.NestedDictionary => TypeStatus.NestedDictionary,
			TypeStatus.Value => TypeStatus.Dictionary,
			TypeStatus.Invalid or _ => TypeStatus.Invalid,
		};
	}

	private enum TypeStatus
	{
		Invalid = -1,
		Value = 0,
		Dictionary = 1,
		NestedDictionary = 2,
	}
}