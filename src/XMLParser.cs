using Boo.Lang.Runtime;
using System;
using UnityEngine;
using UnityScript.Lang;

[NotConverted, NotRenamed]
[Serializable]
public class XMLParser
{
	private char LT;

	private char GT;

	private char SPACE;

	private char QUOTE;

	private char SLASH;

	private char QMARK;

	private char EQUALS;

	private char EXCLAMATION;

	private char DASH;

	private char SQR;

	public XMLParser()
	{
		this.LT = "<"[0];
		this.GT = ">"[0];
		this.SPACE = " "[0];
		this.QUOTE = "\""[0];
		this.SLASH = "/"[0];
		this.QMARK = "?"[0];
		this.EQUALS = "="[0];
		this.EXCLAMATION = "!"[0];
		this.DASH = "-"[0];
		this.SQR = "]"[0];
	}

	[NotRenamed]
	public override XMLNode Parse(string content)
	{
		XMLNode xMLNode = new XMLNode();
		xMLNode["_text"] = string.Empty;
		string empty = string.Empty;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = false;
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		string text4 = string.Empty;
		bool flag6 = false;
		bool flag7 = false;
		bool flag8 = false;
		bool flag9 = false;
		XMLNodeList xMLNodeList = new XMLNodeList();
		XMLNode xMLNode2 = xMLNode;
		for (int i = 0; i < Extensions.get_length(content); i++)
		{
			char c = content[i];
			char c2 = '\0';
			char c3 = '\0';
			char c4 = '\0';
			if (i + 1 < Extensions.get_length(content))
			{
				c2 = content[i + 1];
			}
			if (i + 2 < Extensions.get_length(content))
			{
				c3 = content[i + 2];
			}
			if (i > 0)
			{
				c4 = content[i - 1];
			}
			if (flag6)
			{
				if (c == this.QMARK && c2 == this.GT)
				{
					flag6 = false;
					i++;
				}
			}
			else if (!flag5 && c == this.LT && c2 == this.QMARK)
			{
				flag6 = true;
			}
			else if (flag8)
			{
				if (c2 == this.GT)
				{
					flag8 = false;
					i++;
				}
			}
			else if (flag7)
			{
				if (c4 == this.DASH && c == this.DASH && c2 == this.GT)
				{
					flag7 = false;
					i++;
				}
			}
			else if (!flag5 && c == this.LT && c2 == this.EXCLAMATION)
			{
				if (Extensions.get_length(content) > i + 9 && content.Substring(i, 9) == "<![CDATA[")
				{
					flag9 = true;
					i += 8;
				}
				else if (Extensions.get_length(content) > i + 9 && content.Substring(i, 9) == "<!DOCTYPE")
				{
					flag8 = true;
					i += 8;
				}
				else if (Extensions.get_length(content) > i + 2 && content.Substring(i, 4) == "<!--")
				{
					flag7 = true;
					i += 3;
				}
			}
			else if (flag9)
			{
				if (c == this.SQR && c2 == this.SQR && c3 == this.GT)
				{
					flag9 = false;
					i += 2;
				}
				else
				{
					text4 += c;
				}
			}
			else if (flag)
			{
				if (flag2)
				{
					if (c == this.SPACE)
					{
						flag2 = false;
					}
					else if (c == this.GT)
					{
						flag2 = false;
						flag = false;
					}
					if (!flag2 && Extensions.get_length(text3) > 0)
					{
						if (text3[0] == this.SLASH)
						{
							if (Extensions.get_length(text4) > 0)
							{
								xMLNode2["_text"] = xMLNode2["_text"] + text4;
							}
							text4 = string.Empty;
							text3 = string.Empty;
							object arg_35B_0;
							object expr_341 = arg_35B_0 = xMLNodeList.Pop();
							if (!(expr_341 is XMLNode))
							{
								arg_35B_0 = RuntimeServices.Coerce(expr_341, typeof(XMLNode));
							}
							xMLNode2 = (XMLNode)arg_35B_0;
						}
						else
						{
							if (Extensions.get_length(text4) > 0)
							{
								xMLNode2["_text"] = xMLNode2["_text"] + text4;
							}
							text4 = string.Empty;
							XMLNode xMLNode3 = new XMLNode();
							xMLNode3["_text"] = string.Empty;
							xMLNode3["_name"] = text3;
							if (!RuntimeServices.ToBool(xMLNode2[text3]))
							{
								xMLNode2[text3] = new XMLNodeList();
							}
							object arg_404_0;
							object expr_3EA = arg_404_0 = xMLNode2[text3];
							if (!(expr_3EA is UnityScript.Lang.Array))
							{
								arg_404_0 = RuntimeServices.Coerce(expr_3EA, typeof(UnityScript.Lang.Array));
							}
							UnityScript.Lang.Array array = (UnityScript.Lang.Array)arg_404_0;
							array.Push(xMLNode3);
							xMLNodeList.Push(xMLNode2);
							xMLNode2 = xMLNode3;
							text3 = string.Empty;
						}
					}
					else
					{
						text3 += c;
					}
				}
				else if (!flag5 && c == this.SLASH && c2 == this.GT)
				{
					flag = false;
					flag3 = false;
					flag4 = false;
					if (!string.IsNullOrEmpty(text))
					{
						if (!string.IsNullOrEmpty(text2))
						{
							xMLNode2["@" + text] = text2;
						}
						else
						{
							xMLNode2["@" + text] = true;
						}
					}
					i++;
					object arg_4DF_0;
					object expr_4C5 = arg_4DF_0 = xMLNodeList.Pop();
					if (!(expr_4C5 is XMLNode))
					{
						arg_4DF_0 = RuntimeServices.Coerce(expr_4C5, typeof(XMLNode));
					}
					xMLNode2 = (XMLNode)arg_4DF_0;
					text = string.Empty;
					text2 = string.Empty;
				}
				else if (!flag5 && c == this.GT)
				{
					flag = false;
					flag3 = false;
					flag4 = false;
					if (!string.IsNullOrEmpty(text))
					{
						xMLNode2["@" + text] = text2;
					}
					text = string.Empty;
					text2 = string.Empty;
				}
				else if (flag3)
				{
					if (c == this.SPACE || c == this.EQUALS)
					{
						flag3 = false;
						flag4 = true;
					}
					else
					{
						text += c;
					}
				}
				else if (flag4)
				{
					if (c == this.QUOTE)
					{
						if (flag5)
						{
							flag4 = false;
							xMLNode2["@" + text] = text2;
							text2 = string.Empty;
							text = string.Empty;
							flag5 = false;
						}
						else
						{
							flag5 = true;
						}
					}
					else if (flag5)
					{
						text2 += c;
					}
					else if (c == this.SPACE)
					{
						flag4 = false;
						xMLNode2["@" + text] = text2;
						text2 = string.Empty;
						text = string.Empty;
					}
				}
				else if (c != this.SPACE)
				{
					flag3 = true;
					text = string.Empty + c;
					text2 = string.Empty;
					flag5 = false;
				}
			}
			else if (c == this.LT)
			{
				flag = true;
				flag2 = true;
			}
			else
			{
				text4 += c;
			}
		}
		return xMLNode;
	}

	[NotRenamed]
	public override void ParseXML(string content)
	{
	}

	[NotRenamed]
	public override string GetID(int index)
	{
		return string.Empty;
	}

	[NotRenamed]
	public override string GetData(int index)
	{
		return string.Empty;
	}

	[NotRenamed]
	public override int GetNodeCount()
	{
		return 0;
	}
}
