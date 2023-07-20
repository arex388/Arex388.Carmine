# BENCHMARKS



### .NET 7

#### CarmineClientFactory

| Method               |     Mean |   Error |  StdDev |   Gen0 | Allocated |
| -------------------- | -------: | ------: | ------: | -----: | --------: |
| CreateAndCacheClient | 145.7 ns | 0.30 ns | 0.25 ns | 0.0305 |     128 B |

#### Trips

| Method    |     Mean |    Error |    StdDev | Allocated |
| --------- | -------: | -------: | --------: | --------: |
| GetAsync  | 637.7 ms | 30.40 ms |  88.68 ms | 188.57 KB |
| ListAsync | 549.6 ms | 40.09 ms | 113.72 ms | 176.42 KB |

#### Users

| Method    |      Mean |     Error |    StdDev | Allocated |
| --------- | --------: | --------: | --------: | --------: |
| GetAsync  | 223.38 ms | 16.740 ms | 46.942 ms |   6.02 KB |
| ListAsync |  84.39 ms |  1.609 ms |  2.941 ms |   5.86 KB |

#### Vehicles

| Method    |     Mean |    Error |   StdDev | Allocated |
| --------- | -------: | -------: | -------: | --------: |
| GetAsync  | 83.63 ms | 1.632 ms | 2.726 ms |   5.85 KB |
| ListAsync | 83.83 ms | 1.662 ms | 2.383 ms |   5.89 KB |