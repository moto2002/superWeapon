using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

public class MapEditor : MonoBehaviour
{
	public bool ALLXMLTranslation;

	public Transform Cube_Parent;

	public Transform Cube;

	public Transform Cube_Reb;

	public Transform Cube_Gray;

	public Transform Cube_Yellow;

	public Transform Cube_Green;

	public Transform Camera3D_Parent;

	public Camera MainCamera2D;

	public Camera MainCamera3D;

	public Transform Camera3D_Parent1;

	public Camera MainCamera3D1;

	public Transform Camera3D_Parent2;

	public Camera MainCamera3D2;

	public GameObject BtnA;

	public GameObject BtnB;

	public GameObject BtnC;

	public GameObject BtnD;

	public GameObject BtnD1;

	public GameObject BtnD2;

	public GameObject BtnD3;

	public GameObject BtnE;

	public GameObject BtnF;

	public UILabel InputLabel;

	public string InputLabel_Text;

	public UILabel InputLabel_TS;

	public Transform SM_Transform;

	public UILabel SM_Input1;

	public UILabel SM_Input2;

	public UILabel SM_Notice;

	public GameObject SMBtnA;

	public GameObject SMBtnB;

	public int NowMapDataID;

	public string NowMapDataName;

	public Transform BuildingChoose;

	public UILabel BC_Name;

	public UILabel BC_level_label;

	public UILabel BC_star_label;

	public UILabel BC_rank_label;

	public GameObject BC_Btn_Level_Up;

	public GameObject BC_Btn_Level_Down;

	public GameObject BC_Btn_Star_Up;

	public GameObject BC_Btn_Star_Down;

	public GameObject BC_Btn_Rank_Up;

	public GameObject BC_Btn_Rank_Down;

	public GameObject BC_Btn_Copy;

	public GameObject BC_Btn_Delete;

	public GameObject BC_Btn_Finish;

	public GameObject BC;

	public UIScrollView scrollView;

	public UIGrid grid;

	public UIDragScrollView drag;

	public string dataPath;

	public Dictionary<int, XMLMapData> XMLMapDataList = new Dictionary<int, XMLMapData>();

	public Dictionary<Vector2, XMLMapData> XMLResMapDataList = new Dictionary<Vector2, XMLMapData>();

	public Dictionary<int, BuildingModelData> BuildingModelDataList = new Dictionary<int, BuildingModelData>();

	public Dictionary<string, GameObject> BuildingModelList = new Dictionary<string, GameObject>();

	public int MapSize;

	private List<MapMatrix> MapMatrixX = new List<MapMatrix>();

	private int camera_no;

	private bool WallSPattern;

	public bool SameBuilding = true;

	public bool BC_tr_Hover;

	public List<BuildingModelScript> BuildingS;

	public BuildingModelScript NowChooseBuilding;

	public Transform cube;

	private bool c_used;

	private int cx;

	private int cz;

	private int csize;

	private bool result;

	public string OutMap;

	private int num = 100;

	private float speed_x;

	private float speed_y;

	private float speed_z;

	private float time0;

	private Vector2 xy;

	private Vector3 point;

	private float mousedowntime;

	public bool CameraCannotMove;

	private int basic_point_x;

	private int basic_point_z;

	private void Awake()
	{
		this.dataPath = Application.dataPath + "/StreamingAssets/win/";
		AssetBundle assetBundle = AssetBundle.CreateFromFile(this.dataPath + "AllTowers.msg");
		UnityEngine.Object[] array = assetBundle.LoadAll(typeof(GameObject));
		for (int i = 0; i < array.Length; i++)
		{
			UnityEngine.Object @object = array[i];
			this.BuildingModelList.Add(@object.name, @object as GameObject);
		}
		AssetBundle assetBundle2 = AssetBundle.CreateFromFile(this.dataPath + "AllResStyle_0.msg");
		UnityEngine.Object[] array2 = assetBundle2.LoadAll(typeof(GameObject));
		for (int j = 0; j < array2.Length; j++)
		{
			UnityEngine.Object object2 = array2[j];
			this.BuildingModelList.Add(object2.name, object2 as GameObject);
		}
		AssetBundle assetBundle3 = AssetBundle.CreateFromFile(this.dataPath + "AllTanks.msg");
		UnityEngine.Object[] array3 = assetBundle3.LoadAll(typeof(GameObject));
		for (int k = 0; k < array3.Length; k++)
		{
			UnityEngine.Object object3 = array3[k];
		}
		AssetBundle x = AssetBundle.CreateFromFile(this.dataPath + "level_A_3.map");
		if (assetBundle != null && assetBundle2 != null && x != null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			Application.LoadLevel("level_A_3");
		}
		else
		{
			Debug.Log("资源缺失，需更新资源");
		}
		this.ReadBuildingXML();
		this.ReadXMLMapDataList();
		this.ReadXMLResMapDataList();
		this.ReadBuildingLevelUPXML();
		this.ReadBuildingRankUPXML();
		this.ReadTowerRankUPXML();
		UIEventListener.Get(this.BtnA).onClick = new UIEventListener.VoidDelegate(this.BtnA_Call);
		UIEventListener.Get(this.BtnB).onClick = new UIEventListener.VoidDelegate(this.BtnB_Call);
		UIEventListener.Get(this.BtnC).onClick = new UIEventListener.VoidDelegate(this.BtnC_Call);
		UIEventListener.Get(this.BtnD).onClick = new UIEventListener.VoidDelegate(this.BtnD_Call);
		UIEventListener.Get(this.BtnD1).onClick = new UIEventListener.VoidDelegate(this.BtnD_Call);
		UIEventListener.Get(this.BtnD2).onClick = new UIEventListener.VoidDelegate(this.BtnD_Call);
		UIEventListener.Get(this.BtnD3).onClick = new UIEventListener.VoidDelegate(this.BtnD_Call);
		UIEventListener.Get(this.BtnE).onClick = new UIEventListener.VoidDelegate(this.BtnE_Call);
		UIEventListener.Get(this.BtnF).onClick = new UIEventListener.VoidDelegate(this.BtnF_Call);
		this.BtnD1.gameObject.SetActive(false);
		this.BtnD2.gameObject.SetActive(false);
		this.BtnD3.gameObject.SetActive(false);
		UIEventListener.Get(this.BC_Btn_Level_Up).onClick = new UIEventListener.VoidDelegate(this.BuildingChoose_LevelOrStarOrRank_Change);
		UIEventListener.Get(this.BC_Btn_Level_Down).onClick = new UIEventListener.VoidDelegate(this.BuildingChoose_LevelOrStarOrRank_Change);
		UIEventListener.Get(this.BC_Btn_Star_Up).onClick = new UIEventListener.VoidDelegate(this.BuildingChoose_LevelOrStarOrRank_Change);
		UIEventListener.Get(this.BC_Btn_Star_Down).onClick = new UIEventListener.VoidDelegate(this.BuildingChoose_LevelOrStarOrRank_Change);
		UIEventListener.Get(this.BC_Btn_Rank_Up).onClick = new UIEventListener.VoidDelegate(this.BuildingChoose_LevelOrStarOrRank_Change);
		UIEventListener.Get(this.BC_Btn_Rank_Down).onClick = new UIEventListener.VoidDelegate(this.BuildingChoose_LevelOrStarOrRank_Change);
		UIEventListener.Get(this.BC_Btn_Copy).onClick = new UIEventListener.VoidDelegate(this.BC_Btn_Copy_Call);
		UIEventListener.Get(this.BC_Btn_Delete).onClick = new UIEventListener.VoidDelegate(this.BC_Btn_Delete_Call);
		UIEventListener.Get(this.BC_Btn_Finish).onClick = new UIEventListener.VoidDelegate(this.BC_Btn_Finish_Call);
		UIEventListener.Get(this.BC).onHover = new UIEventListener.BoolDelegate(this.BC_Transform_OnHoverCall);
		UIEventListener.Get(this.BC_Btn_Level_Up).onHover = new UIEventListener.BoolDelegate(this.BC_Transform_OnHoverCall);
		UIEventListener.Get(this.BC_Btn_Level_Down).onHover = new UIEventListener.BoolDelegate(this.BC_Transform_OnHoverCall);
		UIEventListener.Get(this.BC_Btn_Star_Up).onHover = new UIEventListener.BoolDelegate(this.BC_Transform_OnHoverCall);
		UIEventListener.Get(this.BC_Btn_Star_Down).onHover = new UIEventListener.BoolDelegate(this.BC_Transform_OnHoverCall);
		UIEventListener.Get(this.BC_Btn_Rank_Up).onHover = new UIEventListener.BoolDelegate(this.BC_Transform_OnHoverCall);
		UIEventListener.Get(this.BC_Btn_Rank_Down).onHover = new UIEventListener.BoolDelegate(this.BC_Transform_OnHoverCall);
		this.BuildingChoose.gameObject.SetActive(false);
		UIEventListener.Get(this.SMBtnA).onClick = new UIEventListener.VoidDelegate(this.SMBtnA_Call);
		UIEventListener.Get(this.SMBtnB).onClick = new UIEventListener.VoidDelegate(this.SMBtnB_Call);
		this.SM_Notice.text = string.Empty;
		this.SM_Transform.gameObject.SetActive(false);
		this.MapSize = 70;
		for (int l = 0; l < this.MapSize; l++)
		{
			MapMatrix mapMatrix = new MapMatrix();
			for (int m = 0; m < this.MapSize; m++)
			{
				bool item = false;
				mapMatrix.Z.Add(item);
			}
			this.MapMatrixX.Add(mapMatrix);
		}
		Transform[] componentsInChildren = this.Cube.GetComponentsInChildren<Transform>();
		for (int n = 0; n < componentsInChildren.Length; n++)
		{
			Transform transform = componentsInChildren[n];
			if (transform.name == "Red")
			{
				transform.transform.renderer.material.color = Color.red;
				this.Cube_Reb = transform;
			}
			else if (transform.name == "Gray")
			{
				transform.transform.renderer.material.color = Color.gray;
				this.Cube_Gray = transform;
			}
			else if (transform.name == "Green")
			{
				transform.transform.renderer.material.color = new Color(0f, 0.85f, 1f, 1f);
				this.Cube_Green = transform;
			}
			else if (transform.name == "Yellow")
			{
				transform.transform.renderer.material.color = Color.yellow;
				this.Cube_Yellow = transform;
			}
		}
		this.Cube_Reb.parent = base.transform;
		this.Cube_Gray.parent = base.transform;
		this.Cube_Green.parent = base.transform;
		this.Cube_Yellow.parent = base.transform;
		this.ChangeColor(this.Cube, Color.gray);
		this.setMapCube();
		this.SameBuilding = true;
		if (this.ALLXMLTranslation)
		{
			this.StartALLXMLTranslation();
		}
	}

	private void StartALLXMLTranslation()
	{
		int num = 0;
		foreach (KeyValuePair<int, XMLMapData> current in this.XMLMapDataList)
		{
			int num2 = 50;
			if (current.Value.terrainType == 4)
			{
				num2 = 70;
			}
			string dataid = string.Empty + current.Value.dataId;
			string text = string.Empty;
			bool flag = false;
			if (current.Value.baseResType >= 100 && current.Value.terrainType == 3)
			{
				Debug.Log("开启修正：" + current.Value.dataId);
				flag = true;
				current.Value.terrainType = 4;
				num2 = 70;
			}
			else if (current.Value.baseResType >= 100 && current.Value.dataId != 91308 && current.Value.dataId != 91309 && current.Value.dataId != 91310 && current.Value.dataId != 91501 && current.Value.dataId != 91502 && current.Value.dataId != 91503 && current.Value.dataId != 91504 && current.Value.dataId != 91505 && current.Value.dataId != 91506 && current.Value.dataId != 91401 && current.Value.dataId != 91402 && current.Value.dataId != 91403 && current.Value.dataId != 91404 && current.Value.dataId != 91405 && current.Value.dataId != 91406 && current.Value.dataId != 91407 && current.Value.dataId != 91408 && current.Value.dataId != 91409 && current.Value.dataId != 91410)
			{
				Debug.Log("开启修正：" + current.Value.dataId);
				flag = true;
				current.Value.terrainType = 4;
				num2 = 70;
			}
			for (int i = 0; i < current.Value.towerList.Count; i++)
			{
				if (current.Value.towerList[i].id != 12)
				{
					if (i > 0)
					{
						text += "|";
					}
					TowerListData towerListData = new TowerListData();
					towerListData.id = current.Value.towerList[i].id;
					towerListData.level = current.Value.towerList[i].level;
					towerListData.star = 0;
					towerListData.rank = 0;
					if (flag)
					{
						towerListData.position = current.Value.towerList[i].position / 50 * num2 + (current.Value.towerList[i].position - 50 * Mathf.RoundToInt((float)(current.Value.towerList[i].position / 50)));
					}
					else
					{
						towerListData.position = current.Value.towerList[i].position / num2 * num2 + (current.Value.towerList[i].position - num2 * Mathf.RoundToInt((float)(current.Value.towerList[i].position / num2)));
					}
					string text2 = text;
					text = string.Concat(new object[]
					{
						text2,
						string.Empty,
						towerListData.id,
						",",
						towerListData.level,
						",",
						towerListData.star,
						",",
						towerListData.rank,
						",",
						towerListData.position
					});
				}
			}
			XElement xElement = XElement.Load(Application.dataPath + "/MapEditor/MapData.xml");
			IEnumerable<XElement> source = from element in xElement.Elements("configure")
			where element.Attribute("dataId").Value == dataid
			select element;
			if (source.Count<XElement>() > 0)
			{
				XElement xElement2 = source.First<XElement>();
				xElement2.SetAttributeValue("dataId", current.Value.dataId);
				xElement2.SetAttributeValue("dataName", current.Value.dataName);
				xElement2.SetAttributeValue("terrainType", current.Value.terrainType);
				xElement2.SetAttributeValue("towerList", text);
			}
			xElement.Save(Application.dataPath + "/MapEditor/MapData.xml");
			num++;
			Debug.Log(string.Concat(new object[]
			{
				"存储进度：",
				num,
				"/",
				this.XMLMapDataList.Count
			}));
		}
	}

	private void BtnA_Call(GameObject backButton)
	{
		for (int i = 0; i < this.BuildingS.Count; i++)
		{
			UnityEngine.Object.Destroy(this.BuildingS[i].gameObject);
		}
		this.BuildingS.Clear();
		Debug.Log("清空");
		for (int j = 0; j < this.MapMatrixX.Count; j++)
		{
			for (int k = 0; k < this.MapMatrixX[j].cube.Count; k++)
			{
				this.MapMatrixX[j].Z[k] = false;
				this.ChangeColor(this.MapMatrixX[j].cube[k].transform, Color.gray);
			}
		}
		this.NowMapDataID = 0;
		this.NowMapDataName = string.Empty;
		this.InputLabel.text = string.Empty;
		this.InputLabel_TS.text = "新建地图成功";
	}

	private void BtnB_Call(GameObject backButton)
	{
		if (this.InputLabel_Text == null || this.InputLabel_Text == string.Empty)
		{
			this.InputLabel_TS.text = "输入DATAID不能为空";
			return;
		}
		int key = this.XMLMapDataList.SingleOrDefault((KeyValuePair<int, XMLMapData> a) => a.Value.dataName == this.InputLabel_Text).Key;
		if (key != 0)
		{
			for (int i = 0; i < this.BuildingS.Count; i++)
			{
				UnityEngine.Object.Destroy(this.BuildingS[i].gameObject);
			}
			this.BuildingS.Clear();
			XMLMapData xMLMapData = this.XMLMapDataList[key];
			if (xMLMapData.terrainType == 4)
			{
				this.MapSize = 50;
			}
			else
			{
				this.MapSize = 50;
			}
			this.NowMapDataID = key;
			this.NowMapDataName = xMLMapData.dataName;
			Debug.Log("清空");
			for (int j = 0; j < this.MapMatrixX.Count; j++)
			{
				for (int k = 0; k < this.MapMatrixX[j].cube.Count; k++)
				{
					this.MapMatrixX[j].Z[k] = false;
					this.ChangeColor(this.MapMatrixX[j].cube[k].transform, Color.gray);
				}
			}
			this.InputLabel_TS.text = "读取成功！地图名称：" + xMLMapData.dataName;
			for (int l = 0; l < xMLMapData.towerList.Count; l++)
			{
				this.CreateBuildingModel(xMLMapData.towerList[l], false, false, false);
			}
			return;
		}
		if (!this.XMLMapDataList.ContainsKey(int.Parse(this.InputLabel_Text)))
		{
			this.InputLabel_TS.text = "该DATAID对应地图不存在";
			return;
		}
		for (int m = 0; m < this.BuildingS.Count; m++)
		{
			UnityEngine.Object.Destroy(this.BuildingS[m].gameObject);
		}
		this.BuildingS.Clear();
		XMLMapData xMLMapData2 = this.XMLMapDataList[int.Parse(this.InputLabel_Text)];
		if (xMLMapData2.terrainType == 4)
		{
			this.MapSize = 70;
		}
		else
		{
			this.MapSize = 50;
		}
		this.NowMapDataID = xMLMapData2.dataId;
		this.NowMapDataName = xMLMapData2.dataName;
		Debug.Log("清空");
		for (int n = 0; n < this.MapMatrixX.Count; n++)
		{
			for (int num = 0; num < this.MapMatrixX[n].cube.Count; num++)
			{
				this.MapMatrixX[n].Z[num] = false;
				this.ChangeColor(this.MapMatrixX[n].cube[num].transform, Color.gray);
			}
		}
		this.InputLabel_TS.text = "读取成功！地图名称：" + xMLMapData2.dataName;
		for (int num2 = 0; num2 < xMLMapData2.towerList.Count; num2++)
		{
			this.CreateBuildingModel(xMLMapData2.towerList[num2], false, false, false);
		}
		Debug.Log("读取植被ID：" + xMLMapData2.terrainType);
		if (this.XMLResMapDataList.ContainsKey(new Vector2((float)xMLMapData2.terrainType, (float)xMLMapData2.baseResType)))
		{
			XMLMapData xMLMapData3 = this.XMLResMapDataList[new Vector2((float)xMLMapData2.terrainType, (float)xMLMapData2.baseResType)];
			Debug.Log("读取植被成功：植被长度：" + xMLMapData3.towerList.Count);
			for (int num3 = 0; num3 < xMLMapData3.towerList.Count; num3++)
			{
				this.CreateBuildingModel(xMLMapData3.towerList[num3], false, false, true);
			}
		}
		else
		{
			Debug.Log("读取植被失败");
		}
	}

	private void BtnC_Call(GameObject backButton)
	{
		this.NowChooseBuilding = null;
		this.SM_Transform.transform.position = Vector3.zero;
		this.SM_Transform.gameObject.SetActive(true);
		this.SM_Input1.text = string.Empty + this.NowMapDataID;
		this.SM_Input2.text = string.Empty + this.NowMapDataName;
		this.SM_Notice.text = string.Empty;
	}

	private void BtnD_Call(GameObject backButton)
	{
		if (backButton.name == "BtnD")
		{
			if (this.BtnD1.gameObject.activeSelf)
			{
				this.BtnD1.gameObject.SetActive(false);
				this.BtnD2.gameObject.SetActive(false);
				this.BtnD3.gameObject.SetActive(false);
				this.CameraCannotMove = false;
				UIDragScrollView[] componentsInChildren = this.grid.GetComponentsInChildren<UIDragScrollView>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					UIDragScrollView uIDragScrollView = componentsInChildren[i];
					if (uIDragScrollView)
					{
						UnityEngine.Object.Destroy(uIDragScrollView.gameObject);
					}
				}
			}
			else
			{
				this.NowChooseBuilding = null;
				this.BtnD1.gameObject.SetActive(true);
				this.BtnD2.gameObject.SetActive(true);
				this.BtnD3.gameObject.SetActive(true);
				this.CameraCannotMove = true;
				this.SetBuildingCard(1);
			}
		}
		else if (backButton.name == "BtnD1")
		{
			this.SetBuildingCard(1);
		}
		else if (backButton.name == "BtnD2")
		{
			this.SetBuildingCard(2);
		}
		else if (backButton.name == "BtnD3")
		{
			this.SetBuildingCard(3);
		}
	}

	private void BtnE_Call(GameObject backButton)
	{
		this.camera_no++;
		if (this.camera_no % 2 == 0)
		{
			this.Camera3D_Parent1.gameObject.SetActive(true);
			this.Camera3D_Parent2.gameObject.SetActive(false);
			this.Camera3D_Parent = this.Camera3D_Parent1;
			this.MainCamera3D = this.MainCamera3D1;
			this.WallSPattern = false;
		}
		else if (this.camera_no % 2 == 1)
		{
			this.Camera3D_Parent2.gameObject.SetActive(true);
			this.Camera3D_Parent1.gameObject.SetActive(false);
			this.Camera3D_Parent = this.Camera3D_Parent2;
			this.MainCamera3D = this.MainCamera3D2;
			this.WallSPattern = true;
		}
	}

	private void BtnF_Call(GameObject backButton)
	{
		if (!this.SameBuilding)
		{
			this.SameBuilding = true;
			backButton.GetComponentInChildren<UILabel>().text = "[批量]\n已开启";
		}
		else
		{
			this.SameBuilding = false;
			backButton.GetComponentInChildren<UILabel>().text = "[批量]\n已关闭";
		}
	}

	private void SMBtnA_Call(GameObject backButton)
	{
		XElement xElement = XElement.Load(Application.dataPath + "/MapEditor/MapData.xml");
		IEnumerable<XElement> source = from element in xElement.Elements("configure")
		where element.Attribute("dataId").Value == this.SM_Input1.text
		select element;
		XMLMapData xMLMapData = new XMLMapData();
		xMLMapData.dataId = int.Parse(this.SM_Input1.text);
		xMLMapData.dataName = this.SM_Input2.text;
		if (this.MapSize == 70)
		{
			xMLMapData.terrainType = 4;
		}
		else
		{
			xMLMapData.terrainType = 3;
		}
		string text = string.Empty;
		for (int i = 0; i < this.BuildingS.Count; i++)
		{
			if (i > 0)
			{
				text += "|";
			}
			TowerListData towerListData = new TowerListData();
			towerListData.id = this.BuildingS[i].id;
			towerListData.level = this.BuildingS[i].level;
			towerListData.star = this.BuildingS[i].star;
			towerListData.rank = this.BuildingS[i].rank;
			towerListData.position = (int)this.BuildingS[i].transform.position.x * this.MapSize + (int)this.BuildingS[i].transform.position.z;
			xMLMapData.towerList.Add(i, towerListData);
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				string.Empty,
				towerListData.id,
				",",
				towerListData.level,
				",",
				towerListData.star,
				",",
				towerListData.rank,
				",",
				towerListData.position
			});
			if (this.BuildingS[i].id == 1)
			{
				Debug.Log("  towerlist:" + text);
			}
		}
		if (source.Count<XElement>() > 0)
		{
			XElement xElement2 = source.First<XElement>();
			xElement2.SetAttributeValue("dataId", xMLMapData.dataId);
			xElement2.SetAttributeValue("dataName", xMLMapData.dataName);
			xElement2.SetAttributeValue("terrainType", xMLMapData.terrainType);
			xElement2.SetAttributeValue("towerList", text);
		}
		else
		{
			XElement content = new XElement("configure", new object[]
			{
				new XAttribute("dataId", xMLMapData.dataId),
				new XAttribute("dataName", xMLMapData.dataName),
				new XAttribute("dataType", 0),
				new XAttribute("terrainType", xMLMapData.terrainType),
				new XAttribute("baseResType", 0),
				new XAttribute("towerList", text)
			});
			xElement.Add(content);
		}
		xElement.Save(Application.dataPath + "/MapEditor/MapData.xml");
		this.SM_Notice.text = "保存成功";
		this.InputLabel.text = this.SM_Input1.text;
		this.InputLabel_TS.text = "保存成功！当前地图名称：" + this.SM_Input2.text;
		this.NowMapDataID = int.Parse(this.SM_Input1.text);
		this.NowMapDataName = this.SM_Input2.text;
		if (this.XMLMapDataList.ContainsKey(this.NowMapDataID))
		{
			this.XMLMapDataList.Remove(this.NowMapDataID);
			this.XMLMapDataList.Add(xMLMapData.dataId, xMLMapData);
		}
		else
		{
			this.XMLMapDataList.Add(xMLMapData.dataId, xMLMapData);
		}
		this.SM_Transform.gameObject.SetActive(false);
	}

	private void SMBtnB_Call(GameObject backButton)
	{
		this.SM_Transform.gameObject.SetActive(false);
	}

	private void BuildingCard_Call(GameObject backButton)
	{
		this.CreateBuildingModel(new TowerListData
		{
			id = backButton.GetComponent<BuildingModelScript>().id,
			position = 2000
		}, true, false, false);
		this.BtnD1.gameObject.SetActive(false);
		this.BtnD2.gameObject.SetActive(false);
		this.BtnD3.gameObject.SetActive(false);
		this.CameraCannotMove = false;
		UIDragScrollView[] componentsInChildren = this.grid.GetComponentsInChildren<UIDragScrollView>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			UIDragScrollView uIDragScrollView = componentsInChildren[i];
			if (uIDragScrollView)
			{
				UnityEngine.Object.Destroy(uIDragScrollView.gameObject);
			}
		}
	}

	private void SetBuildingCard(int type)
	{
		UIDragScrollView[] componentsInChildren = this.grid.GetComponentsInChildren<UIDragScrollView>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			UIDragScrollView uIDragScrollView = componentsInChildren[i];
			if (uIDragScrollView)
			{
				UnityEngine.Object.Destroy(uIDragScrollView.gameObject);
			}
		}
		for (int j = 0; j < this.BuildingModelDataList.Count; j++)
		{
			if (this.BuildingModelDataList[j].type == 1 || this.BuildingModelDataList[j].type == 2 || this.BuildingModelDataList[j].type == 3)
			{
				if (type == 1)
				{
					if (this.BuildingModelDataList[j].secondType != 2 && this.BuildingModelDataList[j].secondType != 3 && this.BuildingModelDataList[j].secondType != 6 && this.BuildingModelDataList[j].secondType != 19 && this.BuildingModelDataList[j].secondType != 20)
					{
						goto IL_452;
					}
				}
				else if (type == 2)
				{
					if (this.BuildingModelDataList[j].secondType != 8 && this.BuildingModelDataList[j].secondType != 9)
					{
						goto IL_452;
					}
				}
				else if (type == 3 && this.BuildingModelDataList[j].secondType != 1 && this.BuildingModelDataList[j].secondType != 4 && this.BuildingModelDataList[j].secondType != 5 && this.BuildingModelDataList[j].secondType != 15 && this.BuildingModelDataList[j].secondType != 21)
				{
					goto IL_452;
				}
				GameObject gameObject = NGUITools.AddChild(this.grid.gameObject, this.drag.gameObject);
				Transform[] componentsInChildren2 = gameObject.GetComponentsInChildren<Transform>();
				for (int k = 0; k < componentsInChildren2.Length; k++)
				{
					Transform transform = componentsInChildren2[k];
					if (transform.name == "Name")
					{
						transform.GetComponent<UILabel>().text = this.BuildingModelDataList[j].name;
					}
				}
				UIEventListener.Get(gameObject).onDoubleClick = new UIEventListener.VoidDelegate(this.BuildingCard_Call);
				gameObject.AddComponent<BuildingModelScript>();
				BuildingModelScript component = gameObject.GetComponent<BuildingModelScript>();
				component.id = this.BuildingModelDataList[j].id;
				component.name = this.BuildingModelDataList[j].name;
				component.size = this.BuildingModelDataList[j].size;
				component.bodySize = this.BuildingModelDataList[j].bodySize;
				if (this.BuildingModelList.ContainsKey(this.BuildingModelDataList[j].bodyId))
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate(this.BuildingModelList[this.BuildingModelDataList[j].bodyId], gameObject.transform.position, new Quaternion(0f, 0f, 0f, 0f)) as GameObject;
					gameObject2.transform.parent = gameObject.transform;
					gameObject2.transform.localPosition = new Vector3(0f, 20f, -100f);
					float num = (float)((5 - this.BuildingModelDataList[j].bodySize) / 2 * 10 + 20);
					float num2 = num / gameObject2.transform.localScale.x;
					gameObject2.transform.localScale = Vector3.one * num;
					gameObject2.transform.localEulerAngles = new Vector3(30f, 154f, -18f);
					Transform[] componentsInChildren3 = gameObject2.GetComponentsInChildren<Transform>();
					for (int l = 0; l < componentsInChildren3.Length; l++)
					{
						Transform transform2 = componentsInChildren3[l];
						transform2.gameObject.layer = 5;
						if (transform2.GetComponent<ParticleSystem>())
						{
							transform2.GetComponent<ParticleSystem>().startSize *= num2;
							transform2.GetComponent<ParticleSystem>().startSpeed = 0f;
						}
					}
				}
			}
			IL_452:;
		}
		this.grid.cellWidth = 225f;
		this.grid.Reposition();
	}

	private void BuildingChoose_LevelOrStarOrRank_Change(GameObject backButton)
	{
		if (backButton.name == "Btn_Level_Up")
		{
			this.NowChooseBuilding.level = Mathf.Min(this.BuildingModelDataList[this.NowChooseBuilding.id].level_bodyId.Count - 1, this.NowChooseBuilding.level + 1);
		}
		else if (backButton.name == "Btn_Level_Down")
		{
			this.NowChooseBuilding.level = Mathf.Max(0, this.NowChooseBuilding.level - 1);
		}
		if (backButton.name == "Btn_Star_Up")
		{
			this.NowChooseBuilding.star = Mathf.Min(5, this.NowChooseBuilding.star + 1);
		}
		else if (backButton.name == "Btn_Star_Down")
		{
			this.NowChooseBuilding.star = Mathf.Max(0, this.NowChooseBuilding.star - 1);
		}
		if (backButton.name == "Btn_Rank_Up")
		{
			this.NowChooseBuilding.rank = Mathf.Min(2, this.NowChooseBuilding.rank + 1);
		}
		else if (backButton.name == "Btn_Rank_Down")
		{
			this.NowChooseBuilding.rank = Mathf.Max(0, this.NowChooseBuilding.rank - 1);
		}
		this.CheckBuildModel(this.NowChooseBuilding);
		this.BC_level_label.text = "当前等级：" + this.NowChooseBuilding.level;
		this.BC_star_label.text = "当前星级：" + this.NowChooseBuilding.star;
		this.BC_rank_label.text = "当前阶级：" + this.NowChooseBuilding.rank;
		if (this.SameBuilding)
		{
			List<BuildingModelScript> list = new List<BuildingModelScript>();
			for (int i = 0; i < this.BuildingS.Count; i++)
			{
				if (!(this.BuildingS[i] == this.NowChooseBuilding))
				{
					if (this.BuildingS[i].id == this.NowChooseBuilding.id)
					{
						this.BuildingS[i].level = this.NowChooseBuilding.level;
						this.BuildingS[i].star = this.NowChooseBuilding.star;
						this.BuildingS[i].rank = this.NowChooseBuilding.rank;
						list.Add(this.BuildingS[i]);
					}
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				this.CheckBuildModel(list[j]);
			}
		}
	}

	private void BC_Btn_Copy_Call(GameObject backButton)
	{
		this.CreateBuildingModel(new TowerListData
		{
			id = this.NowChooseBuilding.id,
			level = this.NowChooseBuilding.level,
			star = this.NowChooseBuilding.star,
			rank = this.NowChooseBuilding.rank
		}, true, true, false);
	}

	private void BC_Btn_Delete_Call(GameObject backButton)
	{
		this.BuildingS.Remove(this.NowChooseBuilding);
		this.SetMapMatrixUsed(false, (int)this.NowChooseBuilding.transform.position.x, (int)this.NowChooseBuilding.transform.position.z, this.NowChooseBuilding.size);
		UnityEngine.Object.Destroy(this.NowChooseBuilding.gameObject);
		this.NowChooseBuilding = null;
		this.BuildingChoose.gameObject.SetActive(false);
	}

	private void BC_Btn_Finish_Call(GameObject backButton)
	{
		if (this.SameBuilding)
		{
			List<BuildingModelScript> list = new List<BuildingModelScript>();
			for (int i = 0; i < this.BuildingS.Count; i++)
			{
				if (!(this.BuildingS[i] == this.NowChooseBuilding))
				{
					if (this.BuildingS[i].id == this.NowChooseBuilding.id)
					{
						this.BuildingS[i].level = this.NowChooseBuilding.level;
						this.BuildingS[i].star = this.NowChooseBuilding.star;
						this.BuildingS[i].rank = this.NowChooseBuilding.rank;
						list.Add(this.BuildingS[i]);
					}
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				this.CheckBuildModel(list[j]);
			}
		}
		this.NowChooseBuilding = null;
		this.BuildingChoose.gameObject.SetActive(false);
	}

	private void BC_Transform_OnHoverCall(GameObject backButton, bool hover)
	{
		this.BC_tr_Hover = hover;
	}

	private void CheckBuildModel(BuildingModelScript BM)
	{
		string b = this.BuildingModelDataList[BM.id].bodyId;
		b = this.BuildingModelDataList[BM.id].level_bodyId[BM.level];
		if (this.BuildingModelDataList[BM.id].secondType == 2 || this.BuildingModelDataList[BM.id].secondType == 3 || this.BuildingModelDataList[BM.id].secondType == 8)
		{
			if (this.BuildingModelDataList[BM.id].rank_bodyId.Count == 0)
			{
				return;
			}
			b = this.BuildingModelDataList[BM.id].rank_bodyId[BM.rank];
		}
		if (BM.bodyId != b)
		{
			TowerListData towerListData = new TowerListData();
			towerListData.id = BM.id;
			towerListData.level = BM.level;
			towerListData.star = BM.star;
			towerListData.rank = BM.rank;
			towerListData.position = this.MapSize * (int)BM.transform.position.x + (int)BM.transform.position.z;
			if (this.NowChooseBuilding == BM)
			{
				this.CreateBuildingModel(towerListData, false, true, false);
			}
			else
			{
				this.CreateBuildingModel(towerListData, false, false, false);
			}
			this.BuildingS.Remove(BM);
			UnityEngine.Object.Destroy(BM.gameObject);
		}
	}

	private void CreateBuildingModel(TowerListData towerdata, bool NewCreate = false, bool ModelChange = false, bool IsRes = false)
	{
		string text = this.BuildingModelDataList[towerdata.id].bodyId;
		if (!IsRes)
		{
			text = this.BuildingModelDataList[towerdata.id].level_bodyId[towerdata.level];
			if (towerdata.id != 15)
			{
				if (this.BuildingModelDataList[towerdata.id].secondType == 2 || this.BuildingModelDataList[towerdata.id].secondType == 3 || this.BuildingModelDataList[towerdata.id].secondType == 8)
				{
					if (this.BuildingModelDataList[towerdata.id].rank_bodyId.Count == 0)
					{
						Debug.Log("无模型：" + towerdata.id);
						return;
					}
					text = this.BuildingModelDataList[towerdata.id].rank_bodyId[towerdata.rank];
				}
			}
		}
		if (!this.BuildingModelList.ContainsKey(text))
		{
			Debug.Log(string.Concat(new object[]
			{
				"未获得模型：",
				text,
				" towerdata.id:",
				towerdata.id
			}));
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(this.BuildingModelList[text], new Vector3((float)Mathf.RoundToInt((float)(towerdata.position / this.MapSize)), 0f, (float)(towerdata.position - this.MapSize * Mathf.RoundToInt((float)(towerdata.position / this.MapSize)))), new Quaternion(0f, 0f, 0f, 0f)) as GameObject;
		BuildingModelScript buildingModelScript = gameObject.AddComponent<BuildingModelScript>();
		buildingModelScript.id = towerdata.id;
		buildingModelScript.level = towerdata.level;
		buildingModelScript.star = towerdata.star;
		buildingModelScript.rank = towerdata.rank;
		buildingModelScript.name = this.BuildingModelDataList[towerdata.id].name;
		buildingModelScript.bodyId = text;
		buildingModelScript.size = this.BuildingModelDataList[towerdata.id].size;
		buildingModelScript.bodySize = this.BuildingModelDataList[towerdata.id].bodySize;
		buildingModelScript.position = towerdata.position;
		this.BuildingS.Add(buildingModelScript);
		if (NewCreate)
		{
			this.NowChooseBuilding = buildingModelScript;
			this.NowChooseBuilding.NewBuild = true;
			this.BC_Name.text = this.NowChooseBuilding.name;
			this.BC_level_label.text = "当前等级：" + this.NowChooseBuilding.level;
			this.BC_star_label.text = "当前星级：" + this.NowChooseBuilding.star;
			this.BC_rank_label.text = "当前阶级：" + this.NowChooseBuilding.rank;
			buildingModelScript.transform.position = new Vector3(43f, 0f, 43f);
		}
		else
		{
			this.SetMapMatrixUsed(true, (int)gameObject.transform.position.x, (int)gameObject.transform.position.z, buildingModelScript.size);
		}
		if (ModelChange)
		{
			this.NowChooseBuilding = buildingModelScript;
		}
	}

	private void setMapCube()
	{
		for (int i = 0; i < this.MapMatrixX.Count; i++)
		{
			for (int j = 0; j < this.MapMatrixX[i].Z.Count; j++)
			{
				this.cube = (UnityEngine.Object.Instantiate(this.Cube) as Transform);
				this.cube.transform.parent = this.Cube_Parent.transform;
				this.cube.transform.position = new Vector3((float)i, 0f, (float)j);
				this.cube.transform.localScale = new Vector3(1f, 0f, 1f);
				this.MapMatrixX[i].cube.Add(this.cube.gameObject);
			}
		}
	}

	private bool CheckMapMatrixUsed(bool used, int x, int z, int size)
	{
		if (this.c_used == used && this.cx == x && this.cz == z && this.csize == size)
		{
			return this.result;
		}
		this.c_used = used;
		this.cx = x;
		this.cz = z;
		this.csize = size;
		bool flag = true;
		for (int i = 0; i < this.MapMatrixX.Count; i++)
		{
			for (int j = 0; j < this.MapMatrixX[i].cube.Count; j++)
			{
				if (this.MapMatrixX[i].cube[j].transform.name == "C_Red")
				{
					this.ChangeColor(this.MapMatrixX[i].cube[j].transform, Color.green);
				}
				if (this.MapMatrixX[i].cube[j].transform.name == "C_Yellow")
				{
					this.ChangeColor(this.MapMatrixX[i].cube[j].transform, Color.gray);
				}
			}
		}
		int num = x - size / 2;
		int num2 = z - size / 2;
		for (int k = 0; k < size; k++)
		{
			for (int l = 0; l < size; l++)
			{
				if (this.MapMatrixX[num + k].cube[num2 + l].transform.name == "C_Gray" || this.MapMatrixX[num + k].cube[num2 + l].transform.name == "C_Gray(Clone)")
				{
					this.ChangeColor(this.MapMatrixX[num + k].cube[num2 + l].transform, Color.yellow);
				}
				if (this.MapMatrixX[num + k].Z[num2 + l])
				{
					this.ChangeColor(this.MapMatrixX[num + k].cube[num2 + l].transform, Color.red);
					flag = false;
				}
			}
		}
		this.result = flag;
		return flag;
	}

	private void SetMapMatrixUsed(bool used, int x, int z, int size)
	{
		if (used)
		{
			int num = x - size / 2;
			int num2 = z - size / 2;
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					if (num + i >= 0 && num + i <= this.MapMatrixX.Count - 1)
					{
						if (num2 + j >= 0 && num2 + j <= this.MapMatrixX[num + i].Z.Count - 1)
						{
							this.MapMatrixX[num + i].Z[num2 + j] = true;
							this.ChangeColor(this.MapMatrixX[num + i].cube[num2 + j].transform, Color.green);
						}
					}
				}
			}
		}
		else
		{
			int num3 = x - size / 2;
			int num4 = z - size / 2;
			for (int k = 0; k < size; k++)
			{
				for (int l = 0; l < size; l++)
				{
					this.MapMatrixX[num3 + k].Z[num4 + l] = false;
					this.ChangeColor(this.MapMatrixX[num3 + k].cube[num4 + l].transform, Color.gray);
				}
			}
		}
	}

	private void ReadBuildingXML()
	{
		string content = File.ReadAllText(Application.dataPath + "/StreamingAssets/PlannerDataXMl/Building.xml", Encoding.UTF8);
		XMLParser xMLParser = new XMLParser();
		XMLNode xMLNode = xMLParser.Parse(content);
		XMLNodeList nodeList = xMLNode.GetNodeList("configures>0>configure");
		UnitConst.GetInstance().buildingConst = new NewBuildingInfo[nodeList.Count];
		for (int i = 0; i < nodeList.Count; i++)
		{
			BuildingModelData buildingModelData = new BuildingModelData();
			buildingModelData.id = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@id"));
			buildingModelData.bodyId = xMLNode.GetValue("configures>0>configure>" + i + ">@bodyID");
			buildingModelData.size = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@size"));
			buildingModelData.bodySize = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@bodySize"));
			buildingModelData.name = xMLNode.GetValue("configures>0>configure>" + i + ">@name");
			buildingModelData.type = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@type"));
			buildingModelData.secondType = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@secondType"));
			this.BuildingModelDataList.Add(buildingModelData.id, buildingModelData);
		}
	}

	private void ReadBuildingLevelUPXML()
	{
		string content = File.ReadAllText(Application.dataPath + "/StreamingAssets/PlannerDataXMl/BuildingUpSet.xml", Encoding.UTF8);
		XMLParser xMLParser = new XMLParser();
		XMLNode xMLNode = xMLParser.Parse(content);
		XMLNodeList nodeList = xMLNode.GetNodeList("configures>0>configure");
		UnitConst.GetInstance().buildingConst = new NewBuildingInfo[nodeList.Count];
		for (int i = 0; i < nodeList.Count; i++)
		{
			int key = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@itemId"));
			int key2 = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@level"));
			string value = xMLNode.GetValue("configures>0>configure>" + i + ">@bodyID");
			if (this.BuildingModelDataList.ContainsKey(key))
			{
				this.BuildingModelDataList[key].level_bodyId.Add(key2, value);
			}
		}
	}

	private void ReadBuildingRankUPXML()
	{
		string content = File.ReadAllText(Application.dataPath + "/StreamingAssets/PlannerDataXMl/BuildingUpGrade.xml", Encoding.UTF8);
		XMLParser xMLParser = new XMLParser();
		XMLNode xMLNode = xMLParser.Parse(content);
		XMLNodeList nodeList = xMLNode.GetNodeList("configures>0>configure");
		UnitConst.GetInstance().buildingConst = new NewBuildingInfo[nodeList.Count];
		for (int i = 0; i < nodeList.Count; i++)
		{
			int key = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@itemId"));
			int key2 = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@level"));
			string value = xMLNode.GetValue("configures>0>configure>" + i + ">@bodyId");
			if (this.BuildingModelDataList.ContainsKey(key))
			{
				this.BuildingModelDataList[key].rank_bodyId.Add(key2, value);
			}
		}
	}

	private void ReadTowerRankUPXML()
	{
		string content = File.ReadAllText(Application.dataPath + "/StreamingAssets/PlannerDataXMl/TowerUpGrade.xml", Encoding.UTF8);
		XMLParser xMLParser = new XMLParser();
		XMLNode xMLNode = xMLParser.Parse(content);
		XMLNodeList nodeList = xMLNode.GetNodeList("configures>0>configure");
		UnitConst.GetInstance().buildingConst = new NewBuildingInfo[nodeList.Count];
		for (int i = 0; i < nodeList.Count; i++)
		{
			int key = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@itemId"));
			int key2 = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@level"));
			string value = xMLNode.GetValue("configures>0>configure>" + i + ">@bodyId");
			if (this.BuildingModelDataList.ContainsKey(key))
			{
				this.BuildingModelDataList[key].rank_bodyId.Add(key2, value);
			}
		}
	}

	private void ReadXMLMapDataList()
	{
		string content = File.ReadAllText(Application.dataPath + "/MapEditor/MapData.xml", Encoding.UTF8);
		XMLParser xMLParser = new XMLParser();
		XMLNode xMLNode = xMLParser.Parse(content);
		XMLNodeList nodeList = xMLNode.GetNodeList("configures>0>configure");
		UnitConst.GetInstance().buildingConst = new NewBuildingInfo[nodeList.Count];
		for (int i = 0; i < nodeList.Count; i++)
		{
			XMLMapData xMLMapData = new XMLMapData();
			xMLMapData.dataId = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@dataId"));
			if (this.XMLMapDataList.ContainsKey(xMLMapData.dataId))
			{
				Debug.Log("Is the sameID:" + xMLMapData.dataId);
			}
			else
			{
				xMLMapData.dataName = xMLNode.GetValue("configures>0>configure>" + i + ">@dataName");
				xMLMapData.dataType = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@dataType"));
				xMLMapData.terrainType = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@terrainType"));
				xMLMapData.baseResType = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@baseResType"));
				string value = xMLNode.GetValue("configures>0>configure>" + i + ">@towerList");
				int num = 0;
				if (!string.IsNullOrEmpty(value))
				{
					string[] array = value.Split(new char[]
					{
						'|'
					});
					for (int j = 0; j < array.Length; j++)
					{
						string text = array[j];
						if (text.Contains(","))
						{
							TowerListData towerListData = new TowerListData();
							towerListData.id = int.Parse(text.Split(new char[]
							{
								','
							})[0]);
							towerListData.level = int.Parse(text.Split(new char[]
							{
								','
							})[1]);
							if (int.Parse(text.Split(new char[]
							{
								','
							})[2]) > 100000000)
							{
								towerListData.star = 0;
								towerListData.rank = 0;
								towerListData.position = int.Parse(text.Split(new char[]
								{
									','
								})[2]);
							}
							else
							{
								towerListData.star = int.Parse(text.Split(new char[]
								{
									','
								})[2]);
								towerListData.rank = int.Parse(text.Split(new char[]
								{
									','
								})[3]);
								towerListData.position = int.Parse(text.Split(new char[]
								{
									','
								})[4]);
							}
							if (towerListData.position >= 2550)
							{
							}
							bool flag = false;
							foreach (KeyValuePair<int, TowerListData> current in xMLMapData.towerList)
							{
								if (current.Value.position == towerListData.position)
								{
									flag = true;
									Debug.Log(string.Concat(new object[]
									{
										"data.dataId:",
										xMLMapData.dataId,
										"    towerList_data.id :",
										towerListData.id,
										"position:",
										towerListData.position
									}));
								}
								else
								{
									if (xMLMapData.terrainType == 4)
									{
										this.MapSize = 70;
									}
									else
									{
										this.MapSize = 50;
									}
									int num2 = (this.BuildingModelDataList[current.Value.id].size + this.BuildingModelDataList[towerListData.id].size) / 2;
									int num3 = current.Value.position / this.MapSize;
									int num4 = current.Value.position - this.MapSize * Mathf.RoundToInt((float)(current.Value.position / this.MapSize));
									int num5 = towerListData.position / this.MapSize;
									int num6 = towerListData.position - this.MapSize * Mathf.RoundToInt((float)(towerListData.position / this.MapSize));
									if (Mathf.Abs(num3 - num5) < num2 && Mathf.Abs(num4 - num6) < num2 && this.num >= 90)
									{
										Debug.Log(string.Concat(new object[]
										{
											"建筑有重叠：关卡ID",
											xMLMapData.dataId,
											"  重叠建筑A：ID：",
											current.Value.id,
											"位置：",
											current.Value.position,
											"  重叠建筑B：ID：",
											towerListData.id,
											"位置：",
											towerListData.position
										}));
										this.num--;
									}
								}
							}
							if (!flag)
							{
								xMLMapData.towerList.Add(num, towerListData);
								num++;
							}
							else
							{
								Debug.Log("重复坐标");
							}
						}
					}
				}
				this.XMLMapDataList.Add(xMLMapData.dataId, xMLMapData);
			}
		}
	}

	private void ReadXMLResMapDataList()
	{
		string content = File.ReadAllText(Application.dataPath + "/StreamingAssets/PlannerDataXMl/TerrainDatas.xml", Encoding.UTF8);
		XElement xElement = XElement.Load(Application.dataPath + "/StreamingAssets/PlannerDataXMl/TerrainDatas.xml");
		XMLParser xMLParser = new XMLParser();
		XMLNode xMLNode = xMLParser.Parse(content);
		XMLNodeList nodeList = xMLNode.GetNodeList("configures>0>configure");
		UnitConst.GetInstance().buildingConst = new NewBuildingInfo[nodeList.Count];
		for (int i = 0; i < nodeList.Count; i++)
		{
			XMLMapData data = new XMLMapData();
			data.terrainType = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@typeId"));
			data.dataName = xMLNode.GetValue("configures>0>configure>" + i + ">@terrName");
			data.dataType = int.Parse(xMLNode.GetValue("configures>0>configure>" + i + ">@sizeX"));
			IEnumerable<XElement> source = from element in xElement.Elements("configure")
			where element.Attribute("typeId").Value == string.Empty + data.terrainType
			select element;
			if (source.Count<XElement>() > 0)
			{
				IEnumerable<XAttribute> source2 = source.Elements("res").Attributes("resType");
				IEnumerable<XAttribute> source3 = source.Elements("res").Attributes("resInfo");
				for (int j = 0; j < source2.Count<XAttribute>(); j++)
				{
					XMLMapData xMLMapData = new XMLMapData();
					xMLMapData.terrainType = data.terrainType;
					xMLMapData.dataName = data.dataName;
					xMLMapData.dataType = data.dataType;
					xMLMapData.baseResType = int.Parse(source2.ElementAt(j).Value);
					string value = source3.ElementAt(j).Value;
					int num = 0;
					if (!string.IsNullOrEmpty(value))
					{
						string[] array = value.Split(new char[]
						{
							'|'
						});
						for (int k = 0; k < array.Length; k++)
						{
							string text = array[k];
							if (text.Contains(","))
							{
								TowerListData towerListData = new TowerListData();
								towerListData.id = int.Parse(text.Split(new char[]
								{
									','
								})[0]);
								towerListData.level = int.Parse(text.Split(new char[]
								{
									','
								})[1]);
								towerListData.star = 0;
								towerListData.rank = 0;
								towerListData.position = int.Parse(text.Split(new char[]
								{
									','
								})[2]);
								bool flag = false;
								foreach (KeyValuePair<int, TowerListData> current in xMLMapData.towerList)
								{
									if (current.Value.position == towerListData.position)
									{
										flag = true;
										Debug.Log("重复坐标：" + data.dataId);
									}
								}
								if (!flag)
								{
									xMLMapData.towerList.Add(num, towerListData);
									num++;
								}
							}
						}
					}
					this.XMLResMapDataList.Add(new Vector2((float)xMLMapData.terrainType, (float)xMLMapData.baseResType), xMLMapData);
				}
			}
		}
	}

	private void ChangeColor(Transform building, Color color)
	{
		if ((color == Color.red && building.name == "C_Red") || (color == Color.gray && building.name == "C_Gray(Clone)") || (color == Color.gray && building.name == "C_Gray") || (color == Color.green && building.name == "C_Green") || (color == Color.yellow && building.name == "C_Yellow"))
		{
			return;
		}
		Transform[] componentsInChildren = building.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			if (transform != building)
			{
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
		if (color == Color.red)
		{
			building.name = "C_Red";
			Transform transform2 = UnityEngine.Object.Instantiate(this.Cube_Reb) as Transform;
			transform2.parent = building.transform;
			transform2.transform.localPosition = Vector3.zero;
			transform2.transform.localScale = new Vector3(1f, 0f, 1f);
		}
		else if (color == Color.gray)
		{
			building.name = "C_Gray";
			Transform transform3 = UnityEngine.Object.Instantiate(this.Cube_Gray) as Transform;
			transform3.parent = building.transform;
			transform3.transform.localPosition = Vector3.zero;
			transform3.transform.localScale = new Vector3(1f, 0f, 1f);
		}
		else if (color == Color.green)
		{
			building.name = "C_Green";
			Transform transform4 = UnityEngine.Object.Instantiate(this.Cube_Green) as Transform;
			transform4.parent = building.transform;
			transform4.transform.localPosition = Vector3.zero;
			transform4.transform.localScale = new Vector3(1f, 0f, 1f);
		}
		else if (color == Color.yellow)
		{
			building.name = "C_Yellow";
			Transform transform5 = UnityEngine.Object.Instantiate(this.Cube_Yellow) as Transform;
			transform5.parent = building.transform;
			transform5.transform.localPosition = Vector3.zero;
			transform5.transform.localScale = new Vector3(1f, 0f, 1f);
		}
	}

	private void Update()
	{
		this.InputLabel_Text = this.InputLabel.text;
		if (this.SM_Transform.gameObject.activeSelf)
		{
			if (this.SM_Input1.text == null || this.SM_Input1.text == string.Empty)
			{
				this.SM_Notice.text = "所保存地图ID不能为空";
				this.SM_Notice.color = Color.red;
			}
			else if (this.XMLMapDataList.ContainsKey(int.Parse(this.SM_Input1.text)))
			{
				this.SM_Notice.text = "已存在同ID地图，确认保存将覆盖同ID地图";
				this.SM_Notice.color = Color.red;
			}
			else
			{
				this.SM_Notice.text = "不存在同ID地图，确认保存将新增该ID地图";
				this.SM_Notice.color = Color.yellow;
			}
			return;
		}
		if (Input.GetMouseButton(0))
		{
			if (this.xy.x == 0f && this.xy.y == 0f)
			{
				this.xy = new Vector2(Input.mousePosition.x / (float)Screen.width - 0.5f, Input.mousePosition.y / (float)Screen.height - 0.5f);
			}
			this.time0 += Time.deltaTime;
			if (this.time0 > 0.05f)
			{
				this.time0 = 0f;
				this.speed_x += Input.mousePosition.x / (float)Screen.width - 0.5f - this.xy.x;
				this.speed_z += Input.mousePosition.y / (float)Screen.height - 0.5f - this.xy.y;
				this.xy = new Vector2(Input.mousePosition.x / (float)Screen.width - 0.5f, Input.mousePosition.y / (float)Screen.height - 0.5f);
			}
		}
		else
		{
			this.time0 = 0f;
			this.xy = Vector2.zero;
		}
		if (Input.GetAxis("Mouse ScrollWheel") != 0f)
		{
			this.speed_y = 3f * Input.GetAxis("Mouse ScrollWheel");
		}
		this.speed_x = Mathf.MoveTowards(this.speed_x, 0f, Mathf.Abs(this.speed_x) * 5f * Time.deltaTime);
		this.speed_y = Mathf.MoveTowards(this.speed_y, 0f, Mathf.Abs(this.speed_y) * 5f * Time.deltaTime);
		this.speed_z = Mathf.MoveTowards(this.speed_z, 0f, Mathf.Abs(this.speed_z) * 5f * Time.deltaTime);
		if (!this.CameraCannotMove)
		{
			if (this.NowChooseBuilding != null && this.NowChooseBuilding.transform.position.y == 0.5f)
			{
				this.Camera3D_Parent.transform.Translate(5f * this.speed_x, 0f, 5f * this.speed_z);
			}
			else
			{
				this.Camera3D_Parent.transform.Translate(-10f * this.speed_x, 0f, -10f * this.speed_z);
			}
		}
		this.MainCamera3D.transform.Translate(0f, 0f, 6f * this.speed_y);
		if (Input.GetMouseButtonDown(0))
		{
			for (int i = 0; i < this.BuildingS.Count; i++)
			{
				float num = Vector2.Distance(new Vector2(this.point.x, this.point.z), new Vector2(this.BuildingS[i].transform.position.x, this.BuildingS[i].transform.position.z));
				if (num <= (float)this.BuildingS[i].bodySize * 0.8f)
				{
					this.NowChooseBuilding = this.BuildingS[i];
					this.NowChooseBuilding.NewBuild = false;
					this.NowChooseBuilding.OldPos = new Vector2(this.BuildingS[i].transform.position.x, this.BuildingS[i].transform.position.z);
					this.BC_Name.text = this.NowChooseBuilding.name;
					this.BC_level_label.text = "当前等级：" + this.NowChooseBuilding.level;
					this.BC_star_label.text = "当前星级：" + this.NowChooseBuilding.star;
					this.BC_rank_label.text = "当前阶级：" + this.NowChooseBuilding.rank;
				}
			}
		}
		if (Input.GetMouseButton(0) && this.NowChooseBuilding != null)
		{
			float num2 = Vector2.Distance(new Vector2(this.point.x, this.point.z), new Vector2(this.NowChooseBuilding.transform.position.x, this.NowChooseBuilding.transform.position.z));
			if (num2 <= (float)this.NowChooseBuilding.bodySize * 0.8f)
			{
				this.mousedowntime += Time.deltaTime;
			}
			if (this.mousedowntime > 0.5f || this.CameraCannotMove)
			{
				if (this.NowChooseBuilding.transform.position.y == 0f && this.mousedowntime > 0.5f)
				{
					this.SetMapMatrixUsed(false, (int)this.NowChooseBuilding.transform.position.x, (int)this.NowChooseBuilding.transform.position.z, this.NowChooseBuilding.size);
				}
				this.NowChooseBuilding.transform.position = new Vector3(Mathf.Max((float)(0 + this.NowChooseBuilding.size / 2), Mathf.Min((float)(50 - this.NowChooseBuilding.size / 2 - 1), this.point.x)), 0.5f, Mathf.Max((float)(0 + this.NowChooseBuilding.size / 2), Mathf.Min((float)(50 - this.NowChooseBuilding.size / 2 - 1), this.point.z)));
				this.CheckMapMatrixUsed(false, (int)this.NowChooseBuilding.transform.position.x, (int)this.NowChooseBuilding.transform.position.z, this.NowChooseBuilding.size);
			}
		}
		else
		{
			this.mousedowntime = 0f;
			if (this.NowChooseBuilding != null && this.NowChooseBuilding.transform.position.y == 0.5f)
			{
				if (this.CheckMapMatrixUsed(false, (int)this.NowChooseBuilding.transform.position.x, (int)this.NowChooseBuilding.transform.position.z, this.NowChooseBuilding.size))
				{
					this.NowChooseBuilding.transform.position = new Vector3((float)((int)this.NowChooseBuilding.transform.position.x), 0f, (float)((int)this.NowChooseBuilding.transform.position.z));
					this.SetMapMatrixUsed(true, (int)this.NowChooseBuilding.transform.position.x, (int)this.NowChooseBuilding.transform.position.z, this.NowChooseBuilding.size);
				}
				else if (this.NowChooseBuilding.NewBuild)
				{
					this.BuildingS.Remove(this.NowChooseBuilding);
					UnityEngine.Object.Destroy(this.NowChooseBuilding.gameObject);
				}
				else
				{
					this.NowChooseBuilding.transform.position = new Vector3((float)((int)this.NowChooseBuilding.OldPos.x), 0f, (float)((int)this.NowChooseBuilding.OldPos.y));
					this.SetMapMatrixUsed(true, (int)this.NowChooseBuilding.transform.position.x, (int)this.NowChooseBuilding.transform.position.z, this.NowChooseBuilding.size);
				}
				for (int j = 0; j < this.MapMatrixX.Count; j++)
				{
					for (int k = 0; k < this.MapMatrixX[j].cube.Count; k++)
					{
						if (this.MapMatrixX[j].cube[k].transform.name == "C_Red")
						{
							this.ChangeColor(this.MapMatrixX[j].cube[k].transform, Color.green);
						}
						if (this.MapMatrixX[j].cube[k].transform.name == "C_Yellow")
						{
							this.ChangeColor(this.MapMatrixX[j].cube[k].transform, Color.gray);
						}
					}
				}
				this.NowChooseBuilding = null;
			}
		}
		if (this.NowChooseBuilding != null)
		{
			this.BuildingChoose.gameObject.SetActive(true);
			this.Cube_Parent.transform.position = new Vector3(0f, 0f, 0f);
			Vector3 vector = Camera.main.WorldToScreenPoint(this.NowChooseBuilding.transform.position);
			vector = new Vector3(vector.x - 0.5f * (float)Screen.width, vector.y - 0.54f * (float)Screen.height, 0f);
			this.BuildingChoose.transform.localPosition = new Vector3(vector.x, vector.y, 0f);
		}
		else
		{
			this.BuildingChoose.gameObject.SetActive(false);
			this.Cube_Parent.transform.position = new Vector3(50000f, 0f, 0f);
		}
		if (this.BuildingChoose.gameObject.activeSelf)
		{
			if (this.BC_tr_Hover)
			{
			}
		}
		else
		{
			this.BC_tr_Hover = false;
		}
		if (this.CameraCannotMove)
		{
			return;
		}
		if (!this.BC_tr_Hover)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit))
			{
				GameObject gameObject = raycastHit.collider.gameObject;
				if (gameObject.name != "shuinidi")
				{
					return;
				}
				this.point = raycastHit.point;
			}
		}
		if (this.WallSPattern)
		{
			if (Input.GetKey(KeyCode.Backspace))
			{
				for (int l = 0; l < this.BuildingS.Count; l++)
				{
					if (this.BuildingS[l].id == 90)
					{
						float num3 = Vector2.Distance(new Vector2(this.point.x, this.point.z), new Vector2(this.BuildingS[l].transform.position.x, this.BuildingS[l].transform.position.z));
						if (num3 <= (float)this.BuildingS[l].bodySize * 0.8f)
						{
							GameObject gameObject2 = this.BuildingS[l].gameObject;
							this.SetMapMatrixUsed(false, (int)this.BuildingS[l].transform.position.x, (int)this.BuildingS[l].transform.position.z, this.BuildingS[l].size);
							this.BuildingS.Remove(this.BuildingS[l]);
							UnityEngine.Object.Destroy(gameObject2.gameObject);
						}
					}
				}
			}
			if (Input.GetKey(KeyCode.Space) && this.basic_point_x != (int)this.point.x && this.basic_point_z != (int)this.point.z)
			{
				this.basic_point_x = (int)this.point.x;
				this.basic_point_z = (int)this.point.z;
				if (this.CheckMapMatrixUsed(false, (int)this.point.x, (int)this.point.z, 2))
				{
					this.CreateBuildingModel(new TowerListData
					{
						id = 90,
						position = (int)this.point.x * this.MapSize + (int)this.point.z
					}, false, false, false);
				}
			}
		}
		if (Input.GetKey(KeyCode.F9))
		{
			float num4 = 0f;
			foreach (BuildingModelScript current in this.BuildingS)
			{
				if (current.id == 90)
				{
					num4 += 1f;
				}
			}
			this.InputLabel_TS.text = "城墙数量：" + num4;
		}
	}
}
