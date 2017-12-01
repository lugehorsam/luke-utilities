namespace Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    
    public class Lerp<T> : IEnumerator<T>
    {        
        private readonly float _targetDuration;
        private readonly Func<T, T, float, T> _lerp;
        private readonly T _start;
        private readonly T _end;
        
        private float _elapsedTime;
        
                
        public Lerp(T start, T end, float targetDuration, Func<T, T, float, T> lerp)
        {
            _start = start;
            _end = end;
            _lerp = lerp;
            _targetDuration = targetDuration;
            Current = _start;
        }
 
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if (_elapsedTime >= _targetDuration)
            {
                return false;
            }
            
            Current = _lerp(_start, _end, _elapsedTime/_targetDuration);
            _elapsedTime += Time.deltaTime;
            
            return true;
        }

        public void Reset()
        {
            _elapsedTime = 0f;
            Current = _start;
        }

        public T Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}
