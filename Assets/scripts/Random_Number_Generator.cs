using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public struct Random_Number_Generator
    {
        public static int nro_A = -2;
        public static int nro_B = -2;

        public static void Roll_A(int min,int max,int[] ignore)
        {
            nro_A = Roll_Number(min, max, ignore);
        }

        public static void Roll_B(int min, int max, int[] ignore)
        {
            nro_B = Roll_Number(min, max, ignore);
        }

        private static int Roll_Number(int min, int max, int[] ignore)
        {
            Random random = new Random();
            int number = random.Next(min, max);
            if (!ignore.Contains(number))
                return number;
            else
               return Roll_Number(min, max, ignore);
            
        }
    }
}
