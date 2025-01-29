// See https://aka.ms/new-console-template for more information

#region Setup
using OOPsReview;

Employment? foundEmployment = null;
string SEARCHTERM = "PG II";

Console.WriteLine("Collection Queries");

List<Employment> Employments = CreateCollection();

#endregion

#region Loops
Console.WriteLine("\n--Loops Section--");

for (int i = 0; i < Employments.Count; i++)
{
    Console.WriteLine(Employments[i]);
}

foreach (var employment in Employments)
{
    Console.WriteLine(employment);
}

for (int i = 0;i < Employments.Count; i++)
{
    if (Employments[i].Title.Equals(SEARCHTERM))
    {
        foundEmployment = Employments[i];
        i = Employments.Count;
    }
}

TestForFoundItem(foundEmployment, SEARCHTERM);
foundEmployment = null;

foreach (var employment in Employments)
{
    if (employment.Title.Equals(SEARCHTERM))
    {
        foundEmployment = employment;
    }
}

TestForFoundItem(foundEmployment, SEARCHTERM);
foundEmployment = null;
#endregion

#region Inbuilt

Console.WriteLine("\n--Inbuilt Collection Methods--");

foundEmployment = null;

//e is just like var employment from foreach
//This will find the first instance that matches.
foundEmployment = Employments.Find(e  => e.Title.Equals(SEARCHTERM));

TestForFoundItem(foundEmployment, SEARCHTERM);

//Finds the first instance that matches the partial string
foundEmployment = Employments.Find(e => e.Title.Contains("PG"));

TestForFoundItem(foundEmployment, "PG");

//Finds the last instance that matches the partial string
foundEmployment = Employments.FindLast(e => e.Title.Contains("PG"));

TestForFoundItem(foundEmployment, "PG");

if (Employments.Any(e => e.Title.Contains("SP")))
{
    Console.WriteLine("Employee was once a supervisor of some level.");
}
else
{
    Console.WriteLine("Employee has never been a supervisor");
}

#endregion

#region LINQ

Console.WriteLine("\n--LINQ Section--");

List<Employment>? foundCollection = null;

//LINQ Method Syntax
foundCollection = Employments.Where(e => e.Title.Contains("PG")).ToList();

foreach (var employment in foundCollection)
{
    Console.WriteLine($"An employment of PG was found: {employment}");
}

foundCollection = null;

//LINQ Query Syntax
foundCollection = 
    (
        from employment in Employments
        where employment.Title.Contains("PG")
        select employment
    ).ToList();

foreach (var employment in foundCollection)
{
    Console.WriteLine($"An employment of PG was found: {employment}");
}
#endregion

#region Helper Functions

static List<Employment> CreateCollection()
{
    List<Employment> newCollection = new List<Employment>();
    
    newCollection.Add(new Employment("PG I", SupervisoryLevel.Entry,
                        DateTime.Parse("May 1, 2010"), 0.5));
    newCollection.Add(new Employment("PG II", SupervisoryLevel.TeamMember,
                    DateTime.Parse("Nov 1, 2010"), 3.2));
    newCollection.Add(new Employment("PG III", SupervisoryLevel.TeamLeader,
                    DateTime.Parse("Jan 6, 2014"), 8.6));
    newCollection.Add(new Employment("SP I", SupervisoryLevel.Supervisor,
                    DateTime.Parse("Jul 22, 2022"), 2.25));
    
    return newCollection;
}

static void TestForFoundItem(Employment? employment, string searchTerm)
{
    Console.WriteLine();

    if(employment == null)
    {
        Console.WriteLine($"Person had no {searchTerm} employment.");
    }
    else
    {
        Console.WriteLine($"Person had {searchTerm} employment: {employment}.");
    }
}

#endregion
