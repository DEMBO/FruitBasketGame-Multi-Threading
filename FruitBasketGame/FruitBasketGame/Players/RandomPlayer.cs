using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitBasketGame.Players
{
    public class RandomPlayer : FruitBasketPlayer
    {
        public RandomPlayer(string name) : base(name)
        {
        }

        public override int GetSupposedValue()
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            
            return rnd.Next(_range.Item1, _range.Item2);
        }
    }
}
