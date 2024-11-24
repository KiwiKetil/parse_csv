namespace ParseCsv.ParseV1;
public class Person
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required int Age { get; set; }
    public required string Country { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName} {Email} {Age} {Country}";
    }
}

  

//James,Weirdo,john.doeexample.com,abc,USA
//Jane, Smith,jane.smith @example.com,34, Canada
//"Indiana", Jones, indaemail.no,4z4, Fran'ce
//Jens, Jensemann
