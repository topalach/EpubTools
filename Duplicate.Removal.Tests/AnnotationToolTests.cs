using System.Linq;
using Duplicate.Removal.Annotations;
using Duplicate.Removal.Tests.Base;
using Xunit;

namespace Duplicate.Removal.Tests
{
    public class AnnotationToolTests : EpubExtractionTests
    {
        private const string InputFilePath = ".\\Input\\AnnotationTool.epub";

        public AnnotationToolTests(EpubExtractionFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void GetsAllAnnotations()
        {
            var accessor = new EpubAccessor(InputFilePath);
            var extractedEpub = accessor.ExtractToFolder(fixture.OutputFolder);

            var annotationTool = new AnnotationTool(extractedEpub);
            var annotations = annotationTool.GetAnnotations();

            Assert.Equal(5, annotations.Count());
        }
    }
}
