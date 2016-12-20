namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.Validation.Arguments
{
    using System;
    using FluentAssertions;
    using Infrastructure.Validation.Arguments;
    using Ploeh.AutoFixture;
    using Xunit;

    public class IntExtensionsClass
    {
        public class ShouldBeGreaterThanZeroMethod : IntExtensionsClass
        {
            private readonly IFixture fixture;

            public ShouldBeGreaterThanZeroMethod()
            {
                fixture = new Fixture();
            }

            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            [InlineData(-2)]
            [InlineData(-3)]
            [InlineData(-100)]
            [InlineData(int.MinValue)]
            public void ShouldThrowArgumentOutOfRangeExceptionWhenValueIsLessThanOne(int value)
            {
                var parameterName = fixture.Create<string>();
                value.Invoking(x => x.ShouldBeGreaterThanZero(parameterName))
                     .ShouldThrow<ArgumentOutOfRangeException>()
                     .And.ParamName.Should()
                     .Be(parameterName);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(100)]
            [InlineData(int.MaxValue)]
            public void ShouldNotThrowExceptionIfValueIsGreaterThanZero(int value)
            {
                value.Invoking(x => x.ShouldBeGreaterThanZero(fixture.Create<string>())).ShouldNotThrow();
            }
        }
    }
}
