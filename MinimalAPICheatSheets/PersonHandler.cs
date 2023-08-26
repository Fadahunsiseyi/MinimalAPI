namespace MinimalAPICheatSheets;

public class PersonHandler
{
    public Person HandleGet()
    {
        return new Person("John", "Doe", 42);
    }
    public Person HandleGetById(int id)
    {
        return new Person("esther", "joy",id);
    }
}

public  record Person(string FirstName, string LastName, int Age);
