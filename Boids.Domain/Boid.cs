using Boids.Domain.Structures;

namespace Boids.Domain;

public class Boid
{
    /// <summary>
    /// Minimum distance birds should keep to the edge of the field
    /// </summary>
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

    public void MoveTowardsGroup(Boids boids, float factor)
    {
        // Calculate the center of the group
        Position center = boids.GetGroupCenter();

        // Move towards the center of the group
        Speed = new Speed(
            Speed.X + (center.X - Position.X) * factor,
            Speed.Y + (center.Y - Position.Y) * factor
        );
    }

    public void AdjustSpeedToGroup(Boids boids, float factor)
    {
        // Calculate the average velocity of the group
        Speed averageSpeed = boids.GetAverageSpeed();
        
        // Adjust velocity to the group
        float adjustmentX = (Speed.X - averageSpeed.X) * factor;
        float adjustmentY = (Speed.Y - averageSpeed.Y) * factor;
        Speed.Increase(adjustmentX, adjustmentY);
    }

    public void AvoidCollisionWithWall(float maxWidth, float maxHeight, float factor)
    {
        // Avoid horizontal collision with wall
        if (Position.X < Padding)
            Speed.Increase(factor, 0);
        if (Position.X > maxWidth - Padding)
            Speed.Increase(-factor, 0);

        // Avoid vertical collision with wall
        if (Position.Y < Padding)
            Speed.Increase(0, factor);
        if (Position.Y > maxHeight - Padding)
            Speed.Increase(0, -factor);
    }

    public void Move()
    {
        Speed = Speed.SetSpeed(MovementSpeed);
        Position = Position.Move(Speed);
    }
}