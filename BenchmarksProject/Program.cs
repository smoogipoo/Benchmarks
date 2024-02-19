using BenchmarkDotNet.Running;

namespace BenchmarksProject
{
    internal class Program
    {
        public static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
#if DEBUG
                                                                   .Run(args, new DebugInProcessConfig());
#else
                                                                   .Run(args);
#endif
    }
}
