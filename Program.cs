using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Snake_R
{
    class Program
    {

        static void Main(string[] args)
        {
            //Na podstawie zmiennej wywoływane są różne funckje 
            Game game = new Game();
            //Gra trwa dopóki Print_Option nie jest ustawione na 3
            while (game.Print_Option != 3)
            {
                if (game.Print_Option == 0)
                {
                    //Print_Option ustawione na 0 wywołuje funckje Menu
                    game.Menu();
                }
                else if (game.Print_Option == 1)
                {
                    //Print_Option ustawione na 1 wywołuje funckję Start_Game
                    game.Start_Game();
                }
                else if (game.Print_Option == 2)
                {
                    //Print_Option ustawione na 2 wywołuje funckję View_Scores
                    game.View_Scores();
                }
            }
        }
    }
}
