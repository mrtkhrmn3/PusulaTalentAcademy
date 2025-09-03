using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

namespace PusulaTalentAcademy
{
    public static class MaxIncreasingSubArrayAsJson
    {
        public static string MaxIncreasingSubarrayAsJson(List<int> numbers)
        {
            if (numbers == null || numbers.Count == 0)
            {
                return "[]";
            }

            List<int> best = new List<int>();
            List<int> current = new List<int>() { numbers[0] };

            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i] > numbers[i - 1])
                {
                    current.Add(numbers[i]);
                }
                else
                {
                    if(Sum(current)>Sum(best))
                        best = new List<int>(current);

                    current = new List<int> { numbers[i] };
                }
            }

            if (Sum(current) > Sum(best))
            {
                best = current;
            }
                
            return JsonSerializer.Serialize(best);
        }

        private static int Sum(List<int> list)
        {
            int total = 0;
            foreach (var n in list)
            {
                total += n;
            }
            return total;
        }
    }
}
