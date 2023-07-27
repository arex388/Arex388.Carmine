using Arex388.Carmine.Benchmarks;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<CarmineClientFactory>();
BenchmarkRunner.Run<Trips>();
BenchmarkRunner.Run<Users>();
BenchmarkRunner.Run<Vehicles>();