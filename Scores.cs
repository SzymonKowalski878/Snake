using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Snake_R
{
    class Scores
    {

        private string[,] scores = new string[5, 3];
        private string[,] write = new string[255, 3];
        //funkcja służąca do sprawdzenia wyników dla konkretnego poziomu
        public void Read_Scores_From_File()
        {
            //sprawdzam czy istnieje folder MySnake na dysku c
            if (!Directory.Exists(@"C:\MySnake"))
            {
                Directory.CreateDirectory(@"C:\MySnake");
            }
            //sprawdzam czy w folderze MySnake (który zostanie stworzony jeśli go nie ma) znajduje się plik tekstowy SnakeScores
            //jeśli nie znajduje się tam to tworzę go i zapisuję w nim domyślne dane
            if (!File.Exists(@"C:\MySnake\SnakeScores.txt"))
            {
                File.CreateText(@"C:\MySnake\SnakeScores.txt").Close();
                
                StreamWriter write_to_file = new StreamWriter(@"C:\MySnake\SnakeScores.txt", false);
                
                for (int i = 10; i <= 60; i++)
                {
                    for(int j = 0; j <= 4; j++)
                    {
                        write_to_file.Write(i+";Brak nazwy;"+"0");
                        write_to_file.WriteLine();
                        write_to_file.Flush();
                    }
                }
                write_to_file.Close();
            }
            
            Console.Clear();
            //wybór poziomu dla którego chcę sprawdzić wyniki
            Console.WriteLine("Dla jakiego poziomu chcesz sprawdzić wyniki?");
            string map = Console.ReadLine();
            Console.Clear();
            //konwersja stringa na int
            int map_size = Int32.Parse(map);
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\MySnake\SnakeScores.txt");
            //pierwsza pętla odpowiada za poziom, każdy poziom ma 5 miejsc na statystki
            for (int i = 10; i <= map_size; i++)
            {
                //pętla odpowiadająca za statystyki na konkretnym poziomie
                for (int j = 0; j <= 4; j++)
                {
                    //readline powoduje ze indeks lini caly czas przeskakuje, w ten sposob dostaje sie do dalszych lini
                    string line = file.ReadLine();
                    //gdy pętla dotrze do odpowiedniego poziomu to wtedy kazda linia jest dzielona na podstawie ;
                    if (i == map_size)
                    {
                        String[] splited = line.Split(';');
                        scores[j, 0] = splited[0];
                        scores[j, 1] = splited[1];
                        scores[j, 2] = splited[2];
                    }
                }
            }
            //zamykam plik aby przy ponownej próbie nie doszło do błedu z procesami
            file.Close();
            //wyświetlam to co wcześniej dzieliłem 
            for (int i = 0; i <= 4; i++)
            {
                string to_print = "";
                for (int j = 0; j <= 2; j++)
                {
                    to_print += scores[i, j] + " ";
                }
                Console.WriteLine(to_print);
            }
        }

        //funkcja pobiera z pliku wyniki porównuje je z wynikiem pod koniec gry
        //jeśli wynik z gry przewyższa któryś z wyników z pliku to gracz może ale nie musi
        //zapisać wynik, potem wyniki konkretnego poziomu są wyświetlone
        public void Compare_Score(int map_size, int score)
        {
            //otwieram plik ze statystykami i umieszczam je w zmiennej write
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\MySnake\SnakeScores.txt");
            for (int i = 0; i <= 254; i++)
            {

                string line = file.ReadLine();
                String[] splited = line.Split(';');
                write[i, 0] = splited[0];
                write[i, 1] = splited[1];
                write[i, 2] = splited[2];
            }
            //zamykam plik aby nie doszło do błędu z procesami
            file.Close();


            int poziom = 10;//no poziom no
            int x = 0;//zmienna która będzie wskazywała indeks pod którym będą zapisywane wyniki
            //praktyczie tak samo jak w liście z kordami, wyniki "przesuwają się"
            string tmp;//zmienna wynik z pliku za który na samym początku będzie podstawiony wynik z gry
            string tmpn;//to co u góry tylko że z nazwą którą poda gracz
            string tmp2;//zmienna która przechowywać będzie wynik który zostanie zasłonięty w następnej iteracji
            string tmp2n;//to samo tylko że z nazwą
            //pętla aby dotrzeć do poziomu z którego chcemy zobaczyć wyniki
            while (poziom <= map_size)
            {
                
                if (poziom == map_size)
                {

                    for (int i = 0; i <= 4; i++, x++)
                    {
                        //wynik z gry porównuję z wynikami z pliku tak długo aż wynik z gry bedzie większy
                        //od wyniku z pliku lub aż skończy się pętla
                        int wynik = int.Parse(write[x, 2]);
                        if (wynik < score)
                        {
                            //jeśli wynik z gry jest większy od wyniku z pliku to gracz może zapisać wynik
                            Console.WriteLine("\nGratulacje twój wynik przewyższa mieści się w tabeli wyników czy chcesz go zapisać? \nWciśnij 1 jeśli tak lub coś innego jeśli nie");
                            ConsoleKeyInfo cki = Console.ReadKey();
                            Console.WriteLine();
                            if (char.IsDigit(cki.KeyChar) && int.Parse(cki.KeyChar.ToString()) == 1)
                            {
                                Console.WriteLine("Podaj nazwę pod którą zapisać wynik");
                                string name_of_player = Console.ReadLine();
                                
                                tmp = write[x, 2];//zapisuję pierwszy wynik z pliku do zmiennej tymczasowej
                                //bo zaraz będzie zasłonięty
                                tmpn = write[x, 1];//to samo ale z nazwą
                                write[x, 2] = score.ToString();//podstawienie wyniku
                                write[x, 1] = name_of_player;//podstawienie nazwy
                                i++;//cpt obvious xd
                                x++;
                                while (i <= 4)
                                {
                                    tmp2 = write[x, 2];//do zmiennej tymczasowej zapisuję wynik z pliku który będzie zasłonięty
                                    //w następnej iteracji
                                    tmp2n = write[x, 1];//to samo ale z nazwą
                                    write[x, 2] = tmp;//podstawiam wynik z poprzedniej iteracji
                                    write[x, 1] = tmpn;//to samo ale z nazwą
                                    tmp = tmp2;
                                    tmpn = tmp2n;
                                    x++;
                                    i++;
                                }
                            }
                            //jesli gracz nie chce zapisywac wyniku to nie ma sensu dalej porównywać następnych wyników 
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                //tak długo jak poziom nie jest równy rozmiarowi mapy
                //zwiększam x o 5 bo każdy poziom ma 5 wyników
                else
                {
                    x += 5;
                }
                //no i zwiększam poziom
                poziom++;

            }

            //tworzę streamwriter i usuwam aktualną zawartość pliku tekstowego
            StreamWriter write_to_file = new StreamWriter(@"C:\MySnake\SnakeScores.txt", false);
            for (int i = 0; i <= 254; i++)
            {
                write_to_file.Write(write[i, 0] + ";" + write[i, 1] + ";" + write[i, 2]);
                write_to_file.WriteLine();
                write_to_file.Flush();
            }
            Console.Clear();
            poziom = 10;
            //ustawiam poziom na 10 bo od czegoś trzeba zacząć
            x = 0;//to samo
            while (poziom <= map_size)
            {
                if (poziom == map_size)
                {
                    for (int i = 0; i <= 4; i++, x++)
                    {
                        //wyświetlam statystyki dla konkretnego poziomu
                        Console.WriteLine(write[x, 0] + ":::" + write[x, 1] + ":::" + write[x, 2]);
                    }
                }
                else
                {
                    x += 5;
                }
                poziom++;
            }
            //zamykam plik aby przy następnym wykonaniu się funkcji nie doszło do błędu z procesem
            write_to_file.Close();

        }

    }
}
