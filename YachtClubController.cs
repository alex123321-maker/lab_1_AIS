using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab_1
{
    internal class YachtClubController
    {
        private string filePath; // Путь к файлу CSV

        public YachtClubController(string filePath)
        {
            this.filePath = filePath;
        }

        // Метод для чтения всех записей из файла и возврата списка объектов YachtClub
        public List<YachtClub> ReadAllRecords()
        {
            List<YachtClub> yachtClubs = new List<YachtClub>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');

                        // Проверяем, что данные в строке корректны, иначе пропускаем строку
                        if (data.Length >= 4)
                        {
                            YachtClub club = new YachtClub
                            {
                                Name = data[0],
                                Address = data[1],
                                NumberOfYachts = int.Parse(data[2]),
                                NumberOfPlaces = int.Parse(data[3]),
                                HasPool = bool.Parse(data[4])
                            };

                            yachtClubs.Add(club);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Ошибка чтения файла: {e.Message}");
                Console.WriteLine("Нажмите Enter для продолжения...");
                Console.ReadLine();
            }

            return yachtClubs;
        }

        // Метод для записи списка объектов YachtClub в файл CSV
        public void WriteRecords(List<YachtClub> yachtClubs)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (YachtClub club in yachtClubs)
                    {
                        string line = $"{club.Name},{club.Address},{club.NumberOfYachts},{club.NumberOfPlaces},{club.HasPool}";
                        writer.WriteLine(line);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Ошибка записи в файл: {e.Message}");
            }
        }

        // Метод для добавления новой записи в список и сохранения в файле
        public void AddRecord(YachtClub club)
        {
            List<YachtClub> yachtClubs = ReadAllRecords();
            yachtClubs.Add(club);
            WriteRecords(yachtClubs);
        }

        // Метод для удаления записи по индексу
        public void RemoveRecord(int index)
        {
            List<YachtClub> yachtClubs = ReadAllRecords();
            if (index >= 0 && index < yachtClubs.Count)
            {
                yachtClubs.RemoveAt(index);
                WriteRecords(yachtClubs);
            }
            else
            {
                Console.WriteLine("Неверный индекс для удаления записи.");
            }
        }
    }
}
