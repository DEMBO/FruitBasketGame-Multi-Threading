using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame
{
    public class GuessItem<T>
    {
        public GuessItem(string playerName, T value)
        {
            PlayerName = playerName;
            Value = value;
        }

        public string PlayerName { get; private set; }

        public T Value { get; private set; }
    }
}
