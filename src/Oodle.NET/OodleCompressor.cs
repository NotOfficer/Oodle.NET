using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Oodle.NET
{
#if NET5_0_OR_GREATER
	public unsafe class OodleCompressor : IDisposable
#else
	public class OodleCompressor : IDisposable
#endif
	{
		private readonly IntPtr _handle;

#if NET5_0_OR_GREATER
		public readonly delegate* unmanaged[Cdecl]<OodleLZ_Compressor, byte[], long, byte[], OodleLZ_CompressionLevel, long, long, long, long, long, long> Compress;
		public readonly delegate* unmanaged[Cdecl]<byte[], long, byte[], long, OodleLZ_FuzzSafe, OodleLZ_CheckCRC, OodleLZ_Verbosity, long, long, long, long, long, long, OodleLZ_Decode_ThreadPhase, long> Decompress;
		public readonly delegate* unmanaged[Cdecl]<OodleLZ_Compressor, long, long> MemorySizeNeeded;
#else
		public delegate long OodleLZ_Compress(OodleLZ_Compressor compressor, byte[] src, long src_size, byte[] dst, OodleLZ_CompressionLevel level, long opts, long context, long unused, long scratch, long scratch_size);
		public readonly OodleLZ_Compress Compress;

		public delegate long OodleLZ_Decompress(byte[] src, long src_size, byte[] dst, long dst_size, OodleLZ_FuzzSafe fuzz, OodleLZ_CheckCRC crc, OodleLZ_Verbosity verbosity, long context, long e, long callback, long callback_ctx, long scratch, long scratch_size, OodleLZ_Decode_ThreadPhase thread_phase);
		public readonly OodleLZ_Decompress Decompress;

		public delegate long OodleLZDecoder_MemorySizeNeeded(OodleLZ_Compressor compressor, long size);
		public readonly OodleLZDecoder_MemorySizeNeeded MemorySizeNeeded;
#endif

		public OodleCompressor(string libraryPath, Assembly assembly, DllImportSearchPath? searchPath = null) : this(NativeLibrary.Load(libraryPath, assembly, searchPath)) { }
		public OodleCompressor(string libraryPath) : this(NativeLibrary.Load(libraryPath)) { }
		public OodleCompressor(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentOutOfRangeException(nameof(handle), $"Failed to initialize {nameof(OodleCompressor)}");
			}

			_handle = handle;
			var compressAddress = NativeLibrary.GetExport(_handle, "OodleLZ_Compress");
			var decompressAddress = NativeLibrary.GetExport(_handle, "OodleLZ_Decompress");
			var memorySizeNeededAddress = NativeLibrary.GetExport(_handle, "OodleLZDecoder_MemorySizeNeeded");

#if NET5_0_OR_GREATER
			Compress = (delegate* unmanaged[Cdecl]<OodleLZ_Compressor, byte[], long, byte[], OodleLZ_CompressionLevel, long, long, long, long, long, long>)compressAddress;
			Decompress = (delegate* unmanaged[Cdecl]<byte[], long, byte[], long, OodleLZ_FuzzSafe, OodleLZ_CheckCRC, OodleLZ_Verbosity, long, long, long, long, long, long, OodleLZ_Decode_ThreadPhase, long>)decompressAddress;
			MemorySizeNeeded = (delegate* unmanaged[Cdecl]<OodleLZ_Compressor, long, long>)memorySizeNeededAddress;
#else
			Compress = Marshal.GetDelegateForFunctionPointer<OodleLZ_Compress>(compressAddress);
			Decompress = Marshal.GetDelegateForFunctionPointer<OodleLZ_Decompress>(decompressAddress);
			MemorySizeNeeded = Marshal.GetDelegateForFunctionPointer<OodleLZDecoder_MemorySizeNeeded>(memorySizeNeededAddress);
#endif
		}

		public static uint GetCompressionBound(uint bufferSize)
		{
			return bufferSize + 274 * ((bufferSize + 0x3FFFF) / 0x40000);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}

			if (_handle != IntPtr.Zero)
			{
				NativeLibrary.Free(_handle);
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}

	public enum OodleLZ_FuzzSafe
	{
		No,
		Yes
	}

	public enum OodleLZ_CheckCRC
	{
		No,
		Yes
	}

	public enum OodleLZ_Verbosity
	{
		None,
		Max = 3
	}

	public enum OodleLZ_Decode_ThreadPhase
	{
		ThreadPhase1 = 0x1,
		ThreadPhase2 = 0x2,

		Unthreaded = ThreadPhase1 | ThreadPhase2
	}
}