using Xunit;

namespace Duplicate.Removal.Tests.Base
{
    [CollectionDefinition("Epub extraction")]
    public class EpubExtractionCollection : ICollectionFixture<EpubExtractionFixture>
    {
    }
}
