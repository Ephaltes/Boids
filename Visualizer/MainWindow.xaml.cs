using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

using Boids.Domain;

using SkiaSharp.Views.Desktop;

namespace Visualizer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private const int BoidCount = 500;
    private readonly List<int> _fpsList = new(20);

    private readonly GameField _gameField = new(BoidCount);
    private readonly Stopwatch _stopwatch = new();
    private Timer _timer;
    
    private int _fps;
    public MainWindow()
    {
        InitializeComponent();

        _stopwatch.Start();
        CompositionTarget.Rendering += Render;

        _timer = new Timer(CalculateAverageFps,null, 15_000,0);
    }

    private void CalculateAverageFps(object? state)
    {
        _fpsList.RemoveRange(0, 5);
        double averageFps = _fpsList.Average();
        MessageBox.Show($"Average FPS: {averageFps}");
    }

    private void Render(object? sender, EventArgs e)
    {
        if (ShouldDisplayFps())
        {
            _fpsList.Add(_fps);
            lb_fps.Content = $"{_fps} FPS";
            _stopwatch.Restart();
            _fps = 0;
        }

        //_gameField.MoveBoidsSerial();
        //_gameField.MoveBoidsParallel(4);
        _gameField.MoveBoidsParallel(8);

        canvas.InvalidateVisual();
        _fps++;
    }

    private bool ShouldDisplayFps()
    {
        return _stopwatch.ElapsedMilliseconds >= 1000;
    }

    private void canvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        Renderer.Render(e.Surface.Canvas, _gameField);
    }
}