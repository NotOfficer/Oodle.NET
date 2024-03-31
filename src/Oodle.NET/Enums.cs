namespace OodleDotNet;

public enum OodleVerbosity
{
	None = 0,
	Minimal = 1,
	Some = 2,
	Lots = 3,
	Force32 = 0x40000000
}

public enum OodleFuzzSafe
{
	No = 0,
	Yes = 1
}

public enum OodleCheckCRC
{
	No = 0,
	Yes = 1,
	Force32 = 0x40000000
}

public enum OodleDecodeThreadPhase
{
	Phase1 = 1,
	Phase2 = 2,
	All = 3,
	Unthreaded = All
}

public enum OodleCompressionLevel
{
	/// <summary>
	/// don't compress, just copy raw bytes
	/// </summary>
	None=0,
	/// <summary>
	/// super fast mode, lower compression ratio
	/// </summary>
	SuperFast=1,
	/// <summary>
	/// fastest LZ mode with still decent compression ratio
	/// </summary>
	VeryFast=2,
	/// <summary>
	/// fast - good for daily use
	/// </summary>
	Fast=3,
	/// <summary>
	/// standard medium speed LZ mode
	/// </summary>
	Normal=4,

	/// <summary>
	/// optimal parse level 1 (faster optimal encoder)
	/// </summary>
	Optimal1=5,
	/// <summary>
	/// optimal parse level 2 (recommended baseline optimal encoder)
	/// </summary>
	Optimal2=6,
	/// <summary>
	/// optimal parse level 3 (slower optimal encoder)
	/// </summary>
	Optimal3=7,
	/// <summary>
	/// optimal parse level 4 (very slow optimal encoder)
	/// </summary>
	Optimal4=8,
	/// <summary>
	/// optimal parse level 5 (don't care about encode speed, maximum compression)
	/// </summary>
	Optimal5=9,

	/// <summary>
	/// faster than SuperFast, less compression
	/// </summary>
	HyperFast1=-1,
	/// <summary>
	/// faster than HyperFast1, less compression
	/// </summary>
	HyperFast2=-2,
	/// <summary>
	/// faster than HyperFast2, less compression
	/// </summary>
	HyperFast3=-3,
	/// <summary>
	/// fastest, less compression
	/// </summary>
	HyperFast4=-4,

	/// <summary>
	/// alias hyperfast base level
	/// </summary>
	HyperFast=HyperFast1,
	/// <summary>
	/// alias optimal standard level
	/// </summary>
	Optimal = Optimal2,
	/// <summary>
	/// maximum compression level
	/// </summary>
	Max     = Optimal5,
	/// <summary>
	/// fastest compression level
	/// </summary>
	Min     = HyperFast4,

	Force32 = 0x40000000,
	Invalid = Force32
}

public enum OodleCompressor
{
	Invalid = -1,
	/// <summary>
	/// None = memcpy, pass through uncompressed bytes
	/// </summary>
	None = 3,

	/// <summary>
	/// Fast decompression and high compression ratios, amazing!
	/// </summary>
	Kraken = 8,
	/// <summary>
	/// Leviathan = Kraken's big brother with higher compression, slightly slower decompression.
	/// </summary>
	Leviathan = 13,
	/// <summary>
	/// Mermaid is between Kraken 
	/// </summary>& Selkie - crazy fast, still decent compression.
	Mermaid = 9,
	/// <summary>
	/// Selkie is a super-fast relative of Mermaid.  For maximum decode speed.
	/// </summary>
	Selkie = 11,
	/// <summary>
	/// Hydra, the many-headed beast = Leviathan, Kraken, Mermaid, or Selkie (see $OodleLZ_About_Hydra)
	/// </summary>
	Hydra = 12,

	/// <summary>
	/// no longer supported as of Oodle 2.9.0
	/// </summary>
	BitKnit = 10,
	/// <summary>
	/// no longer supported as of Oodle 2.9.0
	/// </summary>
	LZB16 = 4,
	/// <summary>
	/// no longer supported as of Oodle 2.9.0
	/// </summary>
	LZNA = 7,
	/// <summary>
	/// no longer supported as of Oodle 2.9.0
	/// </summary>
	LZH = 0,
	/// <summary>
	/// no longer supported as of Oodle 2.9.0
	/// </summary>
	LZHLW = 1,
	/// <summary>
	/// no longer supported as of Oodle 2.9.0
	/// </summary>
	LZNIB = 2,
	/// <summary>
	/// no longer supported as of Oodle 2.9.0
	/// </summary>
	LZBLW = 5,
	/// <summary>
	/// no longer supported as of Oodle 2.9.0
	/// </summary>
	LZA = 6,

	Count = 14,
	Force32 = 0x40000000
}
