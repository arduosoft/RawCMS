using RawCMS.Library.Schema;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Service.Contracts
{
    public interface ICollectionMetadata
    {
        void LoadCollections();

        ICollection<CollectionSchema> GetCollectionSchemas();
    }
}
