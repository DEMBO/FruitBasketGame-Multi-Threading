using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitBasketGame
{
    public class Maybe<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _values;

        public Maybe()
        {
            _values = new T[0];
        }

        public Maybe(T value)
        {
            if (value == null)
            {
                _values = new T[0];
                return;
            }

            _values = new[] { value };
        }
        
        public T Value
        {
            get
            {
                if (!_values.Any())
                {
                    throw new InvalidOperationException("Maybe has no value, please check that Maybe has value before accessing it");
                }

                return _values.First();
            }
        }

        public bool IsEmpty
        {
            get { return !_values.Any(); }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
