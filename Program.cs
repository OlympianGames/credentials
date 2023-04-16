public static class Program
{
    public static Random random = new Random();

    public const string NamePath = "Project Files/config/Names.txt";
    public const string PlacePath = "Project Files/config/Places.txt";
    public const string AgePath = "Project Files/config/Age.txt";
    public const string HeightPath = "Project Files/config/Height.txt";

    public static void Main()
    {
        GenerateCredentials(500);
    }

    public static void GenerateCredentials(int amount)
    {
        List<Task> tasks = new List<Task>();

        for (int i = 0; i < amount; i++)
        {
            tasks.Add(Task.Factory.StartNew(() => { GenerateCredential();}));
        }

        Task.WaitAll(tasks.ToArray());

        // i copied this from stack overflow and it didn't seem needed
        // https://stackoverflow.com/questions/8815147/run-a-method-multiple-times-simultaneously-in-c-sharp
        //then add the result of all the tasks to r in a treadsafe fashion
        // r = tasks.Select(task => task.Result).ToList();
    }

    public static void GenerateCredential()
    {
        Credentials credentials = new Credentials(RandomName(), RandomPlace(), RandomAge(), RandomHeight());

        Console.WriteLine($"Name - {credentials.name}");
        Console.WriteLine($"Place - {credentials.place}");
        Console.WriteLine($"Birthday - {credentials.age.birthDate}");
        Console.WriteLine($"Height - {credentials.height.height}");
        Console.WriteLine();

        SaveCredentials(credentials);
    }

    public static void SaveCredentials(Credentials credentials)
    {
        string fileName = credentials.name;
        string path = $"Project Files/output/{fileName}.txt";
        try
        {
            // code for checking if there is duplicates, and if so deletes them.
            // there is 22k names fuck duplicates amirgiht 
            
            // if(File.Exists(path))
            // {
            //     int duplicate = 0;
            //     while (File.Exists($"{path} - {duplicate}"))
            //     {
            //         duplicate += 1;
            //     }
                
            //     fileName = $"{fileName} - {duplicate}";
            // }

            using(StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(credentials.ToString());
            }
        }
        catch (System.Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public static string RandomName()
    {
        try
        {
            var lines = File.ReadAllLines(NamePath);
            return lines[random.Next(0, lines.Length - 1)];
        }
        catch (System.Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex);
            Console.ForegroundColor = ConsoleColor.White;

            return "error";
        }
    }

    public static string RandomPlace()
    {
        try
        {
            var lines = File.ReadAllLines(PlacePath);
            return lines[random.Next(0, lines.Length - 1)];  
        }
        catch (System.Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex);
            Console.ForegroundColor = ConsoleColor.White;
            
            return "error";
        }
    }

    public static Age RandomAge()
    {
        try
        {
            var lines = File.ReadAllLines(AgePath);

            int minAge = int.Parse(lines[1]);
            int maxAge = int.Parse(lines[4]);

            int yearsOld = random.Next(minAge, maxAge);
            int daysOld = yearsOld * 365;
        
            return new Age(daysOld);
        }
        catch (System.Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex);
            Console.ForegroundColor = ConsoleColor.White;
            
            return new Age(0);
        }
    }

    public static Height RandomHeight()
    {
        try
        {
            var lines = File.ReadAllLines(HeightPath);

            int minFeet = int.Parse(lines[1]);
            int minInches = int.Parse(lines[2]);

            int maxFeet = int.Parse(lines[5]);
            int maxInches = int.Parse(lines[6]);
            
            int randomFeet = RandomInt(minFeet, maxFeet);
            int randomInches = RandomInt(minInches, maxInches);

            return new Height(randomFeet, randomInches);   
        }
        catch (System.Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex);
            Console.ForegroundColor = ConsoleColor.White;
            
            return new Height(0);
        }
    }

    public static int RandomInt(int bottom = 0, int top = 10)
    {
        try
        {
            int min = bottom;
            int max = top;

            if(min > max)
                return random.Next(max, min);

            else if(max > min)
                return random.Next(min, max);

            return bottom;   
        }
        catch (System.Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex);
            Console.ForegroundColor = ConsoleColor.White;
            
            return bottom;
        }
    }
}