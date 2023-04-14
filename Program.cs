public static class Program
{
    public static Random random = new Random();

    public static void Main()
    {
        for (int i = 0; i < 1000; i++)
        {
            Credentials credentials = new Credentials(RandomName(), RandomPlace(), RandomAge(), RandomHeight());

            Console.WriteLine($"Name - {credentials.name}");
            Console.WriteLine($"Place - {credentials.place}");
            Console.WriteLine($"Birthday - {credentials.age.birthDate}");
            Console.WriteLine($"Height - {credentials.height.height}");
            Console.WriteLine();

            SaveCredentials(credentials);
        }
    }

    public static void SaveCredentials(Credentials credentials)
    {
        using(StreamWriter writer = new StreamWriter($"Project Files/output/{credentials.name}.txt"))
        {
            writer.Write(credentials.ToString());
        }
    }

    public static string RandomName()
    {
        var lines = File.ReadAllLines("Project Files/config/Names.txt");
        return lines[random.Next(0, lines.Length - 1)];
    }

    public static string RandomPlace()
    {
        var lines = File.ReadAllLines("Project Files/config/Places.txt");
        return lines[random.Next(0, lines.Length - 1)];
    }

    public static Age RandomAge()
    {
        var lines = File.ReadAllLines("Project Files/config/Age.txt");

        int minAge = int.Parse(lines[1]);
        int maxAge = int.Parse(lines[4]);

        int yearsOld = random.Next(minAge, maxAge);
        int daysOld = yearsOld * 365;
    
        return new Age(daysOld);
    }

    public static Height RandomHeight()
    {
        var lines = File.ReadAllLines("Project Files/config/Height.txt");

        int minFeet = int.Parse(lines[1]);
        int minInches = int.Parse(lines[2]);

        int maxFeet = int.Parse(lines[5]);
        int maxInches = int.Parse(lines[6]);
        
        int randomFeet = RandomInt(minFeet, maxFeet);
        int randomInches = RandomInt(minInches, maxInches);

        return new Height(randomFeet, randomInches);
    }

    public static int RandomInt(int bottom = 0, int top = 10)
    {
        int min = bottom;
        int max = top;

        if(min > max)
            return random.Next(max, min);

        if(max > min)
            return random.Next(min, max);

        return 0;
    }
}