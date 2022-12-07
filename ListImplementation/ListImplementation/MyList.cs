using System.Collections;

namespace ListImplementation;

/*
 * To Do:
 * Create an implementation of IList<T> that uses an array to store elements. 
 * When maximum size is reached and we need to store more elements, double the size of the array. 
 * If possible, don't use methods from the Array class such as CopyTo. 
 */

public class MyList<T> : IList<T>
{
    private T[] _list;

    private int _itemCount;

    private const int _startSize = 4;

    public MyList()
    {
        _list = new T[0];
    }

    public MyList(int capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException("size", "Non-negative number required.");
        }

        _list = new T[capacity];
    }

    public MyList(IEnumerable collection)
    {
        if (collection == null)
        {
            throw new ArgumentNullException("array");
        }

        var col = (ICollection)collection;

        if (col != null)
        {
            _itemCount = col.Count;
            _list = new T[_itemCount];

            if (_itemCount > 0)
            {
                col.CopyTo(_list, 0);
            }
        }
        else
        {
            _list = new T[0];
        }
    }

    public T this[int index]
    {
        get
        {
            if (index >= _itemCount)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index was out of range.");
            }

            return _list[index];
        }
        set
        {
            if (index >= _itemCount)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index was out of range.");
            }

            _list[index] = value;
        }
    }

    public int Count
    {
        get { return _itemCount; }
    }

    public void Add(T item)
    {
        if (_list.Length == _itemCount)
        {
            IncreaseCapacity();
        }

        _list[_itemCount] = item;
        _itemCount++;
    }

    public void Clear()
    {
        if (_itemCount > 0)
        {
            _list = new T[0];
            _itemCount = 0;
        }
    }

    public bool Contains(T item)
    {
        EqualityComparer<T> comparer = EqualityComparer<T>.Default;

        for (int i = 0; i < _itemCount; i++)
        {
            if (comparer.Equals(_list[i], item))
            {
                return true;
            }
        }

        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new Enumerator(this);
    }

    public int IndexOf(T item)
    {
        EqualityComparer<T> comparer = EqualityComparer<T>.Default;

        for (int i = 0; i < _itemCount; i++)
        {
            if (comparer.Equals(_list[i], item))
            {
                return i;
            }
        }

        return -1;
    }

    public void Insert(int index, T item)
    {
        if (_list.Length == _itemCount)
        {
            IncreaseCapacity();
        }

        // move all elements of the array one position further, starting from the specified index 
        for (int i = _itemCount; i > index; i--)
        {
            _list[i] = _list[i - 1];
        }

        _list[index] = item;
        _itemCount++;
    }

    public bool Remove(T item)
    {
        EqualityComparer<T> comparer = EqualityComparer<T>.Default;

        for (int i = 0; i < _itemCount; i++)
        {
            if (comparer.Equals(_list[i], item))
            {
                RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public void RemoveAt(int index)
    {
        // We move all elements after the index one position to the left

        for (int i = index; i < _itemCount - 1; i++)
        {
            _list[i] = _list[i + 1];
        }

        _itemCount--;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return new Enumerator(this);
    }

    private void IncreaseCapacity()
    {
        var currentSize = _list.Length;

        if (currentSize == Array.MaxLength)
        {
            throw new OutOfMemoryException("List maximum capacity has been reached and more elements cannot be added.");
        }

        var newSize = currentSize == 0 ? _startSize : currentSize * 2;

        if ((uint)newSize > Array.MaxLength)
        {
            newSize = Array.MaxLength;
        }

        var newList = new T[newSize];

        for (int i = 0; i < currentSize; i++)
        {
            newList[i] = _list[i];
        }

        _list = newList;
    }

    public struct Enumerator : IEnumerator<T>, IEnumerator
    {
        private readonly MyList<T> _list;
        private int _index;
        private T? _current;

        internal Enumerator(MyList<T> list)
        {
            _list = list;
            _index = 0;
            _current = default;
        }

        public T Current
        {
            get { return _current!; }
        }

        object IEnumerator.Current
        {
            get
            {
                if (_index == 0 || _index > _list._itemCount)
                {
                    throw new IndexOutOfRangeException();
                }

                return Current;
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_index < _list._itemCount)
            {
                _current = _list[_index];
                _index++;
                return true;
            }

            _current = default;
            return false;
        }

        public void Reset()
        {
            _index = 0;
            _current = default;
        }
    }

    #region Not Needed

    public bool IsReadOnly
    {
        get { return false; }
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    #endregion
}