using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var formula = "2 + 1 - 4";
            var expr = ClassLibrary1.MathParser.parse(formula);
        }
    }
}
