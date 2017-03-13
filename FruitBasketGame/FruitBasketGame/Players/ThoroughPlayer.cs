using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitBasketGame.Players
{
    public class ThoroughPlayer : FruitBasketPlayer
    {
        private int _currentValue;

        public ThoroughPlayer(string name) : base(name)
        {
        }

        public override void Init(Tuple<int, int> range)
        {
            base.Init(range);
            _currentValue = range.Item1;
        }

        public override int GetSupposedValue()
        {
            return _currentValue++;
        }
    }
}
