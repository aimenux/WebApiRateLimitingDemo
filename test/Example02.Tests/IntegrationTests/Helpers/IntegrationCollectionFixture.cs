namespace Example02.Tests.IntegrationTests.Helpers;

[CollectionDefinition(CollectionName)]
public class IntegrationCollectionFixture : ICollectionFixture<IntegrationWebApplicationFactory>
{
    public const string CollectionName = nameof(IntegrationCollectionFixture);
}