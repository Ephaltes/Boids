﻿using System;

namespace Boids.Domain.Structures;

public struct Position
{
    public float X
    {
        get;
        private set;
    }
    
    public float Y
    {
        get;
        private set;
    }

    public Position(float x, float y)
    {
        X = x;
        Y = y;
    }
    
    public Position Move(Speed speed)
    {
        return new Position(X + speed.X, Y + speed.Y);
    }
    
   public float DistanceTo(Position other)
   {
       double xDiffSquared = Math.Pow(X - other.X, 2);
       double yDiffSquared = Math.Pow(Y - other.Y, 2);
       return (float) Math.Sqrt(xDiffSquared + yDiffSquared);
   }
}