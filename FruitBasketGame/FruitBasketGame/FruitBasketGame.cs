using GuessingGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FruitBasketGame
{
    public class FruitBasketGame : GuessingGame<int>
    {
        private List<GuessItem<int>> _history;

        private DateTime _startTime;

        private TimeSpan _timeLimit;

        private int _attemptsNumber;

        private int _answer;

        public FruitBasketGame(IEnumerable<GuessingPlayer<int>> players, Tuple<int,int> range) : base(players, range)
        {
            _history = new List<GuessItem<int>>();
            _attemptsNumber = _range.Item2 - _range.Item1;
            _timeLimit = new TimeSpan(0,0,0,0,1500);
        }

        public override int GenerateNewValue()
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            _answer = rnd.Next(_range.Item1, _range.Item2);

            return _answer;
        }

        public override string Start()
        {
            var players = Players.ToArray();
            var sessions = new Task<string>[players.Length];
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            _startTime = DateTime.Now;

            for (int i = 0; i < sessions.Length; i++)
            {
                var player = players[i];
                sessions[i] = Task<string>.Factory.StartNew(() => PlayerSession(player, token), token);
            }
            var sessionNumber = Task.WaitAny(sessions);
            tokenSource.Cancel();

            return sessions[sessionNumber].Result;

        }

        public override string PlayerSession(GuessingPlayer<int> player, CancellationToken capturedToken)
        {
            var attemptsNumberExceeded = false;
            var timeExceeded = false;
            int weight;

            player.Init(_range);
            

            while (!(attemptsNumberExceeded || timeExceeded))
            {
                UpdateHistoryForOverseers(ref player);

                weight = player.GetSupposedValue();

                lock (_history)
                {
                    _history.Add(new GuessItem<int>(player.Name, weight));

                    if (weight == _answer)
                    {
                        return String.Format("The winner is {0} with {1} attempts total used", player.Name, _history.Count);
                    }
                }

                if (weight != _answer)
                {
                    Thread.Sleep(Math.Abs(_answer - weight));
                }


                var timeGap = DateTime.Now - _startTime;
                timeExceeded = timeGap >= _timeLimit;

                attemptsNumberExceeded = _history.Count() == _attemptsNumber;

                capturedToken.ThrowIfCancellationRequested();
            }

            return GetFailResult(player, attemptsNumberExceeded);
        }

        private void UpdateHistoryForOverseers(ref GuessingPlayer<int> player)
        {
            if (player is IHistoryOverseer)
            {
                GuessItem<int>[] history;
                lock (_history)
                {
                    history = _history.ToArray();
                }
                    ((IHistoryOverseer)player).UpdateHistory(history);
            }
        }

        private string GetFailResult(GuessingPlayer<int> player, bool attemptsNumberExceeded)
        {
            lock (_history)
            {
                var closestGuess = _history.Aggregate((g, ng) => Math.Abs(g.Value - _answer) < Math.Abs(ng.Value - _answer) ? g : ng);

                var resultMessage = String.Format("The closest guess was {0} by {1}", closestGuess.PlayerName, closestGuess.Value);

                if (attemptsNumberExceeded)
                {
                    return "Attempts number exceeded." + resultMessage;
                }
                else
                {
                    return "Time exceeded." + resultMessage;
                }
            }
        }

    }
}
