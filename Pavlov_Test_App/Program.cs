//#define xml
using Pavlov_Test_App.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml;

List<Box> allBoxes = new List<Box>();
List<Pallet> Pallets = new List<Pallet>();

Console.WriteLine("Здравствуйте! Вас приветствует тестовое задание от кандидата Павлова Владимира.\n Чтобы загрузить списки коробок и паллет нажмите любую клавишу");
Console.ReadKey();
FillBoxesList();
FillPalletsList();
Console.WriteLine("Нажмите любую клавишу, чтобы добавить коробки в паллеты и отобразить отсортированный в соответствии с заданием список.");
Console.ReadKey();
FillPalets();
SortPalletsFirst();
Console.WriteLine("Нажмите любую клавишу, чтобы отобразить три паллета с наибольшим сроком годности");
Console.ReadKey();
SortPalletsSecond();
Console.WriteLine("\n");
Console.WriteLine("Работа тестового приложения завершена!Нажмите любую клавишу для выхода из программы.");
Console.ReadKey();
void SortPalletsFirst()//первая сортировка в соответствии с заданием
{
    var sortedPallets = Pallets.GroupBy(x => x.ExpirationDate).OrderBy(x => x.Key).ToList();
    foreach(var group in sortedPallets)
    {
        Console.WriteLine($"--------------------{group.Key}");
        var sortedGroup = group.Select(x => x).ToList().OrderBy(x => x.ExpirationDate).ThenBy(x => x.Weight);
        foreach(var pallet in sortedGroup) 
        {
        string boxnumbers = string.Join(",", pallet.boxes.Select(x => x.Id));
        Console.WriteLine($"Паллета номер: {pallet.Id} Общий вес с коробками: {pallet.Weight} Срок годности: {pallet.ExpirationDate.ToString()}\nОбщий объём паллеты: {pallet.Volume} Номера коробок в паллете: {boxnumbers}");
        }
    }
    Console.WriteLine("Группировка и сортировка выполнены успешно!\n");
}
void SortPalletsSecond()//вторая сортировка в соответствии с заданием
{
    Pallets = Pallets.OrderByDescending(x => x.boxes.Max(a => a.ExpirationDate)).ThenBy(x => x.Volume).Take(3).ToList();
    Console.WriteLine("Дополнительная сортировка выполнена успешно!");
    foreach (Pallet pallet in Pallets)
    {
        string boxnumbers = string.Join(",", pallet.boxes.Select(x => x.Id));
        Console.WriteLine($"Паллета номер: {pallet.Id} Общий вес с коробками: {pallet.Weight} Срок максимальной годности: {pallet.boxes.Max(x => x.ExpirationDate).ToString()}\nОбщий объём паллеты: {pallet.Volume} Номера коробок в паллете: {boxnumbers}");
    }
}
void FillBoxesList()//вытаскиваем из XML(либо рандомизируем) коробки, добавляем в общий список
{
#if (xml)
    try
    {
        XmlDocument? xDoc = new XmlDocument();
        xDoc.Load("C:\\Users\\Vladimir\\Desktop\\Projects\\Тестовые\\Pavlov_Test_App\\Boxes.xml");
        XmlElement? xRoot = xDoc.DocumentElement;
        int id = 1, height = 1, width = 1, depth = 1, weight = 1;
        DateOnly date = new DateOnly();
        foreach (XmlElement element in xRoot)
        {
            foreach (XmlNode child in element.ChildNodes)
            {
                switch (child.Name)
                {
                    case "id":
                        id = int.Parse(child.InnerText);
                        break;
                    case "height":
                        height = int.Parse(child.InnerText);
                        break;
                    case "width":
                        width = int.Parse(child.InnerText);
                        break;
                    case "depth":
                        depth = int.Parse(child.InnerText);
                        break;
                    case "weight":
                        weight = int.Parse(child.InnerText);
                        break;
                    case "date":
                        date = DateOnly.Parse(child.InnerText);
                        break;
                }
            }
            Box box = new Box(id, height, width, depth, weight, date);
            var results = new List<ValidationResult>();
            var context = new ValidationContext(box);
            if (!Validator.TryValidateObject(box, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine("////////------ОШИБКА------////////");
                    Console.WriteLine($"Ошибка при внесении данных в коробке под номером {id}. Текст ошибки:");
                    Console.WriteLine(error.ErrorMessage);
                    Console.WriteLine("////////------------------////////");
                }
            }
            else
            {
                allBoxes.Add(box);
            }
        }

        Console.WriteLine($"В список коробок успешно добавлены {allBoxes.Count} элемент(-ов, -а)!");
    }
    catch(Exception ex) { 
        Console.Write(ex.Message);
    }
#else
    Random random = new Random();
    for(int i = 0; i < 30; i++)
    {
        allBoxes.Add(new Box(i, random.Next(1, 15), random.Next(1, 15), random.Next(1, 15), random.Next(1, 15), new DateOnly(random.Next(2020,2023), random.Next(1,12), random.Next(1,28))));
    }
    Console.WriteLine($"В список коробок успешно добавлены 30 элементов!");
#endif

    }
void FillPalletsList()//вытаскиваем из XML(либо рандомизируем) паллеты, добавляем в общий список {
{
#if (xml)
    try
    {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("C:\\Users\\Vladimir\\Desktop\\Projects\\Тестовые\\Pavlov_Test_App\\Pallets.xml");
        XmlElement? xRoot = xDoc.DocumentElement;
        int id = 1, height = 1, width = 1, depth = 1;
        foreach (XmlElement element in xRoot)
        {
            foreach (XmlNode child in element.ChildNodes)
            {
                switch (child.Name)
                {
                    case "id":
                        id = int.Parse(child.InnerText);
                        break;
                    case "height":
                        height = int.Parse(child.InnerText);
                        break;
                    case "width":
                        width = int.Parse(child.InnerText);
                        break;
                    case "depth":
                        depth = int.Parse(child.InnerText);
                        break;
                }
            }
            Pallet pallet = new Pallet(id, height, width, depth);
            var results = new List<ValidationResult>();
            var context = new ValidationContext(pallet);
            if (!Validator.TryValidateObject(pallet, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine("////////------ОШИБКА------////////");
                    Console.WriteLine($"Ошибка при внесении данных в паллете под номером {id}. Текст ошибки:");
                    Console.WriteLine(error.ErrorMessage);
                    Console.WriteLine("////////------------------////////");
                }
            }
            else
            {
                Pallets.Add(pallet);
            }
        }
        Console.WriteLine($"В список паллет успешно добавлены {Pallets.Count} элемент(-ов, -а)!");
    }
    catch(Exception ex) { Console.WriteLine(ex.Message); }
#else
    Random random = new Random();
    for(int i = 0; i < 10; i++)
    {
        Pallets.Add(new Pallet(random.Next(1, 15), random.Next(1, 15), random.Next(20, 30), random.Next(20, 30)));
    }
    Console.WriteLine($"В список паллет успешно добавлены 10 элементов!");
#endif
}  
void FillPalets()//распределяем коробки по паллетам
{
    int index = 0;
    try
    {
        foreach (Box box in allBoxes)
        {
            Pallets[index].AddBox(box);
            index++;
            if (index == Pallets.Count) { index = 0; }
        }
    }
    catch (InvalidDataException ex)
    {
        Console.WriteLine(ex.Message);
    }
}
