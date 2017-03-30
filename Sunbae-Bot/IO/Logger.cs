using System;
using System.Collections.Generic;
using System.Text;

namespace Sunbae_Bot.IO
{
    class Log
    {
        private enum LogType : byte
        {
            Log,
            Warn,
            Error,
            Debug
        }
        public static void Taexify()
        {
            Console.Title = $"Sunbae (선배) Bot";

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Sunbae Bot {Sunbae.version}");
        }
        public static void Debug(string message, params object[] args)
        {
            WriteLog("Debug", ConsoleColor.White, message, args);
        }
        public static void Inform(string message, params object[] args)
        {
            WriteLog("Info", ConsoleColor.Cyan, message, args);
        }
        public static void Error(string message, params object[] args)
        {
            WriteLog("Error", ConsoleColor.Red, message, args);
        }
        public static void Error(Exception exception)
        {
            Error(true ? exception.ToString() : exception.Message); // TODO: Exception stack trace configuration.
        }
        public static void Success(string value, params object[] args)
        {
            WriteLog("Success", ConsoleColor.Green, value, args);
        }
        public static void Hex(string label, byte[] value, params object[] args)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format(label, args));
            sb.Append('\n');

            if (value == null || value.Length == 0)
            {
                sb.Append("(Empty)");
            }
            else
            {
                int lineSeparation = 0;

                foreach (byte b in value)
                {
                    if (lineSeparation == 16)
                    {
                        sb.Append('\n');
                        lineSeparation = 0;
                    }

                    sb.AppendFormat("{0:X2} ", b);
                    lineSeparation++;
                }
            }
            WriteLog("Hex", ConsoleColor.Magenta, sb.ToString());
        }
        public static void SkipLine()
        {
            Console.WriteLine();
        }
        public static void Hex(string label, byte b, params object[] args)
        {
            Hex(label, new byte[] { b }, args);
        }
        public static void WriteLog(string type, ConsoleColor color, string message, params object[] args)
        {
            lock (typeof(Log))
            {
                string output = $"[{type}] {string.Format(message, args)}";
                SkipLine();
                Console.ForegroundColor = color;
                Console.WriteLine(output);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
