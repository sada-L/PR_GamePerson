﻿namespace PR_GamePerson;

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
    
    public string Name
    {
        get { return _name; }
    }
    //Ввод данных
    public Person(string name, int healthMax, int coorX, int coorY, bool camp, int damage, int wins)
    {
        _name = name;
        _healthMax = healthMax;
        _health = _healthMax;
        _coor[0] = coorX;
        _coor[1] = coorY;
        _camp = camp;
        _damage = damage;
        _wins = wins;
    }
    public Person()
    {
        Info();
    }
    void Info()
    {
        Console.Write($"Введите имя, лагерь(+/-), максимальное здоровье, местоположение(две координаты)\n" + $">");
        string[] s = Console.ReadLine().Split();
        if(s[1] == "+") 
        { _camp = true; }
        else
        { _camp = false; }
        _name = s[0];
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
        $"Победы: {_wins}\n" +
        $"--------------------"); 
    }
    //Перемещение
    void Move(List<Person> persons)
    {
        if (_life == false) 
            return;
        Console.WriteLine("Идти: | A | V | < | > |");
        while (true)
        {
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
            {
                if (_coor[0] == p._coor[0] &&
                    _coor[1] == p._coor[1])
                {
                    if (p._life == true)
                    {
                        if (_camp != p._camp)
                        {
                            FightIn(persons);
                        }
                    }
                }
            }
        }   
    }
    //Интер битвы
    void FightIn(List<Person> persons)
    {
        if(_life == false)
            return;
        Console.Write
        ("УВАГА!!!\n" +
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
        {
            if (_coor[0] == p._coor[0] &&
                _coor[1] == p._coor[1])
            {
                if (p._life == true)
                {
                    if (_camp != p._camp)
                    {
                        persFildEnem.Add(p);
                    }
                    else
                    {
                        persFildFren.Add(p);
                    }
                }
            }
        }
        //перечисление учасников битвы
        Console.WriteLine("Ваша команда:");
        foreach (Person p in persFildFren)
        {
            p.Print();
        }
        Console.WriteLine("Команда противника:");
        foreach (Person p in persFildEnem)
        {
            p.Print();
        }
        Console.WriteLine("Нажмите Enter, чтобы продолжить...");
        Console.ReadLine();
        while (true)
        { 
            //создание переменных урона
           int FrenDam = 0;
           int EnemDam = 0;
           //суммирование урона живых членов команд
           foreach (Person p in persFildFren)
           {
                FrenDam += p._damage;
           }
           foreach (Person p in persFildEnem)
           {
                EnemDam += p._damage;
           } 
           //деление суммарного урона на количество членов
           FrenDam /= persFildEnem.Count;
           EnemDam /= persFildFren.Count;
           //нанесение урона 
           foreach (Person p in persFildFren)
           {
               p._health -= EnemDam;
               if (p._health <= 0)
               {
                   p._life = false;
               }
           } 
           foreach (Person p in persFildEnem)
           {
               p._health -= FrenDam;
               if (p._health <= 0)
               {
                   p._life = false;
               }
           }
           //проверка жив ли гг 
           if (_life == false)
           {
               Console.WriteLine("Вы умерли");
               return;
           }
           Console.WriteLine("\nВы смогли отбросить врага и нашли момент восстановить силы.");
           //проверка ОЗ
           Console.WriteLine(
               "-------------------------------\n" +
               "ОЗ вашей команды: \n" +
               "-------------------------------");
           foreach (Person p in persFildFren)
           {
               if (p._life == true)
               {
                   Console.WriteLine($"Имя: {p._name}, ОЗ: {p._health}, урона получено: {EnemDam}");
               }
           }
           Console.WriteLine(
               "-------------------------------\n" +
               "ОЗ ваших врагов: \n" +
               "-------------------------------");
           foreach (Person p in persFildEnem)
           {
               if (p._life == true)
               {
                   Console.WriteLine($"Имя: {p._name}, ОЗ: {p._health}, урона получено: {FrenDam}");
               }
           }
           //лечение союзников 
           while (true)
           {  
               Console.Write("-------------------------------\n" +
                             "Хотите вылечить союзника: +/-\n" + ">");
               if (Console.ReadLine() == "+")
               {
                   Console.Write("Выберете кого будете лечить:\n" + ">");
                   string srh = Console.ReadLine();
                   foreach (Person p in persFildFren)
                   {
                       if (srh == p._name)
                       {
                           Doc(p);
                       }
                   }
               }
               else
               {
                   break;
               }
           }
           //лечение гг
           Console.Write("-------------------------------\n" +
                         "Хотите вылечить себя: +/-\n" + ">");
           if (Console.ReadLine() == "+")
           {
               Vost();
           }
           //проверка кто победил 
           if (persFildEnem.Count(person => person._life == true) == 0)
           {
               Console.WriteLine("Побида!!!");
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
            {
                if (_coor[0] == p._coor[0] &&
                    _coor[1] == p._coor[1])
                {
                    if (_camp != p._camp)
                    {
                        p._life = false;
                    }
                }
            }
            Console.WriteLine("Все враги уничтожены");
        }
        else
        {
            Console.WriteLine("Для ульты необходимо хотя бы 10 побед.\n" +
                              "Битва неизбежна");
            Fight(persons);
        }
    }
    //Лечение
    void Doc(Person p)
    {
        while (true)
        {
            Console.Write("Сколько восстановить оз:\n" + ">");
            int hp = Convert.ToInt32(Console.ReadLine());
            if (hp < _health)
            {
                if (hp < p._healthMax)
                {
                    p._health += hp;
                    _health -= hp;
                    break;
                }
                else
                {
                    Console.WriteLine("Нельзя лечить больше максимального ОЗ");
                }
            }
            else
            {
                Console.WriteLine
                ("Вы у мамы один, а товарищей много.\n" +
                 "Нельзя тратить здоровья больше чем имеется");
            }
        }
    }
    //Полное восстановление 
    void Vost()
    {
        if (_wins >= 5)
        {
            _health = _healthMax;
            _wins -= 5;
        }
        else
        {
            Console.WriteLine("Победи в 5 битвах, чтобы восстановить ОЗ");
        }
    }
    
    //Пользовательский интерфейс 
    public void Menu(List<Person> persons)
    {
        if (_life == false)
        {
            Console.WriteLine("Персонаж мертв, нажмите Enter для выхода");
            Console.ReadLine();
            return;
        }
        else
        {
            if (_life == false)
            {
                Console.WriteLine("Персонаж мертв, нажмите Enter для выхода");
                Console.ReadLine();
                return;
            }
            else
            {
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
                         "3. Лечение\n" +
                         "4. Выход\n" +
                         ">");
                        switch (Convert.ToInt32(Console.ReadLine()))
                        {
                            case 1:
                                Print();
                                break;
                            case 2:
                                Move(persons);
                                break;
                            case 3:
                                Vost();
                                break;
                            case 4: return;
                        }
                    }
                } 
            }
        }
    }
}