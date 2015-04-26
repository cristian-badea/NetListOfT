using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetListOfT.App
{
    class Program
    {
        static void Main(string[] args)
        {
            MyList<int> myList = new MyList<int>();

            //void Add(T item)
            myList.Add(32);
            myList.Add(16);
            myList.Add(8);
            myList.Add(4);
            myList.Add(2);

            //testam metodele

            //int Count { get; }
            Console.WriteLine(myList.Count);

            Console.WriteLine();

            //T this[int index] { get; set; }
            for (int i = 0; i < myList.Count; i++)
            {
                Console.Write(myList[i] + " ");
            }

            Console.WriteLine();

            myList[2] = 100;

            myList.DisplayList();
            Console.WriteLine();

            //void Clear();
            myList.Clear();

            Console.WriteLine("Dupa Clear avem : " + myList.Count + " elemente in lista");
            Console.WriteLine();

            //repopulam lista
            myList.Add(32);
            myList.Add(16);
            myList.Add(8);
            myList.Add(4);
            myList.Add(2);

            //bool Contains(T item)
            Console.WriteLine("Lista contine elementul 14 : {0}", myList.Contains(14).ToString());
            Console.WriteLine("Lista contine elementul 16 : {0}", myList.Contains(16).ToString());
            myList.DisplayList();
            Console.WriteLine();

            //void RemoveAt(int index)
            myList.RemoveAt(2);
            Console.WriteLine("Am sters elementul de la indexul 2, noua lista este :");
            myList.DisplayList();
            Console.WriteLine();

            MyList<int> sortedList =  myList.FindAll( (x) => x>9 ? true : false);
            sortedList.DisplayList();

            Console.ReadKey();
        }
    }
}
