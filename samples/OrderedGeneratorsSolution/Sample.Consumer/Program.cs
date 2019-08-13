// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;

namespace Sample.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var f = new Foot();

            f.Test2();

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}