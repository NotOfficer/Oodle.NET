using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using Xunit;

namespace OodleDotNet.Tests;

public class Tests
{
	private readonly Oodle _oodle = new(Path.Combine(
		Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
		@"Libraries\oo2core_9_win64.dll"));

	[Fact]
	public void CompressAndDecompress()
	{
		const OodleCompressor compressor = OodleCompressor.Kraken;
		var randomString = GetRandomString(8192);
		var randomStringBuffer = Encoding.ASCII.GetBytes(randomString);

		var compressedBufferSize = _oodle.GetCompressedBufferSizeNeeded(compressor, randomStringBuffer.Length);
		Assert.NotEqual(0, compressedBufferSize);

		var compressedBuffer = new byte[compressedBufferSize];
		var compressedSize = (int)_oodle.Compress(compressor, OodleCompressionLevel.Max, randomStringBuffer, compressedBuffer);
		Assert.NotEqual(0, compressedSize);

		var decompressedBuffer = new byte[randomStringBuffer.Length];
		var decompressedSize = (int)_oodle.Decompress(compressedBuffer.AsSpan(0, compressedSize), decompressedBuffer);
		Assert.NotEqual(0, decompressedSize);

		Assert.Equal(decompressedSize, randomStringBuffer.Length);
		Assert.True(randomStringBuffer.AsSpan().SequenceEqual(decompressedBuffer));
	}

	private static string GetRandomString(int length)
	{
		const string chars = "0123456789ABCDEF";
		var result = new string('\0', length);
		var resultReadonlySpan = result.AsSpan();
		var resultSpan = MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in resultReadonlySpan.GetPinnableReference()), length);

		for (var i = 0; i < resultSpan.Length; i++)
		{
			resultSpan[i] = chars[Random.Shared.Next(chars.Length)];
		}

		return result;
	}
}
