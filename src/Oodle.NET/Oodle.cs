using System.Runtime.InteropServices;

namespace OodleDotNet;

/// <summary>
/// Wrapper class for the native oodle library
/// </summary>
public unsafe partial class Oodle : IDisposable
{
    /// <summary>
    /// Library handle for the current oodle instance
    /// </summary>
    public nint Handle { get; }

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
        Handle = handle;

        nint compressAddress = NativeLibrary.GetExport(Handle, "OodleLZ_Compress");
        nint decompressAddress = NativeLibrary.GetExport(Handle, "OodleLZ_Decompress");
        nint compressedBufferSizeNeeded = NativeLibrary.GetExport(Handle, "OodleLZ_GetCompressedBufferSizeNeeded");

        CompressFunctionPointer = (delegate* unmanaged<OodleCompressor, void*, IntPtr, void*, OodleCompressionLevel, void*, void*, void*, void*, IntPtr, IntPtr>)compressAddress;
        DecompressFunctionPointer = (delegate* unmanaged<void*, IntPtr, void*, IntPtr, OodleFuzzSafe, OodleCheckCrc, OodleVerbosity, void*, IntPtr, void*, void*, void*, IntPtr, OodleDecodeThreadPhase, IntPtr>)decompressAddress;
        GetCompressedBufferSizeNeededFunctionPointer = (delegate* unmanaged<OodleCompressor, IntPtr, IntPtr>)compressedBufferSizeNeeded;
    }

    private void ReleaseUnmanagedResources()
    {
        if (Handle != nint.Zero)
            NativeLibrary.Free(Handle);
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
