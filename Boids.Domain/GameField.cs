using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Boids.Domain.Structures;

namespace Boids.Domain;

public class GameField
{
    private const float MaxSpeed = 1f;
    private const float MinSpeed = 0.3f;
    private readonly float _height;

    private readonly float _width;

    public GameField(float width, float height, int boidCount)
    {
        _width = width;
        _height = height;
        Boids = new List<Boid>();
        GenerateBoids(boidCount);
    }

    public List<Boid> Boids
    {
        get;
    }

    private void GenerateBoids(int boidCount)
    {
        Random random = new(Guid.NewGuid().GetHashCode());

        for (int i = 0; i < boidCount; i++)
        {
            Boids.Add(new Boid(
                new Position(
                    (float)(random.NextDouble() * _width),
                    (float)random.NextDouble() * _height),
                new Speed(
                    (float)random.NextDouble() * (MaxSpeed - MinSpeed) + MinSpeed,
                    (float)random.NextDouble() * (MaxSpeed - MinSpeed) + MinSpeed),
                (float)random.NextDouble() * (MaxSpeed - MinSpeed) + MinSpeed
            ));
        }
    }

    public void MoveBoids()
    {
        // foreach (Boid boid in Boids)
        // {
        //     boid.MoveTowardsGroup(Boids, 0.0001f);
        //     boid.FlyWithGroup(Boids, 0.01f);
        //     boid.AvoidCollisionWithWall(_width, _height, 0.05f);
        //     boid.Move();
        // }
        Parallel.ForEach(Boids, boid =>
                                {
                                    boid.MoveTowardsGroup(Boids, 0.0001f);
                                    boid.FlyWithGroup(Boids, 0.01f);
                                    boid.AvoidCollisionWithWall(_width, _height, 0.05f);
                                    boid.Move();
                                });
    }
}