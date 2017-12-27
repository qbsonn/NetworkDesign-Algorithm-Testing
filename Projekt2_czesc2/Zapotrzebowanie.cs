using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt2_czesc2
{
  public  class Zapotrzebowanie
    {
        public int id;
        public double rozmiar;
        public int WezelPoczatek;
        public int WezelKoniec;
        public int[] tablica_uzytych_laczy;
      public List<int> krawedzie;

        public Zapotrzebowanie()
        {
            id = 0;
            rozmiar = 0;
            WezelPoczatek = 0;
            WezelKoniec = 0;
            krawedzie = new List<int>();

        }
        public Zapotrzebowanie(int id1, int wp, int wk, double rozmiar1)
        {
            id = id1;
            rozmiar = rozmiar1;
            WezelPoczatek = wp;
            WezelKoniec = wk;
            krawedzie = new List<int>();
        }

    }
}
