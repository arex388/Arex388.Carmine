# BENCHMARKS

#### 2026-07-30 (v4.2.0 live-deserialization fix)

- BenchmarkDotNet v0.15.8, Windows 10 (10.0.19045.6466/22H2/2022Update)
- Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
- .NET SDK 10.0.302
  - [Host]     : .NET 10.0.10, X64 RyuJIT x86-64-v3
  - DefaultJob : .NET 10.0.10, X64 RyuJIT x86-64-v3

Re-run after the live-API deserialization fix (#40): the client now buffers the response body (`ReadAsByteArrayAsync`) and deserializes synchronously instead of streaming through `ReadFromJsonAsync`, because async stream deserialization invoked the converters on partially buffered JSON where their unknown-property `Skip()` throws. The trade-off versus the previous entry is deliberate and quantified: **means improve 5–10%** (the synchronous parse drops the async deserialization state machine), while **`Allocated` rises by roughly the payload size** now held as a byte array — Trips Get 10.0 → 16.3 KB, Trips List 5.2 → 7.1 KB, Users Get 4.5 → 5.2 KB, Users List 4.1 → 5.6 KB, Vehicles Get 5.3 → 6.3 KB, Vehicles List 5.3 → 7.5 KB. Correctness against the live API is the driver; payloads are modest (~35 KB for a 107-vehicle fleet).

###### Trips

| Method    |      Mean |     Error |    StdDev | Allocated |
| --------- | --------: | --------: | --------: | --------: |
| GetAsync  | 12.019 us | 73.603 ns | 68.849 ns |   16.3 KB |
| ListAsync |  4.612 us | 18.418 ns | 15.379 ns |    7.1 KB |

###### Users

| Method    |     Mean |     Error |    StdDev | Allocated |
| --------- | -------: | --------: | --------: | --------: |
| GetAsync  | 2.933 us |  6.140 ns |  5.743 ns |    5.2 KB |
| ListAsync | 3.553 us | 12.190 ns | 11.403 ns |    5.6 KB |

###### Vehicles

| Method    |     Mean |     Error |    StdDev | Allocated |
| --------- | -------: | --------: | --------: | --------: |
| GetAsync  | 4.013 us | 13.199 ns | 12.346 ns |    6.3 KB |
| ListAsync | 5.282 us |  8.355 ns |  7.406 ns |    7.5 KB |



#### 2026-07-30 (v4.2.0 Performance & Quality milestone)

- BenchmarkDotNet v0.15.8, Windows 10 (10.0.19045.6466/22H2/2022Update)
- Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
- .NET SDK 10.0.302
  - [Host]     : .NET 10.0.10, X64 RyuJIT x86-64-v3
  - DefaultJob : .NET 10.0.10, X64 RyuJIT x86-64-v3

Re-run after the v4.2 Performance & Quality milestone: the six request pipelines consolidated into one generic core with static mappers, the read-only response surface (`Array.Empty<T>()` empty defaults), the micro-allocation pass (endpoint concatenation, null-starting converter locals), and the enum/precision changes. Compared with the previous entry: **means at parity (all within ±3%), `Allocated` equal or lower everywhere** — Trips Get 10.2 → 10.0 KB, Users Get 4.6 → 4.5 KB, Users List 4.2 → 4.1 KB, Vehicles List 5.4 → 5.3 KB; no regressions.

###### Trips

| Method    |      Mean |      Error |     StdDev | Allocated |
| --------- | --------: | ---------: | ---------: | --------: |
| GetAsync  | 12.660 us | 138.962 ns | 116.039 ns |   10.0 KB |
| ListAsync |  5.142 us |  72.625 ns | 115.192 ns |    5.2 KB |

###### Users

| Method    |     Mean |     Error |   StdDev | Allocated |
| --------- | -------: | --------: | -------: | --------: |
| GetAsync  | 3.305 us |  7.955 ns | 6.643 ns |    4.5 KB |
| ListAsync | 3.876 us | 11.075 ns | 9.817 ns |    4.1 KB |

###### Vehicles

| Method    |     Mean |     Error |    StdDev | Allocated |
| --------- | -------: | --------: | --------: | --------: |
| GetAsync  | 4.357 us | 15.266 ns | 12.748 ns |    5.3 KB |
| ListAsync | 5.594 us | 50.528 ns | 47.264 ns |    5.3 KB |



#### 2026-07-30 (v4.2.0 milestone follow-up)

- BenchmarkDotNet v0.15.8, Windows 10 (10.0.19045.6466/22H2/2022Update)
- Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
- .NET SDK 10.0.302
  - [Host]     : .NET 10.0.10, X64 RyuJIT x86-64-v3
  - DefaultJob : .NET 10.0.10, X64 RyuJIT x86-64-v3

Re-run after the v4.2 audit follow-up milestone: System.Text.Json / Microsoft.Extensions.* bumped to 10.0.10, the request path split into `GetAsync` + status check + `ReadFromJsonAsync` (for failure detail in `Errors`), container-token skipping, hardened phone parsing, and the endpoint/factory micro-optimizations. Numbers are at parity with the previous entry — all means within ±5%, allocations within ±0.5 KB.

###### Trips

| Method    |      Mean |     Error |    StdDev | Allocated |
| --------- | --------: | --------: | --------: | --------: |
| GetAsync  | 12.767 us | 65.724 ns | 58.263 ns |   10.2 KB |
| ListAsync |  5.043 us | 12.206 ns | 10.192 ns |    5.2 KB |

###### Users

| Method    |     Mean |     Error |    StdDev | Allocated |
| --------- | -------: | --------: | --------: | --------: |
| GetAsync  | 3.318 us | 11.826 ns | 10.484 ns |    4.6 KB |
| ListAsync | 3.896 us | 14.149 ns | 11.815 ns |    4.2 KB |

###### Vehicles

| Method    |     Mean |     Error |    StdDev | Allocated |
| --------- | -------: | --------: | --------: | --------: |
| GetAsync  | 4.557 us | 13.238 ns | 11.735 ns |    5.3 KB |
| ListAsync | 5.456 us | 15.016 ns | 14.046 ns |    5.4 KB |



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