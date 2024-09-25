using Arex388.Carmine.Benchmarks;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<TripsBenchmarks>();
BenchmarkRunner.Run<UsersBenchmarks>();
BenchmarkRunner.Run<VehiclesBenchmarks>();