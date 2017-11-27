using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApplication3
{
    public static class General
    {
        public static double? ExtractNumeric(string input)
        {
            if (input == null) return null;

            var mod = input.StartsWith("(") && input.EndsWith(")") ? -1 : 1;

            double output;
            var justNumbers = Regex.Replace(input, @"[^0-9.-]", "");

            return (double.TryParse(justNumbers, out output) ? (double?)output : null) * mod;
        }
    }
}
