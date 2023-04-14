public struct Credentials
{
    public string name;
    public string place;
    public Age age;
    public Height height;

    public Credentials(string name, string place, Age age, Height height)
    {
        this.name = name;
        this.place = place;
        this.age = age;
        this.height = height;
    }

    public override string ToString()
    {
        string name = $"Name - {this.name}";
        string place = $"Place - {this.place}";
        string birthday = $"Birthday - {this.age.birthDate}";
        string height = $"Height - {this.height.height}";

        return $"{name}\n{place}\n{birthday}\n{height}";
    }
}

public struct Age
{
    public string birthDate;
    public int daysOld;

    public Age(int daysOld)
    {
        this.daysOld = daysOld;
        this.birthDate = DateTime.Today.AddDays(-daysOld).ToString("MM/dd/yyyy");
    }
}

public struct Height
{
    public string height;
    public int feet;
    public float inches;

    public Height(int feet, float inches = 0)
    {
        this.height = $"{feet}'{inches}";
        this.feet = feet;
        this.inches = inches;
    }
}