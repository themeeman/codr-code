using Codr.Data.Queries;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using System;

namespace Codr.Data {
    public static class DocumentStoreHolder {
        private static readonly Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(() => {
            var store = new DocumentStore() {
                Urls = new[] {"http://localhost:6969/" },
                Database = "Codr"
            }.Initialize();
            
            IndexCreation.CreateIndexes(typeof(Users_Emails).Assembly, store);
            return store;
        });

        public static IDocumentStore Store => store.Value;
    }
}
