using GuessingGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitBasketGame.Players
{
    public class ThoroughCheaterPlayer : FruitBasketPlayer, IHistoryOverseer
    {
        private int _currentValue;

        private int[] _history;

        public ThoroughCheaterPlayer(string name) : base(name)
        {
        }

        public override void Init(Tuple<int, int> range)
        {
            base.Init(range);
            _currentValue = range.Item1;
        }

        public override int GetSupposedValue()
        {
            while (_history.Contains(_currentValue))
            {
                _currentValue++;
            }

            return _currentValue++;
        }

        public void UpdateHistory(IEnumerable<GuessItem<int>> history)
        {
            _history = history.Select(g => g.Value).ToArray();
        }
    }
}
