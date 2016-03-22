using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP_Lab.ViewModels
{
    public class TestPageVM : INotifyPropertyChanged
    {
        static ObservableCollection<string> list = new ObservableCollection<string>() { "test1", "test2", "test3" };
        static ObservableCollection<string> list2 = new ObservableCollection<string>() { "test11", "test22", "test33" };
        static ObservableCollection<string> list3 = new ObservableCollection<string>() { "test111", "test222", "test333" };

        private ObservableCollection<ContentL> content = new ObservableCollection<ContentL>()
        {
            new ContentL("H1", list),
            new ContentL("H2", list2),
            new ContentL("H3", list3),
            new ContentL("H4", list),
            new ContentL("H5", list2),
            new ContentL("H6", list3),
        };


        public ObservableCollection<ContentL> Content
        {
            get { return content; }
            set { content = value;
                OnPropertyChanged("Content");
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
    public class ContentL
    {
        public string Name { get; set; }
        public ObservableCollection<string> MoreName { get; set; }

        public ContentL(string name, ObservableCollection<string> collection)
        {
            this.Name = name;
            this.MoreName = collection;
        }
    }
}
