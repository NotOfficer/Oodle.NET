<div align="center">

# Oodle.NET

A .NET wrapper for Oodle

[![GitHub release](https://img.shields.io/github/v/release/NotOfficer/Oodle.NET?logo=github)](https://github.com/NotOfficer/Oodle.NET/releases/latest) [![Nuget](https://img.shields.io/nuget/v/Oodle.NET?logo=nuget)](https://www.nuget.org/packages/Oodle.NET) ![Nuget DLs](https://img.shields.io/nuget/dt/Oodle.NET?logo=nuget) [![GitHub issues](https://img.shields.io/github/issues/NotOfficer/Oodle.NET?logo=github)](https://github.com/NotOfficer/Oodle.NET/issues) [![GitHub License](https://img.shields.io/github/license/NotOfficer/Oodle.NET)](https://github.com/NotOfficer/Oodle.NET/blob/master/LICENSE)

</div>

## Example Usage

```cs
using Oodle.NET;

using var oodle = new OodleCompressor(@"C:\Test\oo2core_8_win64.dll");
var compressedBuffer = System.IO.File.ReadAllBytes(@"C:\Test\Example.bin");
var decompressedBuffer = new byte[decompressedSize];
var result = oodle.Decompress(compressedBuffer, compressedBuffer.Length, decompressedBuffer, decompressedSize, OodleLZ_FuzzSafe.No, OodleLZ_CheckCRC.No, OodleLZ_Verbosity.None, 0L, 0L, 0L, 0L, 0L, 0L, OodleLZ_Decode_ThreadPhase.Unthreaded);
```

### Info

The `UsmapOptions.OodlePath` is only required for loading oodle compressed usmap files.  
Oodle decompression has been tested on windows and linux (x64).

### NuGet

```md
Install-Package Oodle.NET
```

### Contribute

If you can provide any help, may it only be spell checking please contribute!  
I am open for any contribution.
