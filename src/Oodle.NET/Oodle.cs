using System.Runtime.InteropServices;

namespace OodleDotNet;

/// <summary>
/// Wrapper class for the native oodle library
/// </summary>
public unsafe partial class Oodle : IDisposable
{
    private readonly nint _handle;

    /// <summary>
    /// Initializes via a native oodle library path
    /// </summary>
    /// <returns/>
    /// <param name="libraryPath">The path of the native oodle library to be loaded</param>
    /// <inheritdoc cref="NativeLibrary.Load(string)"/>
    public Oodle(string libraryPath) : this(NativeLibrary.Load(libraryPath)) { }

    /// <summary>
    /// Initializes via a native oodle library handle
    /// </summary>
    /// <param name="handle">The handle of the native oodle library to be loaded</param>
    /// <exception cref="ArgumentNullException">If the handle is invalid</exception>
    /// <exception cref="EntryPointNotFoundException">If the handle/library is invalid or not supported</exception>
    public Oodle(nint handle)
    {
        Util.ThrowIfNull(handle);
        _handle = handle;

        var compressAddress = NativeLibrary.GetExport(_handle, "OodleLZ_Compress");
        var decompressAddress = NativeLibrary.GetExport(_handle, "OodleLZ_Decompress");
        var compressedBufferSizeNeeded = NativeLibrary.GetExport(_handle, "OodleLZ_GetCompressedBufferSizeNeeded");

        CompressFunctionPointer = (delegate* unmanaged<OodleCompressor, void*, IntPtr, void*, OodleCompressionLevel, void*, void*, void*, void*, IntPtr, IntPtr>)compressAddress;
        DecompressFunctionPointer = (delegate* unmanaged<void*, IntPtr, void*, IntPtr, OodleFuzzSafe, OodleCheckCrc, OodleVerbosity, void*, IntPtr, void*, void*, void*, IntPtr, OodleDecodeThreadPhase, IntPtr>)decompressAddress;
        GetCompressedBufferSizeNeededFunctionPointer = (delegate* unmanaged<OodleCompressor, IntPtr, IntPtr>)compressedBufferSizeNeeded;
    }

    private void ReleaseUnmanagedResources()
    {
        if (_handle != nint.Zero)
            NativeLibrary.Free(_handle);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    ~Oodle()
    {
        ReleaseUnmanagedResources();
    }
}
