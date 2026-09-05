## W01 Assignment: Build .NET Applications with C#

- Create a web API with ASP.NET Core controllers
```
GET /pizza      200 OK
GET /pizza/3    404 Not Found
POST /pizza     201 Created
PUT /pizza/3    204 Not Content
GET /pizza/3    200 OK
DELETE /pizza/3 204 Not Content
```
- Work with files and directories in a .NET app
```
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
```