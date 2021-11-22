
for (int i = 0; i < 20; i++)
{
    WriteLine(MyPerson.Generate());
    await T.Delay(100);
}