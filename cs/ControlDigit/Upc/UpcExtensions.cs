using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;

namespace ControlDigit
{
    public static class UpcExtensions
    {
        public static int CalculateUpc(this long number)
        {
            Int32[] digits = NumberToDigits(number).ToArray();
            return 0;
        }
        public static IEnumerable<Int32> NumberToDigits(Int64 number)
        {
            while (number != 0)
            {
                yield return (Int32)number % 10;
                number /= 10;
            }
        }
    }
}
