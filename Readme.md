```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3737/23H2/2023Update/SunValley3)
AMD Ryzen 9 7950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK 8.0.101
  [Host]               : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 8.0             : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET Framework 4.7.2 : .NET Framework 4.8.1 (4.8.9241.0), X64 RyuJIT VectorSize=256


```
| Method                  | Job                  | Runtime              | Mean        | Error     | StdDev    | Ratio | RatioSD | Code Size |
|------------------------ |--------------------- |--------------------- |------------:|----------:|----------:|------:|--------:|----------:|
| InlinedCall             | .NET 8.0             | .NET 8.0             |    93.96 ns |  1.361 ns |  1.207 ns |  1.00 |    0.00 |      28 B |
| NotInlinedCall          | .NET 8.0             | .NET 8.0             |   837.60 ns |  2.149 ns |  1.905 ns |  8.92 |    0.11 |      93 B |
| EventInvoke             | .NET 8.0             | .NET 8.0             |   343.54 ns |  1.754 ns |  1.370 ns |  3.66 |    0.04 |     220 B |
| EventInvokeTwoListeners | .NET 8.0             | .NET 8.0             | 3,589.77 ns | 21.840 ns | 18.237 ns | 38.24 |    0.61 |      57 B |
|                         |                      |                      |             |           |           |       |         |           |
| InlinedCall             | .NET Framework 4.7.2 | .NET Framework 4.7.2 |    95.42 ns |  1.473 ns |  1.378 ns |  1.00 |    0.00 |      44 B |
| NotInlinedCall          | .NET Framework 4.7.2 | .NET Framework 4.7.2 | 1,017.96 ns | 19.889 ns | 18.604 ns | 10.67 |    0.29 |      89 B |
| EventInvoke             | .NET Framework 4.7.2 | .NET Framework 4.7.2 | 1,023.16 ns |  5.145 ns |  4.813 ns | 10.72 |    0.17 |      97 B |
| EventInvokeTwoListeners | .NET Framework 4.7.2 | .NET Framework 4.7.2 | 2,654.00 ns |  9.589 ns |  8.970 ns | 27.82 |    0.38 |      63 B |
