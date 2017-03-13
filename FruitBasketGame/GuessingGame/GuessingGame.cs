using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuessingGame
{
    public abstract class GuessingGame<T>
    {
        protected readonly Tuple<T, T> _range;

        public GuessingGame(IEnumerable<GuessingPlayer<T>> players, Tuple<T,T> range)
        {
            Players = players;
            _range = range;
        }

        public IEnumerable<GuessingPlayer<T>> Players { get; private set; }

        public abstract T GenerateNewValue();

        public abstract string Start();

        public abstract string PlayerSession(GuessingPlayer<T> player, CancellationToken capturedToken);
    }
}
