using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Homework16
{
    class Program
    {
        static string logFilePath = "log.txt";

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Просмотр содержимого директории");
                Console.WriteLine("2. Создание файла/директории");
                Console.WriteLine("3. Удаление файла/директории");
                Console.WriteLine("4. Копирование файла/директории");
                Console.WriteLine("5. Перемещение файла/директории");
                Console.WriteLine("6. Чтение из файла");
                Console.WriteLine("7. Запись в файл");
                Console.WriteLine("8. Вывод лога операций");
                Console.WriteLine("9. Выход");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewDirectoryContents();
                        break;
                    case "2":
                        CreateFileOrDirectory();
                        break;
                    case "3":
                        DeleteFileOrDirectory();
                        break;
                    case "4":
                        CopyFileOrDirectory();
                        break;
                    case "5":
                        MoveFileOrDirectory();
                        break;
                    case "6":
                        ReadFromFile();
                        break;
                    case "7":
                        WriteToFile();
                        break;
                    case "8":
                        DisplayLog();
                        break;
                    case "9":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Некорректный ввод. Повторите попытку.");
                        break;
                }
            }
            Console.ReadKey();
        }

        static void ViewDirectoryContents()
        {
            Console.WriteLine("Введите путь к директории:");
            string path = Console.ReadLine();

            try
            {
                string[] files = Directory.GetFiles(path);
                string[] directories = Directory.GetDirectories(path);

                Console.WriteLine("Файлы:");
                foreach (var file in files)
                {
                    Console.WriteLine(Path.GetFileName(file));
                }

                Console.WriteLine("Директории:");
                foreach (var directory in directories)
                {
                    Console.WriteLine(Path.GetFileName(directory));
                }

                LogAction($"Просмотр содержимого директории: {path}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                LogAction($"Ошибка при просмотре содержимого директории: {path}. {ex.Message}");
            }
        }

        static void CreateFileOrDirectory()
        {
            Console.WriteLine("Выберите тип (1 - файл, 2 - директория):");
            string typeChoice = Console.ReadLine();

            if (typeChoice != "1" && typeChoice != "2")
            {
                Console.WriteLine("Некорректный выбор типа.");
                return;
            }

            Console.WriteLine("Введите путь:");
            string path = Console.ReadLine();

            try
            {
                if (typeChoice == "1")
                {
                    File.Create(path).Close();
                    Console.WriteLine($"Файл создан: {path}");
                    LogAction($"Создан файл: {path}");
                }
                else
                {
                    Directory.CreateDirectory(path);
                    Console.WriteLine($"Директория создана: {path}");
                    LogAction($"Создана директория: {path}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                LogAction($"Ошибка при создании файла/директории: {path}. {ex.Message}");
            }
        }

        static void DeleteFileOrDirectory()
        {
            Console.WriteLine("Выберите тип (1 - файл, 2 - директория):");
            string typeChoice = Console.ReadLine();

            if (typeChoice != "1" && typeChoice != "2")
            {
                Console.WriteLine("Некорректный выбор типа.");
                return;
            }

            Console.WriteLine("Введите путь:");
            string path = Console.ReadLine();

            try
            {
                if (typeChoice == "1")
                {
                    File.Delete(path);
                    Console.WriteLine($"Файл удален: {path}");
                    LogAction($"Удален файл: {path}");
                }
                else
                {
                    Directory.Delete(path, true);
                    Console.WriteLine($"Директория удалена: {path}");
                    LogAction($"Удалена директория: {path}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                LogAction($"Ошибка при удалении файла/директории: {path}. {ex.Message}");
            }
        }

        static void CopyFileOrDirectory()
        {
            Console.WriteLine("Выберите тип (1 - файл, 2 - директория):");
            string typeChoice = Console.ReadLine();

            if (typeChoice != "1" && typeChoice != "2")
            {
                Console.WriteLine("Некорректный выбор типа.");
                return;
            }

            Console.WriteLine("Введите путь исходного объекта:");
            string sourcePath = Console.ReadLine();

            Console.WriteLine("Введите путь назначения:");
            string destinationPath = Console.ReadLine();

            try
            {
                if (typeChoice == "1")
                {
                    File.Copy(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine($"Файл скопирован: {sourcePath} -> {destinationPath}");
                    LogAction($"Скопирован файл: {sourcePath} -> {destinationPath}");
                }
                else
                {
                    CopyDirectory(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine($"Директория скопирована: {sourcePath} -> {destinationPath}");
                    LogAction($"Скопирована директория: {sourcePath} -> {destinationPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                LogAction($"Ошибка при копировании файла/директории: {sourcePath} -> {destinationPath}. {ex.Message}");
            }
        }

        static void MoveFileOrDirectory()
        {
            Console.WriteLine("Выберите тип (1 - файл, 2 - директория):");
            string typeChoice = Console.ReadLine();

            if (typeChoice != "1" && typeChoice != "2")
            {
                Console.WriteLine("Некорректный выбор типа.");
                return;
            }

            Console.WriteLine("Введите путь исходного объекта:");
            string sourcePath = Console.ReadLine();

            Console.WriteLine("Введите путь назначения:");
            string destinationPath = Console.ReadLine();

            try
            {
                if (typeChoice == "1")
                {
                    File.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine($"Файл перемещен: {sourcePath} -> {destinationPath}");
                    LogAction($"Перемещен файл: {sourcePath} -> {destinationPath}");
                }
                else
                {
                    Directory.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine($"Директория перемещена: {sourcePath} -> {destinationPath}");
                    LogAction($"Перемещена директория: {sourcePath} -> {destinationPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                LogAction($"Ошибка при перемещении файла/директории: {sourcePath} -> {destinationPath}. {ex.Message}");
            }
        }

        static void ReadFromFile()
        {
            Console.WriteLine("Введите путь к файлу:");
            string filePath = Console.ReadLine();

            try
            {
                string content = File.ReadAllText(filePath);
                Console.WriteLine($"Содержимое файла {filePath}:\n{content}");
                LogAction($"Прочитан файл: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                LogAction($"Ошибка при чтении файла: {filePath}. {ex.Message}");
            }
        }

        static void WriteToFile()
        {
            Console.WriteLine("Введите путь к файлу:");
            string filePath = Console.ReadLine();

            Console.WriteLine("Введите текст для записи:");
            string content = Console.ReadLine();

            try
            {
                File.WriteAllText(filePath, content);
                Console.WriteLine($"Текст успешно записан в файл: {filePath}");
                LogAction($"Записан текст в файл: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                LogAction($"Ошибка при записи текста в файл: {filePath}. {ex.Message}");
            }
        }

        static void DisplayLog()
        {
            try
            {
                string logContent = File.ReadAllText(logFilePath);
                Console.WriteLine($"Лог операций:\n{logContent}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void LogAction(string action)
        {
            try
            {
                File.AppendAllText(logFilePath, $"{DateTime.Now}: {action}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в лог: {ex.Message}");
            }
        }

        static void CopyDirectory(string source, string destination)
        {
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            foreach (string file in Directory.GetFiles(source))
            {
                string dest = Path.Combine(destination, Path.GetFileName(file));
                File.Copy(file, dest, true);
            }

            foreach (string dir in Directory.GetDirectories(source))
            {
                string dest = Path.Combine(destination, Path.GetFileName(dir));
                CopyDirectory(dir, dest);
            }
        }
    }

}
