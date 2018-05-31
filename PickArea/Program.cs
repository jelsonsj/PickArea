using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PickArea
{
    class Program
    {
        static void Main(string[] args)
        {

            var path = AppDomain.CurrentDomain.BaseDirectory + "\\source.txt";
            var lines = File.ReadAllLines(path);
            List<Region> lstSource = new List<Region>();
            foreach (var item in lines)
            {
                string str = System.Text.RegularExpressions.Regex.Replace(item, @"\s+", ",");

                string[] sd = str.Split(',');

                Region regionObject = new Region();
                regionObject.ID = Convert.ToInt32((sd[0] + "").Trim());
                regionObject.Name = (sd[1] + "").Trim();
                lstSource.Add(regionObject);
            }
            lstSource.Add(new Region
            {
                ID = 100000,
                Name = "中国"
            });

            lstSource.Add(new Region
            {
                ID = 110100,
                Name = "北京市"
            });
            lstSource.Add(new Region
            {
                ID = 120100,
                Name = "天津市"
            });
            lstSource.Add(new Region
            {
                ID = 310100,
                Name = "上海市"

            });
            lstSource.Add(new Region
            {
                ID = 500100,
                Name = "重庆市"
            });

            lstSource = lstSource.OrderBy(p => p.ID).ToList();

            int country = 100000;
            int province = 0;
            int city = 0;
            foreach (var region in lstSource)
            {
                if (region.ID == country)
                {
                    region.Level = 0;
                    region.PID = 0;
                }
                else
                {
                    var minusCountry = region.ID - country;
                    //整除万的编码为省份
                    if (minusCountry % 10000 == 0)
                    {
                        province = region.ID;
                        region.PID = country;
                        region.Level = 1;
                    }
                    else
                    {
                        var minusProvince = region.ID - province;
                        if (minusProvince % 100 == 0)
                        {
                            city = region.ID;
                            region.PID = province;
                            region.Level = 2;
                        }
                        else
                        {
                            region.PID = city;
                            region.Level = 3;
                        }
                    }
                }
                region.PinYin = NPinyin.Pinyin.GetPinyin(region.Name);
                region.ShortPinYin = NPinyin.Pinyin.GetInitials(region.Name);
                region.First = region.ShortPinYin[0];
            }

            List<string> lstSql = new List<string>();

            foreach (var obj in lstSource)
            {
                lstSql.Add(string.Format("insert into region (id,pid,level,name) values({0},{1},{2},'{3}');", obj.ID, obj.PID, obj.Level, obj.Name));
            }

            var result = AppDomain.CurrentDomain.BaseDirectory + "\\last.txt";

            File.WriteAllLines(result, lstSql.ToArray());


        }
    }
}
