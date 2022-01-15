using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common
{
    public static class RandomIdGenerator
    {
        public static string Generate(string prefix= "A-")
        {
            var id = Guid.NewGuid().ToString();
            string first = id.Split('-')[0];
            return $"{prefix}{first}".ToUpperInvariant();
        }
    }
}
