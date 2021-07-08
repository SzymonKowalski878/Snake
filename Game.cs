using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Snake_R
{
    class Game
    {
        private int difficulty;

        public int Difficulty
        {
            get
            {
                if (difficulty == 1)
                {
                    return 1000;
                }
                else if (difficulty == 2)
                {
                    return 700;
                }
                else if (difficulty == 3)
                {
                    return 500;
                }
                else
                {
                    return 300;
                }
            }

            set
            {
                this.difficulty = value;
            }
        }


        private int map_size;

        private int print_option = 0;

        public int Print_Option
        {
            get
            {
                return print_option;
            }
        }


        public void Set_Parameters()
        {
            Console.WriteLine("Podaj liczbę z przedziału od 1 do 4 aby wybrać prędkość");
            ConsoleKeyInfo cki = Console.ReadKey(true);
            char dif = cki.KeyChar;
            while (char.IsDigit(dif) == false || int.Parse(dif.ToString()) == 0 || int.Parse(dif.ToString()) >= 5)
            {
                Console.WriteLine("Podaj liczbę z przedziału od 1 do 4 aby wybrać prędkość");
                cki = Console.ReadKey(true);
                dif = cki.KeyChar;
            }
            Console.WriteLine(dif);
            Console.WriteLine("Wybierz rozmiar mapy od 10 do 60");
            string map = Console.ReadLine();

            while (Decimal.TryParse(map, out decimal result) == false || Decimal.TryParse(map, out decimal result2) == true && result2 <= 9 || Decimal.TryParse(map, out decimal result3) == true && result3 >= 61)
            {
                Console.WriteLine("Wybierz rozmiar mapy od 10 do 100");
                map = Console.ReadLine();
            }
            Console.WriteLine();
            this.difficulty = dif - '0';
            this.map_size = Int32.Parse(map);
            Console.Clear();
        }

        Scores scores = new Scores();

        public void Menu()
        {
            Console.Clear();
            Console.WriteLine("Wybierz co chcesz zrobić");
            Console.WriteLine("Start gry? Naciśnij 1");
            Console.WriteLine("Statystyki? Naciśnij 2");
            Console.WriteLine("Zamknij grę? Naciśnij 3");
            ConsoleKeyInfo cki = Console.ReadKey(true);
            char halo = cki.KeyChar;
            while (halo != '1' && halo != '2' && halo != '3')
            {
                Console.WriteLine("Wybrano niepoprawny klawisz, wybierz ponownie");
                cki = Console.ReadKey(true);
                halo = cki.KeyChar;
            }
            if (halo == '1')
            {
                this.print_option = 1;
            }
            else if (halo == '2')
            {
                this.print_option = 2;
            }
            else if (halo == '3')
            {
                this.print_option = 3;
            }
            Console.Clear();
        }
        //funckja pokazująca wyniki
        public void View_Scores()
        {
            
            scores.Read_Scores_From_File();
            Console.WriteLine("Wybierz co chcesz zrobić");
            Console.WriteLine("Start gry? Naciśnij 1");
            Console.WriteLine("Statystki innego poziomu? Naciśnij 2");
            Console.WriteLine("Zamknij grę? Naciśnij 3");
            ConsoleKeyInfo cki = Console.ReadKey(true);
            Console.Clear();
            char halo = cki.KeyChar;
            while (halo != '1' && halo != '2' && halo != '3')
            {
                Console.WriteLine("Wybrano niepoprawny klawisz, wybierz ponownie");
                cki = Console.ReadKey(true);
                halo = cki.KeyChar;
            }
            //ustawiam opcje do wyświetlania
            if (halo == '1')
            {
                this.print_option = 1;
            }
            else if (halo == '2')
            {
                this.print_option = 2;
            }
            else if (halo == '3')
            {
                this.print_option = 3;
            }
        }

        //funkcja odpowiadająca za grę
        public void Start_Game()
        {
            Console.Clear();
            //funkcja ustawiająca parametry rozmiar mapy i prędkość gry tj difficulty
            Set_Parameters();
            Player player = new Player(this.map_size);
            Board board = new Board();
            board.Generate_Board(this.map_size);
            int score = 0;
            ConsoleKeyInfo cki;
            char halo;
            Print_Board.Print_board(player.Snake_Cords, board._Board, board.Objective_Position, this.map_size,score);

            //gra trwa tak długo aż wystąpi kolizja 
            while (Board.Collision_Detection(player.Snake_Cords, this.map_size) == false)
            {
                player.Tick_Check = false;
                while (Console.KeyAvailable == true && player.Tick_Check == false)
                {
                    cki = Console.ReadKey(true);
                    halo = cki.KeyChar;
                    player.Change_Player_Direction(halo);
                }
                player.Change_Position();
                player.Have_I_Eaten = false;
                //sprawdzam czy doszło do zjedzenia, jeśli tak to na podstawie trudności zwiększam wynik
                if (board.Does_It_Eat(player.Snake_Cords[0]))
                {
                    board.Generate_Objective_Position(player.Snake_Cords, this.map_size);
                    player.Have_I_Eaten = true;
                    if (difficulty == 1)
                    {
                        score += 100;
                    }
                    else if (difficulty == 2)
                    {
                        score += 150;
                    }
                    else if (difficulty == 3)
                    {
                        score += 200;
                    }
                    else if (difficulty == 4)
                    {
                        score += 250;
                    }
                }
                //wyświetlam planszę i czyszczę buffor
                Print_Board.Print_board(player.Snake_Cords, board._Board, board.Objective_Position, this.map_size,score);
                player.FlushKeyboard();
                Thread.Sleep(this.Difficulty);//to odpowiada za długość trwania jednego wykonania sie pętli
            }
            scores.Compare_Score(map_size, score);//porównuję wyniki
            Console.WriteLine("Koniec gry, co chcesz teraz zrobić?");
            Console.WriteLine("Restart gry? Naciśnij 1");
            Console.WriteLine("Przejść do menu? Naciśnij 2");
            Console.WriteLine("Zobaczyć wyniki? Naciśnij 3");
            Console.WriteLine("Koniec gry? Naciśnij 4");
            cki = Console.ReadKey(true);
            halo = cki.KeyChar;
            while (halo != '1' && halo != '2' && halo != '3' && halo != '4')
            {
                Console.WriteLine("Wybrano niepoprawny klawisz, wybierz ponownie");
                cki = Console.ReadKey(true);
                halo = cki.KeyChar;
            }
            //ustawiam opcję wyświetlania
            if (halo == '1')
            {
                this.print_option = 1;
            }
            else if (halo == '2')
            {
                this.print_option = 0;
            }
            else if (halo == '3')
            {
                this.print_option = 2;
            }
            else if (halo == '4')
            {
                this.print_option = 3;
            }
        }
    }
}
