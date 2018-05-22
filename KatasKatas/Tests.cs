using System.Linq;
using Xunit;

namespace BowlingGameKata
{

    public class Tests
    {
        [Theory]
        [InlineData("[0,0],[0,0],[0,0],[0,0],[0,0],[0,0],[0,0],[0,0],[0,0],[0,0]", 10)]
        [InlineData("[0,1],[0,0],[0,0],[0,0],[0,0],[0,0],[0,0],[0,55]", 8)]
        [InlineData("[0,1]", 1)]
        [InlineData("[0,1],[0,12]", 2)]
        public void BowlingGameFrameStringParserShouldReturnCorrectFrameCollection(string frameString, int numElements)
        {
            IBowlingFrameParser parser = new BowlingFrameStringParser();
            var output = parser.Parse(frameString);

            Assert.NotEmpty(output);
            Assert.Equal(numElements, output.Count);
        }

        [Theory]
        [InlineData("[1,3]", 4)]
        [InlineData("[3,6]", 9)]
        [InlineData("[4,6],[4,1]", 14)]
        [InlineData("[4,5],[1,1]", 9)]
        [InlineData("[1,9],[1,1]", 11)]
        [InlineData("[10,0],[10,0],[10,0]", 30)]
        public void BowlingFrameShouldReturnCorrectResultForProvidedFrameString(string frameString, int expectedResult)
        {
            IBowlingFrameParser parser = new BowlingFrameStringParser();
            var frames = parser.Parse(frameString);

            var firstFrame = frames.ToList()[0];

            Assert.Equal(expectedResult, firstFrame.Result());
        }

        [Theory]
        [InlineData("[0,0],[0,0],[0,0],[0,0],[0,0],[0,0],[0,0],[0,0],[0,0],[0,0]", 0)]
        [InlineData("[3,3],[3,3],[3,3],[3,3],[3,3],[3,3],[3,3],[3,3],[3,3],[3,3]", 60)]
        [InlineData("[4,6],[4,6],[4,6],[4,6],[4,6],[4,6],[4,6],[4,6],[4,6],[4,6,4]", 140)]
        [InlineData("[10,0],[10,0],[10,0],[10,0],[10,0],[10,0],[10,0],[10,0],[10,0],[0,0]", 240)]
        [InlineData("[10,0],[10,0],[10,0],[10,0],[10,0],[10,0],[10,0],[10,0],[10,0],[10,10,10]", 300)]
        [InlineData("[10,0],[10,0],[10,10,3]", 83)]
        public void BowlingGameShouldReturnCorrectResultForProvidedFrameString(string frameString, int expectedResult)
        {
            IBowlingFrameParser parser = new BowlingFrameStringParser();
            var frames = parser.Parse(frameString);

            IBowlingGame game = new Game(frames);

            Assert.Equal(expectedResult, game.Result());
        }
    }
}
