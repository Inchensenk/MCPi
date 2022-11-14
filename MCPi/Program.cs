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

Для добавления в очередь пула потоков асинхронных вычислительных операций
обычно вызывают один из следующих методов класса ThreadPool:
static Boolean QueueUserWorkItem(WaitCallback callBack);
static Boolean QueueUserWorkItem(WaitCallback callBack, Object state);
    Эти методы ставят «рабочий элемент» вместе с дополнительными данными
состояния в очередь пула потоков и сразу возвращают управление приложению.
Рабочим элементом называется указанный в параметре callback метод, который
будет вызван потоком из пула. Этому методу можно передать один параметр
через аргумент state (данные состояния). Без этого параметра версия метода
QueueUserWorkItem передает методу обратного вызова значение null. Все заканчи-
вается тем, что один из потоков пула обработает рабочий элемент, приводя к вызову
указанного метода. Создаваемый метод обратного вызова должен соответствовать
делегату System.Threading.WaitCallback, который определяется так:
    delegate void WaitCallback(Object state);
ПримечАние
Сигнатуры делегатов WaitCallback и TimerCallback,
а также делегата ParameterizedThreadStart совпадают. Если вы определяете метод, совпадающий с этой сигнатурой, он может быть вызван
через метод ThreadPool.QueueUserWorkItem при помощи объекта System.Threading.
Timer или System.Threading.Thread.

ThreadPool: Предоставляет пул потоков, который можно использовать для выполнения задач, отправки рабочих элементов,
обработки асинхронного ввода-вывода, ожидания от имени других потоков и обработки таймеров.

ThreadPool.SetMaxThreads: Задает количество запросов к пулу потоков, которые могут быть активными одновременно. 
Все запросы, превышающие это количество, остаются в очереди до тех пор, пока потоки пула не станут доступны.

 */