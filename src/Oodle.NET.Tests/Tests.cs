using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace OodleDotNet.Tests;

public class Tests : IAsyncLifetime
{
	private string _filePath = null!;
	private Oodle _oodle = null!;

	public async Task InitializeAsync()
	{
		_filePath = await DownloadAsync();
		_oodle = new Oodle(_filePath);
	}

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

	public Task DisposeAsync()
	{
		_oodle.Dispose();
		File.Delete(_filePath);
		return Task.CompletedTask;
	}

	private static string GetRandomString(int length)
	{
		const string pool = "0123456789ABCDEF";
		return RandomNumberGenerator.GetString(pool, length);
	}

	public static async Task<string> DownloadAsync()
	{
		if (!OperatingSystem.IsWindows() && !OperatingSystem.IsLinux())
		{
			throw new PlatformNotSupportedException("this test is not supported on the current platform");
		}

		const string baseUrl = "https://github.com/WorkingRobot/OodleUE/releases/download/2024-11-01-726/"; // 2.9.13
		string url;
		string entryName;

		if (OperatingSystem.IsWindows())
		{
			url = baseUrl + "msvc.zip";
			entryName = "bin/Release/oodle-data-shared.dll";
		}
		else if (OperatingSystem.IsLinux())
		{
			url = baseUrl + "gcc.zip";
			entryName = "lib/Release/liboodle-data-shared.so";
		}
		else
		{
			throw new UnreachableException();
		}

		using var client = new HttpClient(new SocketsHttpHandler
		{
			UseProxy = false,
			UseCookies = true,
			AutomaticDecompression = DecompressionMethods.All
		});
		using var response = await client.GetAsync(url);
		response.EnsureSuccessStatusCode();
		await using var responseStream = await response.Content.ReadAsStreamAsync();
		using var zip = new ZipArchive(responseStream, ZipArchiveMode.Read);
		var entry = zip.GetEntry(entryName);
		ArgumentNullException.ThrowIfNull(entry, "oodle entry in zip not found");
		await using var entryStream = entry.Open();
		var filePath = Path.GetTempFileName();
		await using var fs = File.Create(filePath);
		await entryStream.CopyToAsync(fs);
		return filePath;
	}
}
