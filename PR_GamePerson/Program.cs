using PR_GamePerson;
AccChose accChose = new AccChose();
while (true)
{
    Person hero = accChose.AccMenu();
    hero.Menu(accChose.Acc);
}