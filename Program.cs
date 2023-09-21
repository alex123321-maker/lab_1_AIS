using lab_1;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "D:/учёба 5 сем/архетектуры ИС/lab_1/yachtclubs.csv"; // Путь к файлу CSV
        YachtClubController dataHandler;
        List<YachtClub> yachtClubs = new List<YachtClub>();


        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл yachtclubs.csv не существует.");
            Console.Write("Введите путь для создания файла (нажмите Enter для использования пути по умолчанию): ");
            string customPath = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(customPath))
            {
                filePath = customPath;
            }
            dataHandler = new YachtClubController(filePath);
            dataHandler.WriteRecords(yachtClubs);
        }

        dataHandler = new YachtClubController(filePath);

        yachtClubs = dataHandler.ReadAllRecords();



        while (true)
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Вывести все яхт-клубы");
            Console.WriteLine("2. Добавить новый яхт-клуб");
            Console.WriteLine("3. Удалить яхт-клуб по индексу");
            Console.WriteLine("4. Изменить яхт-клуб по индексу");
            Console.WriteLine("5. Загрузить яхт-клубы из файла");
            Console.WriteLine("6. Сохранить яхт-клубы в файл");
            Console.WriteLine("7. Выйти");

            var choice = Console.ReadKey().Key; // Получаем нажатую клавишу

            switch (choice)
            {
                case ConsoleKey.D1:
                    DisplayYachtClubs(yachtClubs);
                    break;
                case ConsoleKey.D2:
                    AddYachtClub(dataHandler, yachtClubs);
                    break;
                case ConsoleKey.D3:
                    RemoveYachtClub(dataHandler, yachtClubs);
                    break;
                case ConsoleKey.D4:
                    ModifyYachtClub(dataHandler, yachtClubs);
                    break;
                case ConsoleKey.D5:
                    yachtClubs = dataHandler.ReadAllRecords();
                    break;
                case ConsoleKey.D6:
                    dataHandler.WriteRecords(yachtClubs);
                    break;
                case ConsoleKey.D7:
                    Environment.Exit(0);
                    break;
                case ConsoleKey.Escape: // Выход по клавише "Esc"
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        } 
    }
    static void DisplayYachtClubs(List<YachtClub> yachtClubs)
    {
        Console.Clear();
        Console.WriteLine("\x1b[3J");
        Console.WriteLine("Список яхт-клубов:");
        for (int i = 0; i < yachtClubs.Count; i++)
        {
            Console.WriteLine($"------------------------------------");
            Console.WriteLine($"Индекс: {i}");
            Console.WriteLine($"------------------------------------");
            Console.WriteLine($"Название: {yachtClubs[i].Name}");
            Console.WriteLine($"Адрес: {yachtClubs[i].Address}");
            Console.WriteLine($"Количество яхт: {yachtClubs[i].NumberOfYachts}");
            Console.WriteLine($"Количество мест: {yachtClubs[i].NumberOfPlaces}");
            Console.WriteLine($"Наличие бассейна: {yachtClubs[i].HasPool}");
        }
        Console.WriteLine("Нажмите Enter для продолжения...");
        Console.ReadLine();
    }

    static void AddYachtClub(YachtClubController dataHandler, List<YachtClub> yachtClubs)
    {
        Console.Clear();
        Console.WriteLine("Добавление нового яхт-клуба:");

        try
        {
            Console.Write("Название: ");
            string name = Console.ReadLine();

            Console.Write("Адрес: ");
            string address = Console.ReadLine();

            Console.Write("Количество яхт: ");
            int numberOfYachts = int.Parse(Console.ReadLine());

            Console.Write("Количество мест: ");
            int numberOfPlaces = int.Parse(Console.ReadLine());

            Console.Write("Наличие бассейна (true/false): ");
            bool hasPool = bool.Parse(Console.ReadLine());

            YachtClub newClub = new YachtClub
            {
                Name = name,
                Address = address,
                NumberOfYachts = numberOfYachts,
                NumberOfPlaces = numberOfPlaces,
                HasPool = hasPool
            };

            yachtClubs.Add(newClub);
            Console.WriteLine("Яхт-клуб успешно добавлен.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: Некорректный формат ввода.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }

        Console.WriteLine("Нажмите Enter для продолжения...");
        Console.ReadLine();
    }

    static void RemoveYachtClub(YachtClubController dataHandler, List<YachtClub> yachtClubs)
    {
        Console.Clear();
        Console.WriteLine("Удаление яхт-клуба по индексу:");
        Console.Write("Введите индекс яхт-клуба для удаления: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < yachtClubs.Count)
        {
            yachtClubs.RemoveAt(index);
            Console.WriteLine("Яхт-клуб успешно удален.");
        }
        else
        {
            Console.WriteLine("Некорректный индекс для удаления яхт-клуба.");
        }

        Console.WriteLine("Нажмите Enter для продолжения...");
        Console.ReadLine();
    }

    static void ModifyYachtClub(YachtClubController dataHandler, List<YachtClub> yachtClubs)
    {
        Console.Clear();
        Console.WriteLine("Изменение яхт-клуба по индексу:");
        Console.Write("Введите индекс яхт-клуба для изменения: ");

        try
        {
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < yachtClubs.Count)
            {
                Console.WriteLine("Текущие данные для яхт-клуба:");
                Console.WriteLine($"Название: {yachtClubs[index].Name}");
                Console.WriteLine($"Адрес: {yachtClubs[index].Address}");
                Console.WriteLine($"Количество яхт: {yachtClubs[index].NumberOfYachts}");
                Console.WriteLine($"Количество мест: {yachtClubs[index].NumberOfPlaces}");
                Console.WriteLine($"Наличие бассейна: {yachtClubs[index].HasPool}");
                Console.WriteLine();

                Console.Write("Новое название (оставьте пустым для сохранения текущего): ");
                string name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    yachtClubs[index].Name = name;
                }

                Console.Write("Новый адрес (оставьте пустым для сохранения текущего): ");
                string address = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(address))
                {
                    yachtClubs[index].Address = address;
                }

                Console.Write("Новое количество яхт (оставьте пустым для сохранения текущего): ");
                string numberOfYachtsStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(numberOfYachtsStr))
                {
                    if (int.TryParse(numberOfYachtsStr, out int numberOfYachts))
                    {
                        yachtClubs[index].NumberOfYachts = numberOfYachts;
                    }
                    else
                    {
                        Console.WriteLine("Некорректное значение для количества яхт.");
                    }
                }

                Console.Write("Новое количество мест (оставьте пустым для сохранения текущего): ");
                string numberOfPlacesStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(numberOfPlacesStr))
                {
                    if (int.TryParse(numberOfPlacesStr, out int numberOfPlaces))
                    {
                        yachtClubs[index].NumberOfPlaces = numberOfPlaces;
                    }
                    else
                    {
                        Console.WriteLine("Некорректное значение для количества мест.");
                    }
                }

                Console.Write("Наличие бассейна (true/false) (оставьте пустым для сохранения текущего): ");
                string hasPoolStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(hasPoolStr))
                {
                    if (bool.TryParse(hasPoolStr, out bool hasPool))
                    {
                        yachtClubs[index].HasPool = hasPool;
                    }
                    else
                    {
                        Console.WriteLine("Некорректное значение для наличия бассейна.");
                    }
                }


                Console.WriteLine("Яхт-клуб успешно изменен.");
            }
            else
            {
                Console.WriteLine("Некорректный индекс для изменения яхт-клуба.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: Некорректный формат ввода.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }

        Console.WriteLine("Нажмите Enter для продолжения...");
        Console.ReadLine();
    }

}

