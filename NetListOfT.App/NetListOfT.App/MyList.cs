using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetListOfT.App
{
    public class MyList<T> : IEnumerable<T>
    {
        private const int defaultCapacity = 4;

        private T[] items;
        private int size;

        static readonly T[] emptyArray = new T[0];

        //int Count { get; } : întoarce numărul de elemente
        public int Count
        {
            get
            {
                return size;
            }
        }

        //propietate de gestionare a dimensiunii vectorului in care tinem elementele MyList
        private int Capacity
        {
            get
            {
                return items.Length;
            }
            set
            {
                if(value < size)
                {
                    throw new ArgumentOutOfRangeException("capacity", "Nu corespund dimensiunea curenta MyList cu cea noua");
                }

                //schimbam dimensiunea vectorului
                if(value != items.Length)
                {
                    if(value > 0)     
                    {
                        T[] newItems = new T[value];
                        if(size > 0)   //daca vectorul contine deja elemente
                        {
                            Array.Copy(items, 0, newItems, 0, size);
                        }
                        items = newItems;
                    }
                    else
                    {
                        items = emptyArray;
                    }
                }
            }
        }

        //constructor fara parametrii
        public MyList()
        {
            items = emptyArray;
        }

        //constructor cu parametru de dimensiune
        public MyList(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity", "The capacity of a MyList must be higher or equal than 0");
            }
            if (capacity == 0)
            {
                items = emptyArray;
            }
            else
            {
                items = new T[capacity];
            }

        }

        //constructor cu parametru de collection
        public MyList(IEnumerable<T> enumerableCollection)
        {
            if(enumerableCollection == null)
            {
                throw new ArgumentNullException("collection", "The collection is null");
            }
            ICollection<T> collection = enumerableCollection as ICollection<T>;

            if(collection != null)
            {
                int count = collection.Count;
                if(count == 0)
                {
                    items = emptyArray;
                }
                else
                {
                    items = new T[count];
                    collection.CopyTo(items, 0);
                    size = count;
                }
            }
            else //daca enumerable nu contine elemente dar enumeratorul lui contine 
            {
                size = 0;
                items = emptyArray;

                using(IEnumerator<T> enumerator = collection.GetEnumerator())
                {
                    while(enumerator.MoveNext())
                    {
                        Add(enumerator.Current);
                    }
                }
            }
        }

        //void Add(T item) : adaugă un element la sfîrșitul listei
        public void Add(T item)
        {
            //daca am ocupat tot spatiu alocat , atunci marim spatiul
            if (size == items.Length)
                EnsureCapacity();
            items[size++] = item;
        }

        private void EnsureCapacity()
        {
            int newCapacity;
            if (items.Length == 0)
            {
                newCapacity = defaultCapacity;
            }
            else
            {
                try
                {
                    newCapacity = checked(items.Length * 2);
                }
                catch(OverflowException e)
                {
                    newCapacity = System.Int32.MaxValue;
                }
            }
            Capacity = newCapacity;
        }

        //T this[int index] { get; set; }: acces la elementul de la poziția index; aruncă ArgumentOutOfRangeException dacă indexul e mai mic decît 0 sau mai mare sau egal cu Count
        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0)
                {
                    throw new ArgumentOutOfRangeException("index", "Index out of range");
                }

                return items[index];
            }
            set
            {
                if (index >= Count || index < 0)
                {
                    throw new ArgumentOutOfRangeException("index", "Index out of range");
                }
                items[index] = value;
            }
        }

        // void Clear() : șterge toate elementele din listă
        public void Clear()
        {
            if(size > 0)
            {
                Array.Clear(items, 0, size);
                size = 0;
            }
        }

        //bool Contains(T item) : întoarce true dacă lista conține item și false dacă nu
        public bool Contains(T item)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < size; i++)
            {
                if (comparer.Equals(items[i], item))
                {
                    return true;
                }
            }
            return false;
        }

        //void RemoveAt(int index) : scoate din listă elementul de la poziția index și mută toate elementele de după cu o poziție în față; aruncă ArgumentOutOfRangeException dacă indexul e mai mic decît 0 sau mai mare sau egal cu Count
        public void RemoveAt(int index)
        {
            if (index >= Count || index < 0)
            {
                throw new ArgumentOutOfRangeException("index", "Index out of range");
            }

            size--;
            if(index < size)
            {
                Array.Copy(items, index + 1, items, index, size - index);
            }
            items[size] = default(T);
        }

        //MyList<T> FindAll(Func<T, bool> match): întoarce o nouă listă care conține doar elementele din listă pentru care match întoarce true
        public MyList<T> FindAll(Func<T,bool> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException("match", "Match function is null");
            }
            MyList<T> myList = new MyList<T>();
            for (int i = 0; i < size; i++ )
            {
                if(match(items[i]))
                {
                    myList.Add(items[i]);
                }
            }
            return myList;
        }

        //IEnumerator<T> GetEnumerator() : implementarea interfeței IEnumerable<T>, astfel încît instanțele MyList<T> să poată fi folosite cu foreach
        public IEnumerator<T> GetEnumerator()
        {
            return new MyListEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void DisplayList()
        {
            for (int i = 0; i < this.Count; i++)
            {
                Console.Write(this[i] + " ");
            }
        }
    }
}
