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
using System.Windows.Input;

namespace ATP_Lab.ViewModels
{
    public class SettingViewModel : INotifyPropertyChanged
    {
        private TreeViewModel view = new TreeViewModel();

        #region Constructor
        public SettingViewModel()
        {
            SectionCollection = view.AllCollection;
            DeleteItemListbox = new RelayCommand(DeleteItem, param => this.canExecute);
            AddItemListbox = new RelayCommand(AddItem, param => this.canExecute);
            CorrectшtemListbox = new RelayCommand(CorrectItem, param => this.canExecute);
            ViewPopUp = new RelayCommand(OpenPopUp, param => this.canExecute);
            SaveCollectionListbox = new RelayCommand(SaveListCollection, param => this.canExecute);
        }
        #endregion

        #region Collections
        private ObservableCollection<string> nameSectionContent = new ObservableCollection<string>();
        public ObservableCollection<string> NameSectionContent
        {
            get { return nameSectionContent; }
            set
            {
                nameSectionContent = value;
                OnPropertyChanged("NameSectionContent");
            }
        }
        private ObservableCollection<string> operationNameCollection = new ObservableCollection<string>();
        public ObservableCollection<string> OperationNameCollection
        {
            get { return operationNameCollection; }
            set
            {
                operationNameCollection = value;
                OnPropertyChanged("OperationNameCollection");
            }
        }
        private ObservableCollection<AllCollectionModel> sectionCollection = new ObservableCollection<AllCollectionModel>();
        public ObservableCollection<AllCollectionModel> SectionCollection
        {
            get { return sectionCollection; }
            set
            {
                sectionCollection = value;
                OnPropertyChanged("SectionCollection");
            }
        }
        #endregion

        #region Command
        private ICommand viewPopUp;
        public ICommand ViewPopUp
        {
            get
            {
                return viewPopUp;
            }
            set
            {
                viewPopUp = value;
            }
        }
        private ICommand deleteItemListbox;
        public ICommand DeleteItemListbox
        {
            get
            {
                return deleteItemListbox;
            }
            set
            {
                deleteItemListbox = value;
            }
        }
        private ICommand addItemListbox;
        public ICommand AddItemListbox
        {
            get
            {
                return addItemListbox;
            }
            set
            {
                addItemListbox = value;
            }
        }
        private ICommand correctItemListbox;
        public ICommand CorrectшtemListbox
        {
            get
            {
                return correctItemListbox;
            }
            set
            {
                correctItemListbox = value;
            }
        }
        private ICommand saveCollectionListbox;
        public ICommand SaveCollectionListbox
        {
            get
            {
                return saveCollectionListbox;
            }
            set
            {
                saveCollectionListbox = value;
            }
        }
        #endregion

        #region Properties
        private bool canExecute = true;
        private int selectedIndex = 0;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                SetCollection();
            }
        }
        private int listBoxSelectedIndex = 0;
        public int ListBoxSelectedIndex
        {
            get { return listBoxSelectedIndex; }
            set
            {
                listBoxSelectedIndex = value;
                OnPropertyChanged("ListBoxSelectedIndex");
            }
        }
        private bool popUp = false;
        public bool PopUp
        {
            get { return popUp; }
            set
            {
                popUp = value;
                OnPropertyChanged("PopUp");
            }
        }

        private string selectedItem;
        public string SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        private string inputText;
        public string InputText
        {
            get { return inputText; }
            set
            {
                inputText = value;
                OnPropertyChanged("InputText");
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Show PopUp menu
        /// </summary>
        /// <param name="obj"></param>
        private void OpenPopUp(object obj)
        {
            PopUp = PopUp == true ? false : true;
        }

        /// <summary>
        /// Correct SelectedItem
        /// </summary>
        /// <param name="obj"></param>
        private void CorrectItem(object obj)
        {
            OperationNameCollection[ListBoxSelectedIndex] = InputText;
        }

        /// <summary>
        /// Add new Item
        /// </summary>
        /// <param name="obj"></param>
        private void AddItem(object obj)
        {
            OperationNameCollection.Add(InputText);
        }

        /// <summary>
        /// Delete Item
        /// </summary>
        /// <param name="obj"></param>
        private void DeleteItem(object obj)
        {
            OperationNameCollection.Remove(SelectedItem);
            SaveListCollection(null);
        }

        /// <summary>
        /// Save of the collection to a file
        /// </summary>
        /// <param name="obj"></param>
        private void SaveListCollection(object obj)
        {
            string json = JsonConvert.SerializeObject(OperationNameCollection);
            string Name = view.AllCollection[SelectedIndex].Name;
            System.IO.File.WriteAllText(Name + ".txt", json);
        }

        /// <summary>
        /// Collections reading from a file
        /// </summary>
        private void SetCollection()
        {
            OperationNameCollection = new ObservableCollection<string>(ReturnCollectionOutFile());
        }

        private IEnumerable<string> ReturnCollectionOutFile()
        {
            string text = "";
            string Name = view.AllCollection[SelectedIndex].Name;
            using (StreamReader streamReader = new StreamReader(Name + ".txt", Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<ObservableCollection<string>>(text);
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
