using System.Threading.Tasks;

namespace Boids.Domain;

public class GameField
{
    public const int Width = 1280;
    public const int Height = 720;
    
    public Boids Boids
    {
        get;
    }

    public GameField(int boidCount)
    {
        Boids = Boids.Generate(boidCount);
    }

    public void MoveBoidsParallel()
    {
        Parallel.ForEach(Boids, boid => 
                                {
                                    boid.MoveTowardsGroup(Boids, 0.0001f);
                                    boid.AdjustSpeedToGroup(Boids, 0.01f);
                                    boid.AvoidCollisionWithWall(Width, Height, 0.1f);
                                    boid.Move();
                                });
    }
    
    public void MoveBoidsParallel(int maxDegreeOfParallelism)
    {
        Parallel.ForEach(Boids, new ParallelOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism }, boid => 
                                {
                                    boid.MoveTowardsGroup(Boids, 0.0001f);
                                    boid.AdjustSpeedToGroup(Boids, 0.01f);
                                    boid.AvoidCollisionWithWall(Width, Height, 0.1f);
                                    boid.Move();
                                });
    }
    public void MoveBoidsSerial()
    {
        foreach (Boid boid in Boids)
        {
            boid.MoveTowardsGroup(Boids, 0.0001f);
            boid.AdjustSpeedToGroup(Boids, 0.01f);
            boid.AvoidCollisionWithWall(Width, Height, 0.05f);
            boid.Move();
        }
    }
}