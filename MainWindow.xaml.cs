using ATP_Lab.Model;
using ATP_Lab.ViewModels;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATP_Lab
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SettingPage settingWindow = new SettingPage();
        private TreeViewModel treeViewModel = new TreeViewModel();
        private ListView dragSource = null;
        private ListView parent = null;
        private Operation childData = null;
        private object firstObjectData = null;
        private object secondObjectData = null;
        private int IndexItem = 0;


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = treeViewModel;
            settingWindow.Closing += SettingWindow_Closing;
        }

        private void SettingWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            treeViewModel.SetCollection();
            settingWindow.Hide();
        }

        private void dList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListView parent = (ListView)sender;
            dragSource = parent;
            this.parent = parent;
            object data = GetDataFromListView(dragSource, e.GetPosition(parent));
            secondObjectData = data;
            if (parent.Name == "TestList")
            {
                childData = (Operation)data;
            }
            firstObjectData = data;

        }

        private void dList_Drop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            e.Handled = true;
            ListView parent = (ListView)sender;
            var data = GetDataFromListView(parent, e.GetPosition(parent));
            //main listview
            if (parent.Name == "TestList")
            {
                treeViewModel.Operations.Remove((Operation)firstObjectData);
                treeViewModel.Operations.Insert(GetIndex(data, treeViewModel.Operations), (Operation)firstObjectData);
            }
            //expander listview
            else if (parent.Name == "dList")
            {
                int ind = 0;
                foreach (var item in treeViewModel.Operations)
                {
                    if (item.Name == parent.Tag.ToString())
                    {
                        break;
                    }
                    else
                    {
                        ind++;
                    }

                }
                var s = GetIndex(data, treeViewModel.Operations[ind].MoreOperation);
                var operation = (Operation)firstObjectData;
                foreach (var item in treeViewModel.Operations)
                {
                    if (item.Name == parent.Tag.ToString())
                    {
                        item.MoreOperation.Insert(s, new Operation(operation.Name));
                    }
                    if (item.Name == childData.Name.ToString())
                    {
                        item.MoreOperation.Remove(operation);
                    }
                }
            }
        }

        private int GetIndex(object name, IEnumerable<Operation> collection)
        {
            int x = 0;
            foreach (var item in collection)
            {
                if (item == ((Operation)name))
                {
                    break;
                }
                x++;
            }
            return x + 1;
        }

        private static object GetDataFromListView(ListBox source, Point point)
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
                if (secondObjectData != null)
                {
                    DragDrop.DoDragDrop(parent, secondObjectData, DragDropEffects.Move);
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            settingWindow.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

            EditChildData.IsOpen = EditChildData.IsOpen == true ? false : true;

        }

        private void DeleteChildItem_Click(object sender, RoutedEventArgs e)
        {
            treeViewModel.Operations[TestList.SelectedIndex].MoreOperation.RemoveAt(IndexItem);
        }

        private void EditChildItem_Click(object sender, RoutedEventArgs e)
        {
            if (!EditChildData.IsOpen)
            {
                EditChildData.IsOpen = true;
                EditTextPopUp.Text = treeViewModel.Operations[TestList.SelectedIndex].MoreOperation[IndexItem].Name;
            }
            else
            {
                EditChildData.IsOpen = false;
            }
        }

        private void dList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView parent = (ListView)sender;
            IndexItem = parent.SelectedIndex;
        }

        private void SaveEditItem_Click(object sender, RoutedEventArgs e)
        {
            treeViewModel.Operations[TestList.SelectedIndex].MoreOperation[IndexItem].Name = EditTextPopUp.Text;
        }
    }
}

