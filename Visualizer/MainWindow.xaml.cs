using Boids.Domain;

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Visualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int BoidCount = 1500;

        private readonly GameField _gameField;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private int _fps;

        public MainWindow()
        {
            _gameField = new GameField(BoidCount);
            InitializeComponent();

            _stopwatch.Start();
            CompositionTarget.Rendering += Render;
        }

        private void Render(object? sender, EventArgs e)
        {
            if (ShouldDisplayFps())
            {
                lb_fps.Content = $"{_fps} FPS";
                _stopwatch.Restart();
                _fps = 0;
            }
            
            _gameField.MoveBoidsParallel();
            canvas.InvalidateVisual();
            _fps++;
        }

        private bool ShouldDisplayFps()
        {
            return _stopwatch.ElapsedMilliseconds > 1000;
        }

        private void canvas_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            Renderer.Render(e.Surface.Canvas, _gameField);
        }
    }
}