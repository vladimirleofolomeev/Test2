using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HFStask.IO
{
    class FileFormer
    {
        /// <summary>
        /// Метод проверки существования введенного пути, также файлов в нём
        /// </summary>
        public void CheckFiles()
        {
            if (File.Exists(Path.Combine(Variables.WorkPath, "errors.txt")))
            {
                File.Delete(Path.Combine(Variables.WorkPath, "errors.txt"));
            }

            Variables.files = Directory.GetFiles(Variables.WorkPath, "*.txt");            

            if (Variables.files.Length > 0)
            {                
                Console.WriteLine("Обнаружено {0} файлов, начать их обработку? (y/n)", Variables.files.Length);
                switch (Console.ReadLine())
                {
                    case "y":                                                                   

                        Calculation.FileCalc proc = new Calculation.FileCalc();
                        proc.FileCalculation();
                        break;

                    case "n":
                        Console.Clear();
                        Console.WriteLine("Что ж, вернёмся к началу!");
                        Variables.WorkPath = null;
                        break;
                }
            }
            else
            {                
                Console.WriteLine("В директории нет файлов. Сгенерировать их? (y/n)");

                switch (Console.ReadLine())
                {
                    case "y":
                        try
                        {                            
                            FileGenerator();
                        }
                        catch
                        {
                            Console.WriteLine("Ой, видимо, вы ошиблись и ввели не цифру, а что-то другое?\nПопробуем сначала?");
                        }
                        break;

                    case "n":
                        Console.Clear();
                        Console.WriteLine("Что ж, вернёмся к началу!");
                        Variables.WorkPath = null;
                        break;
                }
            }
            
        }

        /// <summary>
        /// Метод генерации файлов в заданную директорию
        /// </summary>
        /// <param name="defectsProcent"></param>
        private void FileGenerator()
        {
            try
            {
                Random rndFilesCount = new Random();
                int filesCount = rndFilesCount.Next(1, 10000); // Получаем случайние количество файлов для генерации

                for (int i = 0; i < filesCount; i++)
                {
                    CreateSingleFile("testFile" + i.ToString("D4") + ".txt");
                    Console.Clear();
                    Console.WriteLine("Создано {0} файлов из {1}", i , filesCount);
                }
                Console.WriteLine("Файлы успешно созданы");
                CheckFiles();

            }
            catch { Console.WriteLine("Неведомая сила помешала создать файлы... Может быть попробуем другую директорию?\n"); }
            
        }

        /// <summary>
        /// Метод создания единичного файла
        /// </summary>
        /// <param name="fileName">Имя Файла</param>
        private void CreateSingleFile(string fileName)
        {
            Random rnd = new Random();          
            int StringsAmount = rnd.Next(25000);

            using (StreamWriter sw = new StreamWriter(Path.Combine(Variables.WorkPath, fileName)))
            {
                for (int i = 0; i < StringsAmount; i++)
                {
                    // Если случайное значение больше 100, то не брак
                    if (rnd.Next(5000) > 20)
                    {
                        string line = rnd.Next(-1000000, 1000000) + " " + rnd.Next(-1000000, 1000000);
                        sw.WriteLine(line);
                    }
                    // Если случайное значение менее 100, то сточка бракована
                    else { sw.WriteLine("defect String"); }
                }
            }

            

        }
            
    }
}
