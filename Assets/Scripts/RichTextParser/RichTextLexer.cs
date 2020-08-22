/* ==============================================================================
 * 功能描述：TextFormatParser  
 * 创 建 者：jianzhou.yao
 * 创建日期：2020/8/22 11:43:59
 * ==============================================================================*/

using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager.Requests;

namespace JeoYao.UIExtension
{
    public class RichTextLexer
    {
        private string _text;
        private int _index;
        public RichTextLexer()
        {
            
        }

        public List<TextToken> Parse(string text)
        {
            List<TextToken> tokens = new List<TextToken>();
            _text = text;
            int len = _text.Length;
            _index = 0;
            while (_index < len)
            {
                char c = Peek();
                if (c == '<')
                {
                    string tag = ParseTag();
                    tokens.Add(new TextToken(TextToken.TextTokenType.Attribute, tag));
                    //UnityEngine.Debug.Log(tag);
                }
                else
                {
                    string txt = ParseText();
                    tokens.Add(new TextToken(TextToken.TextTokenType.String, txt));
                    //UnityEngine.Debug.Log(txt);
                }
            }
            return tokens;
        }

        private string ParseText()
        {
            string rlt = "";
            while(Peek() != '<')
            {
                char c = Consume();
                rlt += c;
            }
            return rlt;
        }

        private string ParseTag()
        {
            string rlt = "";
            char c;
            do
            {
                c = Consume();
                rlt += c;
            } while (c != '>');
            return rlt;
        }

        private char Peek()
        {
            char c = _text[_index];
            return c;
        }

        private char Consume()
        {
            char c = _text[_index];
            _index++;
            return c;
        }
    }
}

public class TextToken
{
    public TextTokenType Type;
    public string Value;

    public TextToken(TextTokenType type,string value)
    {
        Type = type;
        Value = value;
    }

    public enum TextTokenType
    {
        String,
        Attribute
    }
}

public class TextAttributeBase
{
    public TextAttributeType Type;
    public string Name;
    public string Value;

    public enum TextAttributeType
    {
        Open,
        Close
    }
}

public class RichTextData
{
    public string Value;
    public List<TextAttributeBase> Attributes;

    public RichTextData(string value)
    {
        Value = value;
        Attributes = new List<TextAttributeBase>();
    }

    public void AddAttribute(TextAttributeBase attr)
    {
        Attributes.Add(attr);
    }
}

public class ColorAttribute: TextAttributeBase
{
    public Color Color;
}

public class FontAttribute: TextAttributeBase
{
    
}

public class BoldAttribute: TextAttributeBase
{

}

public class ItalicsAttribute : TextAttributeBase
{

}