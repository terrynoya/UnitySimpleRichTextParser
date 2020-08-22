/* ==============================================================================
 * 功能描述：RichTextParseHelper  
 * 创 建 者：jianzhou.yao
 * 创建日期：2020/8/22 14:40:37
 * ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JeoYao.UIExtension
{
    public class RichTextParseHelper
    {
        public static Regex CLOSE_TAG_REG = new Regex("</([a-z]+)>");
        public static Regex OPEN_TAG_REG = new Regex(@"<([a-z]+)(=#?\w+)?>");

        public static TextAttributeBase ParseTextAttribute(string txt)
        {
            TextAttributeBase rlt = null;
            if (CLOSE_TAG_REG.IsMatch(txt))
            {
                rlt = new TextAttributeBase();
                Match match = CLOSE_TAG_REG.Match(txt);
                string name = match.Groups[1].Value;
                rlt.Type = TextAttributeBase.TextAttributeType.Close;
                rlt.Name = name;
            }
            else if (OPEN_TAG_REG.IsMatch(txt))
            {
                rlt = new TextAttributeBase();
                Match match = OPEN_TAG_REG.Match(txt);
                string name = match.Groups[1].Value;
                if (match.Groups.Count > 2)
                {
                    string value = match.Groups[2].Value;
                    rlt.Value = value.Replace("=","");
                }
                rlt.Type = TextAttributeBase.TextAttributeType.Open;
                rlt.Name = name;
            }
            return rlt;
        }
    }
}
