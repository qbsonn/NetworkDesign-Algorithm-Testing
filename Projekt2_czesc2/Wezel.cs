using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt2_czesc2
{
   public  class Wezel
    {
        public int id;
       
      public  bool wybrany;
        int liczba_wezlow;
      public  int[] sasiedzi;

       public Element<double, int,double>[] drogi;
        public Element<double, int, double>[] drogi_pocz;


        public  Wezel (int i, int l)
        {
            id = i;
            sasiedzi = new int[l - 1];
            wybrany = false;
            drogi = new Element<double, int,double>[1];
            drogi_pocz = new Element<double, int, double>[1];
        }


      public  Wezel()
        {



        }
    }
}
