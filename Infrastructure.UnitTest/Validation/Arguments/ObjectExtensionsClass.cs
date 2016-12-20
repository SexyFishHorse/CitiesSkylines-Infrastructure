namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.Validation.Arguments
{
    using System;
    using FluentAssertions;
    using Infrastructure.Validation.Arguments;
    using Ploeh.AutoFixture;
    using Xunit;

    public class ObjectExtensionsClass
    {
        public class ShouldNotBeNullMethod : ObjectExtensionsClass
        {
            private readonly IFixture fixture;

            public ShouldNotBeNullMethod()
            {
                fixture = new Fixture();
            }

            [Fact]
            public void ShouldNotThrowExceptionIfParameterIsNotNull()
            {
                var value = fixture.Create<string>();

                // ReSharper disable once ExpressionIsAlwaysNull
                value.Invoking(x => x.ShouldNotBeNull(fixture.Create<string>()))
                     .ShouldNotThrow();
            }

            [Fact]
            public void ShouldThrowArgumentNullExceptionWhenParameterIsNull()
            {
                var parameterName = fixture.Create<string>();
                string value = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                value.Invoking(x => x.ShouldNotBeNull(parameterName))
                     .ShouldThrow<ArgumentNullException>()
                     .And.ParamName.Should()
                     .Be(parameterName);
            }
        }
    }
}
