using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Duplicate.Removal.Models.EpubContentFiles;

namespace Duplicate.Removal.Models
{
    public class ExtractedEpub : Epub
    {
        public string DirectoryPath { get; }

        public ExtractedEpub(string directoryPath)
        {
            DirectoryPath = directoryPath;

            Mimetype = new TextFile("mimetype");

            MetaInf = ScanDirectory("META-INF");
            Ops = ScanDirectory("OPS");
        }

        private EpubDirectory ScanDirectory(string relativePath)
        {
            var contentFiles = new List<ContentFile>();
            contentFiles.AddRange(GetHtmlFiles(relativePath));

            var directories = GetDirectories(relativePath).ToList();

            return new EpubDirectory(relativePath, contentFiles, directories);
        }

        private IEnumerable<HtmlFile> GetHtmlFiles(string relativeDirPath)
        {
            return GetFiles(relativeDirPath, ".html", x => new HtmlFile(x));
        }

        private IEnumerable<TFile> GetFiles<TFile>(string relativeDirPath, string extension, Func<string, TFile> ctor)
            where TFile : ContentFile
        {
            var subdirPath = Path.Combine(DirectoryPath, relativeDirPath);
            var pattern = $"*{extension}";

            foreach (var filePath in Directory.EnumerateFiles(subdirPath, pattern))
            {
                var fileName = Path.GetFileName(filePath);
                var relativeFilePath = Path.Combine(relativeDirPath, fileName);
                yield return ctor(relativeFilePath);
            }
        }

        private IEnumerable<EpubDirectory> GetDirectories(string relativeDirPath)
        {
            var absoluteDirPath = Path.Combine(DirectoryPath, relativeDirPath);

            foreach (var directoryPath in Directory.EnumerateDirectories(absoluteDirPath))
            {
                var directoryName = Path.GetFileName(directoryPath);
                var relativeSubDirPath = Path.Combine(relativeDirPath, directoryName);
                yield return ScanDirectory(relativeSubDirPath);
            }
        }

        public string GetTextContent(TextFile file)
        {
            var contentFilePath = Path.Combine(DirectoryPath, file.PathInEpub);
            return File.ReadAllText(contentFilePath);
        }
    }
}
