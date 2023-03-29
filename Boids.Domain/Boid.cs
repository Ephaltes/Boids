using System.Collections.Generic;

using Boids.Domain.Structures;

namespace Boids.Domain;

public class Boid
{
    private const int Padding = 30;

    public Boid(Position position, Speed speed, float movementSpeed)
    {
        Position = position;
        Speed = speed;
        MovementSpeed = movementSpeed;
    }

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

    public float MovementSpeed
    {
        get;
        private set;
    }

    public void MoveTowardsGroup(IReadOnlyCollection<Boid> boids, float factor)
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
        Speed = new Speed(
            Speed.X + (groupCenterX - Position.X) * factor,
            Speed.Y + (groupCenterY - Position.Y) * factor
        );
    }

    public void FlyWithGroup(IReadOnlyCollection<Boid> boids, float factor)
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
        Speed = new Speed(
            Speed.X - (Speed.X - meanVelX) * factor,
            Speed.Y - (Speed.Y - meanVelY) * factor
        );
    }
    public void AvoidCollisionWithWall(float maxWidth, float maxHeight)
    {
        //avoid collisions with the walls
        if (Position.X < Padding)
            Speed = new Speed(Speed.X + 0.3f, Speed.Y);
        if (Position.X > maxWidth - Padding)
            Speed = new Speed(Speed.X - 0.3f, Speed.Y);

        if (Position.Y < Padding)
            Speed = new Speed(Speed.X, Speed.Y + 0.3f);
        if (Position.Y > maxHeight - Padding)
            Speed = new Speed(Speed.X, Speed.Y - 0.3f);
    }

    public void Move()
    {
        Speed = Speed.SetSpeed(MovementSpeed);
        Position = Position.Move(Speed);
    }
}