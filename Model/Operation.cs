using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP_Lab.Model
{
    public class Operation
    {

        private ObservableCollection<Operation> operationContent;

        public Operation()
        {
            Name = String.Empty;
        }

        public Operation(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            set;
        }

        public Operation Parent
        {
            get;
            set;
        }

        public ObservableCollection<Operation> MoreOperation
        {
            get
            {
                if (operationContent == null)
                {
                    operationContent = new ObservableCollection<Operation>();
                    operationContent.CollectionChanged += new NotifyCollectionChangedEventHandler(OnMoreStuffChanged);
                }

                return operationContent;
            }
        }

        private void OnMoreStuffChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Operation operation = (Operation)e.NewItems[0];
                operation.Parent = this;
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                Operation operation = (Operation)e.OldItems[0];
                if (operation.Parent == this)
                {
                    operation.Parent = null;
                }
            }
        }

        
    }
}
