using ATP_Lab.Command;
using ATP_Lab.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ATP_Lab.ViewModels
{
    public class TreeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Operation> _Operation;
        private bool canExecute = true;

        private delegate string SetDataOperationContent();

        #region Collections
        private ObservableCollection<string> operationNameCollection = new ObservableCollection<string>() { "Долбежная", "Отделочная", "Токарная", "Протяжная", "Сверлильная" };
        private ObservableCollection<string> instrument = new ObservableCollection<string>() { "Долото", "Долбежный станок", "Отделка", "Отделка2", "Протяжка", "Протяжка", "Сверло", "Сверло", "Протяжка", "Протяжка" };
        private ObservableCollection<string> equipment = new ObservableCollection<string>() { "Долото", "Долбежный станок", "Отделка", "Отделка2" };
        private ObservableCollection<string> control = new ObservableCollection<string>() { "Долото", "Долбежный станок", "Отделка", "Отделка2" };
        private ObservableCollection<string> action = new ObservableCollection<string>() { "Долото", "Долбежный станок", "Отделка", "Отделка2" };
        private ObservableCollection<string> objectAction = new ObservableCollection<string>() { "Долото", "Долбежный станок", "Отделка", "Отделка2" };
        public ObservableCollection<string> OperationNameCollection
        {
            get
            {
                return operationNameCollection;
            }
        }

        public ObservableCollection<Operation> Operation
        {
            get
            {
                return _Operation;
            }
        }
        public ObservableCollection<string> Instrument
        {
            get
            {
                return instrument;
            }
            set { instrument = value;
                OnPropertyChanged("Instrument");
            }

        }
        public ObservableCollection<string> Equipment
        {
            get
            {
                return equipment;
            }
        }
        public ObservableCollection<string> Control
        {
            get
            {
                return control;
            }
        }
        public ObservableCollection<string> Action
        {
            get
            {
                return action;
            }
        }
        public ObservableCollection<string> ObjectAction
        {
            get
            {
                return objectAction;
            }
        }
        #endregion 

        #region Properties
        private string meansOfTechnologicalEquipment = "empty";
        public string MeansOfTechnologicalEquipment { get { return meansOfTechnologicalEquipment; } set { meansOfTechnologicalEquipment = value; OnPropertyChanged("MeansOfTechnologicalEquipment"); } }
        private string itransitionContent = "empty";
        public string ItransitionContent { get { return itransitionContent; } set { itransitionContent = value; OnPropertyChanged("ItransitionContent"); } }
        private string identity = "0";
        public string Identity { get { return identity; } set { identity = value; OnPropertyChanged("Identity"); } }
        private string sizeOne = "D1";
        public string SizeOne { get { return sizeOne; } set { sizeOne = value; OnPropertyChanged("SizeOne"); } }
        private string sizeOneValue = "0";
        public string SizeOneValue { get { return sizeOneValue; } set { sizeOneValue = value; OnPropertyChanged("SizeOneValue"); } }
        private string sizeTwo = "D2";
        public string SizeTwo { get { return sizeTwo; } set { sizeTwo = value; OnPropertyChanged("SizeTwo"); } }
        private string sizeTwoValue = "0";
        public string SizeTwoValue { get { return sizeTwoValue; } set { sizeTwoValue = value; OnPropertyChanged("SizeTwoValue"); } }
        private string sizeThree = "D3";
        public string SizeThree { get { return sizeThree; } set { sizeThree = value; OnPropertyChanged("SizeThree"); } }
        private string sizeThreeValue = "0";
        public string SizeThreeValue { get { return sizeThreeValue; } set { sizeThreeValue = value; OnPropertyChanged("SizeThreeValue"); } }
        private int selectedIndexInstrumentComboBox = 0;
        public int SelectedIndexInstrumentComboBox
        {
            get { return selectedIndexInstrumentComboBox; }
            set
            {
                selectedIndexInstrumentComboBox = value;
                OnPropertyChanged("SelectedIndexInstrumentComboBox");
            }
        }
        private int selectedIndexeEuipmentComboBox = 0;
        public int SelectedIndexeEuipmentComboBox
        {
            get { return selectedIndexeEuipmentComboBox; }
            set
            {
                selectedIndexeEuipmentComboBox = value;
                OnPropertyChanged("SelectedIndexeEuipmentComboBox");
            }
        }
        private int selectedIndexeControlComboBox = 0;
        public int SelectedIndexeControlComboBox
        {
            get { return selectedIndexeControlComboBox; }
            set
            {
                selectedIndexeControlComboBox = value;
                OnPropertyChanged("SelectedIndexeControlComboBox");
            }
        }
        private int selectedActionControlComboBox = 0;
        public int SelectedActionControlComboBox
        {
            get { return selectedActionControlComboBox; }
            set
            {
                selectedActionControlComboBox = value;
                OnPropertyChanged("SelectedActionControlComboBox");
            }
        }
        private int selectedObjectActionControlComboBox = 0;
        public int SelectedObjectActionControlComboBox
        {
            get { return selectedObjectActionControlComboBox; }
            set
            {
                selectedObjectActionControlComboBox = value;
                OnPropertyChanged("SelectedObjectActionControlComboBox");
            }
        }
        private int selectedMainOperationCombobox = 0;
        public int SelectedMainOperationCombobox
        {
            get { return selectedMainOperationCombobox; }
            set
            {
                selectedMainOperationCombobox = value;
                OnPropertyChanged("SelectedMainOperationCombobox");
            }
        }
        private bool meansChoise = false;
        public bool MeansChoise
        {
            get { return meansChoise; }
            set
            {
                meansChoise = value;
                OnPropertyChanged("SelectedObjectActionControlComboBox");
            }
        }
        private bool transitionChoise = false;
        public bool TransitionChoise
        {
            get { return transitionChoise; }
            set
            {
                transitionChoise = value;
                OnPropertyChanged("TransitionChoise");
            }
        }
        #endregion

        #region Commands
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

        private ICommand addOperationChildrenItemCommand;
        public ICommand AddOperationChildrenItemCommand
        {
            get
            {
                return addOperationChildrenItemCommand;
            }
            set
            {
                addOperationChildrenItemCommand = value;
            }
        }

        private ICommand meansOfTechnologicalEquipmentCommand;
        public ICommand MeansOfTechnologicalEquipmentCommand
        {
            get
            {
                return meansOfTechnologicalEquipmentCommand;
            }
            set
            {
                meansOfTechnologicalEquipmentCommand = value;
            }
        }

        private ICommand transitionCommand;
        public ICommand IransitionCommand
        {
            get
            {
                return transitionCommand;
            }
            set
            {
                transitionCommand = value;
            }
        }

        private ICommand openSettingCommand;
        public ICommand OpenSettingCommand
        {
            get
            {
                return openSettingCommand;
            }
            set
            {
                openSettingCommand = value;}
        }
        #endregion

        public TreeViewModel()
        {


            AddOperationItemCommand = new RelayCommand(Add, param => this.canExecute);
            addOperationChildrenItemCommand = new RelayCommand(AddChildrenOperation, param => this.canExecute);
            MeansOfTechnologicalEquipmentCommand = new RelayCommand(MeansOfTechnologicalEquipmentCreate, param => this.canExecute);
            IransitionCommand = new RelayCommand(ItransitionGenerate, param => this.canExecute);
            OpenSettingCommand = new RelayCommand(OpenSettingWindow, param => this.canExecute);

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

        private void OpenSettingWindow(object obj)
        {
            SettingPage win2 = new SettingPage();
            win2.Show();
        }

        public void Add(object obj)
        {
            ComboBox combo = obj as ComboBox;
            Operation source = new Operation(combo.SelectedItem.ToString());
            _Operation.Add(source);
        }

        public void AddChildrenOperation(object obj)
        {
            TreeView combo = obj as TreeView;
            var s = combo.SelectedItem;
        }

        private void MeansOfTechnologicalEquipmentCreate(object obj)
        {
            AddChildrenOperation(MeansChoise,
                MeansOfTechnologicalEquipment,
                SelectedMainOperationCombobox,
                new SetDataOperationContent(SetMeansContent));
        }
        private void ItransitionGenerate(object obj)
        {
            AddChildrenOperation(TransitionChoise, 
                ItransitionContent, 
                SelectedMainOperationCombobox, 
                new SetDataOperationContent(SetTransitionContent));
        }

        private string SetMeansContent()
        {
            MeansOfTechnologicalEquipment = string.Format(
                  "{0}, {1}, {2}", instrument[SelectedIndexInstrumentComboBox],
                  control[SelectedIndexeControlComboBox],
                  equipment[SelectedIndexeEuipmentComboBox]);
            return MeansOfTechnologicalEquipment;
        
    }
        private string SetTransitionContent()
        {
            ItransitionContent = string.Format(
                 "{0} {1} {2}, {3}={4}, {5}={6}, {7}={8}",
                 action[SelectedActionControlComboBox],
                 objectAction[SelectedObjectActionControlComboBox],
                 Identity,
                 SizeOne, SizeOneValue,
                 SizeTwo, SizeTwoValue,
                 SizeThree, SizeThreeValue
                 );
            return ItransitionContent;
        }

        /// <summary>
        /// Add childe operation in collection
        /// </summary>
        /// <param name="acceptAdd">confirms the addition of</param>
        /// <param name="operationContent">content operation</param>
        /// <param name="index">combobox index, for array operaton</param>
        /// <param name="setData"></param>
        private void AddChildrenOperation(bool acceptAdd, string operationContent,int index, SetDataOperationContent setData)
        {
            if(acceptAdd)
            {
                foreach (var item in _Operation)
                {
                    if (item.Name == operationNameCollection[index])
                    {
                        item.MoreOperation.Add(new Operation(operationContent));
                        setData();
                    }
                }
            }
            else
            {
                setData();
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
