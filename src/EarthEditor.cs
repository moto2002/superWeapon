using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

public class EarthEditor : MonoBehaviour
{
	public UITexture MapBMP;

	public GameObject BattlePrefab;

	public GameObject BattleParent;

	public Camera MainCamera;

	public GameObject SaveButton;

	public GameObject ChangeButton;

	public UILabel ChangeButtonLabel;

	public Texture MapBMP_Blue;

	public Texture MapBMP_Red;

	private int now_type = 1;

	public Dictionary<int, EarthEditorStar> EarthEditorStarList = new Dictionary<int, EarthEditorStar>();

	public Dictionary<int, Battle> BattleList = new Dictionary<int, Battle>();

	private void Start()
	{
		this.ReadXML();
		Debug.Log("BattleList.count:" + this.BattleList.Count);
		this.ShowBattle(1);
		UIEventListener.Get(this.SaveButton.gameObject).onClick = new UIEventListener.VoidDelegate(this.Save);
		UIEventListener.Get(this.ChangeButton.gameObject).onClick = new UIEventListener.VoidDelegate(this.Change);
	}

	private void Save(GameObject ga)
	{
		Debug.Log("保存");
		foreach (KeyValuePair<int, EarthEditorStar> item in this.EarthEditorStarList)
		{
			XElement xElement = XElement.Load(Application.dataPath + "/StreamingAssets/PlannerDataXMl/Battle.xml");
			IEnumerable<XElement> source = from element in xElement.Elements("configure")
			where element.Attribute("id").Value == item.Value._ThisBattle.id.ToString()
			select element;
			if (source.Count<XElement>() > 0)
			{
				XElement xElement2 = source.First<XElement>();
				xElement2.SetAttributeValue("coord", string.Format("{0},{1},0", item.Value.JD, item.Value.WD));
			}
			xElement.Save(Application.dataPath + "/StreamingAssets/PlannerDataXMl/Battle.xml");
		}
		this.ReadXML();
		this.ShowBattle(this.now_type);
	}

	private void Change(GameObject ga)
	{
		this.ShowBattle((this.now_type != 1) ? 1 : 2);
	}

	private void Update()
	{
	}

	private void ShowBattle(int type)
	{
		this.now_type = type;
		this.MapBMP.mainTexture = ((type != 1) ? this.MapBMP_Red : this.MapBMP_Blue);
		this.ChangeButtonLabel.text = ((type != 1) ? "切换至普通副本" : "切换至军团副本");
		int num = 1;
		Transform[] componentsInChildren = this.BattleParent.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			if (transform != this.BattleParent.transform)
			{
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
		this.EarthEditorStarList.Clear();
		EarthEditorStar earthEditorStar = new EarthEditorStar();
		foreach (KeyValuePair<int, Battle> current in this.BattleList)
		{
			if (current.Value.type == type)
			{
				if (Mathf.Abs(current.Value.coord[0]) <= 180f)
				{
					if (Mathf.Abs(current.Value.coord[1]) <= 90f)
					{
						current.Value.number = num;
						num++;
						GameObject gameObject = UnityEngine.Object.Instantiate(this.BattlePrefab) as GameObject;
						gameObject.name = current.Key.ToString();
						gameObject.transform.parent = this.BattleParent.transform;
						gameObject.transform.localPosition = Vector3.zero;
						gameObject.transform.localScale = Vector3.one;
						gameObject.GetComponent<EarthEditorStar>()._ThisBattle = current.Value;
						gameObject.GetComponent<EarthEditorStar>().JD = current.Value.coord[0];
						gameObject.GetComponent<EarthEditorStar>().WD = current.Value.coord[1];
						gameObject.GetComponent<EarthEditorStar>().MainCamera = this.MainCamera;
						gameObject.GetComponent<EarthEditorStar>().Init();
						if (earthEditorStar != null)
						{
							gameObject.GetComponent<EarthEditorStar>().LastStar = earthEditorStar;
						}
						earthEditorStar = gameObject.GetComponent<EarthEditorStar>();
						this.EarthEditorStarList.Add(current.Key, gameObject.GetComponent<EarthEditorStar>());
					}
				}
			}
		}
	}

	private void ReadXML()
	{
		this.BattleList.Clear();
		string content = File.ReadAllText(Application.dataPath + "/StreamingAssets/PlannerDataXMl/Battle.xml", Encoding.UTF8);
		XMLParser xMLParser = new XMLParser();
		XMLNode xMLNode = xMLParser.Parse(content);
		XMLNodeList nodeList = xMLNode.GetNodeList("configures>0>configure");
		for (int i = 0; i < nodeList.Count; i++)
		{
			Battle battle = new Battle();
			battle.id = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@id"));
			battle.name = xMLNode.GetValue("configures>0>configure>" + i + ">@name");
			battle.type = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@type"));
			string value = xMLNode.GetValue("configures>0>configure>" + i + ">@nextId");
			if (!string.IsNullOrEmpty(value))
			{
				battle.nextId = int.Parse(value);
			}
			string value2 = xMLNode.GetValue("configures>0>configure>" + i + ">@coord");
			if (!string.IsNullOrEmpty(value2))
			{
				for (int j = 0; j < value2.Split(new char[]
				{
					','
				}).Length; j++)
				{
					battle.coord.Add(float.Parse(value2.Split(new char[]
					{
						','
					})[j]));
				}
			}
			this.BattleList.Add(battle.id, battle);
		}
	}
}
