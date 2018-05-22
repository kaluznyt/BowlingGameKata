using System.Collections.Generic;

namespace BowlingGameKata
{
    public class Game : IBowlingGame
    {
        private readonly ICollection<Frame> _frames;

        public Game(ICollection<Frame> frames)
        {
            _frames = frames;
        }

        public int Result()
        {
            var result = 0;

            foreach (var frame in _frames)
            {
                result += frame.Result();
            }

            return result;
        }
    }
}