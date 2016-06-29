using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFStask
{
    class Program
    {
        private static IO.DirectoryWorker DW;


        static void Main(string[] args)
        {
            DW = new IO.DirectoryWorker(); // Инициализируем новый экземпляр обработчика директорий

            while (true)
            {
                Console.WriteLine("Укажите путь к рабочей директории:");
                DW.SetWorkPath(Console.ReadLine());
            }
        }
    }
}
