using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackPack
{
    public class Result
    {
        public int[] objects { get; private set; }
        public int sumWaga { get; private set; }
        public int sumWartosc { get; private set; }

        public Result(int[] objects, int sumWaga, int sumWartosc)
        {
            this.objects = objects;
            this.sumWaga = sumWaga;
            this.sumWartosc = sumWartosc;
        }
        // /r windows new line fix GUI
        public override string ToString()
        {
            return $"Wybrane przedmioty: {string.Join(", ", objects)}\r\nSuma wagi: {sumWaga}\r\nSuma wartości: {sumWartosc}";
        }
    }
}
