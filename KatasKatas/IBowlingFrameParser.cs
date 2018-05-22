using System.Collections.Generic;

namespace BowlingGameKata
{
    public interface IBowlingFrameParser
    {
        ICollection<Frame> Parse(string frameString);
    }
}