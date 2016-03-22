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

        public ObservableCollection<Operation> MoreOperation
        {
            get
            {
                if (operationContent == null)
                {
                    operationContent = new ObservableCollection<Operation>();
                  
                }

                return operationContent;
            }
        }

        

        
    }
}
