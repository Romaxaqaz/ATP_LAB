using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP_Lab.Model
{
    public class Operation : INotifyPropertyChanged
    {

        private ObservableCollection<Operation> moreOperation = new ObservableCollection<Operation>();

        public Operation()
        {
            Name = String.Empty;
        }

        public Operation(string name)
        {
            Name = name;
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public ObservableCollection<Operation> MoreOperation
        {
            get
            {
                return moreOperation;
            }
            set
            {
                moreOperation = value;
                OnPropertyChanged("MoreOperation");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string Property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
            }
        }


    }
}
