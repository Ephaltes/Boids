using BenchmarkDotNet.Attributes;

using Boids.Domain;

namespace Boids.Benchmark;

[MemoryDiagnoser]
public class Benchmark
{

    [Params(100,1000)]
    public int boidCount;
    [Params(50)]
    public int steps;
    
    [Benchmark()]
    public void Serial()
    {
        GameField gameField = new GameField(boidCount);

        for(int i=0;i<steps;i++)
        {
            gameField.MoveBoidsSerial();
        }
    }

    [Benchmark()]
    public void Parallel_4()
    {
        GameField gameField = new GameField(boidCount);

        for (int i = 0; i < steps; i++)
        {
            gameField.MoveBoidsParallel(4);
        }
    }

    [Benchmark()]
    public void Parallel_8()
    {
        GameField gameField = new GameField(boidCount);

        for(int i=0;i<steps;i++)
        {
            gameField.MoveBoidsParallel(8);
        }
    }
}