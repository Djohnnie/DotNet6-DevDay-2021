Console.WriteLine("Hello, World!");

DateTime currentDateTime = DateTime.Now;
TimeSpan currentTime = DateTime.Now.TimeOfDay;
var numberOfDays = (currentDateTime - DateTime.MinValue).TotalDays;
Console.WriteLine(currentDateTime);


DateOnly dateOnly1 = DateOnly.FromDateTime(currentDateTime);
Console.WriteLine(dateOnly1);

DateOnly dateOnly2 = DateOnly.FromDayNumber((int)numberOfDays);
Console.WriteLine(dateOnly2);

DateOnly dateOnly3 = new DateOnly(2021, 10, 27);
Console.WriteLine(dateOnly3);

TimeOnly timeOnly1 = TimeOnly.FromDateTime(currentDateTime);
Console.WriteLine(timeOnly1);

TimeOnly timeOnly2 = TimeOnly.FromTimeSpan(currentTime);
Console.WriteLine(timeOnly2);

TimeOnly timeOnly3 = new TimeOnly(18, 46);
Console.WriteLine(timeOnly3);