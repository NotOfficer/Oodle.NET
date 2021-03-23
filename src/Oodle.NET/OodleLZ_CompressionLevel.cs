namespace Oodle.NET
{
	public enum OodleLZ_CompressionLevel
	{
		HyperFast4 = -4,
		HyperFast3,
		HyperFast2,
		HyperFast1,
		None,
		SuperFast,
		VeryFast,
		Fast,
		Normal,
		Optimal1,
		Optimal2,
		Optimal3,
		Optimal4,
		Optimal5,
		// TooHigh,

		Min = HyperFast4,
		Max = Optimal5
	}
}