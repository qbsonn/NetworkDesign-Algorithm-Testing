using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Diagnostics;
namespace Projekt2_czesc2
{
    public class Tester
    {
        #region Zmienne, liczby, klasy, tablice, stale
        int liczbawezlow;
        int liczbalaczy;
        int liczbazapotrzebowan;
        
        Wezel[] wezly;
        Lacze[] lacza;
        Zapotrzebowanie[] zapotrzebowania;
        Siec siec;
        Floyd floyd;
        Dijkstra dijkstra;
        Random rnd;


        List<int>[] aaa;
        List<int>[] naj;
        double[,] tablica_polaczen;
        double[] tablica_uzytych;
        double najlepsze;
        StreamReader odczyt;
        string sciezka_wejscia;

        string[] wyrazy;
        string[] wyrazy2;

        double nieskonczonosc = Double.PositiveInfinity;
  

      
        double koszt = 0;

        int[] wybrane_lacza;
       
        Random rand = new Random(DateTime.Now.Millisecond);

        
        
        #endregion

        public Tester()
        {
            rnd = new Random();
            #region Odczyt z pliku
            Console.WriteLine("Przeciagnij tu plik wejsciowy i wcisnij ENTER...");
            sciezka_wejscia = Console.ReadLine();
            if (sciezka_wejscia[0] == '\"') sciezka_wejscia = sciezka_wejscia.Substring(1, sciezka_wejscia.Length - 2);
            Console.WriteLine(" ");
            odczyt = new StreamReader(sciezka_wejscia);
            String linia = "";
            while (linia.Length < 2 || linia[0] == '#')
            {
                linia = odczyt.ReadLine();
            }
            wyrazy = linia.Split(' ');
            if (wyrazy[0] == "WEZLY") liczbawezlow = int.Parse(wyrazy[2]);
            wezly = new Wezel[liczbawezlow + 1];
            wezly[0] = null;
            for (int i = 1; i <= liczbawezlow; i++)//WEZEL ZEROWY JEST PUSTY
            {
                wezly[i] = new Wezel(i, liczbawezlow);
            }

            linia = odczyt.ReadLine();
            while (linia.Length < 2 || linia[0] == '#')
            {
                linia = odczyt.ReadLine();
            }
            wyrazy = linia.Split(' ');
            tablica_polaczen = new double[liczbawezlow + 1, liczbawezlow + 1];

            for (int i = 0; i <= liczbawezlow; i++)
            {
                for (int j = 0; j <= liczbawezlow; j++)
                    tablica_polaczen[i, j] = nieskonczonosc;

            }
            if (wyrazy[0] == "LACZA") liczbalaczy = int.Parse(wyrazy[2]);
            lacza = new Lacze[liczbalaczy + 1]; //Lacze zerowe jest puste
            lacza[0] = null;
            linia = odczyt.ReadLine(); //odczytanie jednej lini z #

            for (int i = 0; i < liczbalaczy; i++)
            {
                linia = odczyt.ReadLine();
                wyrazy = linia.Split(' ');
                wyrazy2 = wyrazy[3].Split('.');
                wyrazy[3] = (wyrazy2[0] + "," + wyrazy2[1]);
                wyrazy2 = wyrazy[4].Split('.');
                wyrazy[4] = (wyrazy2[0] + "," + wyrazy2[1]);
                lacza[i] = new Lacze(int.Parse(wyrazy[0]), int.Parse(wyrazy[1]), int.Parse(wyrazy[2]), double.Parse(wyrazy[3]), double.Parse(wyrazy[4]));

                tablica_polaczen[lacza[i].WezelPoczatek, lacza[i].WezelKoniec] = lacza[i].koszt;


            }

            linia = odczyt.ReadLine();
            while (linia.Length < 2 || linia[0] == '#')
            {
                linia = odczyt.ReadLine();
            }
            wyrazy = linia.Split(' ');
            if (wyrazy[0] == "ZAPOTRZEBOWANIA") liczbazapotrzebowan = int.Parse(wyrazy[2]);
            linia = odczyt.ReadLine();
            zapotrzebowania = new Zapotrzebowanie[liczbazapotrzebowan + 1];
            for (int i = 1; i <= liczbazapotrzebowan; i++)
            {
                linia = odczyt.ReadLine();
                wyrazy = linia.Split(' ');
                wyrazy2 = wyrazy[3].Split('.');

                wyrazy[3] = (wyrazy2[0] + "," + wyrazy2[1]);
                zapotrzebowania[i] = new Zapotrzebowanie(int.Parse(wyrazy[0]), int.Parse(wyrazy[1]), int.Parse(wyrazy[2]), double.Parse(wyrazy[3]));
            }
            odczyt.Close();
            #endregion
            #region Debug - odczyt z pliku
            Console.WriteLine(" ");
            Console.WriteLine("Debug: ");
            Console.WriteLine("Liczba Wezlow: " + liczbawezlow);
            Console.WriteLine("Liczba Laczy: " + liczbalaczy);
            Console.WriteLine("Liczba Zapotrzebowan: " + liczbazapotrzebowan);
            Console.WriteLine("Lacza:");
            for (int i = 0; i < liczbalaczy; i++)
            {
                Console.WriteLine("id: " + lacza[i].id + ", Wezel poczatkowy: " + lacza[i].WezelPoczatek + ", Wezel koncowy: " + lacza[i].WezelKoniec + ", Przeplywnosc: " + lacza[i].pojemnosc + ", Koszt Modulu: " + lacza[i].koszt);
            }
            Console.WriteLine("Zapotrzebowania:");
            for (int i = 1; i <= liczbazapotrzebowan; i++)
            {
                Console.WriteLine("id: " + zapotrzebowania[i].id + ", Wezel poczatkowy: " + zapotrzebowania[i].WezelPoczatek + ", Wezel koncowy: " + zapotrzebowania[i].WezelKoniec + ", Rozmiar: " + zapotrzebowania[i].rozmiar);
            }
            #endregion

            //UZUPELNIENIE INFORMACJI O WEZLACH

            siec = new Siec(lacza, liczbawezlow, liczbalaczy, wezly);

            //zapotrzebowania[nr_zapotrzebowania].tablica_uzytych_laczy = (int[])wybrane_lacza.Clone();
            wybrane_lacza = new int[liczbalaczy + 1];
            


            Console.WriteLine("xx");




            double[,] zamontowane = new double[liczbazapotrzebowan + 1, liczbalaczy];
            double[,] poprzednie = new double[liczbazapotrzebowan + 1, liczbalaczy];

            for (int i = 0; i <= liczbazapotrzebowan; i++)
                for (int j = 0; j < liczbalaczy; j++)
                    poprzednie[i, j] = 0;

            /*
            dijkstra = new Dijkstra(siec);
            int a=1;
            bool[] tab = new bool[liczbazapotrzebowan+1];
            for (int j=1;j<=liczbazapotrzebowan;j++)
            {

                tab[j] = false;
            }



           // Console.WriteLine(liczbazapotrzebowan + "zapot");
            int x = rnd.Next(1, liczbazapotrzebowan+1); 
            while(a<=liczbazapotrzebowan)
            {
              //  Console.WriteLine("NR A: " + a);
               x = rnd.Next(1, liczbazapotrzebowan+1);
                while (tab[x]!=false)
                {
                 //   Console.WriteLine("weszlo+"+x);
                    x = rnd.Next(1, liczbazapotrzebowan+1);
                }

                dijkstra.sciezkaOthers(zapotrzebowania[x].WezelPoczatek, zapotrzebowania[x]);
                dijkstra.sciezkaAB(zapotrzebowania[x].WezelPoczatek, zapotrzebowania[x].WezelKoniec);
                koszt += dijkstra.koszt;
                tab[x] = true;
                a++;
                /*
                for (int j = 0; j < liczbalaczy; j++)

                {
                    zamontowane[i, j] = dijkstra.siec.lacza[j].iloscmodulow - poprzednie[i - 1, j];
                    poprzednie[i, j] = dijkstra.siec.lacza[j].iloscmodulow;

                }
                
            } */


            // Console.WriteLine("koszt:  " + koszt);
            /*
            for (int i = 1; i <= liczbazapotrzebowan; i++)
            {
                for (int j = 0; j < liczbalaczy; j++)
                { Console.Write(zamontowane[i, j] + "  "); }
                Console.WriteLine("");

            }

            */


            int iter = 0;
            Lacze[] rozwiazanie = new Lacze[zapotrzebowania.Length];
            Lacze[] koncowe_rozw = new Lacze[zapotrzebowania.Length];
            Wyzarzanie los = new Wyzarzanie(10000, 0.1, 0.9999);
             najlepsze = Double.PositiveInfinity;
            double akt_roz = Double.PositiveInfinity;
            double xx;


             tablica_uzytych = new double[liczbalaczy];
            double[] tablica_uzytych_akt = new double[liczbalaczy];

            for (int i = 0; i < liczbalaczy; i++)
            {

                tablica_uzytych[i] = 0;
                tablica_uzytych_akt[i] = 0;
            }

            

            double roznica = 0;
            Console.WriteLine("xx");
            aaa = new List<int>[liczbazapotrzebowan + 1];
            naj = new List<int>[liczbazapotrzebowan + 1];
            aaa[0] = null;
            naj[0] = null; 
            // double akt_koszt = Double.PositiveInfinity;
            int a;
            while (los.temp_konc < los.temp)
            {
                iter++;
                koszt = 0;
             //   Console.WriteLine("no siema");

                for (int i = 1; i <= liczbazapotrzebowan; i++)
                {
                    if (zapotrzebowania[i].krawedzie.Count != 0)
                        zapotrzebowania[i].krawedzie.Clear();
                    
                }

                aaa = new List<int>[liczbazapotrzebowan + 1];

                //  siec = new Siec(lacza, liczbawezlow, liczbalaczy, wezly);

                for (int i = 0; i <= liczbazapotrzebowan; i++)
                    for (int j = 0; j < liczbalaczy; j++)
                        poprzednie[i, j] = 0;


                dijkstra = new Dijkstra(siec);
                a = 1;
                bool[] tab = new bool[liczbazapotrzebowan + 1];
                for (int j = 1; j <= liczbazapotrzebowan; j++)
                {

                    tab[j] = false;
                }



                // Console.WriteLine(liczbazapotrzebowan + "zapot");
                int x = rnd.Next(1, liczbazapotrzebowan + 1);
                while (a <= liczbazapotrzebowan)
                {
                    //  Console.WriteLine("NR A: " + a);
                    x = rnd.Next(1, liczbazapotrzebowan + 1);
                    while (tab[x] != false)
                    {
                        //   Console.WriteLine("weszlo+"+x);
                        x = rnd.Next(1, liczbazapotrzebowan + 1);
                    }

                    dijkstra.sciezkaOthers(zapotrzebowania[x].WezelPoczatek, zapotrzebowania[x]);
                    dijkstra.sciezkaAB(zapotrzebowania[x].WezelPoczatek, zapotrzebowania[x].WezelKoniec, zapotrzebowania[x]);
                    koszt += dijkstra.koszt;

                    aaa[x] = zapotrzebowania[x].krawedzie;


                    tab[x] = true;
                    a++;
                }
                /*
                for (int i = 0; i < liczbalaczy; i++)
                {
                    Console.WriteLine(siec.lacza[i].iloscmodulow);


                }
              */


                roznica = koszt - akt_roz;

                if (roznica < 0)
                {
                    akt_roz = koszt;
                    for (int i = 0; i < liczbalaczy; i++)
                    {
                       // tablica_uzytych_akt[i] = lacza[i].iloscmodulow;


                    }

                 

                }

                else
                {
                    xx = rnd.NextDouble();
                    if (xx < Math.Exp(-roznica / los.temp))
                    {
                        akt_roz = koszt;

                        for (int i = 0; i < liczbalaczy; i++)
                        {
                           // tablica_uzytych_akt[i] = lacza[i].iloscmodulow;

                        }

                    }
                }

                /*
                for (int i = 1; i <= liczbazapotrzebowan; i++)
                {
                    
                    for (int j = 0; j < aaa[i].Count; j++)
                    { Console.WriteLine("   " + aaa[i][j]); }

                 

                }

                */

                if (najlepsze > akt_roz)
                {
                    najlepsze = akt_roz;
                    naj = aaa;


                    for (int i = 1; i <= liczbazapotrzebowan; i++)
                    {
                        naj[i] = zapotrzebowania[i].krawedzie;

                    }

                    for (int i = 0; i < liczbalaczy; i++)
                    {
                        
                        tablica_uzytych[i] = lacza[i].iloscmodulow;


                    }

                }
                /*
                for (int j = 0; j < liczbalaczy; j++)

                {
                    zamontowane[i, j] = dijkstra.siec.lacza[j].iloscmodulow - poprzednie[i - 1, j];
                    poprzednie[i, j] = dijkstra.siec.lacza[j].iloscmodulow;

                }
                */
                /*
                for (int i = 0; i < liczbalaczy; i++)
                {
                    Console.WriteLine(tablica_uzytych_akt[i]);


                }
                */
                los.temp = los.temp * los.lambda;



                // Console.WriteLine("koszt:  " + akt_roz);
               

                // Console.WriteLine("koszt:  " + akt_roz);
            }

            Console.WriteLine("najlepsze:  " + najlepsze);






            for (int i = 1; i <= liczbazapotrzebowan;i++)
            {
                Console.Write(i);
                for (int j = 0; j < naj[i].Count; j++)
                { Console.Write( "   " + naj[i][j]); }
              
                Console.WriteLine("");
                Console.WriteLine(naj[i].Count);
                Console.WriteLine("");


            }
            Zapis();
            Console.WriteLine("Liczba iteracji: "+iter);
      }


        public void Zapis()
        { 
            StreamWriter zapis = new StreamWriter(nazwaPlikuWyjscia(sciezka_wejscia));
            zapis.WriteLine("# koszt rozwiazania");
            zapis.WriteLine("KOSZT = " +najlepsze);
            zapis.WriteLine(" ");
            zapis.WriteLine("# liczba zapotrzebowan");
            zapis.WriteLine("ZAPOTRZEBOWANIA = " + liczbazapotrzebowan);
            zapis.WriteLine("# kazde zapotrzebowanie to id. zapotrzebowania oraz zbior uzytych krawedzi");
            for (int i = 1; i <= liczbazapotrzebowan; i++)
            {
                zapis.Write(i);
                for (int j = 0; j < naj[i].Count; j++)
                { zapis.Write("   " + naj[i][j]); }

                zapis.WriteLine("");

            }

            zapis.WriteLine(" ");
            zapis.WriteLine("# liczba krawedzi");
            zapis.WriteLine("LACZA = " + liczbalaczy);
            zapis.WriteLine("# kazde lacze to: id, liczba zainstalowanych modulow przepustowosci");


            for (int i = 0; i < liczbalaczy; i++)
            {
                zapis.Write(lacza[i].id + "  ");
                zapis.Write(tablica_uzytych[i]+ "   ");
                zapis.WriteLine("");

            }

            zapis.Close();
        }
        /*
        for (int i = 1; i <= liczbawezlow; i++)
        {

            for (int j = 0; j <wezly[i].drogi.Length-1; j++)
            { Console.Write(siec.wezly[i].drogi[j].przepustowsci + "  "); }

            Console.WriteLine("");

        }*/

        public string nazwaPlikuWyjscia(string nazwaWejscia)
        {
            String nazwaWyjscia = nazwaWejscia.Replace("input", "output");
            if (nazwaWyjscia == nazwaWejscia)
            {
                nazwaWyjscia = nazwaWyjscia.Remove(nazwaWejscia.LastIndexOf('\\'));
                nazwaWyjscia = nazwaWyjscia + "\\siec_output_bez_nazwy.txt";
                return nazwaWyjscia;
            }
            else
            {
                return nazwaWyjscia;
            }
        }

    }
}

   