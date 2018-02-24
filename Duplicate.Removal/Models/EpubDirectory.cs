using System.Collections.Generic;
using System.Linq;
using Duplicate.Removal.Models.EpubContentFiles;

namespace Duplicate.Removal.Models
{
    public class EpubDirectory
    {
        public string PathInEpub { get; }

        public List<ContentFile> ContentFiles { get; }
        public List<EpubDirectory> Directories { get; }

        public IEnumerable<HtmlFile> HtmlFiles => ContentFiles.OfType<HtmlFile>();

        public EpubDirectory(string pathInEpub, List<ContentFile> contentFiles, List<EpubDirectory> directories)
        {
            PathInEpub = pathInEpub;
            ContentFiles = contentFiles;
            Directories = directories;
        }
    }
}
