using BenchmarkDotNet.Running;

namespace Arex388.Carmine.Benchmarks;

public static class Program {
	public static void Main() {
		BenchmarkRunner.Run<CarmineClientFactory>();
		BenchmarkRunner.Run<Trips>();
		BenchmarkRunner.Run<Users>();
		BenchmarkRunner.Run<Vehicles>();
	}
}