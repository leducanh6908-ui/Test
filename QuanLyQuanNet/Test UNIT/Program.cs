using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_UNIT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Chương trình chính đang chạy...");
            var calc = new Calculator();
            Console.WriteLine($"2 + 3 = {calc.Add(2, 3)}");
        }
    }
}
