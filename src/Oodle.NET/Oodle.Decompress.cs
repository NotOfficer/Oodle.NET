using System.Numerics;

namespace OodleDotNet;

public unsafe partial class Oodle
{
    /// <summary>
    /// Decompress a some data from memory to memory, synchronously.
    /// Decodes data encoded with any Compressor.
    /// </summary>
    /// <returns>
    /// Size of compressed data written, or 0 for failure
    /// </returns>
    public delegate* unmanaged<void*, nint, void*, nint, OodleFuzzSafe, OodleCheckCrc, OodleVerbosity, void*, nint, void*, void*, void*, nint, OodleDecodeThreadPhase, nint> DecompressFunctionPointer { get; }

    /// <inheritdoc cref="DecompressFunctionPointer" />
    public nint Decompress(ReadOnlySpan<byte> source, Span<byte> dest)
    {
        fixed (byte* sourcePtr = source)
        fixed (byte* destPtr = dest)
        {
            nint sourceLen = source.Length;
            nint destLen = dest.Length;
            return DecompressFunctionPointer(sourcePtr, sourceLen, destPtr, destLen,
                OodleFuzzSafe.Yes, OodleCheckCrc.No, OodleVerbosity.None, null, 0, null, null, null, 0, OodleDecodeThreadPhase.Unthreaded);
        }
    }

    /// <inheritdoc cref="DecompressFunctionPointer" />
    public nint Decompress(
        byte[] source, int sourceOffset, int sourceLen,
        byte[] dest, int destOffset, int destLen)
    {
        return Decompress(
            new ReadOnlySpan<byte>(source, sourceOffset, sourceLen),
            new Span<byte>(dest, destOffset, destLen));
    }

    /// <inheritdoc cref="DecompressFunctionPointer" />
    public nint Decompress<T1, T2>(
        void* source, T1 sourceLen, void* dest, T2 destLen)
        where T1 : IBinaryInteger<T1> where T2 : IBinaryInteger<T2>
    {
        return Decompress(
            new ReadOnlySpan<byte>(source, int.CreateChecked(sourceLen)),
            new Span<byte>(dest, int.CreateChecked(destLen)));
    }

    /// <inheritdoc cref="DecompressFunctionPointer" />
    public nint Decompress<T1, T2>(
        nint source, T1 sourceLen, nint dest, T2 destLen)
        where T1 : IBinaryInteger<T1> where T2 : IBinaryInteger<T2>
    {
        return Decompress(
            new ReadOnlySpan<byte>(source.ToPointer(), int.CreateChecked(sourceLen)),
            new Span<byte>(dest.ToPointer(), int.CreateChecked(destLen)));
    }
}
