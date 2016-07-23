using SortListBox.Model;
using SortListBox.Utils;
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

namespace SortListBox
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<ContactInfo> contactList;

        public ObservableCollection<ContactInfo> ContaceList
        {
            get { return contactList; }
            set { contactList = value; }
        }

        public MainWindow()
        {
            InitializeComponent();

            this.InitData();
            var list = this.ContaceList.OrderBy(p => p.NameLetter);
            this.ContaceList = new ObservableCollection<ContactInfo>(list);
            this.listbox.ItemsSource = this.ContaceList;
            this.listbox.DisplayMemberPath = "Name";
        }

        private void InitData()
        {
            this.ContaceList = new ObservableCollection<ContactInfo>();
            this.ContaceList.Add(new ContactInfo() { Name = "北京市" });
            this.ContaceList.Add(new ContactInfo() { Name = "安庆市" });
            this.ContaceList.Add(new ContactInfo() { Name = "南京市" });
            this.ContaceList.Add(new ContactInfo() { Name = "天津市" });
            this.ContaceList.Add(new ContactInfo() { Name = "南昌市" });
            this.ContaceList.Add(new ContactInfo() { Name = "抚州市" });
            this.ContaceList.Add(new ContactInfo() { Name = "武汉市" });
            this.ContaceList.Add(new ContactInfo() { Name = "长沙市" });
            this.ContaceList.Add(new ContactInfo() { Name = "上海市" });
            this.ContaceList.Add(new ContactInfo() { Name = "成都市" });
            this.ContaceList.Add(new ContactInfo() { Name = "重庆市" });
            this.ContaceList.Add(new ContactInfo() { Name = "银川市" });
            this.ContaceList.Add(new ContactInfo() { Name = "石嘴山市" });
            this.ContaceList.Add(new ContactInfo() { Name = "吴忠市" });
            this.ContaceList.Add(new ContactInfo() { Name = "固原市" });
            this.ContaceList.Add(new ContactInfo() { Name = "中卫市" });
            this.ContaceList.Add(new ContactInfo() { Name = "青铜峡市" });
            this.ContaceList.Add(new ContactInfo() { Name = "灵武市" });
            this.ContaceList.Add(new ContactInfo() { Name = "呼和浩特市" });
            this.ContaceList.Add(new ContactInfo() { Name = "包头市" });
            this.ContaceList.Add(new ContactInfo() { Name = "乌海市" });
            this.ContaceList.Add(new ContactInfo() { Name = "赤峰市" });
            this.ContaceList.Add(new ContactInfo() { Name = "通辽市" });
            this.ContaceList.Add(new ContactInfo() { Name = "鄂尔多斯市" });
            this.ContaceList.Add(new ContactInfo() { Name = "呼伦贝尔市" });
            this.ContaceList.Add(new ContactInfo() { Name = "南宁市" });
            this.ContaceList.Add(new ContactInfo() { Name = "柳州市" });
            this.ContaceList.Add(new ContactInfo() { Name = "桂林市" });
            this.ContaceList.Add(new ContactInfo() { Name = "梧州市" });
            this.ContaceList.Add(new ContactInfo() { Name = "北海市" });
            this.ContaceList.Add(new ContactInfo() { Name = "崇左市" });
            this.ContaceList.Add(new ContactInfo() { Name = "来宾市" });
            this.ContaceList.Add(new ContactInfo() { Name = "贺州市" });
            this.ContaceList.Add(new ContactInfo() { Name = "玉林市" });
            this.ContaceList.Add(new ContactInfo() { Name = "哈尔滨市" });
            this.ContaceList.Add(new ContactInfo() { Name = "齐齐哈尔市" });
            this.ContaceList.Add(new ContactInfo() { Name = "石家庄市" });
            this.ContaceList.Add(new ContactInfo() { Name = "唐山市" });
            this.ContaceList.Add(new ContactInfo() { Name = "邯郸市" });
            this.ContaceList.Add(new ContactInfo() { Name = "秦皇岛市" });
        }
    }
}
