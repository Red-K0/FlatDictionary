var leaf1 = new Dictionary<int, string>
{
	[1] = "abc",
	[2] = "def"
};

var leaf2 = new Dictionary<int, string>
{
	[1] = "ghi",
	[2] = "jkl"
};

var root = new Dictionary<int, Dictionary<int, string>>
{
	[5] = leaf1,
	[4] = leaf2
};

var root2 = new Dictionary<int, object>()
{
	[1] = root
};

RedK0.Flat<int> dict = new(root2);
Console.Write(dict[1,1,5]);
