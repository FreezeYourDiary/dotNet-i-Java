using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackPack
{
    public class Problem
    {
        public Przedmiot[] przedmioty;
        // int iloscPrzedmiotow;
        // generator przedmiotow z parametrami
        public Problem(int n, int seed)
        {
            // this.iloscPrzedmiotow = n;
            Random rnd = new Random(seed);
            przedmioty = new Przedmiot[n];

            for (int i = 0; i < n; i++)
            {
                int wartosc = rnd.Next(1, 10);
                int waga = rnd.Next(1, 10);
                przedmioty[i] = new Przedmiot(wartosc, waga);
                Console.WriteLine(przedmioty[i]);
            }
            //SortujPoStosunku();
        }

        public void SortujPoStosunku()
        {
            przedmioty = przedmioty.OrderByDescending(p => (double)p.wartosc / p.waga).ToArray();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < przedmioty.Length; i++)
            {
                var przedmiot = przedmioty[i];
                sb.AppendLine($"N: {i}, {przedmiot.ToString()}");
                // sb.AppendLine($"Stosunek: {(double)przedmiot.wartosc / przedmiot.waga}");
            }
            return sb.ToString();
        }


        public Result Solve(int pojemnosc)
        {
            SortujPoStosunku();
            int[] objects = new int[przedmioty.Length];
            int count = 0; // # objektow dodane do plecaka
            int totalValue = 0;
            int totalWeight = 0;

            for (int i = 0; i < przedmioty.Length; i++)
            {
                if (totalWeight + przedmioty[i].waga <= pojemnosc)
                {
                    totalWeight += przedmioty[i].waga;
                    totalValue += przedmioty[i].wartosc;
                    objects[count++] = i;
                    przedmioty[i].wPlecaku = true;
                }
            }
            Array.Resize(ref objects, count);
            return new Result(objects, totalWeight, totalValue);
        }
    }
}
