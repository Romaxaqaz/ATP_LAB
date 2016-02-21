using ATP_Lab.Command;
using ATP_Lab.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ATP_Lab.ViewModels
{
    public class TreeViewModel
    {
        private ObservableCollection<Operation> _Operation;
        private bool canExecute = true;
        public List<string> operationNameCollection;

        public List<string> OperationNameCollection { get { return operationNameCollection; } }
          

        public TreeViewModel()
        {
            operationNameCollection =
            new List<string>() { "Долбежная", "Отделочная", "Токарная", "Протяжная", "Сверлильная" };
            addOperationItemCommand = new RelayCommand(Add, param => this.canExecute);

            _Operation = new ObservableCollection<Operation>();

            Operation source = new Operation("Операция 1");
            _Operation.Add(source);
            source.MoreOperation.Add(new Operation("Item 1"));
            source.MoreOperation.Add(new Operation("Item 2"));
            source.MoreOperation.Add(new Operation("Item 3"));
            source.MoreOperation.Add(new Operation("Item 4"));

            Operation source2 = new Operation("Операция 2");
            _Operation.Add(source2);
            source2.MoreOperation.Add(new Operation("Item 1"));
            source2.MoreOperation.Add(new Operation("Item 2"));
            source2.MoreOperation.Add(new Operation("Item 3"));
            source2.MoreOperation.Add(new Operation("Item 4"));
        }

        public ObservableCollection<Operation> Operation
        {
            get
            {
                return _Operation;
            }
        }

        private ICommand addOperationItemCommand;
        public ICommand AddOperationItemCommand
        {
            get
            {
                return addOperationItemCommand;
            }
            set
            {
                addOperationItemCommand = value;
            }
        }

        public void Add(object obj)
        {
            ComboBox combo = obj as ComboBox;
            Operation source = new Operation(combo.SelectedItem.ToString());
            _Operation.Add(source);
            
        }
    }
}
