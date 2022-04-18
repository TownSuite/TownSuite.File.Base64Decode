using System.Text;

string inPath = string.Empty;
string outPath = string.Empty;


for (int i = 0; i <= Environment.GetCommandLineArgs().Length - 1; i++)
{
    if (Environment.GetCommandLineArgs()[i] == "-i")
    {
        inPath = Environment.GetCommandLineArgs()[i + 1].Trim();
    }
    else if (Environment.GetCommandLineArgs()[i] == "-o")
    {
        outPath = Environment.GetCommandLineArgs()[i + 1].Trim();
    }
    else if (Environment.GetCommandLineArgs()[i] == "-help" || Environment.GetCommandLineArgs()[i] == "--help")
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("TownSuite.File.Base64Decode -i file/to/decode/values.yaml -o file/to/save/output.yaml");
    }
}

if (String.IsNullOrWhiteSpace(inPath))
{
    Console.WriteLine("An in path must be set.  See --help.");
}

if (String.IsNullOrWhiteSpace(outPath))
{
    Console.WriteLine("An out path must be set.  See --help.");
}

string originalContents = System.IO.File.ReadAllText(inPath);
var convertedContents = new StringBuilder();


using var reader = new StringReader(originalContents);
for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
{

    if (string.IsNullOrWhiteSpace(line))
    {
        convertedContents.Append(line);
        continue;
    }


    
    foreach (string word in line.Split(' '))
    {
        if (string.IsNullOrWhiteSpace(word))
        {
            convertedContents.Append(" ");
            continue;
        }
        try
        {
            string converted = Encoding.UTF8.GetString(Convert.FromBase64String(word));
            convertedContents.AppendLine(converted);
        }
        catch
        {
            convertedContents.Append(word);
        }
    }
}





System.IO.File.WriteAllText(outPath, convertedContents.ToString());