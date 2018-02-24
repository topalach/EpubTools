using System;
using System.IO;
using System.IO.Compression;
using Duplicate.Removal.Models;

namespace Duplicate.Removal
{
    public class EpubAccessor
    {
        private readonly string _sourcePath;

        public EpubAccessor(string sourcePath)
        {
            if (!File.Exists(sourcePath))
            {
                throw new FileNotFoundException();
            }

            var extension = Path.GetExtension(sourcePath);

            if (!extension.Equals(".epub"))
            {
                throw new NotSupportedException("The provided file must be an epub.");
            }

            _sourcePath = sourcePath;
        }

        public ExtractedEpub ExtractToFolder(string folderPath)
        {
            var outputFolderName = Path.GetFileNameWithoutExtension(_sourcePath);
            var outputPath = Path.Combine(folderPath, outputFolderName);

            if (Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath, true);
            }

            Directory.CreateDirectory(outputPath);

            ZipFile.ExtractToDirectory(_sourcePath, outputPath);

            return new ExtractedEpub(outputPath);
        }

        public void Compress(ExtractedEpub epub, string outputFolder, bool forceOverwrite = false)
        {
            var inputFolder = epub.DirectoryPath;

            var folderName = Path.GetFileName(inputFolder);
            var outputZipName = $"{folderName}.epub";

            var outputPath = Path.Combine(outputFolder, outputZipName);

            if (forceOverwrite && File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }

            ZipFile.CreateFromDirectory(inputFolder, outputPath, CompressionLevel.NoCompression, false);
        }
    }
}
