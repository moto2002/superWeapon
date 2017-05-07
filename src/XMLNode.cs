using Boo.Lang;
using Boo.Lang.Runtime;
using System;
using UnityEngine;
using UnityScript.Lang;

[NotConverted, NotRenamed]
[Serializable]
public class XMLNode : Hash
{
	[NotRenamed]
	public override XMLNodeList GetNodeList(string path)
	{
		return this.GetObject(path) as XMLNodeList;
	}

	[NotRenamed]
	public override XMLNode GetNode(string path)
	{
		return this.GetObject(path) as XMLNode;
	}

	[NotRenamed]
	public override string GetValue(string path)
	{
		return this.GetObject(path) as string;
	}

	[NotRenamed]
	private object GetObject(string path)
	{
		string[] array = path.Split(new char[]
		{
			">"[0]
		});
		XMLNode xMLNode = this;
		XMLNodeList xMLNodeList = null;
		bool flag = false;
		object arg_12D_0;
		for (int i = 0; i < Extensions.get_length(array); i++)
		{
			if (flag)
			{
				object arg_5C_0;
				object expr_41 = arg_5C_0 = xMLNodeList[UnityBuiltins.parseInt(array[i])];
				if (!(expr_41 is XMLNode))
				{
					arg_5C_0 = RuntimeServices.Coerce(expr_41, typeof(XMLNode));
				}
				xMLNode = (XMLNode)arg_5C_0;
				flag = false;
			}
			else
			{
				object obj = xMLNode[array[i]];
				if (!(obj is UnityScript.Lang.Array))
				{
					if (i != Extensions.get_length(array) - 1)
					{
						string text = string.Empty;
						for (int j = 0; j <= i; j++)
						{
							text = text + ">" + array[j];
						}
						Debug.Log("xml path search truncated. Wanted: " + path + " got: " + text);
					}
					arg_12D_0 = obj;
					return arg_12D_0;
				}
				xMLNodeList = (XMLNodeList)(obj as UnityScript.Lang.Array);
				flag = true;
			}
		}
		arg_12D_0 = ((!flag) ? xMLNode : xMLNodeList);
		return arg_12D_0;
	}
}
