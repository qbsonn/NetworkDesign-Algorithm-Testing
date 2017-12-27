using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt2_czesc2
{
   public  class Element<K, D,E> where K : IComparable
    {
        public K klucz;
        public D dane;
        public E przepustowsci;

        public Element()
        { 
            klucz = default(K);
            dane = default(D);
            przepustowsci = default(E);
        }

        public Element (K k, D d, E p)
        {
            klucz = k;
            dane = d;
            przepustowsci = p;
        }
        public K zwrocKlucz ()
        {
            return klucz;
        }

        public D zwrocDane() { return dane; }
        public void ustawDane(D d_dane) { dane = d_dane; }

    }
}

