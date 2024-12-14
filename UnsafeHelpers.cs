#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type

namespace FlatDictionary;
internal static unsafe class UnsafeHelpers
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T ReadAs<T>(object? o) => *(T*)(void*)&o;
}
