namespace PR_GamePerson;

public class Person
{
    private string _name;
    private int[] _coor = new int[2];
    private bool _camp;
    private int _health;
    private int _healthMax;
    private bool _life = true;
    private int _damage;
    private int _wins;
    public string Name { get { return _name; } }
    public int CoorX { get { return _coor[0]; } }
    public int CootY { get { return _coor[1]; } }
    public bool Camp { get { return _camp; } }
    public int Health { get { return _health; } }
    public bool Life { get { return _life; } }
    
    //Ввод данных
    public Person(string name, int healthMax, int coorX, int coorY, bool camp, int damage, int wins, bool life)
    {
        _name = name;
        _healthMax = healthMax;
        _health = _healthMax;
        _coor[0] = coorX;
        _coor[1] = coorY;
        _camp = camp;
        _damage = damage;
        _wins = wins;
        _life = life;
    }
    public Person(List<Person> persons) => Info(persons); 
    void Info(List<Person> persons)
    {
        Console.Write($"Введите имя, лагерь(+/-), максимальное здоровье, местоположение(две координаты)\n" + $">");
        string[] s = Console.ReadLine().Split();
        if (s[1] == "+") 
            _camp = true; 
        else _camp = false;
        foreach (Person p in persons)
            if(s[0] == p._name)
                Console.WriteLine("Такой персонаж уже существует");
            else _name = s[0];
        _healthMax = Int32.Parse(s[2]);
        _health = _healthMax;
        _coor[0] = Int32.Parse(s[3]);
        _coor[1] = Int32.Parse(s[4]);
        Random random = new Random();
        _damage = random.Next(_healthMax);
    }
    //Вывод информации
    void Print()
    {
       Console.WriteLine
       ($"-------------------\n" +
        $"Имя: {_name}\n" +
        $"Местоположение: {_coor[0]},{_coor[1]}\n" +
        $"Здоровье: {_health}/{_healthMax}\n" +
        $"Лагерь: {_camp}\n" +
        $"Урон: {_damage}\n" +
        $"Очки побед: {_wins}\n" +
        $"--------------------"); 
    }
    //Перемещение
    void Move(List<Person> persons)
    {
        while (true)
        {
            Console.WriteLine("Идти: | A | V | < | > |");
            if (_life == false) return;
            ConsoleKeyInfo c = Console.ReadKey();
            switch (c.Key)
            {
                case ConsoleKey.UpArrow: _coor[1] += 1; break;
                case ConsoleKey.DownArrow: _coor[1] -= 1; break;
                case ConsoleKey.LeftArrow: _coor[0] -= 1; break;
                case ConsoleKey.RightArrow: _coor[0] += 1; break;
                default: return;
            }
            Console.WriteLine($"{_coor[0]},{_coor[1]}");
            foreach (Person p in persons)
                if (_coor[0] == p._coor[0] && _coor[1] == p._coor[1])
                    if (p._life == true)
                        if (_camp != p._camp)
                            FightIn(persons);
        }   
    }
    //Интер битвы
    void FightIn(List<Person> persons)
    {
        if (_life == false) return;
        Console.Write
        ("---------------| УВАГА |---------------\n" +
         "На вашем пути враги, что будете делать:\n" +
         "1. Сражаться\n" +
         "2. Бежать\n" +
         "3. Ульта\n" +
         ">");
        switch (Convert.ToInt32(Console.ReadLine()))
        {
            case 1: Fight(persons); break;
            case 2: break;
            case 3: Del(persons); break;
        }
    }
    //Битва
    void Fight(List<Person> persons)
    {
        //поиск персонженей на клетке
        List<Person> persFildFren = new List<Person>();
        List<Person> persFildEnem = new List<Person>();
        foreach (Person p in persons)
            if (_coor[0] == p._coor[0] && _coor[1] == p._coor[1])
                if (p._life == true) 
                    if (_camp != p._camp) 
                        persFildEnem.Add(p);
                    else 
                        persFildFren.Add(p);
        //перечисление учасников битвы
        Console.WriteLine("Ваша команда:");
        foreach (Person p in persFildFren) 
            p.Print(); 
        Console.WriteLine("Команда противника:");
        foreach (Person p in persFildEnem) 
            p.Print();
        Console.WriteLine("Нажмите Enter, чтобы продолжить...");
        Console.ReadLine();
        while (true)
        {
            //создание переменных урона
           int FrenDam = 0;
           int EnemDam = 0;
           //суммирование урона живых членов команд
           foreach (Person p in persFildFren) 
               FrenDam += p._damage; 
           foreach (Person p in persFildEnem) 
               EnemDam += p._damage;
           //деление суммарного урона на количество противников 
           FrenDam /= persFildEnem.Count;
           EnemDam /= persFildFren.Count;
           //нанесение урона 
           foreach (Person p in persFildFren)
           {
               p._health -= EnemDam;
               if (p._health <= 0) 
                   p._life = false;
           } 
           foreach (Person p in persFildEnem)
           {
               p._health -= FrenDam;
               if (p._health <= 0) 
                   p._life = false;
           }
           //проверка жив ли гг 
           if (_life == false)
           { Console.WriteLine("\n------------| Вы умерли |------------\n"); return; }
           Console.WriteLine("\n----| Вы смогли отбросить врага и нашли момент восстановить силы. |----\n");
           //проверка ОЗ
           Console.WriteLine("-------------------------------\n" + "ОЗ вашей команды: ");
           foreach (Person p in persFildFren)
               if (p._life == true) 
                   Console.WriteLine($"Имя: {p._name}, ОЗ: {p._health}, урона получено: {EnemDam}");
           Console.WriteLine("\n-------------------------------\n" + "ОЗ ваших врагов: ");
           foreach (Person p in persFildEnem) 
               if (p._life == true) 
                   Console.WriteLine($"Имя: {p._name}, ОЗ: {p._health}, урона получено: {FrenDam}");
           //выбор дествий в бою
           while (true)
           {
               Console.Write
               ("\n--------------------\n" +
                "Что будете делать?\n" +
                "1. Сражаться дальше\n" +
                "2. Восстановить ОЗ\n" +
                "3. Лечить союзников\n" +
                "4. Ульта\n" +
                "5. Бежать\n" +
                ">");
               switch (Convert.ToInt32(Console.ReadLine()))
               {
                   case 1: break;
                   case 2: Vost(); break;
                   case 3: Doc(persons); break;
                   case 4: Del(persFildEnem); break;
                   case 5: return;
               } break;
           }
           //проверка кто победил 
           if (persFildEnem.Count(person => person._life == true) == 0)
           {
               Console.WriteLine("------------| ПОБЕДА |------------\n" + "Идти: | A | V | < | > |");
               _wins += 1;
               return;
           }
           Console.WriteLine("Битва продолжается...");
        }
    }
    
    //Уничтожение 
    void Del(List<Person> persons)
    {
        if (_wins >= 10)
        {
            foreach (Person p in persons) 
                if (_coor[0] == p._coor[0] && _coor[1] == p._coor[1]) 
                    if (_camp != p._camp) 
                        p._life = false;
            Console.WriteLine("Все враги уничтожены");
        }
        else
        {
            Console.WriteLine("Для ульты необходимо хотя бы 10 побед.\n" + "Битва неизбежна");
            Fight(persons);
        }
    }
    //Лечение
    void Doc(List<Person> persons)
    {
        while (true)
        {
            Console.Write("Выберете кого будете лечить:\n" + ">");
            string srh = Console.ReadLine();
            foreach (Person p in persons)
            {
                if (srh == p._name)
                {
                    Console.Write("Сколько восстановить оз:\n" + ">");
                    int hp = Convert.ToInt32(Console.ReadLine());
                    if (hp < _health) 
                        if (hp < p._healthMax)
                        {
                            p._health += hp;
                            _health -= hp;
                            break;
                        }
                        else Console.WriteLine("Нельзя лечить больше максимального ОЗ");
                    else Console.WriteLine("Вы у мамы один, а товарищей много.\n" + "Нельзя тратить здоровья больше чем имеется");
                }
            }
            break;
        }
    }
    //Полное восстановление 
    void Vost()
    {
        if (_wins >= 5)
        {
            _health = _healthMax;
            _wins -= 5;
            Console.WriteLine("Вы полностью восстановили ОЗ");
        }
        else Console.WriteLine("Победи в 5 битвах, чтобы восстановить ОЗ");
    }
    //Пользовательский интерфейс 
    public void Menu(List<Person> persons)
    {
        foreach (Person p in persons)
            if (_coor[0] == p._coor[0] && _coor[1] == p._coor[1])
                if (p._life == true)
                    if (_camp != p._camp)
                        FightIn(persons);
        while (true)
        {   
            if (_life == false)
            {
                Console.WriteLine("Персонаж мертв, нажмите Enter для выхода");
                Console.ReadLine();
                return;
            }
            else
            {
                Console.Write
                ("--------------------------------\n" +
                 "Выберете необходимое действие:\n" +
                 "1. Показать данные персонажа\n" +
                 "2. Передвижение\n" +
                 "3. Восстановить ОЗ\n" +
                 "4. Лечить союзников\n" +
                 "5. Выход\n" +
                 ">");
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1: Print(); break;
                    case 2: Move(persons); break;
                    case 3: Vost(); break;
                    case 4: Doc(persons); break;
                    case 5: return; 
                }
            }
        } 
    }
}