namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.Validation.Arguments
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Infrastructure.Validation.Arguments;
    using Ploeh.AutoFixture;
    using Xunit;

    public class EnumerableExtensionsClass
    {
        public class ShouldNotBeNullOrEmptyMethod : EnumerableExtensionsClass
        {
            private readonly IFixture fixture;

            public ShouldNotBeNullOrEmptyMethod()
            {
                fixture = new Fixture();
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenCollectionIsNotEmpty()
            {
                var list = fixture.CreateMany<string>();

                list.Invoking(x => x.ShouldNotBeNullOrEmpty(fixture.Create<string>())).ShouldNotThrow();
            }

            [Fact]
            public void ShouldThrowArgumentExceptionWhenCollectionIsEmpty()
            {
                var parameterName = fixture.Create<string>();
                var list = new List<string>();

                list.Invoking(x => x.ShouldNotBeNullOrEmpty(parameterName))
                    .ShouldThrow<ArgumentException>()
                    .And.ParamName.Should()
                    .Be(parameterName);
            }

            [Fact]
            public void ShouldThrowArgumentNullExceptionWhenCollectionIsNull()
            {
                var parameterName = fixture.Create<string>();
                List<string> list = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                list.Invoking(x => x.ShouldNotBeNullOrEmpty(parameterName))
                    .ShouldThrow<ArgumentNullException>()
                    .And.ParamName.Should()
                    .Be(parameterName);
            }
        }
    }
}
