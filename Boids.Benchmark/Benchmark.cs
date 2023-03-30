using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

using Boids.Domain;

namespace Boids.Benchmark;

[MemoryDiagnoser]
[RPlotExporter]
[SimpleJob(RunStrategy.Throughput, 1, 3, 5)]
public class Benchmark
{
    [Params(100, 1000, 2000, 5000, 10_000, 20_000)]
    public int boidCount;

    [Benchmark]
    public void Serial()
    {
        GameField gameField = new(boidCount);

        gameField.MoveBoidsSerial();
    }

    [Benchmark]
    public void Parallel_4()
    {
        GameField gameField = new(boidCount);

        gameField.MoveBoidsParallel(4);
    }

    [Benchmark]
    public void Parallel_8()
    {
        GameField gameField = new(boidCount);

        gameField.MoveBoidsParallel(8);
    }
}