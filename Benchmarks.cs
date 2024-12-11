#pragma warning disable CA1822 // Mark members as static

using BenchmarkDotNet.Attributes;
using RedK0;

namespace FlatDictionary;
public class Benchmarks
{
	static readonly Dictionary<int, Dictionary<int, Dictionary<int, string>>> MainDict = new() { [0] = new() { [5] = new() { [0] = "abc", [1] = "def" }, [4] = new() { [2] = "ghi", [3] = "jkl", [4] = "mno" } } };
	static readonly Dictionary<int, string> Box = new() { [0] = "abc", [1] = "def", [2] = "ghi", [3] = "jkl", [4] = "mno" };
	static FlatDictionary<int> FlatDict = new(FlatDictionary<int>.CastToObjectDictionary(MainDict));
	static readonly KeyValuePair<(int, int, int, int, int, int, int, int), object>[] CopySurrogate = new KeyValuePair<(int, int, int, int, int, int, int, int), object>[30];


	[Benchmark]
	public void New() => FlatDict = new(FlatDictionary<int>.CastToObjectDictionary(MainDict));

	[Benchmark]
	public void AddRemove()
	{
		FlatDict.Add((9, 9, 9, 9, 9, 9, 9, 9), "test");
		_ = FlatDict.Remove((9, 9, 9, 9, 9, 9, 9, 9));
	}

	[Benchmark]
	public void Contains() => _ = FlatDict.Contains(new((9, 9, 9, 9, 9, 9, 9, 9), "test"));

	[Benchmark]
	public void ContainsKey() => _ = FlatDict.ContainsKey((9, 9, 9, 9, 9, 9, 9, 9));

	[Benchmark]
	public void ContainsValue() => _ = FlatDict.ContainsValue("test");

	[Benchmark]
	public void CopyTo() => FlatDict.CopyTo(CopySurrogate, 0);

	[Benchmark]
	public void EnsureCapacityResize()
	{
		_ = FlatDict.EnsureCapacity(50);
		FlatDict.TrimExcess();
	}

	[Benchmark]
	public void Method() => _ = FlatDict.TryGetValue((9, 9, 9, 9, 9, 9, 9, 9), out object? _);
}
