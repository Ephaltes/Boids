using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Boids.Domain.Structures;

namespace Boids.Domain;

public class GameField
{
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
                new Position((float)(random.NextDouble() * _width), (float)random.NextDouble() * _height),
                new Speed((float)random.NextDouble() * 2 - 1, (float)random.NextDouble() * 2 - 1),
                random.Next(1, 5)
            ));
        }
    }

    public void MoveBoids()
    {
        Parallel.ForEach(Boids, boid =>
                                {
                                    boid.MoveTowardsGroup(Boids);
                                    boid.AvoidCollisionWithBoids(Boids);
                                    boid.FlyWithGroup(Boids);
                                    boid.AvoidCollisionWithWall(_width, _height);
                                    boid.Move();
                                });
    }
}