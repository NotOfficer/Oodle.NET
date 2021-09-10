using System;
using System.Runtime.InteropServices;

namespace Oodle.NET
{
	public
#if NET5_0_OR_GREATER
	unsafe
#endif
	class OodleCompressor : IDisposable
	{
		private readonly IntPtr _handle;

#if NET5_0_OR_GREATER
		public readonly delegate* unmanaged[Cdecl]<OodleLZ_Compressor, byte[], long, byte[], OodleLZ_CompressionLevel, long, long, long, long, long, long> CompressBuffer;
		public readonly delegate* unmanaged[Cdecl]<OodleLZ_Compressor, IntPtr, long, IntPtr, OodleLZ_CompressionLevel, long, long, long, long, long, long> Compress;
		public readonly delegate* unmanaged[Cdecl]<byte[], long, byte[], long, OodleLZ_FuzzSafe, OodleLZ_CheckCRC, OodleLZ_Verbosity, long, long, long, long, long, long, OodleLZ_Decode_ThreadPhase, long> DecompressBuffer;
		public readonly delegate* unmanaged[Cdecl]<IntPtr, long, IntPtr, long, OodleLZ_FuzzSafe, OodleLZ_CheckCRC, OodleLZ_Verbosity, long, long, long, long, long, long, OodleLZ_Decode_ThreadPhase, long> Decompress;
		public readonly delegate* unmanaged[Cdecl]<OodleLZ_Compressor, long, long> MemorySizeNeeded;
#else
		public delegate long OodleLZ_CompressBuffer(OodleLZ_Compressor compressor, byte[] src, long src_size, byte[] dst, OodleLZ_CompressionLevel level, long opts, long context, long unused, long scratch, long scratch_size);
		public readonly OodleLZ_CompressBuffer CompressBuffer;
		public delegate long OodleLZ_Compress(OodleLZ_Compressor compressor, IntPtr src, long src_size, IntPtr dst, OodleLZ_CompressionLevel level, long opts, long context, long unused, long scratch, long scratch_size);
		public readonly OodleLZ_Compress Compress;

		public delegate long OodleLZ_DecompressBuffer(byte[] src, long src_size, byte[] dst, long dst_size, OodleLZ_FuzzSafe fuzz, OodleLZ_CheckCRC crc, OodleLZ_Verbosity verbosity, long context, long e, long callback, long callback_ctx, long scratch, long scratch_size, OodleLZ_Decode_ThreadPhase thread_phase);
		public readonly OodleLZ_DecompressBuffer DecompressBuffer;
		public delegate long OodleLZ_Decompress(IntPtr src, long src_size, IntPtr dst, long dst_size, OodleLZ_FuzzSafe fuzz, OodleLZ_CheckCRC crc, OodleLZ_Verbosity verbosity, long context, long e, long callback, long callback_ctx, long scratch, long scratch_size, OodleLZ_Decode_ThreadPhase thread_phase);
		public readonly OodleLZ_Decompress Decompress;

		public delegate long OodleLZDecoder_MemorySizeNeeded(OodleLZ_Compressor compressor, long size);
		public readonly OodleLZDecoder_MemorySizeNeeded MemorySizeNeeded;
#endif

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
			CompressBuffer = (delegate* unmanaged[Cdecl]<OodleLZ_Compressor, byte[], long, byte[], OodleLZ_CompressionLevel, long, long, long, long, long, long>)compressAddress;
			Compress = (delegate* unmanaged[Cdecl]<OodleLZ_Compressor, IntPtr, long, IntPtr, OodleLZ_CompressionLevel, long, long, long, long, long, long>)compressAddress;
			DecompressBuffer = (delegate* unmanaged[Cdecl]<byte[], long, byte[], long, OodleLZ_FuzzSafe, OodleLZ_CheckCRC, OodleLZ_Verbosity, long, long, long, long, long, long, OodleLZ_Decode_ThreadPhase, long>)decompressAddress;
			Decompress = (delegate* unmanaged[Cdecl]<IntPtr, long, IntPtr, long, OodleLZ_FuzzSafe, OodleLZ_CheckCRC, OodleLZ_Verbosity, long, long, long, long, long, long, OodleLZ_Decode_ThreadPhase, long>)decompressAddress;
			MemorySizeNeeded = (delegate* unmanaged[Cdecl]<OodleLZ_Compressor, long, long>)memorySizeNeededAddress;
#else
			CompressBuffer = Marshal.GetDelegateForFunctionPointer<OodleLZ_CompressBuffer>(compressAddress);
			Compress = Marshal.GetDelegateForFunctionPointer<OodleLZ_Compress>(compressAddress);
			DecompressBuffer = Marshal.GetDelegateForFunctionPointer<OodleLZ_DecompressBuffer>(decompressAddress);
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
}