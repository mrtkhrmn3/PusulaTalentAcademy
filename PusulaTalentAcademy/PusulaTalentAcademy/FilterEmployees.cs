using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace PusulaTalentAcademy
{
    public static class FilterEmployees
    {
        //class ismiyle method ismi aynı olduğunda hata verdiği için method ismini değiştirmek zorunda kaldım.
        public static string Filteremployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
        {
            if (employees == null || !employees.Any())
                return JsonSerializer.Serialize(new
                {
                    Names = new List<string>(),
                    TotalSalary = 0m,
                    AverageSalary = 0m,
                    MinSalary = 0m,
                    MaxSalary = 0m,
                    Count = 0
                });

            var filtered = employees
                .Where(e =>
                    e.Age >= 25 && e.Age <= 40 &&
                    (e.Department == "IT" || e.Department == "Finance") &&
                    e.Salary >= 5000m && e.Salary <= 9000m &&
                    //dosya'da işe giriş tarihi 2017’den sonra yazıyor yani 2017'de işe girenleri dönmemeli. Sanırsam Giriş 5 yapıldığında boş çıktı dönmesi gerekiyor Elif'i dönmemeli.
                    e.HireDate.Year > 2017
                )
                .ToList();

            var names = filtered
                .OrderByDescending(e => e.Name.Length)
                .ThenBy(e => e.Name)
                .Select(e => e.Name)
                .ToList();

            decimal totalSalary = filtered.Sum(e => e.Salary);
            decimal averageSalary = filtered.Count > 0 ? Math.Round(filtered.Average(e => e.Salary), 2) : 0m;
            decimal minSalary = filtered.Count > 0 ? filtered.Min(e => e.Salary) : 0m;
            decimal maxSalary = filtered.Count > 0 ? filtered.Max(e => e.Salary) : 0m;
            int count = filtered.Count;

            var result = new
            {
                Names = names,
                TotalSalary = totalSalary,
                AverageSalary = averageSalary,
                MinSalary = minSalary,
                MaxSalary = maxSalary,
                Count = count
            };

            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = false
            };

            return JsonSerializer.Serialize(result, options);
        }
    }
}
