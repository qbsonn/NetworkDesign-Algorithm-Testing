//Implementacja kopca (stogu)
//Andrzej Borucki
//www.algorytm.org

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projekt2_czesc2
{
    class MinHeap<T, D,E> where T : IComparable
    {

        public List<Element<T, D,E>> data;
       
      public  MinHeap(int l)
        {
            data = new List<Element<T, D,E>>(2 *l);

    }

      
        public void Insert(T k, D w, E p)
        {
            Element<T, D,E> n = new Element<T, D,E>(k, w,p);
            data.Add(n);

            int i = data.Count - 1;
            while (i > 0)
            {
                int j = (i + 1) / 2 - 1;

                // Check if the invariant holds for the element in data[i]
                T v = data[j].klucz;
                if (v.CompareTo(data[i].klucz) < 0 || v.CompareTo(data[i].klucz) == 0)
                {
                    break;
                }

                // Swap the elements
                Element<T, D,E> tmp = data[i];
                data[i] = data[j];
                data[j] = tmp;

                i = j;
            }
        }

        public Element<T, D,E> ExtractMin()
        {
            if (data.Count < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            Element<T, D,E> min = data[0];
            data[0] = data[data.Count - 1];
            data.RemoveAt(data.Count - 1);
            this.MinHeapify(0);
            return min;
        }

        public Element<T, D,E> Peek()
        {
            return data[0];
        }

        public int Count
        {
            get { return data.Count; }
        }

        private void MinHeapify(int i)
        {
            int smallest;
            int l = 2 * (i + 1) - 1;
            int r = 2 * (i + 1) - 1 + 1;

            if (l < data.Count && (data[l].klucz.CompareTo(data[i].klucz) < 0))
            {
                smallest = l;
            }
            else
            {
                smallest = i;
            }

            if (r < data.Count && (data[r].klucz.CompareTo(data[smallest].klucz) < 0))
            {
                smallest = r;
            }

            if (smallest != i)
            {
                Element<T, D,E> tmp = data[i];
                data[i] = data[smallest];
                data[smallest] = tmp;
                this.MinHeapify(smallest);
            }
        }
    }
}