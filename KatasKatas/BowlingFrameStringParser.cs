using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BowlingGameKata
{
    public class BowlingFrameStringParser : IBowlingFrameParser
    {
        private const string FirstRoll = "firstAttempt";
        private const string SecondRoll = "secondAttempt";
        private const string ThirdRoll = "thirdAttempt";

        readonly string parseFrameRegex = $@"(\[(?<{FirstRoll}>\d+),(?<{SecondRoll}>\d+)(,(?<{ThirdRoll}>\d+))*\])+";

        public ICollection<Frame> Parse(string frameString)
        {
            var frameRegex = new Regex(parseFrameRegex);

            if (!frameRegex.IsMatch(frameString))
            {
                throw new ArgumentException($"{nameof(frameString)} is invalid bowling frame definition string.");
            }

            var matches = frameRegex.Matches(frameString);

            var frames = new List<Frame>();

            foreach (Match match in matches)
            {
                var firstAttemptPoints = int.Parse(match.Groups[FirstRoll].Value);
                var secondAttemptPoints = int.Parse(match.Groups[SecondRoll].Value);

                var thirdAttemptMatch = match.Groups[ThirdRoll];
                var thirdAttemptPoints = 0;
                
                if (thirdAttemptMatch != null && !string.IsNullOrWhiteSpace(thirdAttemptMatch.Value))
                {
                    thirdAttemptPoints = int.Parse(thirdAttemptMatch.Value);
                }

                var frame = new Frame(firstAttemptPoints, secondAttemptPoints, thirdAttemptPoints);

                frames.Add(frame);
            }

            for (int frameIndex = 0; frameIndex < matches.Count - 1; frameIndex++)
            {
                frames[frameIndex].NextFrame = frames[frameIndex + 1];
            }

            return frames;
        }
    }
}