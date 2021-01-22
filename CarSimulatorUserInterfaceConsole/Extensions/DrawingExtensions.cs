using CarSimulatorUserInterfaceConsole.Model;
using Pastel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CarSimulatorUserInterfaceConsole.Extensions
{
    internal static class DrawingExtensions
    {
        public static int DrawBasicBoxes(int left, int top, IReadOnlyCollection<BasicBox> basicBoxes,
            Color backgroundColor, Color textColor)
        {
            Console.SetCursorPosition(left, top);
            var startingLeftValue = left;
            var boxWidth = basicBoxes.Max(x => Math.Max(x.Value.Length, x.Title.Length)) + 2;
            var consoleWidth = Console.WindowWidth;
            foreach (var basicBox in basicBoxes)
            {
                var backgroundColorToUse =
                    basicBox.BackgroundColor == Color.Empty ? backgroundColor : basicBox.BackgroundColor;
                var textColorToUse = basicBox.TextColor == Color.Empty ? textColor : basicBox.TextColor;

                if (left + boxWidth > consoleWidth)
                {
                    top += 6;
                    left = startingLeftValue;
                }

                Console.SetCursorPosition(left, top);
                Console.WriteLine(" ".MultiplyString(boxWidth).PastelBg(backgroundColorToUse));
                Console.SetCursorPosition(left, top + 1);
                Console.WriteLine(" ".MultiplyString(boxWidth).PastelBg(backgroundColorToUse));
                Console.SetCursorPosition(left, top + 2);
                Console.WriteLine(" ".MultiplyString(boxWidth).PastelBg(backgroundColorToUse));
                Console.SetCursorPosition(left, top + 3);
                Console.WriteLine(" ".MultiplyString(boxWidth).PastelBg(backgroundColorToUse));

                Console.SetCursorPosition(left, top + 1);
                Console.WriteLine($"{" ".MultiplyString((boxWidth - basicBox.Title.Length) / 2)}{basicBox.Title}"
                    .Pastel(textColorToUse).PastelBg(backgroundColorToUse));
                Console.SetCursorPosition(left, top + 2);
                Console.WriteLine($"{" ".MultiplyString((boxWidth - basicBox.Value.Length) / 2)}{basicBox.Value}"
                    .Pastel(textColorToUse).PastelBg(backgroundColorToUse));

                left += boxWidth + 5;
            }

            return top + 3;
        }
    }
}