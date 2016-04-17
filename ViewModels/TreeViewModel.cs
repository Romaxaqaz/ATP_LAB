using ATP_Lab.Command;
using ATP_Lab.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
        #region Constructor
        public TreeViewModel()
        {
            AddOperationItemCommand = new RelayCommand(Add, param => this.canExecute);
            addOperationChildrenItemCommand = new RelayCommand(AddChildrenOperation, param => this.canExecute);
            MeansOfTechnologicalEquipmentCommand = new RelayCommand(MeansOfTechnologicalEquipmentCreate, param => this.canExecute);
            IransitionCommand = new RelayCommand(ItransitionGenerate, param => this.canExecute);
            OpenSettingCommand = new RelayCommand(OpenSettingWindow, param => this.canExecute);
            SaveMainCollection = new RelayCommand(SaveProgrammProgress, param => this.canExecute);
            DeleteMainOperationCommand = new RelayCommand(DeleteMainOperation, param => this.canExecute);

            allCollection.Add(new AllCollectionModel("MainOperation", OperationNameCollection));
            allCollection.Add(new AllCollectionModel("Equipment", Equipment));
            allCollection.Add(new AllCollectionModel("Control", Control));
            allCollection.Add(new AllCollectionModel("Action", Action));
            allCollection.Add(new AllCollectionModel("ObjectAction", ObjectAction));
            allCollection.Add(new AllCollectionModel("Instrument", Instrument));

            SetCollection();
            operationID = ReturnLastNumberOutFile(SettingFile);
            NewOperationCollection = ReturnCollectionOutFile<string>(NewCollection);
            Operations = ReturnCollectionOutFile<Operation>(ProgrammProgress);
            if (_Operation == null)
                _Operation = new ObservableCollection<Model.Operation>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Opent setting window
        /// </summary>
        /// <param name="obj"></param>
        private void OpenSettingWindow(object obj)
        {
            SettingPage settingWindow = new SettingPage();
            settingWindow.Show();
        }

        /// <summary>
        /// Add operation
        /// </summary>
        /// <param name="obj"></param>
        public void Add(object obj)
        {
            ComboBox combo = obj as ComboBox;
            string operationName = string.Format("Операция {0} - {1}", operationID, combo.SelectedItem);
            NewOperationCollection.Add(operationName);
            operationID++;
            Operation source = new Operation(operationName);
            _Operation.Add(source);
            System.IO.File.WriteAllText("Setting/numberSetting" + ".txt", operationID.ToString());
        }

        /// <summary>
        /// Add children operation
        /// </summary>
        /// <param name="obj"></param>
        public void AddChildrenOperation(object obj)
        {
            TreeView combo = obj as TreeView;
            var comboBoxItem = combo.SelectedItem;
        }

        /// <summary>
        /// Add to collection means operation
        /// </summary>
        /// <param name="obj"></param>
        private void MeansOfTechnologicalEquipmentCreate(object obj)
        {
            AddChildrenOperation(MeansChoise,
                MeansOfTechnologicalEquipment,
                SelectedIndexNewOperationComboBox,
                new SetDataOperationContent(SetMeansContent));
        }

        /// <summary>
        /// Add itransition operation
        /// </summary>
        /// <param name="obj"></param>
        private void ItransitionGenerate(object obj)
        {
            AddChildrenOperation(TransitionChoise,
                ItransitionContent,
                SelectedIndexNewOperationComboBox,
                new SetDataOperationContent(SetTransitionContent));
        }

        /// <summary>
        /// Save programm params
        /// </summary>
        /// <param name="obj"></param>
        private void SaveProgrammProgress(object obj)
        {
            var json = JsonConvert.SerializeObject(_Operation);
            var newCollection = JsonConvert.SerializeObject(NewOperationCollection);
            SaveToFile(ProgrammProgress, json);
            SaveToFile(NewCollection, newCollection);
            MessageBox.Show("Saved");
        }

        /// <summary>
        /// Set means param
        /// </summary>
        /// <returns></returns>
        private string SetMeansContent()
        {
            MeansOfTechnologicalEquipment = string.Format(
                  "{0}, {1}, {2}", instrument[SelectedIndexInstrumentComboBox],
                  control[SelectedIndexeControlComboBox],
                  equipment[SelectedIndexeEuipmentComboBox]);
            return MeansOfTechnologicalEquipment;

        }

        /// <summary>
        /// Set ITransition params
        /// </summary>
        /// <returns></returns>
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
        /// Set collection from files
        /// </summary>
        public void SetCollection()
        {
            foreach (var item in AllCollection)
            {
                switch (item.Name)
                {
                    case "Instrument":
                        Instrument = ReturnCollectionOutFile<string>(item.Name);
                        break;
                    case "Equipment":
                        Equipment = ReturnCollectionOutFile<string>(item.Name);
                        break;
                    case "Control":
                        Control = ReturnCollectionOutFile<string>(item.Name);
                        break;
                    case "Action":
                        Action = ReturnCollectionOutFile<string>(item.Name);
                        break;
                    case "ObjectAction":
                        ObjectAction = ReturnCollectionOutFile<string>(item.Name);
                        break;
                }

            }
        }

        /// <summary>
        /// Get collection from file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">File name</param>
        /// <returns></returns>
        private ObservableCollection<T> ReturnCollectionOutFile<T>(string name)
        {
            string text = "";
            using (StreamReader streamReader = new StreamReader(name + ".txt", Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            var result = JsonConvert.DeserializeObject<ObservableCollection<T>>(text);
            return result;
        }

        /// <summary>
        /// Get last number collection item
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int ReturnLastNumberOutFile(string name)
        {
            string text = "";
            using (StreamReader streamReader = new StreamReader(name + ".txt", Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            return int.Parse(text);
        }

        /// <summary>
        /// Save to file
        /// </summary>
        /// <param name="name">file name</param>
        /// <param name="content"></param>
        private void SaveToFile(string name, string content)
        {
            System.IO.File.WriteAllText(name + ".txt", content);
        }


        private void DeleteMainOperation(object obj)
        {
            Operations.RemoveAt(MainOperationIndex);
        }

        /// <summary>
        /// Add childe operation in collection
        /// </summary>
        /// <param name="acceptAdd">confirms the addition of</param>
        /// <param name="operationContent">content operation</param>
        /// <param name="index">combobox index, for array operaton</param>
        /// <param name="setData"></param>
        private void AddChildrenOperation(bool acceptAdd, string operationContent, int index, SetDataOperationContent setData)
        {
            if (acceptAdd)
            {
                foreach (var item in Operations)
                {
                    if (item.Name == NewOperationCollection[index])
                    {
                        setData();
                        item.MoreOperation.Add(new Operation(operationContent));
                    }
                }
            }
            else
            {
                setData();
            }

        }

        #endregion

        #region Constants
        private const string ProgrammProgress = "Setting/ProgrammProgress";
        private const string SettingFile = "Setting/numberSetting";
        private const string NewCollection = "CollectionsJSon/NewCollection";
        #endregion

        #region Delegate
        private delegate string SetDataOperationContent();
        #endregion

        #region Collections
        private ObservableCollection<Operation> _Operation = new ObservableCollection<Model.Operation>();
        private ObservableCollection<string> instrument;
        private ObservableCollection<string> equipment;
        private ObservableCollection<string> control;
        private ObservableCollection<string> action;
        private ObservableCollection<string> objectAction;
        private ObservableCollection<string> newOperationCollection = new ObservableCollection<string>();
        private ObservableCollection<AllCollectionModel> allCollection = new ObservableCollection<AllCollectionModel>();
        private ObservableCollection<string> operationNameCollection = new ObservableCollection<string>() { "Долбежная", "Отделочная", "Токарная", "Протяжная", "Сверлильная" };

        public ObservableCollection<string> OperationNameCollection
        {
            get
            {
                return operationNameCollection;
            }
        }
        public ObservableCollection<string> Instrument
        {
            get
            {
                return instrument;
            }
            set
            {
                instrument = value;
                OnPropertyChanged("Instrument");
            }

        }
        public ObservableCollection<string> Equipment
        {
            get
            {
                return equipment;
            }
            set
            {
                equipment = value;
                OnPropertyChanged("Equipment");
            }
        }
        public ObservableCollection<string> Control
        {
            get
            {
                return control;
            }
            set
            {
                control = value;
                OnPropertyChanged("Control");
            }
        }
        public ObservableCollection<string> Action
        {
            get
            {
                return action;
            }
            set
            {
                action = value;
                OnPropertyChanged("Action");
            }
        }
        public ObservableCollection<string> ObjectAction
        {
            get
            {
                return objectAction;
            }
            set
            {
                objectAction = value;
                OnPropertyChanged("ObjectAction");
            }
        }
        public ObservableCollection<Operation> Operations
        {
            get
            {
                return _Operation;
            }
            set
            {
                _Operation = value;
                OnPropertyChanged("Operation");
            }
        }
        public ObservableCollection<string> NewOperationCollection
        {
            get
            {
                return newOperationCollection;
            }
            set
            {
                newOperationCollection = value;
                OnPropertyChanged("NewOperationCollection");
            }

        }
        public ObservableCollection<AllCollectionModel> AllCollection
        {
            get
            {
                return allCollection;
            }
        }
        #endregion

        #region Properties
        private bool canExecute = true;
        private int operationID = 1;
        public int NowSelectedIndex { get; set; } = 1;
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
        private string editNameTextPopUp;
        public string EditNameTextPopUp { get { return editNameTextPopUp; } set { editNameTextPopUp = value; OnPropertyChanged("EditNameTextPopUp"); } }


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
        private int mainOperationIndex;
        public int MainOperationIndex
        {
            get { return mainOperationIndex; }
            set
            {
                NowSelectedIndex = value;
                mainOperationIndex = value;
                OnPropertyChanged("MainOperationIndex");
            }
        }
        private int childOperationIndex;
        public int ChildOperationIndex
        {
            get { return childOperationIndex; }
            set
            {
                childOperationIndex = value;
                OnPropertyChanged("ChildOperationIndex");
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


        private bool openEditPopUpBool = false;
        public bool OpenEditPopUpBool
        {
            get { return openEditPopUpBool; }
            set
            {
                openEditPopUpBool = value;
                OnPropertyChanged("OpenEditPopUpBool");
            }
        }

        private int selectedIndexNewOperationComboBox = 0;
        public int SelectedIndexNewOperationComboBox
        {
            get { return selectedIndexNewOperationComboBox; }
            set
            {
                selectedIndexNewOperationComboBox = value;
                OnPropertyChanged("SelectedIndexNewOperationComboBox");
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
                openSettingCommand = value;
            }
        }

        private ICommand saveMainCollection;
        public ICommand SaveMainCollection
        {
            get
            {
                return saveMainCollection;
            }
            set
            {
                saveMainCollection = value;
            }
        }

        private ICommand deleteMainOperationCommand;
        public ICommand DeleteMainOperationCommand
        {
            get
            {
                return deleteMainOperationCommand;
            }
            set
            {
                deleteMainOperationCommand = value;
            }
        }

        private ICommand deleteChildOperationCommand;
        public ICommand DeleteChildOperationCommand
        {
            get
            {
                return deleteChildOperationCommand;
            }
            set
            {
                deleteChildOperationCommand = value;
            }
        }

        private ICommand openPopUpEditCommand;
        public ICommand OpenPopUpEditCommand
        {
            get
            {
                return openPopUpEditCommand;
            }
            set
            {
                openPopUpEditCommand = value;
            }
        }
        #endregion

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
