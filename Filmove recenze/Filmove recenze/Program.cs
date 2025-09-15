using System;
using System.Collections.Generic;

namespace FilmoveRecenze
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Film> listfilmů = new List<Film>();
            listfilmů.Add(new Film("Frozen", "Jennifer", "Lee", 2018));
            listfilmů.Add(new Film("Arrival", "Denis", "Villeneuve", 2016));
            listfilmů.Add(new Film("Koe no Katachi", "Naoko", "Yamada", 2016));
            Random random = new Random();
            for (int a = 0; a < 3; a++)
            {
                for (int i = 0; i < 15; i++)
                {
                    listfilmů[a].PridaniHodnoceni(random.Next(0, 6));
                }
            }
            Film nejlepsiFilm = listfilmů[0];
            Film nejdelsiFilm = listfilmů[0];
            foreach (Film film in listfilmů)
            {
                if (film.Nazev.Length > nejdelsiFilm.Nazev.Length)
                {
                    nejdelsiFilm = film;
                }
                if (film.Hodnoceni > nejlepsiFilm.Hodnoceni)
                {
                    nejlepsiFilm = film;
                }
                if (film.Hodnoceni < 3)
                {
                    Console.WriteLine(film.Nazev + " je ale odpad! Má hodnocení jen " + film.Hodnoceni);
                }
            }
            Console.WriteLine(nejdelsiFilm.Nazev + " je film s nejdelším názvem.");
            Console.WriteLine(nejlepsiFilm.Nazev + " je film s nejlepším hodnocením.");
            foreach (Film film in listfilmů)
            {
                film.ToString();
            }
        }
    }
    class Film
    {
        public string Nazev { get; set; }
        public string Jmenorezisera { get; set; }
        public string PrijmeniRezisera { get; set; }
        public int RokVzniku { get; set; }
        public double Hodnoceni { get; private set; }
        public List<int> VsechnaHodnoceni { get; private set; }
        public Film(string nazev, string jmenorezisera, string prijmenirezisera, int rokvzniku)
        {
            Nazev = nazev;
            Jmenorezisera = jmenorezisera;
            PrijmeniRezisera = prijmenirezisera;
            RokVzniku = rokvzniku;
            VsechnaHodnoceni = new List<int>();
        }
        public void PridaniHodnoceni(int noveHodnoceni)
        {
            VsechnaHodnoceni.Add(noveHodnoceni);
            if (VsechnaHodnoceni.Count == 0)
            {
                Hodnoceni = 0;
                return;
            }
            int avgRating = 0;
            foreach (int rating in VsechnaHodnoceni)
            {
                avgRating += rating;
            }
            Hodnoceni = avgRating / VsechnaHodnoceni.Count;
        }
        public void PrintRating(List<int> VsechnaHodnoceni)
        {
            if (VsechnaHodnoceni.Count == 0)
            {
                Console.WriteLine("Zatím neexistují žádná hodnocení.");
                return;
            }
            Console.Write("Hodnoceni: ");
            Console.Write(VsechnaHodnoceni[0]);
            for (int a = 1; a < VsechnaHodnoceni.Count; a++)
            {
                Console.Write(", " + VsechnaHodnoceni[1]);
            }
            Console.WriteLine();
        }
        public override string ToString()
        {
            string film = Nazev + " (" + RokVzniku + ", " + PrijmeniRezisera + ", " + Jmenorezisera[0] + ".):" + Hodnoceni;
            Console.WriteLine(film);
            return film;
        }
    }
}
