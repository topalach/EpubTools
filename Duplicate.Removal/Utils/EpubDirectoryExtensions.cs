using System.Collections.Generic;
using System.Linq;
using Duplicate.Removal.Models.EpubContentFiles;

namespace Duplicate.Removal.Utils
{
    public static class EpubDirectoryExtensions
    {
        public static TContentFile SingleByName<TContentFile>(this IEnumerable<TContentFile> files, string fileName)
            where TContentFile : ContentFile
        {
            return files.Single(x => x.PathInEpub.EndsWith(fileName));
        }
    }
}
