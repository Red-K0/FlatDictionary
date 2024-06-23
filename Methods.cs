namespace RedK0; using System.Runtime.CompilerServices;
public partial class Flat<TKey>
{
	/// <summary>
	/// Recursively traverses downwards through nested dictionaries, pulling up values as necessary.
	/// </summary>
	private void FlattenDictionary(KeyValuePair<TKey, object> pair)
	{
		TKey[] Keys = [default, default, default, default, default, default, default];
		int CurrentLayer = -1;

		PullUpValues(pair);

		void PullUpValues(KeyValuePair<TKey, object> pair)
		{
			if (pair.Value.GetType().IsGenericType && pair.Value.GetType().GetGenericTypeDefinition() == typeof(Dictionary<,>))
			{
				CurrentLayer++; Keys[CurrentLayer] = pair.Key;
				foreach (KeyValuePair<TKey, object> FoundPair in Unsafe.As<Dictionary<TKey, object>>(pair.Value))
				{
					PullUpValues(FoundPair);
				}

#pragma warning disable IDE0059 // This is in fact, not unnecessary.
				CurrentLayer--;
#pragma warning restore IDE0059
			}
			else
			{
				_flattenedDictionary[(pair.Key, Keys[0], Keys[1], Keys[2], Keys[3], Keys[4], Keys[5], Keys[6])] = pair.Value;
			}
		}
	}
}