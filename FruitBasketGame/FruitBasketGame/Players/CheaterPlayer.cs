using GuessingGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitBasketGame.Players
{
    public class CheaterPlayer : FruitBasketPlayer, IHistoryOverseer
    {
        private IEnumerable<int> _possibleValues;

        private int[] _history;

        public CheaterPlayer(string name) : base(name)
        {
        }

        public override void Init(Tuple<int, int> range)
        {
            base.Init(range);
            _possibleValues = Enumerable.Range(_range.Item1, _range.Item2 - _range.Item1).ToArray();
        }

        public override int GetSupposedValue()
        {
            return _possibleValues.Except(_history).RandomElement();
        }

        public void UpdateHistory(IEnumerable<GuessItem<int>> history)
        {
            _history = history.Select(g => g.Value).ToArray();
        }
    }
}
