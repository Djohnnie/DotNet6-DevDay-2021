
DoSomething("Hello, World!");


static void DoSomething(string text)
{
    ArgumentNullException.ThrowIfNull(text);
}