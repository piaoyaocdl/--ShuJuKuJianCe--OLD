using System;
using System.Collections.Generic;
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
using 数据库检测.MODEL;

namespace 数据库检测
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (var shujuku = new ShuJuKuDataContext())
            {
                var ls = shujuku.ExecuteQuery<Biaotiaoshu>(
                     @" SELECT  O.NAME as biaoming, I.ROWCNT as xintiaoshu
                        FROM SYS.SYSOBJECTS O, SYSINDEXES I 
                        WHERE   O.ID = I.ID AND  O.XTYPE = 'U' AND  I.INDID < 2").OrderByDescending(x => x.bianhua).ToList();
                shujuyuan_biaotiaoshu = ls;

            }
        }


        private List<Biaotiaoshu> shujuyuan_biaotiaoshu
        {
            set { biaotiaoshuUI.ItemsSource = value; }
            get { return (List<Biaotiaoshu>)biaotiaoshuUI.ItemsSource; }
        }


        private void gengxinshujuUI_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, Biaotiaoshu> ls;
            using (var shujuku = new ShuJuKuDataContext())
            {
                ls = shujuku.ExecuteQuery<Biaotiaoshu>(
                    @" SELECT  O.NAME as biaoming, I.ROWCNT as xintiaoshu
                        FROM SYS.SYSOBJECTS O, SYSINDEXES I 
                        WHERE   O.ID = I.ID AND  O.XTYPE = 'U' AND  I.INDID < 2").ToDictionary<Biaotiaoshu, string>(x => x.biaoming);
            }

            shujuyuan_biaotiaoshu.ForEach(x => { x.jiutiaoshu = x.xintiaoshu; x.xintiaoshu = ls[x.biaoming].xintiaoshu; });
            shujuyuan_biaotiaoshu = shujuyuan_biaotiaoshu.OrderByDescending(x => x.bianhua).ToList();
        }



        private void biaotiaoshuUI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ls01 = (DataGrid)sender;
            var ls02 = (Biaotiaoshu)ls01.SelectedItem;
            if (ls02.bianhua > 0)
            {
                Type t = Type.GetType("数据库检测.MODEL." + ls02.biaoming);

                using (var shujuku = new ShuJuKuDataContext())
                {

                    var ls = shujuku.ExecuteQuery(t, "select top " + ls02.bianhua + " * from " + ls02.biaoming + " order by id desc");
                    xiangxiUI.ItemsSource = ls;

                }
            }


        }
    }
}
