﻿<div align="center">

# 🚀 Oodle.NET

**A .NET wrapper for Oodle**  

[![GitHub release](https://img.shields.io/github/v/release/NotOfficer/Oodle.NET?logo=github)](https://github.com/NotOfficer/Oodle.NET/releases/latest)
[![Nuget](https://img.shields.io/nuget/v/Oodle.NET?logo=nuget)](https://www.nuget.org/packages/Oodle.NET)
![Nuget Downloads](https://img.shields.io/nuget/dt/Oodle.NET?logo=nuget)
[![GitHub issues](https://img.shields.io/github/issues/NotOfficer/Oodle.NET?logo=github)](https://github.com/NotOfficer/Oodle.NET/issues)
[![License](https://img.shields.io/github/license/NotOfficer/Oodle.NET)](https://github.com/NotOfficer/Oodle.NET/blob/master/LICENSE)

</div>

---

## 📦 Installation

Install via [NuGet](https://www.nuget.org/packages/Oodle.NET):

```powershell
Install-Package Oodle.NET
```

---

## ✨ Features

- Thin .NET wrapper over the native Oodle library
- Span-based `Compress`/`Decompress` with array, pointer, and `nint` overloads
- Exposes native entry points and `GetCompressedBufferSizeNeeded`
- Enums for compressors and compression levels
- Targets .NET 8/9, NuGet, MIT

---

## 🔧 Example Usage

```cs
using OodleDotNet;

using var oodle = new Oodle(@"C:\Test\oo2core_9_win64.dll");
var compressedBuffer = System.IO.File.ReadAllBytes(@"C:\Test\Example.bin");
var decompressedBuffer = new byte[decompressedSize];
var result = oodle.Decompress(compressedBuffer, decompressedBuffer);
```

---

## 🤝 Contributing

Contributions are **welcome and appreciated**!

Whether it's fixing a typo, suggesting an improvement, or submitting a pull request — every bit helps.

---

## 📄 License

This project is licensed under the [MIT License](https://github.com/NotOfficer/Oodle.NET/blob/master/LICENSE).

---

<div align="center">

⭐️ Star the repo if you find it useful!  
Feel free to open an issue if you have any questions or feedback.

</div>
