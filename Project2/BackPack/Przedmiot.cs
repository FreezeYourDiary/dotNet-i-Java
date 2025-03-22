using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackPack
{
    public class Przedmiot
    {
        public int wartosc { get; private set; }
        public int waga { get; private set; }
        public bool wPlecaku { get; set; } // przy kopiowaniu posortowanych obiektow do listy zmienna bool nie jest potrzebna 

        public Przedmiot(int wartosc, int waga)
        {
            this.waga = waga;
            this.wartosc = wartosc;
            this.wPlecaku = false;
        }

        public override string ToString()
        {
            return $"Waga = {waga}, Wartość = {wartosc}"; // +
                //$"W plecaku = {wPlecaku}";
        }
    }
}
