namespace PR_GamePerson;

public class Person
{
    private string _name;
    private int _corX;
    private int _corY;
    private bool _camp;
    private int _health;
    private int _healthMax;

    public Person(string name, bool camp, int health, int healthMax)
    {
        _name = name;
        _camp = camp;
        _health = health;
        _healthMax = healthMax;
    }
    //Вывод информации
    public void Print()
    {
       Console.WriteLine
       ($"-------------------\n" +
        $"Имя: {_name}\n" +
        $"Местоположение: {_corX},{_corY}\n" +
        $"Здоровье: {_health}/{_healthMax}\n" +
        $"Лагерь: {_camp}\n" +
        $"--------------------"); 
    }
    //Перемещение по горизонтали 
    public void MoveX()
    {
        Console.WriteLine("Идти: | < | > |");
        string dir = Console.ReadLine();
        if (dir == ">")
        {
            _corX += 1;
        }
        else if (dir == "<")
        {
            _corX -= 1;
        }
        else
        {
            Console.WriteLine("Неверный ввод");
            MoveX();
        }
    }
    //Перемещение по вертикали
    public void MoveY()
    {
        Console.WriteLine("Идти: | A | V |");
        string dir = Console.ReadLine();
        if (dir == "A")
        {
            _corY += 1;
        }
        else if (dir == "V")
        {
            _corY -= 1;
        }
        else
        {
            Console.WriteLine("Неверный ввод");
            MoveY();
        }
    }
    //Уничтожение 
    public void Del()
    {
        
    }
    //Нанесение урона
    public void Uron()
    {
        
    }
    //Лечение
    public void Doc()
    {
        
    }
    //Полное восстановление 
    public void Vost()
    {
        
    }
    //Принадлежность к легерю
    /*public bool Lager()
    {
        
    }*/
    //Пользовательский интерфейс 
    public void Menu()
    {
        while (true)
        {   
            Console.Write
            ("--------------------------------\n" +
             "Выберете необходимое действие:\n" +
             "1. Показать данные авто\n" +
             "2. Заправиться\n" +
             "3. Передвижение\n" +
             "4. Выход\n" +
             ">");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1: Print(); break;
                case 2: MoveX(); break;
                case 3: MoveY(); break;
                case 4: return;
            }
        }
    }
}