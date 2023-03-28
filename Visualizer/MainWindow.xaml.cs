using Boids.Domain;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Visualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameField _gameField;
        private readonly Stopwatch _stopwatch;
        public MainWindow()
        {
            _gameField = new GameField(800, 450, 10);

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer.Start();

            _stopwatch = new Stopwatch();

            InitializeComponent();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _stopwatch.Restart();
            _gameField.MoveBoids();
            canvas.InvalidateVisual();
            double fps = (double) _stopwatch.ElapsedTicks / Stopwatch.Frequency;
            lb_fps.Content = $"{1 / fps:0.00} FPS";
        }

        private void canvas_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            Renderer.Render(e.Surface.Canvas, _gameField);
        }
    }
}