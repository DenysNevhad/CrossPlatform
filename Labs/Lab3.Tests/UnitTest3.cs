using Xunit;

public class BucketProblemTests
{
    [Theory]
    [InlineData(10, 8, 4, 4, "3")]
    [InlineData(10, 8, 4, 5, "IMPOSSIBLE")]
    public void TestSolveBuckets(int b1, int b2, int b3, int t, string expectedOutput)
    {
        string actualOutput = Program.SolveBuckets(b1, b2, b3, t);

        Assert.Equal(expectedOutput, actualOutput);
    }
}
