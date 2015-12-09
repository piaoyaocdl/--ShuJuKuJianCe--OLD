using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数据库检测.MODEL
{
    public class Xiangxi { public long id; }
   public class Biaotiaoshu
    {
        public string biaoming { set; get; }
        public long jiutiaoshu { set; get; }
        public long xintiaoshu { set; get; }
        public long bianhua { get { return xintiaoshu - jiutiaoshu; } set {; } } 
    }
}
