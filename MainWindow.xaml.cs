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
        private Point _lastMouseDown;
        private Operation _targetOperation;
        TreeViewModel treeViewModel;


        public MainWindow()
        {
            InitializeComponent();
            treeViewModel = new TreeViewModel();
            this.DataContext = treeViewModel;
        }

        private void TreeView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                _lastMouseDown = e.GetPosition(TheTreeView);
            }
        }

        private void TreeView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPosition = e.GetPosition(TheTreeView);

                if ((Math.Abs(currentPosition.X - _lastMouseDown.X) > 2.0) ||
                    (Math.Abs(currentPosition.Y - _lastMouseDown.Y) > 2.0))
                {
                    Operation selectedItem = (Operation)TheTreeView.SelectedItem;
                    if ((selectedItem != null) && (selectedItem.Parent != null))
                    {
                        TreeViewItem container = GetContainerFromOperation(selectedItem);
                        if (container != null)
                        {
                            DragDropEffects finalDropEffect = DragDrop.DoDragDrop(container, selectedItem, DragDropEffects.Move);
                            if ((finalDropEffect == DragDropEffects.Move) && (_targetOperation != null))
                            {
                                selectedItem.Parent.MoreOperation.Remove(selectedItem);
                                _targetOperation.MoreOperation.Add(selectedItem);
                                _targetOperation = null;
                            }
                        }
                    }
                }
            }
        }

        private TreeViewItem GetContainerFromOperation(Operation Operation)
        {
            Stack<Operation> _stack = new Stack<Operation>();
            _stack.Push(Operation);
            Operation parent = Operation.Parent;

            while (parent != null)
            {
                _stack.Push(parent);
                parent = parent.Parent;
            }

            ItemsControl container = TheTreeView;
            while ((_stack.Count > 0) && (container != null))
            {
                Operation top = _stack.Pop();
                container = (ItemsControl)container.ItemContainerGenerator.ContainerFromItem(top);
            }
            return container as TreeViewItem;
        }

        private TreeViewItem GetNearestContainer(UIElement element)
        {
            TreeViewItem container = element as TreeViewItem;
            while ((container == null) && (element != null))
            {
                element = VisualTreeHelper.GetParent(element) as UIElement;
                container = element as TreeViewItem;
            }
            return container;
        }

        private void TheTreeView_Drop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            e.Handled = true;

            TreeViewItem container = GetNearestContainer(e.OriginalSource as UIElement);
            if (container != null)
            {
                Operation sourceOperation = (Operation)e.Data.GetData(typeof(Operation));
                Operation targetOperation = (Operation)container.Header;
                if ((sourceOperation != null) && (targetOperation != null))
                {
                    if (!targetOperation.MoreOperation.Contains(sourceOperation))
                    {
                        _targetOperation = targetOperation;
                        e.Effects = DragDropEffects.Move;
                    }
                }
            }
        }

        private void TheTreeView_CheckDropTarget(object sender, DragEventArgs e)
        {
            if (!IsValidDropTarget(e.OriginalSource as UIElement))
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private bool IsValidDropTarget(UIElement target)
        {
            if (target != null)
            {
                TreeViewItem container = GetNearestContainer(target);
                return ((container != null) && (((Operation)container.Header).Parent == null));
            }

            return false;
        }
    }
}

