# BENCHMARKS

#### 2026-07-30

- BenchmarkDotNet v0.15.8, Windows 10 (10.0.19045.6466/22H2/2022Update)
- Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
- .NET SDK 10.0.101
  - [Host]     : .NET 10.0.1 (10.0.1, 10.0.125.57005), X64 RyuJIT x86-64-v3
  - DefaultJob : .NET 10.0.1 (10.0.1, 10.0.125.57005), X64 RyuJIT x86-64-v3

Converters skip unknown properties with `Utf8JsonReader.Skip()` instead of materializing a throwaway `JsonElement`. Not directly comparable to the 2026-01-09 entry: the shared JSON fixtures were also rewritten to real API payload shapes (and the trips fixture is much smaller), so both the payloads and the parsing changed.

###### Trips

| Method    |      Mean |     Error |    StdDev | Allocated |
| --------- | --------: | --------: | --------: | --------: |
| GetAsync  | 13.319 us | 64.036 ns | 56.766 ns |   10.3 KB |
| ListAsync |  4.955 us | 11.958 ns | 10.601 ns |    4.7 KB |

###### Users

| Method    |     Mean |     Error |    StdDev | Allocated |
| --------- | -------: | --------: | --------: | --------: |
| GetAsync  | 3.693 us |  6.507 ns |  5.080 ns |    4.9 KB |
| ListAsync | 4.281 us | 38.851 ns | 34.441 ns |    4.5 KB |

###### Vehicles

| Method    |     Mean |     Error |    StdDev | Allocated |
| --------- | -------: | --------: | --------: | --------: |
| GetAsync  | 4.612 us | 46.474 ns | 45.644 ns |    5.6 KB |
| ListAsync | 5.798 us | 54.515 ns | 48.326 ns |    5.6 KB |



#### 2026-01-09

- BenchmarkDotNet v0.15.8, Windows 10 (10.0.19045.6466/22H2/2022Update)
- Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
- .NET SDK 10.0.101
  - [Host]     : .NET 10.0.1 (10.0.1, 10.0.125.57005), X64 RyuJIT x86-64-v3
  - DefaultJob : .NET 10.0.1 (10.0.1, 10.0.125.57005), X64 RyuJIT x86-64-v3

###### Trips

| Method    |         Mean |       Error |      StdDev |    Gen0 |   Gen1 | Allocated |
| --------- | -----------: | ----------: | ----------: | ------: | -----: | --------: |
| GetAsync  |     837.0 ns |     1.51 ns |     1.26 ns |  0.5064 |      - |   2.07 KB |
| ListAsync | 382,826.1 ns | 2,245.99 ns | 1,991.01 ns | 36.6211 | 6.3477 | 151.58 KB |

###### Users

| Method    |        Mean |     Error |    StdDev |   Gen0 | Allocated |
| --------- | ----------: | --------: | --------: | -----: | --------: |
| GetAsync  |    854.9 ns |   8.77 ns |   8.20 ns | 0.5064 |   2.07 KB |
| ListAsync | 22,637.6 ns | 159.79 ns | 141.65 ns | 1.9531 |   8.06 KB |

###### Vehicles

| Method    |        Mean |    Error |   StdDev |   Gen0 | Allocated |
| --------- | ----------: | -------: | -------: | -----: | --------: |
| GetAsync  |    841.6 ns |  3.40 ns |  2.84 ns | 0.5064 |   2.07 KB |
| ListAsync | 21,770.0 ns | 54.80 ns | 51.26 ns | 3.2959 |  13.52 KB |



#### 2024-09-25

- BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.4894/22H2/2022Update)
- Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
- .NET SDK 8.0.400
  - [Host]     : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2
  - DefaultJob : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2

###### Trips

| Method    |     Mean | Allocated |
| --------- | -------: | --------: |
| GetAsync  | 718.6 ms |  91.05 KB |
| ListAsync | 411.4 ms |   30.2 KB |

###### Users

| Method    |      Mean | Allocated |
| --------- | --------: | --------: |
| GetAsync  | 222.38 ms |  36.49 KB |
| ListAsync |  83.07 ms |   6.39 KB |

###### Vehicles

| Method    |     Mean | Allocated |
| --------- | -------: | --------: |
| GetAsync  | 82.25 ms |   6.43 KB |
| ListAsync | 81.21 ms |   6.33 KB |