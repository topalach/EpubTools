using Duplicate.Removal.Models.EpubContentFiles;

namespace Duplicate.Removal.Models
{
    public class Epub
    {
        public TextFile Mimetype { get; protected set; }
        public EpubDirectory MetaInf { get; protected set; }
        public EpubDirectory Ops { get; protected set; }
    }
}
