using Xunit;

namespace Duplicate.Removal.Tests.Base
{
    [Collection("Epub extraction")]
    public abstract class EpubExtractionTests
    {
        protected readonly EpubExtractionFixture fixture;

        protected string OutputFolder => fixture.OutputFolder;

        protected EpubExtractionTests(EpubExtractionFixture fixture)
        {
            this.fixture = fixture;
        }
    }
}
