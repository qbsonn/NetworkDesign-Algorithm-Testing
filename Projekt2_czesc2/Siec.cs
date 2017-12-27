using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt2_czesc2
{
  public  class Siec
    {
        public
        Lacze[] lacza;
       public  Wezel[] wezly;

        public double[] tablica_kosztow;

       
        public int x;
      public  int liczba_wezlow;
      public  int liczba_laczy;
        public double[,] tablica_drog_Floyd;
        public int[,] tablica_drog_Floyd_nast;
        public double[,] tablica_drog2;
        public int[,] tablica_laczy;
        public double[,] tablica_drog;
        public int[,] tablica_uzyc;
        public Siec(Lacze[] l, int ww, int ll, Wezel[] w)
        {
            //tablica_drog = new double[liczba_wezlow + 1, liczba_wezlow + 1];


       
            liczba_wezlow = ww;
            liczba_laczy = ll;
            tablica_laczy = new int[liczba_wezlow + 1, liczba_wezlow + 1];
            x = 5;
            wezly = w;
            lacza = l;
            tablica_kosztow = new double[liczba_laczy];
            //  Console.Write( liczba_laczy  + "\n");
            int m = 0;
            for (int i=0; i<liczba_laczy;i++)
            {
                Element<double, int,double> n = new Element<double, int,double>();
                // Console.Write("p"+lacza[i].WezelPoczatek+"\n");
                //  Console.Write("k"+lacza[i].WezelKoniec+ "\n");
                n.klucz = 0;


                tablica_laczy[lacza[i].WezelPoczatek, lacza[i].WezelKoniec] = i ;
                n.przepustowsci = 0;
                n.dane = lacza[i].WezelKoniec;
                wezly[lacza[i].WezelPoczatek].drogi[wezly[lacza[i].WezelPoczatek].drogi.Length-1] =n;
             
                Array.Resize<Element<double, int,double>>(ref wezly[lacza[i].WezelPoczatek].drogi, wezly[lacza[i].WezelPoczatek].drogi.Length + 1);
             //   Array.Resize<Element<double, int, double>>(ref wezly[lacza[i].WezelPoczatek].drogi_pocz, wezly[lacza[i].WezelPoczatek].drogi_pocz.Length + 1);
                //   Console.Write(wezly[lacza[i].WezelPoczatek].drogi.Length + "\n");

                tablica_kosztow[i] = lacza[i].koszt;

                tablica_uzyc = new int[liczba_wezlow + 1, liczba_wezlow + 1];

            }
            tablica_drog = new double[liczba_wezlow + 1, liczba_wezlow + 1];

            for (int i = 0; i <= liczba_wezlow; i++)
            {
                for (int j = 0; j <= liczba_wezlow; j++)
                { tablica_drog[i, j] = Double.PositiveInfinity; }
            }



            for (int i = 0; i <= liczba_wezlow; i++)
            {
                for (int j = 0; j <= liczba_wezlow; j++)
                { tablica_uzyc[i,j]= 0; }
            }

            /*
            for (int i = 1; i <= liczba_wezlow; i++)
            {
                for (int j = 0; j < wezly[i].drogi.Length - 1; j++)
                    Console.Write(" p"+ i+"  "+wezly[i].drogi[j].klucz+" ");


            }

    */


            for (int i=1;i<=liczba_wezlow;i++)
            {
                for (int j = 0; j < wezly[i].drogi.Length-1; j++)
                    tablica_drog[i,j] = wezly[i].drogi[j].klucz;


            }


           

        }



       

     public   int zwrocLiczbewezlow() { return liczba_wezlow; }
     public   int zwrocLiczbelaczy() { return liczba_laczy; }
        public void ustawWybor(int i, int w) {
            if (w == 1)
            wezly[i].wybrany = true;
        else if (w==0)
                wezly[i].wybrany = false;
        }
    }
}
