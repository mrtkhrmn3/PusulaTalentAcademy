using System.Linq;
using System.Text.Json;
using System.Collections.Generic;
using System.Xml.Linq;

namespace PusulaTalentAcademy
{
    public static class FilterPeopleFromXml
    {

        //class ismiyle method ismi aynı olduğunda hata verdiği için method ismini değiştirmek zorunda kaldım.
        public static string FilterPeopleFromxml(string xmlData)
        {
            if (string.IsNullOrWhiteSpace(xmlData))
            {
                return JsonSerializer.Serialize(new
                {
                    Names = new List<string>(),
                    TotalSalary = 0,
                    AverageSalary = 0,
                    MaxSalary = 0,
                    Count = 0
                });
            }

            var doc = XDocument.Parse(xmlData);

            var filtered = doc.Descendants("Person")
                .Where(p =>
                {
                    int age = int.Parse(p.Element("Age")?.Value ?? "0");
                    string dept = p.Element("Department")?.Value ?? "";
                    decimal salary = decimal.Parse(p.Element("Salary")?.Value ?? "0");
                    DateTime hireDate = DateTime.Parse(p.Element("HireDate")?.Value ?? DateTime.MinValue.ToString());

                    return age > 30 && dept == "IT" && salary > 5000 && hireDate.Year < 2019;
                })
                .Select(p => new
                {
                    Name = p.Element("Name")?.Value ?? "",
                    Salary = decimal.Parse(p.Element("Salary")?.Value ?? "0")
                })
                .ToList();

            var names = filtered.Select(f => f.Name).OrderBy(n => n).ToList();
            decimal totalSalary = filtered.Sum(f => f.Salary);
            decimal averageSalary = filtered.Count > 0 ? filtered.Average(f => f.Salary) : 0;
            decimal maxSalary = filtered.Count > 0 ? filtered.Max(f => f.Salary) : 0;
            int count = filtered.Count;

            var result = new
            {
                Names = names,
                TotalSalary = totalSalary,
                AverageSalary = averageSalary,
                MaxSalary = maxSalary,
                Count = count
            };

            return JsonSerializer.Serialize(result);
        }
    }
}
