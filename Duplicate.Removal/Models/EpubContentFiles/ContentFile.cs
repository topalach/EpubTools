namespace Duplicate.Removal.Models.EpubContentFiles
{
    public abstract class ContentFile
    {
        public string PathInEpub { get; }

        protected ContentFile(string pathInEpub)
        {
            PathInEpub = pathInEpub;
        }
    }
}
