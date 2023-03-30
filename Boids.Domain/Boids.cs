using System;
using System.Collections;
using System.Collections.Generic;

using Boids.Domain.Structures;

namespace Boids.Domain;

public class Boids : IEnumerable<Boid>
{
    private const float MaxSpeed = 1f;
    private const float MinSpeed = 0.3f;

    private readonly IReadOnlyList<Boid> _boids;

    public Boids(IReadOnlyList<Boid> boids)
    {
        _boids = boids;
    }

    public static Boids Generate(int boidCount)
    {
        List<Boid> boids = new();
        Random random = new(Guid.NewGuid().GetHashCode());

        for (int i = 0; i < boidCount; i++)
        {
            Boid boid = GenerateBoid(random);
            boids.Add(boid);
        }

        return new Boids(boids);
    }
    
    private static Boid GenerateBoid(Random random)
    {
        Position position = new(
            GetFloatInRange(0, GameField.Width, random), 
            GetFloatInRange(0, GameField.Height, random));
        
        Speed speed = new(
            GetFloatInRange(MinSpeed, MaxSpeed, random), 
            GetFloatInRange(MinSpeed, MaxSpeed, random));

        float movementSpeed = GetFloatInRange(MinSpeed, MaxSpeed, random);
        return new(position, speed, movementSpeed);
    }
    
    private static float GetFloatInRange(float min, float max, Random random)
    {
        float multiplier = (float) random.NextDouble();
        float offset = multiplier * (max - min);
        return min + offset;
    }

    public Position GetGroupCenter()
    {
        float groupCenterX = 0;
        float groupCenterY = 0;
        
        // Calculate the center of the group
        foreach (Boid boid in _boids)
        {
            groupCenterX += boid.Position.X;
            groupCenterY += boid.Position.Y;
        }

        groupCenterX /= _boids.Count;
        groupCenterY /= _boids.Count;
        
        return new Position(groupCenterX, groupCenterY);
    }

    public Speed GetAverageSpeed()
    {
        // Calculate the mean velocity of the group
        float averageVelocityX = 0;
        float averageVelocityY = 0;

        foreach (Boid boid in _boids)
        {
            averageVelocityX += boid.Speed.X;
            averageVelocityY += boid.Speed.Y;
        }

        averageVelocityX /= _boids.Count;
        averageVelocityY /= _boids.Count;

        return new Speed(averageVelocityX, averageVelocityY);
    }

    public IEnumerator<Boid> GetEnumerator()
    {
        return _boids.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}