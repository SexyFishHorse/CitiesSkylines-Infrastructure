namespace SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments
{
    using System;
    using System.Collections;
    using JetBrains.Annotations;

    public static class CollectionExtensions
    {
        [AssertionMethod]
        public static void ShouldNotBeNullOrEmpty(
            this ICollection collection,
            [InvokerParameterName] [NotNull] string parameterName)
        {
            collection.ShouldNotBeNull(parameterName);

            if (collection.Count < 1)
            {
                throw new ArgumentException("Collection may not be empty", parameterName);
            }
        }
    }
}
