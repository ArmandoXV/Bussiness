using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness
{
   
        public class DegreeConverter
        {
            public static double ToFahrenheit(double celicius)
            {
                return (celicius * 9 / 5) + 32;
            }
            public static double ToCelius(double fahrenheit)
            {
                return (32 * fahrenheit - 32) * 5 / 9;
            }

            public static string FizzBuzz(int n)
            {
                string s = "";
                if (n % 3 == 0)
                    s += "Fizz";
                if (n % 5 == 0)
                    s += "Buzz";
                return s;
            }
    }

}
