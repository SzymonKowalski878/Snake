using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_R
{
    class Print_Board
    {
        public static void Print_board(List<Player.Cords> snake_cords, string[,] board_to_print, int[,] objective_position, int map_size,int score)
        {
            //inicjalizacja nowego dwu wymiarowego stringa zawierającego wszystkie elementy aktualnej planszy
            string[,] tmp = new string[map_size, map_size];
            Array.Copy(board_to_print, tmp, map_size * map_size);

            //wstawianie + do planszy tam gdzie znajduje się każdy element snakea
            foreach (Player.Cords cord in snake_cords)
            {
                for (int i = 0; i <= cord.x; i++)
                {
                    for (int j = 0; j <= cord.y; j++)
                    {
                        if (cord.x == i && cord.y == j)
                        {
                            tmp[i, j] = "+ ";
                        }
                    }
                }
            }

            // Wstawianie C tam gdzie znajduje się objective
            for (int i = 0; i <= objective_position[0, 0] + 1; i++)
            {

                for (int j = 1; j <= objective_position[0, 1] + 1; j++)
                {
                    if (objective_position[0, 0] == i && objective_position[0, 1] == j)
                    {
                        tmp[i, j] = "C ";
                    }
                }
            }

            //ustawienie cursora(czyli miejsca gdzie zacznie się wypisywanie) na (0,0)
            //zapisywanie całej planszy w postaci stringa i wyświetlenie go
            //każde następne wykonanie się funkcji powoduje nadpisanie planszy
            string to_print = "";
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < map_size; i++)
            {
                for (int j = 0; j < map_size; j++)
                {
                    to_print += tmp[i, j];
                }
                to_print += "\n";
                
            }
            Console.Write(to_print);
            Console.WriteLine(score);
        }
    }
}
