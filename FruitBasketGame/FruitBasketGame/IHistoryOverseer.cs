using GuessingGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitBasketGame
{
    public interface IHistoryOverseer
    {
        void UpdateHistory(IEnumerable<GuessItem<int>> history);
    }
}
