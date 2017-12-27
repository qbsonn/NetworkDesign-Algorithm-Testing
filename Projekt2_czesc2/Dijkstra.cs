using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Projekt2_czesc2
{
    public class Dijkstra
    {
        public Siec siec;
        int[] krawpop;
        public double[] drogi;
        public double[,] wykorzystania;
        public int[] poprzednicy;
        int wybrane;
        int zap;
      public  double koszt;
        Zapotrzebowanie z;
       
        MinHeap <double, int,double> drogi1;

        double nieskonczonosc = Double.PositiveInfinity;

        public Dijkstra(Siec s)
        {

            zap = 0;
            siec = s;
           // x = 0;
            wybrane = 0;
            wykorzystania = new double[siec.liczba_wezlow + 1, siec.liczba_wezlow + 1];
            poprzednicy = new int[siec.zwrocLiczbewezlow() + 1];
            drogi = new double[siec.zwrocLiczbewezlow() + 1];
            // drogi1 = new Lista<double, int>();
            for (int i = 1; i <= siec.liczba_wezlow; i++)
            {
                for (int j = 1; j <= siec.liczba_wezlow; j++)
                {
                    siec.lacza[siec.tablica_laczy[i, j]].wolna_pojemnosc = 0;
                    siec.lacza[siec.tablica_laczy[i, j]].iloscmodulow = 0;
                  
                }

            }

            for (int i = 0; i <=siec.liczba_wezlow; i++)
            {
                for (int j = 0; j <= siec.liczba_wezlow; j++)
                {
                    siec.tablica_uzyc[i, j] = 0;
                    //wykorzystania[i, j] = 0;
                }
            }
        }

        public void sciezkaOthers(int p, Zapotrzebowanie zzz)
        {
            zap = 1;
            z = zzz;
            for (int i = 1; i <= siec.liczba_wezlow; i++)
            {
                for (int j = 0; j < siec.wezly[i].drogi.Length - 1; j++)
                { siec.wezly[i].drogi[j].klucz = siec.tablica_drog[i, j];
                    siec.wezly[i].drogi[j].przepustowsci = siec.lacza[siec.tablica_laczy[i, siec.wezly[i].drogi[j].dane]].wolna_pojemnosc;
                        }
            }



            for (int i = 0; i <= siec.liczba_wezlow; i++)
            {
                for (int j = 0; j <=siec. liczba_wezlow; j++)
                { siec.tablica_uzyc[i, j] = 0;
                    //wykorzystania[i, j] = 0;
                }
            }


            /*
            for (int i = 0; i < siec.liczba_wezlow; i++)
            {
                for (int j = 0; j < siec.liczba_wezlow; j++)
                {
                    Console.WriteLine(siec.tablica_uzyc[i, j]);
                    //wykorzystania[i, j] = 0;
                }
            }
            */
            for (int i = 1; i <= siec.liczba_wezlow; i++)
            {
                for (int j = 0; j < siec.wezly[i].drogi.Length - 1; j++)
                    while (siec.wezly[i].drogi[j].przepustowsci < zzz.rozmiar)
                    {
                        siec.wezly[i].drogi[j].klucz +=  siec.lacza[siec.tablica_laczy[i, siec.wezly[i].drogi[j].dane]].koszt;
                        siec.tablica_uzyc[i, siec.wezly[i].drogi[j].dane] += 1;

                        siec.wezly[i].drogi[j].przepustowsci = siec.wezly[i].drogi[j].przepustowsci + siec.lacza[siec.tablica_laczy[i, siec.wezly[i].drogi[j].dane]].pojemnosc;
                       // Console.WriteLine("aaaaaaaaaaaa");
                    }
            }



            
        


            drogi1 =  new MinHeap<double,int,double>(siec.liczba_wezlow);

           // poprzednicy = new int[siec.zwrocLiczbewezlow() + 1];

            for (int i = 1; i <= siec.zwrocLiczbewezlow(); i++) // 0 jest puste
            {
                if (i != p)
                {
                    siec.ustawWybor(i, 0);
                    poprzednicy[i] = 0;
                    drogi[i] = nieskonczonosc;
                    drogi1.Insert(nieskonczonosc, i,0);
                }
                else
                {
                    drogi1.Insert(0, i,0);
                    siec.ustawWybor(i, 0);
                }
            }

           
            poprzednicy[p] = 0;
            drogi[p] = 0;
            
            wybrane = 0;

            int v = p;
            double m = nieskonczonosc;
            Random rnd=new Random(DateTime.Now.Millisecond); ;

            double prz;
            while (wybrane != siec.liczba_wezlow)

            {
                Element<double, int, double> n = new Element<double, int, double>();
                wybrane++;
                
                while (siec.wezly[v].wybrany != false && drogi1.Count != 0)
                {
                    n = drogi1.ExtractMin();
                    v = n.dane;                 
                }
                 
                // v = drogi1.ExtractMin().dane;



                siec.ustawWybor(v, 1);


               
                
                for (int i = 0; i < siec.wezly[v].drogi.Length - 1; i++)
                {


                    prz = rnd.NextDouble();
                  if (drogi[siec.wezly[v].drogi[i].dane]> siec.wezly[v].drogi[i].klucz + drogi[v])
                    {

                        if (prz >= 0.5)
                        {
                            if (drogi[siec.wezly[v].drogi[i].dane] > siec.wezly[v].drogi[i].klucz + drogi[v])
                            {
                                drogi[siec.wezly[v].drogi[i].dane] = siec.wezly[v].drogi[i].klucz + drogi[v];
                                //siec.ustawWybor(n.dane, 1);
                                poprzednicy[siec.wezly[v].drogi[i].dane] = v;
                                if (siec.wezly[siec.wezly[v].drogi[i].dane].wybrany != true)
                                    drogi1.Insert(siec.wezly[v].drogi[i].klucz + drogi[v], siec.wezly[v].drogi[i].dane, 0);
                            }

                        }
                    }


                }


            }



            

            for (int i = 1; i <= siec.zwrocLiczbewezlow(); i++)
            {

             //   Console.Write("d" + i + "   " + drogi[i]+ "  " + "p" + "   " + poprzednicy[i] + "\n");

            }
            
           

        }


        public void sciezkaAB(int p, int k, Zapotrzebowanie zzz)
        {

            
            int a;
            List<int> pop=new List<int>();
            
            int x = k;

            if (p != k)
            {
                
                a = 0;
          
                int r = 1;
              
                pop.Insert(0,k);
                while(poprzednicy[x]!=0)

                {
                    
                    pop.Insert(pop.Count, poprzednicy[x]);
                        x = poprzednicy[x];
                        r++;
                    
                   
                }
                int y=0;
                int razy = 1;
                /*
                Console.WriteLine("START");
                for (int i = 0; i < pop.Count; i++)
                {
                    
                        Console.WriteLine(pop[i]);
                    
                }
                Console.WriteLine("EnD");
                /*
                 for (int i = 0; i < siec.liczba_wezlow; i++)
                 {
                     for (int j = 0; j < siec.liczba_wezlow; j++)
                     {
                         Console.WriteLine(siec.tablica_uzyc[i, j]);
                         //wykorzystania[i, j] = 0;
                     }
                 }
 */

                x++;
             //  Console.WriteLine("popcount"+x+" !!!!");

                for (int i = 0; i < pop.Count-1; i++)
                {

                   razy = 0;
                    y = siec.tablica_laczy[pop[i+1], pop[i]];
                    razy = siec.tablica_uzyc[pop[i + 1], pop[i]];
                 //  Console.WriteLine(siec.lacza[y].id+"  LLL");
                    siec.lacza[y].iloscmodulow += razy;
                  //  Console.WriteLine(siec.lacza[y].iloscmodulow);
                    zzz.krawedzie.Insert(z.krawedzie.Count,(y+1));

                    siec.lacza[y].wolna_pojemnosc += ((razy * siec.lacza[y].pojemnosc)-z.rozmiar);




                    /*
                    for (int j=0; j<siec.wezly[pop[i+1]].drogi.Length-1;j++)

                    {
                        if (siec.lacza[y].WezelKoniec == siec.wezly[pop[i+1]].drogi[j].dane)
                        {
                           // Console.WriteLine("aaaaaaaaaaaa");
                            if (siec.tablica_drog[pop[i+1],j] != siec.wezly[pop[i+1]].drogi[j].klucz)
                            {
                               // Console.Write(siec.tablica_drog[pop[i+1], j] + "  xxxxx   " + siec.wezly[pop[i+1]].drogi[j].klucz);
                               // Console.WriteLine("");

                                r = (int)(siec.wezly[pop[i+1]].drogi[j].klucz / siec.tablica_drog[pop[i+1], j]);

                            }
                        }
                          

                    }
                   */


                }

              //  Console.WriteLine("Zapotrzebowania " + zzz.krawedzie.Count);

                //  Console.Write("Sciezka (od koncowego do pierwsze)"+sciezka + "\n");
                
               // Console.Write("Sciezka: ");
                for (int i=0; i<pop.Count;i++)
                {

                  //  Console.Write(pop[i] + "  ");

                }
               // Console.WriteLine("");
               
                for (int i = 0; i < siec.liczba_laczy; i++)
                {
                 //   Console.Write(i+1+"  ilosc:   "+siec.lacza[i].iloscmodulow + "\n");
                         }
               
                   for (int i = 0; i <siec.liczba_laczy; i++)
                {
               //    Console.WriteLine(siec.lacza[i].wolna_pojemnosc);
                  //  Console.WriteLine(siec.lacza[i].pojemnosc);
                }


                // Console.Write("Dlugosc sciezki od" + p + " do " + k + "wynosi: " + drogi[k] + "\n");

                koszt = drogi[k];
              //  Console.WriteLine(drogi[k]);

            }
            else
            { a = 0;
                pop.Insert(pop.Count, p);
                koszt = 0;
              //  Console.Write(pop[i] + "  ")
              // Console.Write("Dlugosc sciezki od" + p + " do " + k + "wynosi: " + drogi[k] + "\n"); 
            }
        }

        public double sciezkaAll()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            for (int p = 1; p <= siec.zwrocLiczbewezlow(); p++)
            {
                //sciezkaOthers(p);
                
                    for (int k = 1; k <= siec.zwrocLiczbewezlow(); k++)
                    {
                        
                            //sciezkaAB(p, k);

                    }

                }

                watch.Stop();
                return (watch.ElapsedMilliseconds);

            }


        }

    }