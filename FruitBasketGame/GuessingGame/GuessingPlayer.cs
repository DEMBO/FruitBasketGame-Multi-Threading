using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame
{
    public abstract class GuessingPlayer<T>
    {
        protected Tuple<T, T> _range;

        public GuessingPlayer(string name)
        {
            Name = name;
        }

        public virtual void Init(Tuple<T, T> range)
        {
            _range = range;
        }

        public string Name { get; set; }

        public abstract T GetSupposedValue();
    }
}
