using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt2_czesc2
{
    public class Lacze
    {
        public int id;
        public double pojemnosc;
        public int WezelPoczatek;
        public int WezelKoniec;
        public double koszt;
        public int iloscmodulow;
        public double wolna_pojemnosc;
        public Lacze()
        {
            id = 0;
            pojemnosc = 0;
            WezelPoczatek = 0;
            WezelKoniec = 0;
            koszt = 0;
            iloscmodulow = 0;
            wolna_pojemnosc = 0;
        }

        public Lacze(int id1, int wp, int wk, double poj1, double koszt1)
        {
            id = id1;
            pojemnosc = poj1;
            WezelPoczatek = wp;
            WezelKoniec = wk;
            koszt = koszt1;
            iloscmodulow = 0;
            wolna_pojemnosc = pojemnosc;
        }
    }
}
