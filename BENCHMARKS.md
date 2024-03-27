# BENCHMARKS



### .NET 8

#### CarmineClientFactory

| Method               |     Mean |   Error |  StdDev |   Gen0 | Allocated |
| -------------------- | -------: | ------: | ------: | -----: | --------: |
| CreateAndCacheClient | 117.1 ns | 1.07 ns | 1.00 ns | 0.0305 |     128 B |

#### Trips

| Method    |     Mean |    Error |   StdDev | Allocated |
| --------- | -------: | -------: | -------: | --------: |
| GetAsync  | 947.7 ms | 59.06 ms | 165.6 ms | 127.03 KB |
| ListAsync | 543.0 ms | 47.60 ms | 137.3 ms |  39.95 KB |

#### Users

| Method    |      Mean |     Error |    StdDev | Allocated |
| --------- | --------: | --------: | --------: | --------: |
| GetAsync  | 218.99 ms | 17.949 ms | 49.737 ms |   5.84 KB |
| ListAsync |  83.36 ms |  1.603 ms |  2.140 ms |   5.68 KB |

#### Vehicles

| Method    |     Mean |    Error |   StdDev | Allocated |
| --------- | -------: | -------: | -------: | --------: |
| GetAsync  | 82.54 ms | 1.595 ms | 1.332 ms |   5.65 KB |
| ListAsync | 84.09 ms | 1.536 ms | 2.392 ms |    5.7 KB |



### .NET 7

#### CarmineClientFactory

| Method               |     Mean |   Error |  StdDev |   Gen0 | Allocated |
| -------------------- | -------: | ------: | ------: | -----: | --------: |
| CreateAndCacheClient | 142.4 ns | 0.80 ns | 0.71 ns | 0.0305 |     128 B |

#### Trips

| Method    |     Mean |    Error |   StdDev | Allocated |
| --------- | -------: | -------: | -------: | --------: |
| GetAsync  | 640.0 ms | 27.11 ms | 76.47 ms | 183.66 KB |
| ListAsync | 548.3 ms | 34.35 ms | 99.66 ms | 175.02 KB |

#### Users

| Method    |      Mean |     Error |    StdDev | Allocated |
| --------- | --------: | --------: | --------: | --------: |
| GetAsync  | 257.21 ms | 12.556 ms | 36.428 ms |   6.02 KB |
| ListAsync |  82.45 ms |  1.568 ms |  1.743 ms |   5.79 KB |

#### Vehicles

| Method    |     Mean |    Error |   StdDev | Allocated |
| --------- | -------: | -------: | -------: | --------: |
| GetAsync  | 82.51 ms | 1.645 ms | 2.838 ms |   5.86 KB |
| ListAsync | 83.63 ms | 1.665 ms | 2.543 ms |   5.81 KB |