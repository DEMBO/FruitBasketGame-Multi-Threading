using GuessingGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitBasketGame.Players
{
    public abstract class FruitBasketPlayer : GuessingPlayer<int>
    {
        public FruitBasketPlayer(string name) : base(name)
        {
        }
    }
}
