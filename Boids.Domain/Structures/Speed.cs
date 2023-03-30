using System;

namespace Boids.Domain.Structures;

public struct Speed
{
    public float X
    {
        get;
        set;
    }

    public float Y
    {
        get;
        set;
    }

    public Speed(float x, float y)
    {
        X = x;
        Y = y;
    }

    public void Increase(float incrementX, float incrementY)
    {
        X += incrementX;
        Y += incrementY;
    }

    public float GetDirection()
    {
        if (X == 0 && Y == 0)
            return 0;

        float angle = (float) (Math.Atan(Y / X) * 180 / Math.PI - 90);

        if (X < 0)
            angle += 180;
        
        return angle;
    }

    public Speed SetSpeed(float speed)
    {
        float currentSpeed = (float) Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
        return new Speed(X / currentSpeed * speed, Y / currentSpeed * speed);
    }
}