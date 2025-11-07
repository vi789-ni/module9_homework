using System;
using System.Collections.Generic;
using System.Linq;

namespace CompositePattern
{
    abstract class FileSystemComponent
    {
        public string Name { get; protected set; }
        public FileSystemComponent(string name) => Name = name;

        public abstract void Display(string indent = "");
        public abstract int GetSize();
    }

    class File : FileSystemComponent
    {
        private int size;

        public File(string name, int size) : base(name)
        {
            this.size = size;
        }

        public override void Display(string indent = "")
        {
            Console.WriteLine($"{indent}- Файл: {Name} ({size} KB)");
        }

        public override int GetSize() => size;
    }

    class Directory : FileSystemComponent
    {
        private List<FileSystemComponent> components = new List<FileSystemComponent>();

        public Directory(string name) : base(name) { }

        public void Add(FileSystemComponent component)
        {
            if (components.Any(c => c.Name == component.Name))
            {
                Console.WriteLine($" Компонент с именем '{component.Name}' уже существует в '{Name}'.");
            }
            else
            {
                components.Add(component);
                Console.WriteLine($" Добавлено: {component.Name} в '{Name}'.");
            }
        }

        public void Remove(string name)
        {
            var found = components.FirstOrDefault(c => c.Name == name);
            if (found != null)
            {
                components.Remove(found);
                Console.WriteLine($" Удалено: {name} из '{Name}'.");
            }
            else
            {
                Console.WriteLine($" Компонент '{name}' не найден в '{Name}'.");
            }
        }

        public FileSystemComponent GetSubdirectory(string name)
        {
            return components.FirstOrDefault(c => c is Directory && c.Name == name);
        }

        public override void Display(string indent = "")
        {
            Console.WriteLine($"{indent}+ Папка: {Name}");
            foreach (var component in components)
                component.Display(indent + "   ");
        }

        public override int GetSize()
        {
            int total = 0;
            foreach (var c in components)
                total += c.GetSize();
            return total;
        }
    }

    class Program
    {
        static void Main()
        {
            Directory root = new Directory("Root");
            Directory current = root;

            while (true)
            {
                Console.WriteLine($"\n Текущая папка: {current.Name}");
                Console.WriteLine("1 - Добавить файл");
                Console.WriteLine("2 - Добавить папку");
                Console.WriteLine("3 - Перейти в папку");
                Console.WriteLine("4 - Вернуться на уровень выше");
                Console.WriteLine("5 - Удалить элемент");
                Console.WriteLine("6 - Показать структуру");
                Console.WriteLine("7 - Узнать общий размер");
                Console.WriteLine("0 - Выход");
                Console.Write("Выбор: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введите имя файла: ");
                        string fname = Console.ReadLine();
                        Console.Write("Введите размер файла (KB): ");
                        if (int.TryParse(Console.ReadLine(), out int size))
                        {
                            File f = new File(fname, size);
                            current.Add(f);
                        }
                        else Console.WriteLine("Некорректный размер!");
                        break;

                    case "2":
                        Console.Write("Введите имя новой папки: ");
                        string dname = Console.ReadLine();
                        Directory d = new Directory(dname);
                        current.Add(d);
                        break;

                    case "3":
                        Console.Write("Введите имя папки, в которую хотите перейти: ");
                        string goName = Console.ReadLine();
                        var nextDir = current.GetSubdirectory(goName);
                        if (nextDir is Directory dir)
                        {
                            current = dir;
                            Console.WriteLine($" Перешли в папку '{current.Name}'.");
                        }
                        else
                        {
                            Console.WriteLine($"Папка '{goName}' не найдена.");
                        }
                        break;

                    case "4":
                        if (current != root)
                        {
                            current = root; 
                            Console.WriteLine(" Вернулись в корневую папку.");
                        }
                        else
                        {
                            Console.WriteLine("Вы уже в корне.");
                        }
                        break;

                    case "5":
                        Console.Write("Введите имя компонента для удаления: ");
                        string delName = Console.ReadLine();
                        current.Remove(delName);
                        break;

                    case "6":
                        Console.WriteLine("\n Структура файловой системы:");
                        root.Display();
                        break;

                    case "7":
                        Console.WriteLine($" Общий размер: {root.GetSize()} KB");
                        break;

                    case "0":
                        Console.WriteLine("Выход...");
                        return;

                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }
    }
}

