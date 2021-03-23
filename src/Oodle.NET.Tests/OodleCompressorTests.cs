using System;
using System.Text;

using Xunit;

namespace Oodle.NET.Tests
{
	public class OodleCompressorTests
	{
		private readonly OodleCompressor _oodle;

		public OodleCompressorTests()
		{
			_oodle = new OodleCompressor(@"C:\Games\Fortnite\FortniteGame\Binaries\Win64\oo2core_5_win64.dll");
		}

		[Fact]
		public unsafe void CompressAndDecompress()
		{
			const OodleLZ_Compressor compressor = OodleLZ_Compressor.Kraken;
			var encoding = new ASCIIEncoding();
			var randomString = GetRandomString(0xFFF);
			var randomStringBuffer = encoding.GetBytes(randomString);

			//var memorySizeNeeded = OodleCompressor.GetCompressionBound((uint)randomStringBuffer.Length);
			var memorySizeNeeded = _oodle.MemorySizeNeeded(compressor, randomStringBuffer.Length);
			Assert.NotEqual(0L, memorySizeNeeded);

			var compressedBuffer = new byte[memorySizeNeeded];
			var compressedCount = _oodle.Compress(compressor, randomStringBuffer, randomStringBuffer.Length, compressedBuffer, OodleLZ_CompressionLevel.Normal, 0L, 0L, 0L, 0L, 0L);
			Assert.NotEqual(0L, compressedCount);

			var decompressedBuffer = new byte[randomStringBuffer.Length];
			var decompressedCount = _oodle.Decompress(compressedBuffer, compressedCount, decompressedBuffer, decompressedBuffer.Length, OodleLZ_FuzzSafe.No, OodleLZ_CheckCRC.No, OodleLZ_Verbosity.None, 0L, 0L, 0L, 0L, 0L, 0L, OodleLZ_Decode_ThreadPhase.Unthreaded);
			Assert.NotEqual(0L, decompressedCount);

			var decompressedRandomString = encoding.GetString(decompressedBuffer);

			Assert.Equal((int)decompressedCount, randomStringBuffer.Length);
			Assert.Equal(randomString, decompressedRandomString);
		}

		private static string GetRandomString(int length)
		{
			const string chars = "0123456789ABCDEF";
			var stringChars = new char[length];
			var random = new Random();

			for (var i = 0; i < stringChars.Length; i++)
			{
				stringChars[i] = chars[random.Next(chars.Length)];
			}

			return new string(stringChars);
		}
	}
}