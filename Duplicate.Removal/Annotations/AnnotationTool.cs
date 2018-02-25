using System.Collections.Generic;
using Duplicate.Removal.Models;

namespace Duplicate.Removal.Annotations
{
    public class AnnotationTool
    {
        private readonly ExtractedEpub extractedEpub;

        public AnnotationTool(ExtractedEpub extractedEpub)
        {
            this.extractedEpub = extractedEpub;
        }

        public IEnumerable<Annotation> GetAnnotations()
        {
            throw new System.NotImplementedException();
        }
    }
}
