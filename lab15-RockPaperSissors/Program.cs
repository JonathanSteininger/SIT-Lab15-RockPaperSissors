using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockPaperScissorsLib;
using Newtonsoft.Json;

namespace lab15_RockPaperSissors
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Name");
            string name = Console.ReadLine();
            Application app = new Application("127.0.0.1", 2048, name);
        }
    }
}
