using System;
using System.Linq;

namespace CarSimulatorUserInterfaceConsole.Extensions
{
    internal static class StringExtensions
    {
        public static string MultiplyString(this string text, int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Value can not be lower than 0");
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Text can not be null or empty");
            }

            return string.Concat(Enumerable.Repeat(text, value));
        }
    }
}