namespace Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class Lerp<T> : IEnumerator<T>
    {
        private readonly T _end;
        private readonly Func<T, T, float, T> _lerp;
        private readonly T _start;
        private readonly float _targetDuration;

        private float _elapsedTime;
        private bool _timeHasElapsed;

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
            if (_timeHasElapsed)
            {
                return false;
            }

            if (_elapsedTime >= _targetDuration)
            {
                _timeHasElapsed = true;
            }

            Current = _lerp(_start, _end, _elapsedTime / _targetDuration);
            _elapsedTime += Time.deltaTime;

            return true;
        }

        public void Reset()
        {
            _elapsedTime = 0f;
            Current = _start;
            _timeHasElapsed = false;
        }

        public T Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}
