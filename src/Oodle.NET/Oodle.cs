using System.Runtime.InteropServices;

namespace OodleDotNet;

public unsafe partial class Oodle : IDisposable
{
	private readonly nint _handle;

	public Oodle(string libraryPath) : this(NativeLibrary.Load(libraryPath)) { }
	public Oodle(nint handle)
	{
		Util.ThrowIfNull(handle);
		_handle = handle;

		var compressAddress = NativeLibrary.GetExport(_handle, "OodleLZ_Compress");
		var decompressAddress = NativeLibrary.GetExport(_handle, "OodleLZ_Decompress");
		var compressedBufferSizeNeeded = NativeLibrary.GetExport(_handle, "OodleLZ_GetCompressedBufferSizeNeeded");

		CompressFunctionPointer = (delegate* unmanaged<OodleCompressor, void*, IntPtr, void*, OodleCompressionLevel, void*, void*, void*, void*, IntPtr, IntPtr>)compressAddress;
		DecompressFunctionPointer = (delegate* unmanaged<void*, IntPtr, void*, IntPtr, OodleFuzzSafe, OodleCheckCRC, OodleVerbosity, void*, IntPtr, void*, void*, void*, IntPtr, OodleDecodeThreadPhase, IntPtr>)decompressAddress;
		GetCompressedBufferSizeNeededFunctionPointer = (delegate* unmanaged<OodleCompressor, IntPtr, IntPtr>)compressedBufferSizeNeeded;
	}

	private void ReleaseUnmanagedResources()
	{
		NativeLibrary.Free(_handle);
	}

	public void Dispose()
	{
		ReleaseUnmanagedResources();
		GC.SuppressFinalize(this);
	}

	~Oodle()
	{
		ReleaseUnmanagedResources();
	}
}
