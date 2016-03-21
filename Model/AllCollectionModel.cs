using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP_Lab.Model
{
    public class AllCollectionModel
    {

        private string name = string.Empty;
        private IEnumerable<string> collectionContent;

        public AllCollectionModel(string name, IEnumerable<string> collection)
        {
            this.name = name;
            this.collectionContent = collection;
        }

        public IEnumerable<string> CollectionContent
        {
            get { return collectionContent; }
            set { collectionContent = value; }
        }

        public string Name
        {
            get { return name; }
        }

    }
}
