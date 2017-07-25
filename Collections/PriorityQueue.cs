using System;
using System.Collections.Generic;

public class PriorityQueue<TPriority, TValue> : ICollection<TValue>
{
    private List<KeyValuePair<TPriority, TValue>> _baseHeap;
    private IComparer<TPriority> _comparer;
    private Dictionary<TValue, int> elementPositions; // store positions of all elements so they can be accessed and priorities modified

    public PriorityQueue()
        : this( Comparer<TPriority>.Default )
    {
    }

    public PriorityQueue( IComparer<TPriority> comparer )
    {
        if ( comparer == null )
            throw new ArgumentNullException();

        _baseHeap = new List<KeyValuePair<TPriority, TValue>>();
        _comparer = comparer;
        elementPositions = new Dictionary<TValue, int>();
    }

    public bool TryChangePriority( TValue value, TPriority toPriority )
    {
        int currentPos;
        if ( elementPositions.TryGetValue( value, out currentPos ) )
        {
            TPriority lastPriority = _baseHeap[currentPos].Key;
            int makingBigger = _comparer.Compare( toPriority, lastPriority );
            if ( makingBigger > 0 )
            {
                _baseHeap[currentPos] = new KeyValuePair<TPriority,TValue>( toPriority, value );
                HeapifyFromBeginningToEnd( currentPos ); // if now bigger than children, will bubble down
            }
            else if ( makingBigger < 0 )
            {
                _baseHeap[currentPos] = new KeyValuePair<TPriority,TValue>( toPriority, value );
                HeapifyFromEndToBeginning( currentPos ); // if now smaller than parent, will bubble up
            }
            return true;
        }
        return false;
    }

    public void Enqueue( TPriority priority, TValue value )
    {
        Insert( priority, value );
    }

    private void Insert( TPriority priority, TValue value )
    {
        KeyValuePair<TPriority, TValue> val =
            new KeyValuePair<TPriority, TValue>( priority, value );
        _baseHeap.Add( val );
        int pos = _baseHeap.Count - 1;
        elementPositions.Add( value, pos );

        // heapify after insert, from end to beginning
        HeapifyFromEndToBeginning( pos );
    }

    private int HeapifyFromEndToBeginning( int pos )
    {
        if ( pos >= _baseHeap.Count )
            return -1;

        // heap[i] have children heap[2*i + 1] and heap[2*i + 2] and parent heap[(i-1)/ 2];

        while ( pos > 0 )
        {
            int parentPos = ( pos - 1 ) / 2;
            if ( _comparer.Compare( _baseHeap[parentPos].Key, _baseHeap[pos].Key ) > 0 )
            {
                ExchangeElements( parentPos, pos );
                pos = parentPos;
            }
            else
                break;
        }
        return pos;
    }

    private void ExchangeElements( int pos1, int pos2 )
    {
        var inPos1 = _baseHeap[pos1];
        var inPos2 = _baseHeap[pos2];
        _baseHeap[pos1] = inPos2;
        _baseHeap[pos2] = inPos1;
        elementPositions[inPos1.Value] = pos2;
        elementPositions[inPos2.Value] = pos1;
    }

    public TValue DequeueValue()
    {
        return Dequeue().Value;
    }

    public KeyValuePair<TPriority, TValue> Dequeue()
    {
        if ( !IsEmpty )
        {
            KeyValuePair<TPriority, TValue> result = _baseHeap[0];
            DeleteItem( 0 );
            return result;
        }
        else
            throw new InvalidOperationException( "Priority queue is empty" );
    }

    void DeleteItem( int pos )
    {
        if ( _baseHeap.Count <= pos )
            throw new ArgumentOutOfRangeException( "pos is out of range of heap" );

        if ( _baseHeap.Count <= 1 )
        {
            _baseHeap.Clear();
            elementPositions.Clear();
            return;
        }

        elementPositions.Remove( _baseHeap[pos].Value );
        // move bottom element to gap
        _baseHeap[pos] = _baseHeap[_baseHeap.Count - 1];
        elementPositions[_baseHeap[pos].Value] = pos;
        _baseHeap.RemoveAt( _baseHeap.Count - 1 );

        // heapify
        HeapifyFromBeginningToEnd( pos );
    }

    private void HeapifyFromBeginningToEnd( int pos )
    {
        if ( pos >= _baseHeap.Count )
            return;

        // heap[i] have children heap[2*i + 1] and heap[2*i + 2] and parent heap[(i-1)/ 2];

        while ( true )
        {
            // on each iteration exchange element with its smallest child
            int smallest = pos;
            int left = 2 * pos + 1;
            int right = 2 * pos + 2;
            if ( left < _baseHeap.Count &&
                 _comparer.Compare( _baseHeap[smallest].Key, _baseHeap[left].Key ) > 0 )
                smallest = left;
            if ( right < _baseHeap.Count &&
                 _comparer.Compare( _baseHeap[smallest].Key, _baseHeap[right].Key ) > 0 )
                smallest = right;

            if ( smallest != pos )
            {
                ExchangeElements( smallest, pos );
                pos = smallest;
            }
            else
                break;
        }
    }

    public KeyValuePair<TPriority, TValue> Peek()
    {
        if ( !IsEmpty )
            return _baseHeap[0];
        else
            throw new InvalidOperationException( "Priority queue is empty" );
    }

    public TValue PeekValue()
    {
        return Peek().Value;
    }

    public bool IsEmpty => _baseHeap.Count == 0;

    public int Count => _baseHeap.Count;

    #region ICollection<TValue> Members

    void ICollection<TValue>.Add( TValue item )
    {
        Enqueue( default( TPriority ), item );
    }

    public void Clear()
    {
        _baseHeap.Clear();
        elementPositions.Clear();
    }

    public bool Contains( TValue item )
    {
        return elementPositions.ContainsKey( item );
    }

    public void CopyTo( TValue[] array, int arrayIndex )
    {
        for ( int i = 0; i < _baseHeap.Count; i++ )
        {
            array[arrayIndex + i] = _baseHeap[i].Value;
        }
    }

    public bool IsReadOnly => false;

    public bool Remove( TValue item )
    {
        int currentPos;
        if ( elementPositions.TryGetValue( item, out currentPos ) )
        {
            DeleteItem( currentPos );
            return true;
        }
        return false;
    }

    #endregion

    #region IEnumerable<TValue> Members

    public IEnumerator<TValue> GetEnumerator()
    {
        foreach ( var item in _baseHeap )
            yield return item.Value;
    }

    #endregion

    #region IEnumerable Members

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion
}