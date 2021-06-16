/*
 * Изучите код данного приложения для расчета суммы целых чисел от 0 до N, а затем
 * измените код приложения таким образом, чтобы выполнялись следующие требования:
 * 1. Расчет должен производиться асинхронно.
 * 2. N задается пользователем из консоли. Пользователь вправе внести новую границу в процессе вычислений,
 * что должно привести к перезапуску расчета.
 * 3. При перезапуске расчета приложение должно продолжить работу без каких-либо сбоев.
 */

using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task1.CancellationTokens
{
    class Program
    {
        /// <summary>
        /// The Main method should not be changed at all.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Mentoring program L2. Async/await.V1. Task 1");
            Console.WriteLine("Calculating the sum of integers from 0 to N.");
            Console.WriteLine("Use 'q' key to exit...");
            Console.WriteLine();

            Console.WriteLine("Enter N: ");

            string input = Console.ReadLine();
            while (input != null && input.Trim().ToUpper() != "Q")
            {
                if (int.TryParse(input, out int n))
                {
                    CalculateSum(n);
                }
                else
                {
                    Console.WriteLine($"Invalid integer: '{input}'. Please try again.");
                    Console.WriteLine("Enter N: ");
                }

                input = Console.ReadLine();
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

        private static void CalculateSum(int n)
        {
            var tokenSource = new CancellationTokenSource();

            var sumTask = Calculate(n, tokenSource);

            Console.WriteLine();
           
            Console.WriteLine("Enter N: ");
            
            string input = Console.ReadLine();

            if (int.TryParse(input, out int nx))
            {
                if (!sumTask.IsCompleted)
                {
                    tokenSource.Cancel();

                    Console.WriteLine($"Sum for {n} cancelled...");
                }

                CalculateSum(nx);
            }

            Console.WriteLine("Enter N: ");
        }

        private static async Task Calculate(int n, CancellationTokenSource tokenSource)
        {
            var token = tokenSource.Token;

            var sum = await Calculator.Calculate(n, token);

            Console.WriteLine($"Sum for {n} = {sum}.");
        }
    }
}