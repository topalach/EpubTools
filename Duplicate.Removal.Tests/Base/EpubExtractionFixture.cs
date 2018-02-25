using System;
using System.IO;

namespace Duplicate.Removal.Tests.Base
{
    public class EpubExtractionFixture : IDisposable
    {
        public readonly string OutputFolder = ".\\Output";

        public EpubExtractionFixture()
        {
            ClearOutputDirectory();
        }

        public void Dispose()
        {
            ClearOutputDirectory();
        }

        private void ClearOutputDirectory()
        {
            if (Directory.Exists(OutputFolder))
            {
                Directory.Delete(OutputFolder, true);
            }
        }
    }
}
