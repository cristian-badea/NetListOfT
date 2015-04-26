using System;
using System.Collections;
using System.Collections.Generic;

namespace NetListOfT.App
{
    class MyListEnumerator<T> : IEnumerator<T>
    {
        private MyList<T> myList;
        private int currentIndex;
        private T currentItem;

        public T Current
        {
            get
            {
                return currentItem;
            }
        }
        object IEnumerator.Current
        {
            get
            {
                return currentItem;
            }
        }
        internal MyListEnumerator(MyList<T> myList)
        {
            this.myList = myList;
            currentIndex = -1;
            currentItem = default(T);
        }

        public void Dispose() { }

        public bool MoveNext()
        {
            currentIndex++;
            if(currentIndex >= myList.Count)
            {
                return false;
            }
            currentItem = myList[currentIndex];
            return true;
        }

        public void Reset()
        {
            currentIndex = -1;
            currentItem = default(T);
        }
    }
}
