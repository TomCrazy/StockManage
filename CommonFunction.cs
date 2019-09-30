using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace nsStockManage
{
    class CommonFunction
    {
        //检查二维码的合法性函数
        public static bool checkCodeLegality(String code)
        {
            if (code.Length > 15 && code.Contains("-") && !(code.Contains("@")))
            {
                String[] classes = code.Split('-');
                if (classes.Length > 5)
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

        //检查起始编号的合法性函数
        public static bool checkNumberLegality(String number)
        {
            if (number.Length > 5 && number.Contains("-"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //检查结尾编号的合法性函数
        public static bool checkNumberEndLegality(String startNumber, String endNumber)
        {
            if(startNumber.Length > 5 && endNumber.Length > 5 && startNumber.Contains("-") && endNumber.Contains("-"))
            {
                startNumber = startNumber.Substring(startNumber.Length - 3, 3);
                endNumber = endNumber.Substring(endNumber.Length - 3, 3);
                if (int.Parse(startNumber) < int.Parse(endNumber))
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

        //检查字符串中是否包含中文
        public static bool HasChinese(string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }
    }
}
