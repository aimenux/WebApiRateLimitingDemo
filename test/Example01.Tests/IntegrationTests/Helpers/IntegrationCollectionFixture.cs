namespace Example01.Tests.IntegrationTests.Helpers;

[CollectionDefinition(CollectionName)]
public sealed class IntegrationCollectionFixture : ICollectionFixture<IntegrationWebApplicationFactory>
{
    public const string CollectionName = nameof(IntegrationCollectionFixture);
}