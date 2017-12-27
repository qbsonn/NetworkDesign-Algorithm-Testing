using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt2_czesc2
{
  public  class Wyzarzanie
    {

        int liczba_iteracji;
       public double temp_pocz;
        public double temp_konc;

        double koszt1;
        double koszt2;
        double arg1;
        double arg2;
        double wsp_temp;
      public  double lambda;
      public  double temp;

       public Wyzarzanie( double tp, double tk, double lamb)
        {
           // Random rnd = new Random();
         //   l = liczba_iteracji;
            
            temp_pocz = tp;
            temp_konc = tk;
           
          
            lambda = lamb;
            temp = temp_pocz;
            

            }


        }



    }
