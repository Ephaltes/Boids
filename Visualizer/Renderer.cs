using Boids.Domain;

using SkiaSharp;

namespace Visualizer;

public static class Renderer
{
    private static readonly SKColor _boidColor = SKColors.Black;

    public static void Render(SKCanvas canvas, GameField field)
    {
        canvas.Clear();

        using (SKPath boidPath = new SKPath())
        {
            boidPath.MoveTo(0, 0);
            boidPath.LineTo(-5, -2);
            boidPath.LineTo(0, 8);
            boidPath.LineTo(5, -2);
            boidPath.LineTo(0, 0);

            foreach (Boid boid in field.Boids)
                DrawBoidDirection(boid, canvas, boidPath, _boidColor);
        }
    }

    private static void DrawBoidDirection(Boid boid, SKCanvas canvas, SKPath boidPath, SKColor color)
    {
        using (SKPaint paint = new SKPaint
                               {
                                   IsAntialias = true, 
                                   Color = color
                               })
        {
            canvas.Save();
            canvas.Translate(boid.Position.X, boid.Position.Y);
            canvas.RotateDegrees(boid.Speed.GetDirection());

            canvas.DrawPath(boidPath, paint);
            canvas.Restore();
        }
    }
}