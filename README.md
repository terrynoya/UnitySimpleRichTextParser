# UnitySimpleRichTextParser
a simple rich text structure parser 

# How To Use

```csharp
string content = "ever <i>italic<color=#ffff00>green</color></i>";
RichTextLexer lexer = new RichTextLexer();
List<TextToken> tokens = lexer.Parse(content);
RichTextParser txtparser = new RichTextParser();
List<RichTextData> datas = txtparser.Parse(tokens);
for (int i = 0; i < datas.Count; i++)
{
	RichTextData data = datas[i];
	Debug.Log(data.Value);
	for (int j = 0; j < data.Attributes.Count; j++)
	{
		TextAttributeBase attr = data.Attributes[j];
		Debug.Log("attr:"+attr.Name);
	}
	Debug.Log("================");
}
```