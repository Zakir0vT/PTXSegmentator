using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace PTXSegmentator
{
    class Program
    {
        static void Main(string[] args)
        {
            var prog = new Program();
            var stopWatch = new Stopwatch();
            var provider = prog.CreateContainer();
            var pointsNumb = provider.GetService<GetPointsNumb>();
            
            Console.WriteLine("Enter PTX name:");
            pointsNumb.FilePath = Console.ReadLine();
            pointsNumb.CreateMatrixGrid();
            Console.WriteLine("File contains {0} station", provider.GetService<MatrixGridDto>().GridPoints.Count / 2);
            Console.WriteLine("Enter segmentation value:");
            var selectedPoints = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            var writer = provider.GetService<PointsWriter>();
            writer.PointsInOneFile = selectedPoints;
            stopWatch.Start();
            writer.Write();
            stopWatch.Stop();

            Console.WriteLine("Segmentation calcelled");
            Console.WriteLine($"Needed time (sec) = {stopWatch.Elapsed.TotalMilliseconds / 1000d}");
            Console.ReadKey();
        }

        private IServiceProvider CreateContainer()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<GetPointsNumb>();
            serviceCollection.AddSingleton<MatrixGridDto>();
            serviceCollection.AddSingleton<PointsWriter>();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
