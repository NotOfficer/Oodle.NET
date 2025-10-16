namespace OodleDotNet;

/// <summary/>
public enum OodleVerbosity
{
    /// <summary/>
    None = 0,
    /// <summary/>
    Minimal = 1,
    /// <summary/>
    Some = 2,
    /// <summary/>
    Lots = 3,
    /// <summary/>
    Force32 = 0x40000000
}

/// <summary/>
public enum OodleFuzzSafe
{
    /// <summary/>
    No = 0,
    /// <summary/>
    Yes = 1
}

/// <summary/>
public enum OodleCheckCrc
{
    /// <summary/>
    No = 0,
    /// <summary/>
    Yes = 1,
    /// <summary/>
    Force32 = 0x40000000
}

/// <summary/>
public enum OodleDecodeThreadPhase
{
    /// <summary/>
    Phase1 = 1,
    /// <summary/>
    Phase2 = 2,
    /// <summary/>
    All = 3,
    /// <summary/>
    Unthreaded = All
}

/// <summary>
/// Selection of compression encoder complexity<br/>
/// <br/>
/// Higher numerical value of CompressionLevel = slower compression, but smaller compressed data.<br/>
/// <br/>
/// The compressed stream is always decodable with the same decompressors.<br/>
/// CompressionLevel controls the amount of work the encoder does to find the best compressed bit stream.<br/>
/// CompressionLevel does not primary affect decode speed, it trades off encode speed for compressed bit stream quality.<br/>
/// <br/>
/// I recommend starting with <see cref="Normal"/>, then try up or down if you want<br/>
/// faster encoding or smaller output files.<br/>
/// <br/>
/// The Optimal levels are good for distribution when you compress rarely and decompress often;<br/>
/// they provide very high compression ratios but are slow to encode.  <see cref="Optimal2"/> is the recommended level<br/>
/// to start with of the optimal levels.<br/>
/// <see cref="Optimal4"/> and <see cref="Optimal5"/> are not recommended for common use, they are very slow and provide the maximum compression ratio,<br/>
/// but the gain over <see cref="Optimal3"/> is usually small.<br/>
/// <br/>
/// The <see cref="HyperFast"/> levels have negative numeric CompressionLevel values.<br/>
/// They are faster than <see cref="SuperFast"/> for when you're encoder CPU time constrained or want<br/>
/// something closer to symmetric compression vs. decompression time.<br/>
/// The <see cref="HyperFast"/> levels are currently only available in <see cref="OodleCompressor.Kraken"/>, <see cref="OodleCompressor.Mermaid"/> &amp; <see cref="OodleCompressor.Selkie"/>.<br/>
/// Higher levels of <see cref="HyperFast"/> are faster to encode, eg. <see cref="HyperFast4"/> is the fastest.<br/>
/// <br/>
/// The CompressionLevel does not affect decode speed much.  Higher compression level does not mean<br/>
/// slower to decode.
/// </summary>
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
    /// faster than <see cref="SuperFast"/>, less compression
    /// </summary>
    HyperFast1=-1,
    /// <summary>
    /// faster than <see cref="HyperFast1"/>, less compression
    /// </summary>
    HyperFast2=-2,
    /// <summary>
    /// faster than <see cref="HyperFast2"/>, less compression
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

    /// <summary/>
    Force32 = 0x40000000,
    /// <summary/>
    Invalid = Force32
}

/// <summary>
/// The oodle compressor
/// </summary>
public enum OodleCompressor
{
    /// <summary>
    /// Invalid compressor
    /// </summary>
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
    /// Leviathan = <see cref="Kraken"/>'s big brother with higher compression, slightly slower decompression.
    /// </summary>
    Leviathan = 13,
    /// <summary>
    /// Mermaid is between <see cref="Kraken"/> &amp; <see cref="Selkie"/> - crazy fast, still decent compression.
    /// </summary>
    Mermaid = 9,
    /// <summary>
    /// Selkie is a super-fast relative of <see cref="Mermaid"/>.  For maximum decode speed.
    /// </summary>
    Selkie = 11,
    /// <summary>
    /// Hydra, the many-headed beast = <see cref="Leviathan"/>, <see cref="Kraken"/>, <see cref="Mermaid"/>, or <see cref="Selkie"/>
    /// </summary>
    Hydra = 12,

    /// <summary>
    /// no longer supported as of Oodle 2.9.0
    /// </summary>
    [Obsolete("no longer supported as of Oodle 2.9.0", false)]
    BitKnit = 10,
    /// <summary>
    /// no longer supported as of Oodle 2.9.0
    /// </summary>
    [Obsolete("no longer supported as of Oodle 2.9.0", false)]
    LZB16 = 4,
    /// <summary>
    /// no longer supported as of Oodle 2.9.0
    /// </summary>
    [Obsolete("no longer supported as of Oodle 2.9.0", false)]
    LZNA = 7,
    /// <summary>
    /// no longer supported as of Oodle 2.9.0
    /// </summary>
    [Obsolete("no longer supported as of Oodle 2.9.0", false)]
    LZH = 0,
    /// <summary>
    /// no longer supported as of Oodle 2.9.0
    /// </summary>
    [Obsolete("no longer supported as of Oodle 2.9.0", false)]
    LZHLW = 1,
    /// <summary>
    /// no longer supported as of Oodle 2.9.0
    /// </summary>
    [Obsolete("no longer supported as of Oodle 2.9.0", false)]
    LZNIB = 2,
    /// <summary>
    /// no longer supported as of Oodle 2.9.0
    /// </summary>
    [Obsolete("no longer supported as of Oodle 2.9.0", false)]
    LZBLW = 5,
    /// <summary>
    /// no longer supported as of Oodle 2.9.0
    /// </summary>
    [Obsolete("no longer supported as of Oodle 2.9.0", false)]
    LZA = 6,

    /// <summary>
    /// The count of values in this enum
    /// </summary>
    Count = 14,
    /// <summary/>
    Force32 = 0x40000000
}
