using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;

namespace HFStask.Calculation
{
    class FileCalc
    {
        Timer timerMain = new Timer();

        /// <summary>
        /// Метод обсчета файлов в директории
        /// </summary>
        public void FileCalculation()
        {
            Console.WriteLine("Запускаем расчет");
            IO.Variables.calculationResult = new long();
            IO.Variables.valueList = new List<long>();

            Timer timerMain = new Timer();
            timerMain.Interval = 1000;
            timerMain.Elapsed += TimerMain_Elapsed;
            timerMain.AutoReset = true;
            timerMain.Enabled = true;
            timerMain.Start();

            Parallel.For(0, IO.Variables.files.Length, SingleFileCalc);

            timerMain.Stop();

            IO.Variables.calculationResult = IO.Variables.valueList.Sum();


            Console.Clear();
            Console.WriteLine("Расчет окончен. Финальный результат:{0}\n", IO.Variables.calculationResult);
        }

        private void TimerMain_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.Clear();
            Console.WriteLine("Обработано {0} файлов из {1}",IO.Variables.valueList.Count, IO.Variables.files.Length);
        }

        private void SingleFileCalc(int k)
        {
            long fileSumm = 0;
            int lineCount = 0;
            string tempData;
            
            string[] lines;

            using (StreamReader sr = new StreamReader(IO.Variables.files[k]))
            {
                tempData = sr.ReadToEnd();
            }

            lines = tempData.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {

                string[] line = lines[i].Split(' ');

                try
                {
                    int firstValue = Convert.ToInt32(line[0]);
                    int secondValue = Convert.ToInt32(line[1]);
                    fileSumm += (long)firstValue * (long)secondValue;
                }
                catch
                {
                    lock (this)
                    {
                        using (StreamWriter sw = File.AppendText(Path.Combine(IO.Variables.WorkPath, "errors.txt")))
                        {
                       
                            sw.WriteLine("Ошибка: Файл {0}, строка {1}", Path.GetFileName(IO.Variables.files[k]), lineCount);
                        }
                    }
                }

                lineCount++; // Счетчик строк                       
            }

            IO.Variables.valueList.Add(fileSumm);            
        }
       
    }
}
