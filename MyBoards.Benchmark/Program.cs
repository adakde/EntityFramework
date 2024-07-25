// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using MyBoards.Benchmark;

Console.WriteLine("Hello, World!");
BenchmarkRunner.Run<TrackingBenchmark>();