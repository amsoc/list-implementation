using ListImplementation;

var myList = GetPopulatedMyList(10);
var normalList = GetPopulatedList(10);

Console.WriteLine("MyList:");
PerformListOperations(myList);

Console.WriteLine("\nNormal list:");
PerformListOperations(normalList);


void PerformListOperations(IList<int> list)
{
    DisplayList(list);

    // add an element at the end
    list.Add(20);

    // add an element somewhere in the middle
    list.Insert(5, 15);

    DisplayList(list);

    // remove a couple of elements
    list.Remove(list[2]); // choosing a number in the list to check that it's found
    list.RemoveAt(5);
    DisplayList(list);

    // iterate with enumerator
    DisplayWithEnumerator(list.GetEnumerator());
}

MyList<int> GetPopulatedMyList(int elementCount)
{
    var list = new MyList<int>();

    Random rnd = new Random();

    for (int i = 0; i < elementCount; i++)
    {
        list.Add(rnd.Next(100));
    }

    return list;
}

List<int> GetPopulatedList(int elementCount)
{
    var list = new List<int>();

    Random rnd = new Random();

    for (int i = 0; i < elementCount; i++)
    {
        list.Add(rnd.Next(100));
    }

    return list;
}

void DisplayList(IList<int> list)
{
    string s = "";

    for (int i = 0; i < list.Count; i++)
    {
        s += list![i].ToString() + ", ";
    }

    Console.WriteLine(s.Substring(0, s.Length - 2));
}

void DisplayWithEnumerator(IEnumerator<int> enumerator)
{
    string s = "";

    Console.WriteLine("Iterating with enumerator: ");

    while (enumerator.MoveNext())
    {
        s += enumerator.Current.ToString() + ", ";
    }

    Console.WriteLine(s.Substring(0, s.Length - 2));
}