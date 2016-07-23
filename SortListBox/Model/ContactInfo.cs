using SortListBox.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortListBox.Model
{
    public class ContactInfo
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string nameLetter;

        public string NameLetter
        {
            get { return WordHelper.GetHeadOfSingleChs(Name).ToUpper(); }
            set { nameLetter = value; }
        }
    }
}
