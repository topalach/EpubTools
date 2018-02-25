using System.IO;
using Duplicate.Removal.Tests.Base;
using Xunit;

namespace Duplicate.Removal.Tests
{
    public class EpubAccessorTests : EpubExtractionTests
    {
        private const string InputFilePath = ".\\Input\\test_ebook.epub";

        public EpubAccessorTests(EpubExtractionFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void ExtractsEpub()
        {
            var accessor = new EpubAccessor(InputFilePath);
            var extractedEpub = accessor.ExtractToFolder(fixture.OutputFolder);

            const string ExpectedExtractedPath = ".\\Output\\test_ebook";

            Assert.Equal(ExpectedExtractedPath, extractedEpub.DirectoryPath);
            Assert.True(Directory.Exists(ExpectedExtractedPath));
        }

        [Fact]
        public void CompressesEpub()
        {
            var accessor = new EpubAccessor(InputFilePath);
            var extractedEpub = accessor.ExtractToFolder(fixture.OutputFolder);

            accessor.Compress(extractedEpub, fixture.OutputFolder, true);

            const string ExpectedCompressedPath = ".\\Output\\test_ebook.epub";
            Assert.True(File.Exists(ExpectedCompressedPath));

            var inputFileSize = new FileInfo(InputFilePath).Length;
            var outputFileSize = new FileInfo(ExpectedCompressedPath).Length;

            AssertAround(inputFileSize, outputFileSize, 0.1m);
        }

        private void AssertAround(long expected, long actual, decimal precisionRate)
        {
            var threshold = (long)(expected * precisionRate);
            var lowerThreshold = expected - threshold;
            var upperThreshold = expected + threshold;

            Assert.True(actual >= lowerThreshold);
            Assert.True(actual <= upperThreshold);
        }
    }
}
