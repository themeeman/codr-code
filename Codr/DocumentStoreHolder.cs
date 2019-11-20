using Raven.Client.Documents;
using System;

namespace Codr {
    public static class DocumentStoreHolder {
        private static readonly Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(() => {
            var store = new DocumentStore() {
                Urls = new[] {"http://localhost:6969/" },
                Database = "Codr"
            }.Initialize();
            return store;
        });

        public static IDocumentStore Store => store.Value;
    }
}
