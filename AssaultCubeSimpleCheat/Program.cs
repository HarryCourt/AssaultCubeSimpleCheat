using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Memory;

namespace AssaultCubeSimpleCheat
{
    public partial class Program
    {
        static Mem mem = new Mem();

        public static string rifleAmmo = "ac_client.exe+0x00109B74,150";
        public static string pistolAmmo = "ac_client.exe+0x00109B74,13C";
        public static string grenadeAmount = "ac_client.exe+0x00109B74,158";

        public static string localArmor = "ac_client.exe+0x00109B74,FC";
        public static string localHealth = "ac_client.exe+0x00109B74,F8";

        public static int processId;

        static void Main(string[] args)
        {
            Console.WriteLine("Harry's Dumb and Basic Cheat for Assault Cube");

            processId = mem.GetProcIdFromName("ac_client");

            // If we found the processer, run the code. 
            if (processId > 0)
            {
                mem.OpenProcess(processId);

                // Threads
                Thread writeAmmo = new Thread(InfiniteAmmo) { IsBackground = true };
                Thread writeHealth = new Thread(GodMode) { IsBackground = true };

                // Start threads
                writeAmmo.Start();
                writeHealth.Start();

                Console.WriteLine("Loading...");
                Console.ReadKey();
            }
            // We failed to find AssaultCube
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("AssaultCube was not found.");
                Console.ReadKey();
            }
        }

        // Changing Ammo
        private static void InfiniteAmmo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Infinite Ammo: Enabled");
            Console.ResetColor();

            while (true)
            {
                mem.WriteMemory(rifleAmmo, "int", "144");       // Rifle Ammo Value
                mem.WriteMemory(pistolAmmo, "int", "144");      // Pistol Ammo Value
                mem.WriteMemory(grenadeAmount, "int", "3");     // Grenade Amount
                Thread.Sleep(5);
            }
        }

        private static void GodMode()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("God Mode: Enabled");
            Console.ResetColor();

            while (true)
            {
                mem.WriteMemory(localHealth, "int", "9999");    // Health Value
                mem.WriteMemory(localArmor, "int", "1337");     // Armor Value
                Thread.Sleep(5);
            }
        }
    }
}
