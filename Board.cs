using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_R
{
    class Board
    {
        private string[,] board;
        //generowanie planszy na podstawie pobraniego rozmiaru mapy
        //można by trochę zmienić i wstawić to do klasu Print_Board
        public void Generate_Board(int map_size)
        {
            this.board = new string[map_size, map_size];

            for (int i = 0; i < map_size; i++)
            {
                for (int j = 0; j < map_size; j++)
                {
                    //wstawianie X na obramówce i pustych miejsc w środku 
                    if (i == 0 || i == map_size - 1 || j == 0 || j == map_size - 1)
                    {
                        board[i, j] = "X ";
                    }
                    else
                    {
                        board[i, j] = "  ";
                    }
                }
            }


        }

        public string[,] _Board
        {
            get
            {
                return this.board;
            }
        }

        //objectibe
        private int[,] objective_position = new int[,] { { 2, 3 } };

        public int[,] Objective_Position
        {
            get
            {
                return this.objective_position;
            }
            set
            {
                this.objective_position = value;
            }
        }

        //sprawdzanie kordow czy sie pokrywają
        public static bool Check_If_Correct_Objective(int x, int y, List<Player.Cords> snake_cords)
        {
            foreach (Player.Cords cord in snake_cords)
            {
                if (cord.x == x && cord.y == y)
                {
                    return true;
                }
            }
            return false;
        }

        //generowanie objectiva 
        public void Generate_Objective_Position(List<Player.Cords> snake_cords, int map_size)
        {
            Random random = new Random();
            int cord_x = random.Next(1, map_size - 1);
            int cord_y = random.Next(1, map_size - 1);
            //tak długo jak objective nie jest właściwy to pętla wykonuje się
            //niewłaściwy tzn że objective znajduje się na kordach jednego z elementow snakea 
            while (Check_If_Correct_Objective(cord_x, cord_y, snake_cords))
            {
                cord_x = random.Next(1, map_size - 1);
                cord_y = random.Next(1, map_size - 1);
            }
            int[,] new_objective_position = { { cord_x, cord_y } };
            Objective_Position = new_objective_position;
        }

        //sprawdzanie kolizji snakea z obramówką oraz głowy snakea z ogonem snakea
        public static bool Collision_Detection(List<Player.Cords> snake_cords, int map_size)
        {

            if (snake_cords[0].x <= 0 || snake_cords[0].x >= map_size - 1 || snake_cords[0].y <= 0 || snake_cords[0].y >= map_size - 1)
            {
                return true;
            }

            for (int i = 1; i < snake_cords.Count(); i++)
            {
                if (snake_cords[i].x == snake_cords[0].x && snake_cords[i].y == snake_cords[0].y)
                {
                    return true;
                }
            }
            return false;
        }

        //na podstawie pierwszego koordynatu następuje sprawdzenie czy snake zjadł objective
        public bool Does_It_Eat(Player.Cords head_cord)
        {
            if (head_cord.x == this.objective_position[0, 0] && head_cord.y == this.objective_position[0, 1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
