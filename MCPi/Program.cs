namespace MyApp 
{
    public class Program
    {
        static double sum = 0;

        static void Method_Monte_Karlo(object num_p)
        {
            int n_point = (int)num_p;
            double pi;
            double n_circle = 0;
            Random r = new Random();
            for (int i = 0; i < n_point; i++)
            {
                if (IsPointInCircle(1.0, r.NextDouble(), r.NextDouble()))
                {
                    n_circle++;
                }
            }
            pi = (4 * n_circle) / (double)n_point;
            Console.WriteLine(pi);
        }

        static bool IsPointInCircle(double R, double x, double y)
        {
            return ((x * x + y * y) <= R * R);
        }

        static void Threads(int numThreads, int num)
        {
            ThreadPool.SetMaxThreads(numThreads, 0);
            for (int i = 0; i < numThreads; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(Method_Monte_Karlo), num);
            }
        }

        static void Main(string[] args)
        {
            int numThr;
            int _num;
            Console.WriteLine("Введите количество потоков: ");
            numThr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите максимальное количество точек: ");
            _num = Convert.ToInt32(Console.ReadLine());
            Threads(numThr, _num);
            Console.ReadKey();
        }
    }
}