using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortListBox.MyControls.MyEventArgs
{
    public class ItemMouseLeftButtonDownEventArgs<T> : EventArgs
    {
        public ItemMouseLeftButtonDownEventArgs() { }

        public T NewValue { get; private set; }

        public static ItemMouseLeftButtonDownEventArgs<T> ItemMouseLeftButtonDown(T newValue)
        {
            return new ItemMouseLeftButtonDownEventArgs<T>() { NewValue = newValue };
        }
    }
}
