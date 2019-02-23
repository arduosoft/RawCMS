using RawCMS.Library.Schema;
using System.Collections.Generic;

namespace RawCMS.Library.Service.Contracts
{
    public interface ICollectionMetadata
    {
        void LoadCollections();

        ICollection<CollectionSchema> GetCollectionSchemas();
    }
}