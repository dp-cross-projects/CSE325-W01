
using System.Text;
using Newtonsoft.Json;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");

var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

var salesFiles = FindFiles(storesDirectory);

var salesTotal = CalculateSalesTotal(salesFiles);

var reportSummary = CreateReport(salesFiles, salesTotal);

File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

File.WriteAllText(Path.Combine(salesTotalDir, "report-summary.txt"), reportSummary);

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        var extension = Path.GetExtension(file);
        // The file name will contain the full path, so only check the end of it
        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {      
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);

        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        // Add the amount found in the Total field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

string CreateReport(IEnumerable<string> salesFiles, double salesTotal)
{
    StringBuilder report = new StringBuilder ();
    report.Append("Sales Summary");
    report.Append("\n");
    report.Append("----------------------------");
    report.Append("\n");
    report.Append("Total Sales: ");
    report.Append(salesTotal.ToString("C"));
    report.Append("\n");
    report.Append("Details:");
    report.Append("\n");

    foreach (var file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
        string ammount = data?.Total.ToString("C") ?? "" ;

        report.Append($"{file}: {ammount}");
        report.Append("\n");
    }

    return report.ToString();
}
record SalesData (double Total);