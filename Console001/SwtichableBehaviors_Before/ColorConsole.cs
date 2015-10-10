using System;

namespace SwtichableBehaviors_Before
{
    public static class ColorConsole
    {
        public static void WriteLineGreen(string message)
        {
            WriteMessage(message, ConsoleColor.Green);
        }

        public static void WriteLineYellow(string message)
        {
            WriteMessage(message, ConsoleColor.Yellow);
        }

        private static void WriteMessage(string message, ConsoleColor color)
        {
            var beforeColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = beforeColor;
        }

        public static void WriteLineRed(string message)
        {
            WriteMessage(message, ConsoleColor.Red);
        }
    }
}