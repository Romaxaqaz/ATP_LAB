using ATP_Lab.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ATP_Lab
{
    /// <summary>
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
    public partial class TestPage : Window
    {
        TestPageVM test = new TestPageVM();
        public TestPage()
        {
            InitializeComponent();
            this.DataContext = test;

        }

        ListView dragSource = null;
        ListView parent = null;
        object dt = null;
        object data = null;
        ContentL childData = null;

        private void dList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListView parent = (ListView)sender;
            dragSource = parent;
            this.parent = parent;
            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));
            if (parent.Name == "TestList")
            {
                childData = (ContentL)data;
            }
            this.data = data;
            dt = data;
            
        }

        private void dList_Drop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            e.Handled = true;
            ListView parent = (ListView)sender;
            var data = GetDataFromListBox(parent, e.GetPosition(parent));
            if (parent.Name == "TestList")
            {
                test.Content.Remove((ContentL)dt);
                test.Content.Insert(GetIndex(data, test.Content), (ContentL)dt);
            }
            else if(parent.Name == "dList")
            {
                foreach (var item in test.Content)
                {
                    if (item.Name == parent.Tag.ToString())
                    {
                       item.MoreName.Add(dt.ToString());
                    }
                    if (item.Name == childData.Name.ToString())
                    {
                        item.MoreName.Remove(dt.ToString());
                    }
                }
            }
        }

        private int GetIndex(object name, IEnumerable<ContentL> collection)
        {
            int x = 0;
            foreach (var item in collection)
            {
                if (item == ((ContentL)name))
                {
                    break;
                }
                x++;
            }
            return x+1;
        }

        private static object GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }
                    if (element == source)
                    {
                        return null;
                    }
                }
                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }
            return null;
        }

        private void TestList_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (data != null)
                {
                    DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
                }
            }
        }



    }

}
