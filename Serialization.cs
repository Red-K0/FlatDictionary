using System.Runtime.Serialization;

namespace FlatDictionary;

[Serializable]
public partial class FlatDictionary<TKey, TValue> : ISerializable, IDeserializationCallback
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.")]
	public FlatDictionary(SerializationInfo info, StreamingContext context)
	{
		_seed = new(info.GetUInt64("_seedH"), info.GetUInt64("_seedL"));
		_pairs = (List<KeyValuePair<ulong, TValue>>)info.GetValue("_pairs", typeof(List<KeyValuePair<ulong, TValue>>))!;
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.")]
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("_seedH", _seed >> 64); info.AddValue("_seedL", _seed);

		info.AddValue("_pairs", _pairs, typeof(List<KeyValuePair<ulong, TValue>>));
	}

	public void OnDeserialization(object? sender) { }
}