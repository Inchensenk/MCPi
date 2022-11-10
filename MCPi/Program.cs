namespace MCPi
{
    public class Program
    {
        //static double sum = 0;

        /// <summary>
        /// Расчет числа Пи методом Монте-Карло
        /// </summary>
        /// <param name="allPoints">Общее колличество точек</param>
        static void MonteKarlo(object allPoints)
        {
            
            int amountPointsInSquare = (int)allPoints;//Общее колличество точек
            double pi;
            double amountPointsInCircle = 0;//колличество точек попавших в круг
            Random r = new Random();

            for (int i = 0; i < amountPointsInSquare; i++)
            {
                
                if (IsPointInCircle(1.0, r.NextDouble(), r.NextDouble()))
                {
                    amountPointsInCircle++;
                }
            }

            pi = (4 * amountPointsInCircle) / (double)amountPointsInSquare;

            Console.WriteLine(pi);
        }

        /// <summary>
        /// Вычесление принадлежности точки кругу
        /// </summary>
        /// <param name="R">Радиус круга</param>
        /// <param name="x">Коордиината x</param>
        /// <param name="y">Коордиината y</param>
        /// <returns>Если точка принадлежит кругу, то функция возвращает 1</returns>
        static bool IsPointInCircle(double R, double x, double y)
        {
            return Math.Pow((x - 1), 2) + Math.Pow((y - 1), 2) <= Math.Pow(R, 2);
        }

        /// <summary>
        /// Пулл потоков
        /// </summary>
        /// <param name="amountThreads">Колличество потоков в пулле</param>
        /// <param name="num">Общее колличество точек</param>
        static void Threads(int amountThreads, int num)
        {
            ThreadPool.SetMaxThreads(amountThreads, 0);
            for (int i = 0; i < amountThreads; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(MonteKarlo), num);
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

/*
 QueueUserWorkItem(WaitCallback, Object): Помещает метод в очередь на выполнение и указывает объект, 
содержащий данные для использования методом. Метод выполняется, когда становится доступен поток из пула потоков.

ThreadPool: Предоставляет пул потоков, который можно использовать для выполнения задач, отправки рабочих элементов,
обработки асинхронного ввода-вывода, ожидания от имени других потоков и обработки таймеров.

ThreadPool.SetMaxThreads: Задает количество запросов к пулу потоков, которые могут быть активными одновременно. 
Все запросы, превышающие это количество, остаются в очереди до тех пор, пока потоки пула не станут доступны.

 */