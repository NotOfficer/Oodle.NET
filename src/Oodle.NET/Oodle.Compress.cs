using System.Numerics;

namespace OodleDotNet;

public unsafe partial class Oodle
{
	/// <summary>
	/// Compress some data from memory to memory, synchronously, with Oodle
	/// </summary>
	/// <returns>
	/// Size of compressed data written, or 0 for failure
	/// </returns>
	public delegate* unmanaged<OodleCompressor, void*, nint, void*, OodleCompressionLevel, void*, void*, void*, void*, nint, nint> CompressFunctionPointer { get; }

	/// <inheritdoc cref="CompressFunctionPointer" />
	public nint Compress(OodleCompressor compressor, OodleCompressionLevel level, ReadOnlySpan<byte> source, Span<byte> dest)
	{
		fixed (byte* sourcePtr = source)
		fixed (byte* destPtr = dest)
		{
			var sourceLen = (nint)source.Length;
			return CompressFunctionPointer(compressor, sourcePtr, sourceLen, destPtr, level, null, null, null, null, 0);
		}
	}

	/// <inheritdoc cref="CompressFunctionPointer" />
	public nint Compress(OodleCompressor compressor, OodleCompressionLevel level,
		byte[] source, int sourceOffset, int sourceLen,
		byte[] dest, int destOffset, int destLen)
	{
		return Compress(compressor, level,
			new ReadOnlySpan<byte>(source, sourceOffset, sourceLen),
			new Span<byte>(dest, destOffset, destLen));
	}

	/// <inheritdoc cref="CompressFunctionPointer" />
	public nint Compress<T1, T2>(OodleCompressor compressor, OodleCompressionLevel level,
		void* source, T1 sourceLen, void* dest, T2 destLen)
		where T1 : IBinaryInteger<T1> where T2 : IBinaryInteger<T2>
	{
		return Compress(compressor, level,
			new ReadOnlySpan<byte>(source, int.CreateChecked(sourceLen)),
			new Span<byte>(dest, int.CreateChecked(destLen)));
	}

	/// <inheritdoc cref="CompressFunctionPointer" />
	public nint Compress<T1, T2>(OodleCompressor compressor, OodleCompressionLevel level,
		nint source, T1 sourceLen, nint dest, T2 destLen)
		where T1 : IBinaryInteger<T1> where T2 : IBinaryInteger<T2>
	{
		return Compress(compressor, level,
			new ReadOnlySpan<byte>(source.ToPointer(), int.CreateChecked(sourceLen)),
			new Span<byte>(dest.ToPointer(), int.CreateChecked(destLen)));
	}
}
