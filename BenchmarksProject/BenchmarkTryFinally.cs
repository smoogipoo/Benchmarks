using BenchmarkDotNet.Attributes;

namespace BenchmarksProject
{
    public class BenchmarkTryFinally
    {
        private string? a = null;

        [Benchmark]
        public void WithTryFinally() => withTryFinally("a");

        [Benchmark]
        public void WithTemporary() => withTemporary("a");

        private string withTryFinally(string input)
        {
            try
            {
                return a ?? input;
            }
            finally
            {
                a = null;
            }
        }
        
        private string withTemporary(string input)
        {
            string temp = a ?? input;
            a = null;
            return temp;
        }
    }
}