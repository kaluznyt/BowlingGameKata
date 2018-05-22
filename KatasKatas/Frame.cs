namespace BowlingGameKata
{
    public class Frame : IBowlingFrame
    {
        const int MaxPointsPerRoll = 10;

        public Frame NextFrame { get; set; }

        private bool IsLast => NextFrame == null;

        private readonly int _firstRollPoints;

        public int FirstRoll => _firstRollPoints;

        private readonly int _secondRollPoints;

        public int SecondRoll => _secondRollPoints;

        private readonly int _thirdRollPoints;

        public int ThirdRoll => _thirdRollPoints;

        public int FramePoints => _firstRollPoints + _secondRollPoints + _thirdRollPoints;

        public Frame(int firstRollPoints, int secondRollPoints, int thirdRollPoints)
        {
            _firstRollPoints = firstRollPoints;
            _secondRollPoints = secondRollPoints;
            _thirdRollPoints = thirdRollPoints;
        }

        private int Next2Rolls()
        {
            if (IsLast)
            {
                return 0;
            }

            if (this.NextFrame.FrameResultType == FrameResult.Strike)
            {
                return this.NextFrame.FirstRoll + this.NextFrame.SecondRoll + (NextFrame.NextFrame?.FirstRoll ?? 0);
            }
            else
            {
                return this.NextFrame.FirstRoll;
            }
        }

        private int NextRoll()
        {
            return IsLast ? 0 : this.NextFrame.FirstRoll;
        }

        public int Result()
        {
            switch (FrameResultType)
            {
                case FrameResult.Strike:
                    {
                        return this.FramePoints + this.Next2Rolls();
                    }
                case FrameResult.Spare:
                    {
                        return this.FramePoints + this.NextRoll();
                    }
                default:
                    {
                        return this.FramePoints;
                    }
            }
        }

        public FrameResult FrameResultType
        {
            get
            {
                if (this._firstRollPoints == MaxPointsPerRoll)
                {
                    return FrameResult.Strike;
                }

                if (this._firstRollPoints + this._secondRollPoints >= MaxPointsPerRoll)
                {
                    return FrameResult.Spare;
                }

                return FrameResult.Normal;
            }
        }

        public enum FrameResult
        {
            Normal,
            Spare,
            Strike
        }
    }
}