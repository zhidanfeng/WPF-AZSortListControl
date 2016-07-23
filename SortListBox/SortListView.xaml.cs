using SortListBox.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Media.Animation;

namespace SortListBox
{
    /// <summary>
    /// SortListView.xaml 的交互逻辑
    /// </summary>
    public partial class SortListView : UserControl
    {
        public static String[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I",
            "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V",
            "W", "X", "Y", "Z", "#" };
        private ScrollViewer scrollViewer = null;
        private Dictionary<string, double> letterPostion = new Dictionary<string, double>();

        #region 
        private Timer mTimer = new Timer();
        #endregion

        #region 依赖属性

        #region ItemsSource数据源
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource"
            , typeof(System.Collections.IEnumerable), typeof(SortListView));
        /// <summary>
        /// 水印
        /// </summary>
        public System.Collections.IEnumerable ItemsSource
        {
            get { return (System.Collections.IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        #endregion

        #region DisplayMemberPath
        public static readonly DependencyProperty DisplayMemberPathProperty = DependencyProperty.Register("DisplayMemberPath"
            , typeof(string), typeof(SortListView));
        /// <summary>
        /// 显示的
        /// </summary>
        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }
        #endregion

        #endregion

        public SortListView()
        {
            InitializeComponent();

            this.Loaded += SortListView_Loaded;
            this.MouseEnter += SortListView_MouseEnter;
            this.MouseLeave += SortListView_MouseLeave;
            
        }

        private void SortListView_MouseLeave(object sender, MouseEventArgs e)
        {
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            //
        }

        private void SortListView_MouseEnter(object sender, MouseEventArgs e)
        {
            this.scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            this.InitLetterPosition();
        }

        private void SortListView_Loaded(object sender, RoutedEventArgs e)
        {
            scrollViewer = this.FindChild<ScrollViewer>(this.lvListBoxMain, "sortListViewScrollView");
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;

            this.lvListBoxMain.ItemsSource = this.ItemsSource;
            this.lvListBoxMain.DisplayMemberPath = this.DisplayMemberPath;
            this.SetGroup();

            this.lvLetter.ItemsSource = this.InitLetterData();
            
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            double offset = this.scrollViewer.VerticalOffset;
            foreach(var dict in this.letterPostion)
            {
                if(dict.Value - offset > -15 && dict.Value - offset < 15)
                {
                    this.tb.Text = dict.Key;
                    this.scrollViewer.ScrollToVerticalOffset(dict.Value);
                }
            }
        }

        private ObservableCollection<string> InitLetterData()
        {
            ObservableCollection<string> data = new ObservableCollection<string>();
            foreach(var letter in letters)
            {
                data.Add(letter);
            }
            return data;
        }

        private void SetGroup()
        {
            var alarmCollectionView = CollectionViewSource.GetDefaultView(this.ItemsSource) as ListCollectionView;
            if (alarmCollectionView != null)
            {
                alarmCollectionView.SortDescriptions.Add(new SortDescription("NameLetter", ListSortDirection.Ascending));
                if (alarmCollectionView.GroupDescriptions != null)
                {
                    alarmCollectionView.GroupDescriptions.Add(new PropertyGroupDescription("NameLetter"));
                }
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.lvLetter.SelectedItem == null) return;
            double offset = 0d;

            string selectedLetter = this.lvLetter.SelectedItem.ToString();

            bool isFind = false;
            
            var elementList = this.FindVisualChildren<GroupItem>(this.lvListBoxMain).ToList();
            for (int i = 0; i < elementList.Count; i++)
            {
                var element = elementList[i];
                if (isFind) break;

                offset += element.ActualHeight;

                var textBlockList = this.FindVisualChildren<TextBlock>(element, "GroupHeader");
                foreach (var textBlock in textBlockList)
                {
                    if (textBlock.Text.Equals(selectedLetter))
                    {
                        offset -= element.ActualHeight;
                        scrollViewer.ScrollToVerticalOffset(offset);
                        this.tb.Text = textBlock.Text;
                        isFind = true;
                        break;
                    }
                }
            }
            this.ShowCurrentHeader.Text = selectedLetter;
            this.ShowCenterLetter.Visibility = Visibility.Visible;
            mTimer.Interval = 500;
            mTimer.Elapsed += MTimer_Elapsed;
            mTimer.Enabled = true;
        }
        int flag = 0;
        private void InitLetterPosition()
        {
            if (flag != 0) return;

            flag += 1;

            double offset = 0d;

            var elementList = this.FindVisualChildren<GroupItem>(this.lvListBoxMain).ToList();
            for (int i = 0; i < elementList.Count; i++)
            {
                var element = elementList[i];

                offset += element.ActualHeight;

                var textBlockList = this.FindVisualChildren<TextBlock>(element, "GroupHeader");
                string header = ((System.Windows.Data.CollectionViewGroup)element.Content).Name.ToString();

                element.FindName("GroupHeader");
                
                if (!this.letterPostion.ContainsKey(header))
                {
                    if(this.letterPostion.Count > 0)
                    {
                        this.letterPostion.Add(header, offset - element.ActualHeight);
                    }
                    else
                    {
                        this.letterPostion.Add(header, 0d);
                    }
                }
            }
        }

        private void MTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                this.ShowCenterLetter.Visibility = Visibility.Collapsed;
            });
            
            mTimer.Enabled = false;
        }

        #region
        public ChildType FindVisualChild<ChildType>(DependencyObject obj) where ChildType : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is ChildType)
                {
                    return child as ChildType;
                }
                else
                {
                    ChildType childOfChildren = FindVisualChild<ChildType>(child);
                    if (childOfChildren != null)
                    {
                        return childOfChildren;
                    }
                }
            }
            return null;
        }

        public T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                T childType = child as T;
                if (childType == null)
                {
                    // 住下查要找的元素
                    foundChild = FindChild<T>(child, childName);

                    // 如果找不到就反回
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // 看名字是不是一样
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        //如果名字一样返回
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // 找到相应的元素了就返回 
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj, string childName) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    var frameworkElement = child as FrameworkElement;
                    if (child != null && child is T && frameworkElement.Name.Equals(childName))
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child, childName))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        #endregion

        private void lvLetter_ItemMouseLeftButtonDown(object sender, MyControls.MyEventArgs.ItemMouseLeftButtonDownEventArgs<object> e)
        {
            if (this.lvLetter.SelectedItem == null) return;
            double offset = 0d;

            string selectedLetter = (this.lvLetter.SelectedItem as ListBoxItem).Content.ToString();

            bool isFind = false;
            ScrollViewer scrollViewer = this.FindChild<ScrollViewer>(this.lvListBoxMain, "sortListViewScrollView");
            var elementList = this.FindVisualChildren<GroupItem>(this.lvListBoxMain).ToList();
            for (int i = 0; i < elementList.Count; i++)
            {
                var element = elementList[i];
                if (isFind) break;

                offset += element.ActualHeight;

                var textBlockList = this.FindVisualChildren<TextBlock>(element, "GroupHeader");
                foreach (var textBlock in textBlockList)
                {
                    if (textBlock.Text.Equals(selectedLetter))
                    {
                        offset -= element.ActualHeight;
                        scrollViewer.ScrollToVerticalOffset(offset);

                        this.tb.Text = textBlock.Text;
                        isFind = true;
                        break;
                    }
                }
            }
            this.ShowCurrentHeader.Text = selectedLetter;
            this.ShowCenterLetter.Visibility = Visibility.Visible;
            //mTimer.Interval = 500;
            //mTimer.Elapsed += MTimer_Elapsed;
            //mTimer.Enabled = true;
        }

        private void lvLetter_ItemMouseLeftButtonUp(object sender, MyControls.MyEventArgs.ItemMouseLeftButtonUpEventArgs<object> e)
        {
            this.ShowCenterLetter.Visibility = Visibility.Collapsed;
        }
    }
}
