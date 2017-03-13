using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitBasketGame.Players
{
    public class MemoryPlayer : FruitBasketPlayer
    {
        private IEnumerable<int> _possibleValues;

        private List<int> _history;

        public MemoryPlayer(string name) : base(name)
        {
            _history = new List<int>();
        }

        public override void Init(Tuple<int, int> range)
        {
            base.Init(range);
            _possibleValues = Enumerable.Range(_range.Item1, _range.Item2 - _range.Item1).ToArray();
        }

        public override int GetSupposedValue()
        {
            var guess = _possibleValues.Except(_history).RandomElement();

            _history.Add(guess);

            return guess;
        }
    }
}
