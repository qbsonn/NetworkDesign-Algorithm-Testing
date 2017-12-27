using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Diagnostics;

namespace Projekt2_czesc2
{
    public class Floyd
    {
        public List<string> sciezki = new List<string>();
        public Wezel[] wezly;
        public Lacze[] lacza;
        public Siec siec;
        public double[,] tablica_drog_Floyd;
        public int[,] tablica_drog_Floyd_nast;
        public int liczbawezlow;
        public int liczbalaczy;
        double INF;
        public int[,] R;
        string[] wierzcholki;
        public double[,] G;
        public double[,] g;

        public Floyd(int l, int w)
        {
            liczbawezlow = w;
            liczbalaczy = l;
            INF = Double.PositiveInfinity;
            //   G=new double[liczbawezlow + 1, liczbawezlow + 1];
            R = new int[liczbawezlow + 1, liczbawezlow + 1];
            g = new double[liczbawezlow + 1, liczbawezlow + 1];
           wierzcholki= new string[liczbawezlow + 1];
        }

        public void init(Siec s)
        { //inicjuje graf i pozostale struktury wartosciami poczatkowymi
            for (int i = 1; i <= liczbawezlow; i++)
            {
                for (int j = 1; j <= liczbawezlow; j++)
                {
                    //  G[i,j] = INF;
                    g[i, j] = INF;
                    R[i, j] = -1;
                }
                // G[i,i] = 0;
                g[i, i] = 0;
            }

            for (int i = 0; i < liczbalaczy; i++)
            {
                //  G[siec.lacza[i].WezelPoczatek, siec.lacza[i].WezelKoniec] = siec.lacza[i].dlugosc;
          //      g[siec.lacza[i].WezelPoczatek, siec.lacza[i].WezelKoniec] = siec.lacza[i].dlugosc;

            }



            for (int i = 1; i <=liczbawezlow; i++)
                for (int j = 1; j <= liczbawezlow; j++)
                    if (g[i, j] == INF)
                        R[i, j] = -1;
                    else
                        R[i, j] = i;





        }

        public double floyd(Siec siec)
        {
            Stopwatch watchf = new Stopwatch();
            watchf.Start();
            init(siec);

            //znajduje najkrotsze sciezki miedzy kazda para wierzcholkow w grafie i wypelnia macierz kierowanie ruchem zeby mozna bylo odtworzyc przebieg kazdej drogi
            for (int k = 1; k <= liczbawezlow; k++)
            {
                for (int i = 1; i <= liczbawezlow; i++)
                {
                    for (int j = 1; j <= liczbawezlow; j++)
                    {
                        if (g[i, j] > g[i, k] + g[k, j])
                        { //jezeli droga z i do j, poprzez wierzcholek posredni k jest krotsza niz aktualnie najkrotsza znana trasa i->j to zaktualizuj
                            g[i, j] = g[i, k] + g[k, j];
                            R[i, j] = k; //oznacza to ze idac po sciezce i~>j trzeba przejsc przez k
                        }
                    }
                }
            }


            /*
            for (int i = 1; i <= liczbawezlow; i++)

            { for (int j = 1; j <= liczbawezlow; j++)
                {
                   
                    droga(i, j, i);
                   // Console.Write(wierzcholki[i]+"\n");
                }
            }

    */

            watchf.Stop();
            return (watchf.ElapsedMilliseconds);
        }
    

      void  droga(int u,int v, int i)
        {
           


            //odtwarza najkrotsza sciezke miedzy danymi wierzcholkami wykorzystujac macierz kierowania ruchem
            if (R[u,v] != -1)
            { //dopoki nie dojdziemy do zwyklej krawedzi po ktorej trzeba wejsc to zchodz rekurencyjnie i wypisuj wierzcholek posredni k
                droga(u, R[u,v],i);
                //Console.Write(R[u,v]  +" "); 

                wierzcholki[i] =wierzcholki[i]+ R[u, v] + " ";
                droga(R[u,v], v,i);
            }
        }





        public List<int> getPath(int from, int to)
        {
            if (R == null)
                return null;
            if (R[from, to] == -1)
                return new List<int>();
            if (from == to)
                return new List<int>() { from };
            List<int> ret = new List<int>();
            getPathRec(ret, from, to);
            ret.Add(to);
            return ret;
        }




        private void getPathRec(List<int> path, int from, int to)
        {
            int intermediate = R[from, to];
            if (intermediate == from)
            {
                path.Add(from);
                return;
            }
            getPathRec(path, from, intermediate);
            getPathRec(path, intermediate, to);
        }


        public void sciezkaAll()
        {
            double dlugosc=0;
            double a;
            for (int i = 1; i <= liczbawezlow; i++)
                for (int j = 1; j <= liczbawezlow; j++)
                {
                    /*
                  foreach(int x in  getPath(i, j))
                    {
                        Console.Write(x + " ");

                    }
                    */
                    getPath(i, j);
                   // Console.Write("\n");
                    dlugosc = g[i, j];
                   // Console.Write("Droga z " + i + " do  " + j + " wynosi:"+ dlugosc + "\n");

                   
                }
        }

        public void wyswiet()
        {


            for (int i = 1; i <=liczbawezlow; i++)
            {
                for (int j = 1; j <=liczbawezlow; j++)
                {
                  //  cout << i + 1 << "->" << j + 1 << " dl. " << g[i][j] << ", przebieg: " << i + 1 << " ";

                    Console.Write(i + " -> " + j + " dl. " + g[i, j] + ", przebieg: " + i+"  ");
                    droga(i, j,i);
                  //  cout << j + 1 << endl;
                    Console.Write(j + "\n");
                }
            }
        }


    }
}
