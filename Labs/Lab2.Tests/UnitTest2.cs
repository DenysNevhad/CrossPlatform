using System;
using Xunit;

public class ProgramTests
{
    [Fact]
    public void CountDecodingWays_ValidInput_ReturnsCorrectResult()
    {
        string input = "1025";
        long result = Program.CountDecodingWays(input);
        Assert.Equal(4, result);

        input = "33222";
        result = Program.CountDecodingWays(input);
        Assert.Equal(8, result);
    }
}
