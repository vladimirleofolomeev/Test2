using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HFStask.IO
{
    class DirectoryWorker
    {
        FileFormer _fileFormer = new FileFormer(); // Инициализируем новый экземпляр обработчика файлов

        /// <summary>
        /// Метод ввода, поиска и создания рабочей директории
        /// </summary>
        /// <param name="userPath"></param>
        public void SetWorkPath(string userPath)
        {
            // Проверяем, есть ли такая директория
            if (Directory.Exists(userPath))
            {
                Console.WriteLine("Директория найдена");
                Variables.WorkPath = userPath;
                _fileFormer.CheckFiles();
            }
            else
            {                
                Console.WriteLine("Такой директории не существует, создать? (y/n)");

                switch (Console.ReadLine())
                {
                    case "y":
                        try
                        {
                            Directory.CreateDirectory(userPath);
                            Console.WriteLine("Директория успешно создана.");
                            Variables.WorkPath = userPath;
                            _fileFormer.CheckFiles();
                        }
                        catch
                        {
                            Console.WriteLine("Что-то пошло не так, вохможно не существует такого диска?");
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
    }
}
