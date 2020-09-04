using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using nsStockManage;

namespace StockManage
{
    class Tool
    {
        public int idTools;
        public String Code { get; }
        public String toolName;
        public String description;
        public String functionState;
        public String storageState;
        public String category;
        public String materialNumber;
        public String number;
        public String library;
        public String shelf;
        public String layer;
        public String position;
        public String line;
        public String workstation;
        public String borrower;
        public String operator1;
        public int lendDuration;
        public String[] model;
        public String manufacturer;
        public float price;
        public String lifeType;
        public int lifeSpan;
        public int lifeLeft;
        public int repairTimes;
        public int lendTimes;
        public String purchaseDate;
        public String scrapDate;
        public String lastTime;
        public String remarks;

        /// <summary>
        /// 带工装编码的构造函数，代表数据库中的已有工装。
        /// </summary>
        /// <param name="code">工装编码</param>
        public Tool(String code)
        {
            Code = code;
            Get();
        }

        /// <summary>
        /// 从数据库中获取该工装的信息
        /// </summary>
        /// <returns></returns>
        private bool Get()
        {
            DBConnection connection = new DBConnection();
            String sql = "SELECT * FROM tools WHERE code = '" + Code + "' LIMIT 3;";
            DataSet ds = connection.Select(sql);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 1)
                {
                    MessageBox.Show("工装不唯一，请核对编码！");
                    return false;
                }
                if (!int.TryParse(ds.Tables[0].Rows[0][0].ToString(), out idTools))
                {
                    idTools = -1;
                }
                toolName = ds.Tables[0].Rows[0][2].ToString();
                description = ds.Tables[0].Rows[0][3].ToString();
                functionState = ds.Tables[0].Rows[0][4].ToString();
                storageState = ds.Tables[0].Rows[0][5].ToString();
                category = ds.Tables[0].Rows[0][6].ToString();
                materialNumber = ds.Tables[0].Rows[0][7].ToString();
                number = ds.Tables[0].Rows[0][8].ToString();
                library = ds.Tables[0].Rows[0][9].ToString();
                shelf = ds.Tables[0].Rows[0][10].ToString();
                layer = ds.Tables[0].Rows[0][11].ToString();
                position = ds.Tables[0].Rows[0][12].ToString();
                line = ds.Tables[0].Rows[0][13].ToString();
                workstation = ds.Tables[0].Rows[0][14].ToString();
                borrower = ds.Tables[0].Rows[0][15].ToString();
                operator1 = ds.Tables[0].Rows[0][16].ToString();
                if (!int.TryParse(ds.Tables[0].Rows[0][17].ToString(), out lendDuration))
                {
                    lendDuration = -1;
                }
                //model = ds.Tables[0].Rows[0][18].ToString();
                manufacturer = ds.Tables[0].Rows[0][19].ToString();
                if (!float.TryParse(ds.Tables[0].Rows[0][20].ToString(), out price))
                {
                    price = -1;
                }
                lifeType = ds.Tables[0].Rows[0][21].ToString();
                if (!int.TryParse(ds.Tables[0].Rows[0][22].ToString(), out lifeSpan))
                {
                    lifeSpan = -1;
                }
                if (!int.TryParse(ds.Tables[0].Rows[0][23].ToString(), out lifeLeft))
                {
                    lifeLeft = -1;
                }
                if (!int.TryParse(ds.Tables[0].Rows[0][24].ToString(), out repairTimes))
                {
                    repairTimes = -1;
                }
                if (!int.TryParse(ds.Tables[0].Rows[0][25].ToString(), out lendTimes))
                {
                    lendTimes = -1;
                }
                purchaseDate = ds.Tables[0].Rows[0][26].ToString();
                scrapDate = ds.Tables[0].Rows[0][27].ToString();
                lastTime = ds.Tables[0].Rows[0][28].ToString();
                remarks = ds.Tables[0].Rows[0][29].ToString();
                return true;
            }
            else
            {
                MessageBox.Show("未找到该工装信息！");
                return false;
            }

        }

        /// <summary>
        /// 新购入库
        /// </summary>
        /// <param name=""></param>
        /// <returns>返回该工装的序号(idTools)</returns>
        public int NewIn()
        {
            return 0;
        }

        public bool NewIn_Batch()
        {
            return true;
        }

        /// <summary>
        /// 检查编码合法性
        /// </summary>
        /// <param name="code">工装编码</param>
        /// <returns>合法返回true，非法返回false</returns>
        public bool CheckCode(String code)
        {
            if (code.Length > 10 && code.Contains("-") && code.Length < 30)
            {
                String[] temp = code.Split('-');
                if (temp.Length == 3)
                {
                    if (temp[0].Length > 0 && temp[1].Length > 0 && temp[2].Length == 6)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }








    }
}
