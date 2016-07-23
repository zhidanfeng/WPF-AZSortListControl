using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortListBox.MyControls.MyEventArgs
{
    public class ItemMouseLeftButtonUpEventArgs<T> : EventArgs
    {
        public ItemMouseLeftButtonUpEventArgs() { }

        public T NewValue { get; private set; }

        public static ItemMouseLeftButtonUpEventArgs<T> ItemMouseLeftButtonUp(T newValue)
        {
            return new ItemMouseLeftButtonUpEventArgs<T>() { NewValue = newValue };
        }
    }
}
