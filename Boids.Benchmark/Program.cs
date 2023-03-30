// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;

using Boids.Benchmark;

BenchmarkRunner.Run<Benchmark>();
Console.WriteLine("Hello, World!");
