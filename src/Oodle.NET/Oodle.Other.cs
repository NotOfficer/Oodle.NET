using System.Numerics;

namespace OodleDotNet;

public unsafe partial class Oodle
{
	/// <summary>
	/// Return the size you must alloc the compressed buffer.
	/// The compressed buffer must be allocated at least this big.
	/// </summary>
	public delegate* unmanaged<OodleCompressor, nint, nint> GetCompressedBufferSizeNeededFunctionPointer { get; }

	/// <inheritdoc cref="GetCompressedBufferSizeNeededFunctionPointer" />
	public nint GetCompressedBufferSizeNeeded<T>(OodleCompressor compressor, T sourceLen) where T : IBinaryInteger<T>
		=> GetCompressedBufferSizeNeededFunctionPointer(compressor, nint.CreateChecked(sourceLen));

}
