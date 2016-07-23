using SortListBox.MyControls.MyEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SortListBox.MyControls
{
    public class ListBoxEx : ListBox
    {
        #region 事件
        /// <summary>
        /// 行单击
        /// </summary>
        public event EventHandler<ItemMouseLeftButtonDownEventArgs<object>> ItemMouseLeftButtonDown;
        public event EventHandler<ItemMouseLeftButtonUpEventArgs<object>> ItemMouseLeftButtonUp;
        #endregion

        #region 重写函数
        /// <summary>
        /// 重写ListViewItem之后，该方法需重写
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            var item = new ListBoxItemEx();
            item.MouseLeftButtonDown += Item_MouseLeftButtonDown;
            item.MouseLeftButtonUp += Item_MouseLeftButtonUp;
            return item;
        }
        #endregion

        #region 事件实现
        private void Item_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItemEx item = sender as ListBoxItemEx;
            this.SelectedItem = item.Content;
            if (this.ItemMouseLeftButtonDown != null)
            {
                this.ItemMouseLeftButtonDown(this, ItemMouseLeftButtonDownEventArgs<object>.ItemMouseLeftButtonDown(item.Content));
            }
        }

        private void Item_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBoxItemEx item = sender as ListBoxItemEx;
            this.SelectedItem = item.Content;
            if (this.ItemMouseLeftButtonUp != null)
            {
                this.ItemMouseLeftButtonUp(this, ItemMouseLeftButtonUpEventArgs<object>.ItemMouseLeftButtonUp(item.Content));
            }
        }
        #endregion
    }

    /// <summary>
    /// 重写ListViewItem，定义行单击、双击事件
    /// </summary>
    public class ListBoxItemEx : System.Windows.Controls.ListBoxItem
    {
        
    }
}
