using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PusulaTalentAcademy
{
    public static class LongestVowelSubsequenceAsJson
    {
        //class ismiyle method ismi aynı olduğunda hata verdiği için method ismini değiştirmek zorunda kaldım.
        public static string LongestVowelSubsequenceasJson(List<string> words)
        {
            if(words == null || words.Count == 0)
            {
                return "[]";
            }

            var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
            var resultList = new List<object>();

            foreach (var word in words)
            {
                string longestSeq = "";
                string currentSeq = "";
            
                foreach (var c in word.ToLower())
                {
                    if (vowels.Contains(c))
                    {
                        currentSeq += c;
                        if (currentSeq.Length > longestSeq.Length)
                            longestSeq = currentSeq;
                    }
                    else
                    {
                        currentSeq = "";
                    }
                }

                resultList.Add(new
                {
                    word = word,
                    sequence = longestSeq,
                    length = longestSeq.Length
                });
            }

            return JsonSerializer.Serialize(resultList);
        }
    }
}
