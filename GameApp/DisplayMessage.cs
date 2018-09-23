using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp
{
    static public class DisplayMessage
    {
        static public void DisplayTortleIsDead()
        {
            Console.Write("The tortle is dead, he can not move. Use a LetsGetCheck potion to resurrect.");
        }


        static public void DisplayForward()
        {
            Console.Write("Tortle goes forward.");
        }


        static public void DisplayHitMine()
        {
            Console.Write("The Tortle hits a mine, but it is not harmful according the example.");
        }


        static public void DisplayFallDown()
        {
            Console.Write("The tortle fall down in a limitless pit... the tortle lose all its health points.");
        }


        static public void DisplayExit()
        {
            Console.Write("The tortle found the exit! Congratulation!!");
        }


        static public void DisplayTortleFacing(string direction)
        {
            Console.Write("The tortle is now facing " + direction);
        }
    }
}
