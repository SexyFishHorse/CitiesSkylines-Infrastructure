namespace SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments
{
    using System;
    using JetBrains.Annotations;

    public static class ObjectExtensions
    {
        [AssertionMethod]
        public static void ShouldNotBeNull(this object value, [NotNull] [InvokerParameterName] string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}
