namespace PR_GamePerson;

public class AccChose
{
    private List<Person> _acc = new List<Person>();
    public List<Person> Acc
    {
        get { return _acc; }
    }
    //Добавление персонажа
    void AddAcc()
    {
        _acc.Add(new Person());
        Console.WriteLine("Персонаж добавлен");
    }
    //Удаление персонажа
    void DelAcc()
    {
        Console.Write("Какого персонажа хотите удалить: ");
        int index = Convert.ToInt32(Console.ReadLine());
        if (index >= 0 && index < _acc.Count)
            _acc.RemoveAt(index);
    }
    //Выбор персонажа
    Person GetAcc()
    {
        Console.Write("Введите индекс персонажа: ");
        int index = Convert.ToInt32(Console.ReadLine());
        if (index >= 0 && index < _acc.Count)
            return _acc[index];
        return null;
    }
    //Вывод информации
    void Info()
    {
        for(int i = 0; i < _acc.Count; i++)
        {
            Console.WriteLine($"Индекс счета: {i}, Имя: {_acc[i].Name}");
        }
    }
    //Интефейс управления персонажами
    public Person AccMenu()
    {
        while (true)
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
    }

    void Test()
    {
        Person a = new Person("GG", 90, 0,0, true, 50, 20);
        Person b = new Person("Enem", 100, 0, 5, false, 40,0);
        Person c = new Person("Fren", 100, 0, 5, true, 20,0);
        Person s = new Person("Enem", 100, 0, 5, false, 40,0);
        Person t = new Person("Enem", 100, 0, 5, false, 40,0);
        Person j = new Person("Enem", 100, 0, 5, false, 40,0);
        Person n = new Person("Enem", 100, 0, 5, false, 40,0);
        Person v = new Person("Enem", 100, 0, 5, false, 40,0);
        Person p = new Person("Enem", 100, 0, 5, false, 40,0);
        _acc.Add(a);
        _acc.Add(b);
        _acc.Add(c);
        _acc.Add(s);
        _acc.Add(t);
        _acc.Add(j);
        _acc.Add(n);
        _acc.Add(v);
        _acc.Add(p);
        Console.WriteLine("Персонаж добавлен");
    }
}
