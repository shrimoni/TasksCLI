using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksCLI.Utils
{
    public class StringOperations
    {
        public static int GetMaximumWordLength(string[] words)
        {
            var maximumLength = words[0].Length;
            foreach(var word in words)
            {
                if (word.Length > maximumLength)
                    maximumLength = word.Length;
            }

            return maximumLength;
        }
    }
}
