using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Internal.Execution;

namespace ControlDigit
{
    public static class SnilsExtensions
    {
        public static Int32 CalculateSnils(this Int64 number)
        {
            Int32[] digits = NumberToDigitsFromLeft(number).ToArray();
            Int32 sum = 0;
            for (Int32 i = digits.Length - 1; i > -1; i--)
            {
                sum += digits[i] * (i + 1);
            }

            return GetControlDigitFromControlSum(sum);

        }

        private static Int32 GetControlDigitFromControlSum(Int32 sum)
        {
            if (sum < 100)
                return sum;

            if (sum == 100 || sum == 101)
                return 0;

            return GetControlDigitFromControlSum(sum % 101);
        }
        public static IEnumerable<Int32> NumberToDigitsFromLeft(Int64 number)
        {
            while (number != 0)
            {
                yield return (Int32)number % 10;
                number /= 10;
            }
        }
    }
}
