# BENCHMARKS

- BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.4894/22H2/2022Update)
- Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
- .NET SDK 8.0.400
  - [Host]     : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2
  - DefaultJob : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2



#### 2024-09-25 (.NET 8)

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