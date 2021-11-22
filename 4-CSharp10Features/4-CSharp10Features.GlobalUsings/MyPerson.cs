namespace _4_CSharp10Features.GlobalUsings;

internal record MyPerson(string Name, string FirstName, byte Age)
{
    public static MyPerson Generate()
    {
        return new Faker<MyPerson>()
            .CustomInstantiator(faker =>
            {
                return new MyPerson(
                    faker.Name.FirstName(),
                    faker.Name.FirstName(),
                    faker.Random.Byte(1, 100));
            })
            .Generate();
    }
}