using System.Linq;
using Duplicate.Removal;
using Duplicate.Removal.Utils;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            const string EpubPath = "./Input/chlopi.epub";
            const string OutputFolder = "./Output";

            var accessor = new EpubAccessor(EpubPath);
            var extractedEpub = accessor.ExtractToFolder(OutputFolder);

            var mimeType = extractedEpub.Mimetype;
            var mimeTypeContent = extractedEpub.GetTextContent(mimeType);

            var annotations = extractedEpub.Ops.HtmlFiles.SingleByName("annotations.html");
            var annotationsContent = extractedEpub.GetTextContent(annotations);

            accessor.Compress(extractedEpub, OutputFolder, true);
        }
    }
}
