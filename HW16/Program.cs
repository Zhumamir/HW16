using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW16
{

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Добро пожаловать в Простой Файловый Менеджер!");

            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Просмотр содержимого директории");
                Console.WriteLine("2. Создание файла или директории");
                Console.WriteLine("3. Удаление файла или директории");
                Console.WriteLine("4. Копирование файла или директории");
                Console.WriteLine("5. Перемещение файла или директории");
                Console.WriteLine("6. Чтение из файла");
                Console.WriteLine("7. Запись в файл");
                Console.WriteLine("8. Выйти");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayDirectoryContents();
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
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                        break;
                }
            }
        }

        static void DisplayDirectoryContents()
        {
            Console.Write("Введите путь к директории: ");
            string directoryPath = Console.ReadLine();

            try
            {
                string[] files = Directory.GetFiles(directoryPath);
                string[] directories = Directory.GetDirectories(directoryPath);

                Console.WriteLine("\nСписок файлов:");
                foreach (string file in files)
                {
                    Console.WriteLine($"- {Path.GetFileName(file)}");
                }

                Console.WriteLine("\nСписок директорий:");
                foreach (string directory in directories)
                {
                    Console.WriteLine($"- {Path.GetFileName(directory)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CreateFileOrDirectory()
        {
            Console.Write("Введите путь: ");
            string path = Console.ReadLine();

            Console.WriteLine("Выберите тип (1 - Файл, 2 - Директория):");
            string typeChoice = Console.ReadLine();

            try
            {
                if (typeChoice == "1")
                {
                    File.Create(path).Close();
                    Console.WriteLine($"Файл создан по пути: {path}");
                }
                else if (typeChoice == "2")
                {
                    Directory.CreateDirectory(path);
                    Console.WriteLine($"Директория создана по пути: {path}");
                }
                else
                {
                    Console.WriteLine("Неверный выбор типа.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void DeleteFileOrDirectory()
        {
            Console.Write("Введите путь к файлу или директории: ");
            string path = Console.ReadLine();

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    Console.WriteLine($"Файл удален: {path}");
                }
                else if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    Console.WriteLine($"Директория удалена: {path}");
                }
                else
                {
                    Console.WriteLine("Файл или директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CopyFileOrDirectory()
        {
            Console.Write("Введите путь к исходному файлу или директории: ");
            string sourcePath = Console.ReadLine();

            Console.Write("Введите путь к целевому месту: ");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)), true);
                    Console.WriteLine($"Файл скопирован в: {Path.Combine(destinationPath, Path.GetFileName(sourcePath))}");
                }
                else if (Directory.Exists(sourcePath))
                {
                    CopyDirectory(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine($"Директория скопирована в: {Path.Combine(destinationPath, Path.GetFileName(sourcePath))}");
                }
                else
                {
                    Console.WriteLine("Файл или директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void MoveFileOrDirectory()
        {
            Console.Write("Введите путь к исходному файлу или директории: ");
            string sourcePath = Console.ReadLine();

            Console.Write("Введите путь к целевому месту: ");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine($"Файл перемещен в: {Path.Combine(destinationPath, Path.GetFileName(sourcePath))}");
                }
                else if (Directory.Exists(sourcePath))
                {
                    Directory.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine($"Директория перемещена в: {Path.Combine(destinationPath, Path.GetFileName(sourcePath))}");
                }
                else
                {
                    Console.WriteLine("Файл или директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void ReadFromFile()
        {
            Console.Write("Введите путь к файлу: ");
            string filePath = Console.ReadLine();

            try
            {
                string content = File.ReadAllText(filePath);
                Console.WriteLine($"Содержимое файла {Path.GetFileName(filePath)}:\n{content}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void WriteToFile()
        {
            Console.Write("Введите путь к файлу: ");
            string filePath = Console.ReadLine();

            Console.WriteLine("Введите текст для записи в файл:");
            string content = Console.ReadLine();

            try
            {
                File.WriteAllText(filePath, content);
                Console.WriteLine($"Текст записан в файл {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CopyDirectory(string sourcePath, string destinationPath)
        {
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            string[] files = Directory.GetFiles(sourcePath);
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destinationPath, fileName);
                File.Copy(file, destFile, true);
            }

            string[] directories = Directory.GetDirectories(sourcePath);
            foreach (string directory in directories)
            {
                string dirName = Path.GetFileName(directory);
                string destDir = Path.Combine(destinationPath, dirName);
                CopyDirectory(directory, destDir);
            }
        }
    }

}
