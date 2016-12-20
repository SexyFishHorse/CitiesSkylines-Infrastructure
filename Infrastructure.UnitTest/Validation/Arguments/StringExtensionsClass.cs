namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.Validation.Arguments
{
    using System;
    using FluentAssertions;
    using Infrastructure.Validation.Arguments;
    using Ploeh.AutoFixture;
    using Xunit;

    public class StringExtensionsClass
    {
        public class ShouldNotBeNullOrEmptyMethod : StringExtensionsClass
        {
            private readonly IFixture fixture;

            public ShouldNotBeNullOrEmptyMethod()
            {
                fixture = new Fixture();
            }

            [Fact]
            public void ShouldNotThrowExceptionIfStringIsNotEmpty()
            {
                var value = fixture.Create<string>();

                value.Invoking(x => x.ShouldNotBeNullOrEmpty(fixture.Create<string>())).ShouldNotThrow();
            }

            [Fact]
            public void ShouldThrowArgumentExceptionIfStringIsEmpty()
            {
                var parameterName = fixture.Create<string>();
                var value = string.Empty;

                value.Invoking(x => x.ShouldNotBeNullOrEmpty(parameterName))
                     .ShouldThrow<ArgumentException>()
                     .And.ParamName.Should()
                     .Be(parameterName);
            }

            [Fact]
            public void ShouldThrowArgumentNullExceptionIfValueIsNull()
            {
                var parameterName = fixture.Create<string>();
                string value = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                value.Invoking(x => x.ShouldNotBeNullOrEmpty(parameterName))
                     .ShouldThrow<ArgumentNullException>()
                     .And.ParamName.Should()
                     .Be(parameterName);
            }
        }
    }
}
