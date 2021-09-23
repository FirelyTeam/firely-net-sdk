``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19043.1237 (21H1/May2021Update)
11th Gen Intel Core i7-1185G7 3.00GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-rc.1.21463.6
  [Host]     : .NET 5.0.9 (5.0.921.35908), X64 RyuJIT
  DefaultJob : .NET 5.0.9 (5.0.921.35908), X64 RyuJIT


```
|                     Method |     Mean |    Error |   StdDev |      Gen 0 |     Gen 1 | Gen 2 | Allocated |
|--------------------------- |---------:|---------:|---------:|-----------:|----------:|------:|----------:|
| ToTypedElementOnSourceNode | 885.8 ms | 26.63 ms | 77.26 ms | 23000.0000 | 3000.0000 |     - |    143 MB |
