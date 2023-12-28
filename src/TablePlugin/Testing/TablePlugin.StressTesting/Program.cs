namespace TablePlugin.StressTesting
{
    using System.Diagnostics;
    using System.IO;
    using System;
    using Microsoft.VisualBasic.Devices;
    using TablePlugin.Connector;
    using TablePlugin.Model;
    using System.Collections.Generic;

    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new TableBuilder();
            var stopWatch = new Stopwatch();
            var parameters = new Parameters();
            parameters.ParamsDictionary = new Dictionary<ParameterType.ParamType, Parameter>()
            {
                { ParameterType.ParamType.TableLength, new Parameter(800, 600, 1200) },
                { ParameterType.ParamType.TableWidth, new Parameter(800, 600, 1200) },
                { ParameterType.ParamType.TableHeight, new Parameter(450, 400, 500) },
                { ParameterType.ParamType.ShelfLength, new Parameter(700, 300, 1100) },
                { ParameterType.ParamType.ShelfWidth, new Parameter(700, 300, 1100) },
                { ParameterType.ParamType.ShelfHeight, new Parameter(25, 10, 40) },
                { ParameterType.ParamType.SupportSize, new Parameter(40, 30, 50) },
                { ParameterType.ParamType.ShelfFloorDistance, new Parameter(150, 30, 360) },
                { ParameterType.ParamType.BracingSize, new Parameter(30, 20, 45) },
                { ParameterType.ParamType.WheelSize, new Parameter(0, 0, 70) },
            };
            var streamWriter = new StreamWriter($"log.txt", true);
            Process currentProcess = Process.GetCurrentProcess();
            var count = 0;

            while (count < 51)
            { 
                const double gigabyteInByte = 0.000000000931322574615478515625; 
                stopWatch.Start();
                builder.Build(parameters);
                stopWatch.Stop();

                var computerInfo = new ComputerInfo(); 
                var usedMemory = (computerInfo.TotalPhysicalMemory
                    - computerInfo.AvailablePhysicalMemory)
                    * gigabyteInByte;
                 
                streamWriter.WriteLine(
                    $"{++count}\t{stopWatch.Elapsed:hh\\:mm\\:ss}\t{usedMemory}"); 
                streamWriter.Flush();
                stopWatch.Reset();
            }

            streamWriter.Close();
            streamWriter.Dispose();
            Console.Write($"End {new ComputerInfo().TotalPhysicalMemory}");
        }
    }
}
