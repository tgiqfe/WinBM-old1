﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBM.Lib
{
    /// <summary>
    /// 文字列の中の計算式を返す
    /// 例)
    /// 1234 + 1 - 10  を指定した場合に、1225 を返す
    /// </summary>
    internal class CalculateData
    {
        /// <summary>
        /// int型で返す。小数点第一位で四捨五入
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int ComputeInt(string text)
        {
            if (int.TryParse(text, out int num))
            {
                return num;
            }

            //  計算可能な文字列かどうかの判定を、try～catchで実施
            try
            {
                var dataTable = new System.Data.DataTable();
                var result = dataTable.Compute(text, "");
                switch (result)
                {
                    case int i:
                        return i;
                    case long l:
                        return l > int.MaxValue ? int.MaxValue : (int)l;
                    case float f:
                        return (int)Math.Round(f, MidpointRounding.AwayFromZero);
                    case double d:
                        return (int)Math.Round(d, MidpointRounding.AwayFromZero);
                    case decimal c:
                        return (int)Math.Round(decimal.ToDouble(c), MidpointRounding.AwayFromZero);
                }
            }
            catch { }

            return 0;
        }

        /// <summary>
        /// long型で返す。小数点第一位で四捨五入
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static long ComputeLong(string text)
        {
            if (long.TryParse(text, out long num))
            {
                return num;
            }

            try
            {
                var dataTable = new System.Data.DataTable();
                var result = dataTable.Compute(text, "");
                switch (result)
                {
                    case int i:
                        return i;
                    case long l:
                        return l;
                    case float f:
                        return (long)Math.Round(f, MidpointRounding.AwayFromZero);
                    case double d:
                        return (long)Math.Round(d, MidpointRounding.AwayFromZero);
                    case decimal c:
                        return (long)Math.Round(decimal.ToDouble(c), MidpointRounding.AwayFromZero);
                }
            }
            catch { }

            return 0;

        }

        /// <summary>
        /// double型で返す
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static double ComputeDouble(string text)
        {
            if (double.TryParse(text, out double num))
            {
                return num;
            }

            try
            {
                var dataTable = new System.Data.DataTable();
                var result = dataTable.Compute(text, "");
                switch (result)
                {
                    case int i:
                        return i;
                    case long l:
                        return l;
                    case float f:
                        return f;
                    case double d:
                        return d;
                    case decimal c:
                        return decimal.ToDouble(c);
                }
            }
            catch { }

            return 0;
        }
    }
}
