using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PickArea
{
    public class Region
    {
        //行政区代码
        public int ID { get; set; }
        //上级行政区代码
        public int PID { get; set; }
        //级别  0为国家 1为省级 2为地级  3为县 级别  4为乡级别(目前只获取到县级别
        public int Level { get; set; }

        //行政区名称
        public string Name { get; set; }

        //扩展字段
        public string PinYin { get; set; }

        //简拼
        public string ShortPinYin { get; set; }

        //首字母拼音
        public char First { get; set; }

        //邮编
        public string zipCode { get; set; }

        //区号
        public string cityCode { get; set; }

        //中心点位置
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
    }
}
