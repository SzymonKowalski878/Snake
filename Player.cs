using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_R
{
    class Player
    {
        public class Cords
        {
            public int x;
            public int y;
            public Cords(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        private List<Cords> snake_cords = new List<Cords>();

        //dodawanie pierwszego elementu do listy cords, znajduje się w środku mapy
        public Player(int map_size)
        {
            map_size = map_size / 2;
            this.snake_cords.Add(new Cords(map_size, map_size));
        }

        public List<Cords> Snake_Cords
        {
            get
            {
                return this.snake_cords;
            }
        }

        private char direction = 'w';

        public char Direction
        {
            get
            {
                return this.direction;
            }
        }

        private bool tick_check = true;

        //zmienna sprawdzająca czy w jednym wykonaniu się pętli doszło do zmiany kierunku ruchu
        public bool Tick_Check
        {
            get
            {
                return tick_check;
            }

            set
            {
                if (value == true)
                {
                    this.tick_check = value;
                }
                else if (value == false)
                {
                    this.tick_check = value;
                }
            }
        }

        //funkcja zmieniająca kierunek poruszania się snakea
        public void Change_Player_Direction(char new_direction)
        {
            if (new_direction == 'w' && this.direction != 's' && this.direction != 'w')
            {
                this.direction = 'w';
                this.tick_check = true;
            }
            else if (new_direction == 's' && this.direction != 'w' && this.direction != 's')
            {
                this.direction = 's';
                this.tick_check = true;
            }
            else if (new_direction == 'a' && this.direction != 'd' && this.direction != 'a')
            {
                this.direction = 'a';
                this.tick_check = true;
            }
            else if (new_direction == 'd' && this.direction != 'a' && this.direction != 'd')
            {
                this.direction = 'd';
                this.tick_check = true;
            }
        }


        private bool have_i_eaten = false;
        //zmienna sprawdzająca czy w danym wykonaniu się petli doszło do "zjedznia" objectiva
        public bool Have_I_Eaten
        {
            get
            {
                return have_i_eaten;
            }
            set
            {
                if (value == true)
                {
                    this.have_i_eaten = true;
                }
                if (value == false)
                {
                    this.have_i_eaten = false;
                }
            }
        }

        //funckja zmieniająca pozycję wszystkich elementów listy z koordynatami snakea
        public void Change_Position()
        {
            //tworzę zmienną tymczasową przechowującą pierwsze koordynaty aby potem w pętli podstawić, w pętli będzie to
            //kord o indeksie i-1
            Cords tmp_cord1 = new Cords(this.snake_cords[0].x, this.snake_cords[0].y);
            //tworzę zmienną tymczasową przechowującą ostatni koordynat przed zmianą, jeśli doszło do zjedzenia
            //objectiva to te kordy zostaną dodane na koniec listy
            Cords last_cord_before_change = new Cords(this.snake_cords[this.snake_cords.Count() - 1].x, this.snake_cords[this.snake_cords.Count() - 1].y);
            for (int i = 1; i < this.snake_cords.Count(); i++)
            {
                //druga zmienna tymczasowa przechowująca koordy o itym indeksie
                Cords tmp_cord2 = new Cords(this.snake_cords[i].x, this.snake_cords[i].y);
                //podstawianie i-1 tych koordynatów do i-tych koordynatów
                this.snake_cords[i].x = tmp_cord1.x;
                this.snake_cords[i].y = tmp_cord1.y;
                //podstawienie i-tych koordynatów do pierwszej zmiennej tymczasowej która w następnej iteracji
                //będzie i-1 tą iteracją
                tmp_cord1.x = tmp_cord2.x;
                tmp_cord1.y = tmp_cord2.y;
            }

            //zmiana koordynatu pierwszego elementu na podstawie aktualnego kierunku poruszania
            if (this.direction == 'w')
            {
                this.snake_cords[0].x--;
            }
            else if (this.direction == 's')
            {
                this.snake_cords[0].x++;
            }
            else if (this.direction == 'a')
            {
                this.snake_cords[0].y--;
            }
            else if (this.direction == 'd')
            {
                this.snake_cords[0].y++;
            }

            //jeśli doszło do zjedzenia to dodaję ostatni koordynat sprzed zmiany na koniec aktualnej listy
            if (this.have_i_eaten)
            {
                this.snake_cords.Add(new Cords(last_cord_before_change.x, last_cord_before_change.y));
            }
        }

        //funkcja czycząca buffer z wciśniętymi klawiszami
        //wykonuje się przy wywołaniu funkcja zmieniającej kierunek w celu zapobiegnięcia kolejkowaniu się ruchów
        public void FlushKeyboard()
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
            }
        }
    }
}
