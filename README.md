<div align="center">

# Oodle.NET

A .NET wrapper for Oodle

[![GitHub release](https://img.shields.io/github/v/release/NotOfficer/Oodle.NET?logo=github)](https://github.com/NotOfficer/Oodle.NET/releases/latest) [![Nuget](https://img.shields.io/nuget/v/Oodle.NET?logo=nuget)](https://www.nuget.org/packages/Oodle.NET) ![Nuget DLs](https://img.shields.io/nuget/dt/Oodle.NET?logo=nuget) [![GitHub issues](https://img.shields.io/github/issues/NotOfficer/Oodle.NET?logo=github)](https://github.com/NotOfficer/Oodle.NET/issues) [![GitHub License](https://img.shields.io/github/license/NotOfficer/Oodle.NET)](https://github.com/NotOfficer/Oodle.NET/blob/master/LICENSE)

</div>

## Example Usage

```cs
using OodleDotNet;

using var oodle = new Oodle(@"C:\Test\oo2core_9_win64.dll");
var compressedBuffer = System.IO.File.ReadAllBytes(@"C:\Test\Example.bin");
var decompressedBuffer = new byte[decompressedSize];
var result = oodle.Decompress(compressedBuffer, decompressedBuffer);
```

### NuGet

```md
Install-Package Oodle.NET
```

### Contribute

If you can provide any help, may it only be spell checking please contribute!  
I am open for any contribution.
