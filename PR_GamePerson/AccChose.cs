namespace PR_GamePerson;

public class AccChose
{
    private List<Person> _acc = new List<Person>();
    public List<Person> Acc { get { return _acc; } }
    //Добавление персонажа
    void AddAcc()
    {
        _acc.Add(new Person(_acc));
        Console.WriteLine("Персонаж добавлен");
    }
    //Удаление персонажа
    void DelAcc()
    {
        while (true)
        {
            Console.Write("Какого персонажа хотите удалить: ");
            int index = Convert.ToInt32(Console.ReadLine());
            if (index >= 0 && index < _acc.Count) 
            {_acc.RemoveAt(index); return; }
            else Console.WriteLine("Неверный индекс персонажа, попробуйте ещё раз");
        }
    }
    //Выбор персонажа
    Person GetAcc()
    {
        while (true)
        {
            Console.Write("Введите индекс персонажа: ");
            int index = Convert.ToInt32(Console.ReadLine());
            if (index >= 0 && index < _acc.Count) 
                return _acc[index];
            else Console.WriteLine("Неверный индекс персонажа, попробуйте ещё раз");
        }
    }
    //Вывод информации
    void Info()
    {
        for(int i = 0; i < _acc.Count; i++) 
            Console.WriteLine($"Индекс персонажа: {i}, Имя: {_acc[i].Name}, лагерь: {_acc[i].Camp}, cтатус: {_acc[i].Life}, ОЗ: {_acc[i].Health}, коорды: {_acc[i].CoorX},{_acc[i].CootY}");
    }
    //Интефейс управления персонажами
    public Person AccMenu()
    {
        while (true)
        {
            try
            {
                Console.Write
                ("------------------------------\n" +
                 "Выберете необходимое действие:\n" +
                 "0. Просмореть доступных персонажей\n" +
                 "1. Добавить персонажа\n" +
                 "2. Удалить персонажа\n" +
                 "3. Выбрать персонажа\n" +
                 "4. Выход\n" +
                 ">");
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 0: Info(); break;
                    case 1: AddAcc(); break;
                    case 2: DelAcc(); break;
                    case 3: return GetAcc(); 
                    case 4: return null;
                    case 5: Test(); break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR");
            }
        }
    }

    void Test()
    {
        Person a = new Person("GG", 200, 0,0, true, 60, 0, true);
        _acc.Add(a);
        Random random = new Random();
        for (int i = 0; i < 30; i++)
        {
            string n = Convert.ToString(random.Next(100, 1000));
            foreach (Person p in _acc)
                if (n == p.Name) 
                    n = Convert.ToString(random.Next(100, 1000));
            int h = random.Next(50, 200);
            int cX = random.Next(-10, 10);
            int cY = random.Next(-10, 10);
            int d = random.Next(50);
            _acc.Add(new Person(n, h,cX,cY,false,d,0,true));
        }
        for (int i = 0; i < 30; i++)
        {
            string n = Convert.ToString(random.Next(100, 1000));
            foreach (Person p in _acc)
                if (n == p.Name) 
                    n = Convert.ToString(random.Next(100, 1000));
            int h = random.Next(50, 200);
            int cX = random.Next(-10, 10);
            int cY = random.Next(-10, 10);
            int d = random.Next(100);
            _acc.Add(new Person(n, h,cX,cY,true,d,0,true));
        }
        Console.WriteLine("Персонаж добавлен");
    }
}
