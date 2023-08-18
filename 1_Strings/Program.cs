#pragma warning disable CS8321
#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
#pragma warning disable CS0253 // Possible unintended reference comparison; left hand side needs cast

using System.Diagnostics;
using System.Text;

// https://learn.microsoft.com/en-us/dotnet/api/system.string?view=net-6.0

// A string is a sequential collection of characters that's used to represent text. A String object is a 
// sequential collection of System.Char objects that represent a string; a System.Char object corresponds 
// to a UTF-16 code unit. The value of the String object is the content of the sequential collection of
//  System.Char objects, and that value is immutable (that is, it is read-only). 
// For more information about the immutability of strings, see the Immutability and the StringBuilder class section. 
// The maximum size of a String object in memory is 2-GB, or about 1 billion characters.

// 🎖️ https://github.com/microsoft/referencesource/blob/master/mscorlib/system/string.cs
// Line 570 public bool Equals(String value) {
// https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/String.cs

void Benchmark(Func<int, string> myMethodName, int param)
{
    Stopwatch stopWatch = new Stopwatch();
    stopWatch.Start();

    var s = myMethodName.Invoke(param);

    stopWatch.Stop();
    TimeSpan ts = stopWatch.Elapsed;
    Console.WriteLine(string.Format("RunTime: {0:00}.{1:000}", ts.Seconds, ts.Milliseconds));
}

void Part1_StringClass()
{
    System.String s1 = "abcd";
    Console.Write("s1[1]\t");
    Console.WriteLine(s1[1]);
    // Outputs "b"
    Console.Write("\r\n");

    // string s2 = "abcd";
    // Console.Write("s2[1]\t");
    // Console.WriteLine(s2[1]);

    // var s3 = new string(new char[] { 'a', 'b', 'c', 'd' });
    // Console.Write("s3\t");
    // Console.WriteLine(s3);

    // Console.Write("s3[1]\t");
    // Console.WriteLine(s3[1]);

    // Console.WriteLine($"Length: {s3.Length}");    

    // var s4 = new string('a', 100);    
    // Console.Write("s4\t");
    // Console.WriteLine(s4);
}

void Part2_Ascii()
{
    // ASCII
    // https://en.wikipedia.org/wiki/ASCII
    // char a = 'a';
    // Console.WriteLine(a);
    // Console.WriteLine((int)a);

    // Console.WriteLine();
    // var capitala = 'A';
    // Console.WriteLine((int)capitala);

    // char b = (char)('B' + 32);
    // Console.WriteLine(b);
    
    // char b2 = (char)('B' + 'a' - 'A');
    // Console.WriteLine(b2);

    void AsciiTable()
    {
        //Console.OutputEncoding = System.Text.Encoding.ASCII;        
        //for (int i = 32; i < 128; i++)
        for (int i = 'A'; i < 'z'; i++)
        {
            Console.Write(" " + (char)i);
            if (i % 16 == 0)
            { // 16*16 = 256
                Console.Write("\n");
            }
        }
    }

    AsciiTable();
}

void Part3_Unicode()
{    // Unicode UTF-16
    // https://home.unicode.org/
    // var upper = "š".ToUpper();
    // Console.WriteLine(upper);    
    Console.OutputEncoding = Encoding.UTF8;
    // Console.WriteLine(upper);

    // In the current implementation at least, strings take up 20+(n/2)*4 bytes (rounding the value of n/2 down)
    Console.WriteLine("\U0001F914");
    Console.WriteLine("🎖️");

    Console.WriteLine("🧑‍🔧");
    Console.WriteLine("!?");
}

void Part4_Comparison()
{
    var ca1 = new char[] { 'a', 'b', 'c', 'd' };
    Console.WriteLine(ca1);        
    var ca2 = new char[] { 'a', 'b', 'c', 'd' };    
    Console.WriteLine("(char[]) ca1 == ca2 {0}", ca1 == ca2);    

    Console.WriteLine("(string) ca1 == ca2 {0}", new string(ca1) == new string(ca2));        

    string string1 = "Test";

    string1 += " String";

    Console.WriteLine(string1);
    // Outputs "Test String"

    string string2 = "Test String";

    // compares values, comparison is true
    Console.WriteLine(
       "string1 == string2 {0} by value", string1 == string2
    );

    // compares references, comparison is false
    Console.WriteLine(
       "string1 == string2 {0} by reference", string.ReferenceEquals(string1, string2)
    );

    Object object3 = "Test String";

    // unintended reference comparasion, comparison is true
    Console.WriteLine(
       "string2 == object3 {0} by reference", string2 == object3
    );

    string string4 = new string(string2);

    // compares values, comparison is true
    Console.WriteLine(
       "string1 == string4 {0} by value", string1 == string4
    );

    // unintended reference comparasion, comparison is false    
    Console.WriteLine(
       "object3 == string4 {0} by reference", object3 == string4
    );

    // var cyrillic = "Ћирилица";
    // var cyrillic1 = cyrillic.ToLower();
    // var cyrillic2 = cyrillic.ToUpper().ToLower();

    // Console.WriteLine("cyrillic1 == cyrillic2 {0}", cyrillic1 == cyrillic2);

    var rho = "ϱ";
    var rho1 = rho.ToLower();
    var rho2 = rho.ToUpper().ToLower();
    Console.WriteLine("rho1 == rho2 {0}", rho1 == rho2);

    var test = string.Equals(rho1, rho2, StringComparison.InvariantCultureIgnoreCase);
    Console.WriteLine("rho1 == rho2 {0}", test);
}

void Part5_Immutability()
{
    // var s1 = "Hello" + ' ' + "World";
    // Console.WriteLine(s1);

    // var s2 = "Hello";

    // // readonly
    // // s2[1] = 'a';

    // var s2copy = s2;
    // Console.WriteLine("s2: {0}, s2copy: {1}", s2, s2copy);
    // Console.WriteLine("s2 == s2copy {0} by reference", string.ReferenceEquals(s2, s2copy));        
    // s2 += " World";
    // Console.WriteLine("s2: {0}, s2copy: {1}", s2, s2copy);
    // Console.WriteLine("s2 == s2copy {0} by reference", string.ReferenceEquals(s2, s2copy));     

    string ConcatenationBadIdea(int count = 10000)
    {
        string s = string.Empty;
        for (int i = 0; i < count; i++)
            s += i.ToString();
        return s;
    }

    string ConcatenationUseStringBuilder(int count = 10000)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < count; i++)
            sb.Append(i);

        return sb.ToString();
    }

    string ConcatenationUseJoin(int count = 10000)
    {
        var numbers = Enumerable.Range(1, count).ToArray();
        return string.Join("", numbers);
    }

    var count = 100000;
    //Benchmark(ConcatenationBadIdea, count);
    Benchmark(ConcatenationUseStringBuilder, count);
    Benchmark(ConcatenationUseJoin, count);
}

void Part6_Methods()
{
    var s = "test";
    var s2 = "test2";
    string.IsNullOrEmpty(s);
    string.IsNullOrWhiteSpace(s);
    var c = string.Compare(s, s2);

    var csv = "value1,value2,value3";
    var csvRow = csv.Split(',');
    Console.WriteLine(csvRow[1]);

    Console.WriteLine("Substring: {0}", csv.Substring(4, 10));

    foreach (var csvc in csv)
    {
        if (csvc > '0' && csvc <= '9')
            Console.Write(csvc);
    }
}

void Part7_Formatting()
{
    decimal pricePerOunce = 17.36m;
    var s = string.Format("The current price is {0:C2} per ounce.", pricePerOunce);
    Console.WriteLine(s);

    string time = string.Format("It is now {0:d} at {0:t}", DateTime.Now);
    Console.WriteLine(time);

    Console.WriteLine();
    int[] years = { 2013, 2014, 2015 };
    int[] population = { 1025632, 1105967, 1148203 };
    var sb = new StringBuilder();
    sb.Append(string.Format("{0,6} {1,15}\n\n", "Year", "Population"));
    for (int index = 0; index < years.Length; index++)
        sb.Append(string.Format("{0,6} {1,15:N0}\n", years[index], population[index]));        

    Console.WriteLine(sb);
}

Part1_StringClass();
//Part2_Ascii();
//Part3_Unicode();
//Part4_Comparison();
//Part5_Immutability();
//Part6_Methods();
//Part7_Formatting();
