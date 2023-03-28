using System.Collections.Generic;

using Boids.Domain.Structures;

namespace Boids.Domain;

public class Boid
{
    public Position Position
    {
        get;
        private set;
    }
    
    public Speed Speed
    {
        get;
        private set;
    }

    public int MovementSpeed
    {
        get;
        private set;
    }

    private const int Padding = 100;
    public Boid(Position position, Speed speed, int movementSpeed = 2)
    {
        Position = position;
        Speed = speed;
        MovementSpeed = movementSpeed;
    }

    public void MoveTowardsGroup(IReadOnlyCollection<Boid> boids)
    {
        float groupCenterX = 0;
        float groupCenterY = 0;

        //calculate the center of the group
        foreach (Boid boid in boids)
        {
            groupCenterX += boid.Position.X;
            groupCenterY += boid.Position.Y;
        }
        
        groupCenterX /= boids.Count;
        groupCenterY /= boids.Count;

        //move towards the center of the group
        //Speed = new Speed(groupCenterX - Position.X, groupCenterY - Position.Y);
        Speed = new Speed(1, 1);
    }

    public void FlyWithGroup(IReadOnlyCollection<Boid> boids)
    {
        //calculate the mean velocity of the group
        float meanVelX = 0;
        float meanVelY = 0;
        
        foreach (Boid boid in boids)
        {
                meanVelX += boid.Speed.X;
                meanVelY += boid.Speed.Y;
        }
        meanVelX /= boids.Count;
        meanVelY /= boids.Count;
        
        //fly with the group
        Speed = new Speed(meanVelX, meanVelY);
    }
    
    public void AvoidCollisionWithBoids(IReadOnlyCollection<Boid> boids)
    {
        //avoid collisions with other boids
        foreach (Boid boid in boids)
        {
            if (boid != this && Position.DistanceTo(boid.Position) < Padding)
            {
                Speed = new Speed(Position.X - boid.Position.X, Position.Y - boid.Position.Y);
            }
        }
    }
    
    public void AvoidCollisionWithWall(float maxWidth, float maxHeight)
    {
        //avoid collisions with the walls
        if (Position.X < Padding)
        {
            Speed = new Speed(1, 0);
        }
        else if (Position.X > maxWidth - Padding)
        {
            Speed = new Speed(-1, 0);
        }
        
        if (Position.Y < Padding)
        {
            Speed = new Speed(0, 1);
        }
        else if (Position.Y > maxHeight - Padding)
        {
            Speed = new Speed(0, -1);
        }
    }
    
    public void Move()
    {
        Speed = Speed.SetSpeed(MovementSpeed);
        Position = Position.Move(Speed);
    }
}