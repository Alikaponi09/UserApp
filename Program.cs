using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace UserApp
{
    public delegate void PersonAction();

    class Program
    {
        public static List<Person> list = new List<Person>();

        static void Main(string[] args)
        {
            bool extit = true;
            while (extit)
            {
                Console.Clear();
                Console.WriteLine("Введите цифру соответвующию определённому действию" +
                    "\nДоступные действия:" +
                    "\n1 - Добавить пользователя" +
                    "\n2 - Вывести список пользователей" +
                    "\n3 - Удалить пользователя по ID" +
                    "\n4 - Изменить пользователя по ID" +
                    "\n5 - Сохранить список в файл" +
                    "\n6 - Прочитать список из файла" +
                    "\n7 - Прекращения работы");
                int.TryParse(Console.ReadLine().ToString(), out int x);
                switch (x)
                {
                    case 1:
                        Action(AddPerson);
                        break;
                    case 2:
                        Action(Print);
                        break;
                    case 3:
                        Action(DeletePerson);
                        break;
                    case 4:
                        Action(EditPerson);
                        break;
                    case 5:
                        Action(SaveInFile);
                        break;
                    case 6:
                        Action(ReadFile);
                        break;
                    case 7:
                        extit = false;
                        break;
                    default:
                        Console.WriteLine("Номер не соответствует ни одной функии программы\nПопробуйте снова");
                        break;
                }

            }
            Console.WriteLine("\nПрощайте " + Environment.UserName);
            Thread.Sleep(50);
        }

        private static void ErrorMessage(string str) 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n" + str);
        }

        private static bool StringCheck(string nameParametr, string str) 
        {
            if (!string.IsNullOrEmpty(str)) return true;
            ErrorMessage($"{nameParametr} пустое");
            return false;
        }

        private static Person ReturnPersonInID() 
        {
            Console.WriteLine("Введите ID пользователя");
            int.TryParse(Console.ReadLine().ToString(), out int ID);
            return list.FirstOrDefault(x => x.ID == ID);
        }

        private static void Action(PersonAction personAction)
        {
            do
            {
                Console.Clear();
                personAction?.Invoke();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nДля повтора нажмите Q\nНажмите любую другую клавишу для выхода в меню");
                
            } while (Console.ReadLine().ToLower() == "q");
            
        }

        public static void ReadFile()
        {
            list = new List<Person>(1000);
            var file = File.ReadAllLines("Users.txt");
            foreach (var item in file)
            {
                var subItem = item.Split();
                list.Add(new Person(Convert.ToInt32(subItem[0]), subItem[1], subItem[2]));
            }
            list.RemoveAll(x => x == null);
            Console.WriteLine("\nЧтение завершено");
        }

        public static void SaveInFile()
        {
            File.WriteAllText("Users.txt", string.Join("\n", list.Select(x => $"{x.ID} {x.Name} {x.LastName}")));
            Console.WriteLine("\nФайл сохранён");
        }

        public static void AddPerson()
        {
            Person person = new Person();
            Console.WriteLine("Введите имя");
            person.SetName(Console.ReadLine());
            Console.WriteLine("Введите фамилию");
            person.SetLastName(Console.ReadLine());
            if (!StringCheck("Имя пользователя", person.Name)) return;
            if (!StringCheck("Фамилия пользователя", person.LastName)) return;
            list.Add(person);
            Console.WriteLine("\nПользователь добавлен");
        }

        public static void EditPerson()
        {            
            var person = ReturnPersonInID();
            if (person == null) 
            {
                ErrorMessage("Пользователь не найден");
                return; 
            }
            Console.WriteLine($"Имя: {person.Name}\nВведите новое имя");
            person.SetName(Console.ReadLine());
            Console.WriteLine($"Фамилия: {person.LastName}\nВведите новую фамилию");
            person.SetLastName(Console.ReadLine());
            if (!StringCheck("Имя пользователя", person.Name)) return;
            if (!StringCheck("Фвмилия пользователя", person.LastName)) return;
            list[list.FindIndex(x => x.ID == person.ID)] = person;
            Console.WriteLine("\nПользователь изменён");
        }

        public static void DeletePerson()
        {
            if (!list.Remove(ReturnPersonInID()))
            {
                ErrorMessage("Пользователь не найден");
                return;
            }
            Console.WriteLine("\nПoльзователь удалён");
        }

        public static void Print()
        {
            Console.WriteLine("\n" + string.Join("\n", list.Select(x => x.Name)));
        }
    }
}
