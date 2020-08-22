/* ==============================================================================
 * 功能描述：RichTextParser  
 * 创 建 者：jianzhou.yao
 * 创建日期：2020/8/22 14:27:32
 * ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace JeoYao.UIExtension
{
    public class RichTextParser
    {
        private Stack<TextAttributeBase> _attrStack;
        private Stack<RichTextData> _textStack;

        private List<RichTextData> _datas;

        public RichTextParser()
        {
            _attrStack = new Stack<TextAttributeBase>();
            _textStack = new Stack<RichTextData>();
            _datas = new List<RichTextData>();
        }

        public List<RichTextData> Datas
        {
            get { return _datas; }
        }

        public List<RichTextData> Parse(List<TextToken> tokens)
        {
            _attrStack.Clear();

            int len = tokens.Count;
            int i = 0;
            while (i < len)
            {
                TextToken token = tokens[i];
                switch (token.Type)
                {
                    case TextToken.TextTokenType.String:
                        DoString(token);
                        break;
                    case TextToken.TextTokenType.Attribute:
                        DoAttribute(token);
                        break;
                    default:
                        break;
                }
                i++;
            }

            while (_textStack.Count > 0)
            {
                _datas.Add(_textStack.Pop());
            }
            _datas.Reverse();

            return _datas;
        }

        private void DoString(TextToken token)
        {
            RichTextData data = new RichTextData(token.Value);
            AddAttributeToText(data);
            _textStack.Push(data);
        }

        private void DoAttribute(TextToken token)
        {
            TextAttributeBase attr = RichTextParseHelper.ParseTextAttribute(token.Value);
            if (attr.Type == TextAttributeBase.TextAttributeType.Close)
            {
                TextAttributeBase prevAttr = _attrStack.Peek();
                if(prevAttr.Name == attr.Name && prevAttr.Type == TextAttributeBase.TextAttributeType.Open)
                {
                    _attrStack.Pop();
                }
            }
            else
            {
                _attrStack.Push(attr);
            }
        }

        private void AddAttributeToText(RichTextData data)
        {
            for (int i = 0; i < _attrStack.Count; i++)
            {
                TextAttributeBase attr = _attrStack.ElementAt(i);
                data.AddAttribute(attr);
            }
        }
    }
}
