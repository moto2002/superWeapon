using DG.Tweening;
using msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LegionMapManager : MonoBehaviour
{
	public enum BattleType
	{
		普通副本,
		军团副本
	}

	public LegionMapManager.BattleType NowBattleType;

	public static LegionMapManager _inst;

	public GameObject Earth_Model_Parent;

	public Body_Model Earth_Model;

	public Body_Model Earth_Light1_Model;

	public Body_Model Earth_Light2_Model;

	public Body_Model Space_Model;

	public Light Space_Light;

	public Camera Earth_Camera;

	public bool IsCompile;

	private float model_pos_y;

	public Material Material_Earth_Blue;

	public Material Material_Earth_Red;

	public Material Material_Earth_Light_Blue;

	public Material Material_Earth_Light_Red;

	private Dictionary<int, Geographic> Geographic_List = new Dictionary<int, Geographic>();

	public int PlayBattle;

	private bool CSWarZoneList_request;

	private float time0;

	private Vector2 xy;

	public float speed_x;

	public float speed_z;

	private float time1;

	public bool Enlargement;

	public EarthStar EnlargementStar;

	private Vector3 Enlargement_Camera_Pos;

	public float Enlargement_Earth_Y;

	public GameObject Enlargement_Camera_Go;

	private bool First;

	public float Earth_JD;

	private List<UITexture> Geo_Star = new List<UITexture>();

	public List<EarthStar> EarthStarList = new List<EarthStar>();

	private EarthStar LastEarthStar;

	public float timeA;

	public float timeB;

	public void OnDestroy()
	{
		LegionMapManager._inst = null;
	}

	public void OnEnable()
	{
	}

	public void OnDisable()
	{
	}

	private void Awake()
	{
		LegionMapManager._inst = this;
	}

	private void CreateEarth_Model()
	{
		FuncUIManager.inst.HideFuncUI("ArmyPeoplePanlManager");
		FuncUIManager.inst.HideFuncUI("MainUIPanel");
		FuncUIManager.inst.HideFuncUI("ResourcePanel");
		FuncUIManager.inst.OpenFuncUI("LegionBattlePanel", SenceType.Island);
		base.transform.parent = UIManager.inst.transform;
		this.Earth_Model_Parent = new GameObject("Earth_Model_Parent");
		this.Earth_Model_Parent.transform.parent = base.transform;
		this.Earth_Model_Parent.transform.localPosition = Vector3.zero;
		this.Earth_Model_Parent.transform.localScale = Vector3.one;
		this.Earth_Model_Parent.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
		if (!this.Earth_Model)
		{
			this.Earth_Model = PoolManage.Ins.GetModelByBundleByName("earth", null);
			this.Earth_Light1_Model = PoolManage.Ins.GetModelByBundleByName("earth", null);
			this.Earth_Light2_Model = PoolManage.Ins.GetModelByBundleByName("earth", null);
			this.Earth_Light1_Model.tr.parent = this.Earth_Model.tr;
			this.Earth_Light1_Model.tr.localPosition = Vector3.zero;
			this.Earth_Light1_Model.tr.localScale = Vector3.zero;
			this.Earth_Light2_Model.tr.parent = this.Earth_Model.tr;
			this.Earth_Light2_Model.tr.localPosition = Vector3.zero;
			this.Earth_Light2_Model.tr.localScale = Vector3.zero;
		}
		this.Space_Model = PoolManage.Ins.GetModelByBundleByName("Space", null);
		this.Space_Model.tr.parent = this.Earth_Model_Parent.transform;
		this.Space_Model.tr.localPosition = new Vector3(0f, -6f, 0f);
		this.Space_Model.tr.localEulerAngles = new Vector3(-90f, 0f, 0f);
		this.Space_Model.tr.localScale = Vector3.one * 150f;
		this.Earth_Model.tr.parent = this.Earth_Model_Parent.transform;
		this.Earth_Model.tr.localPosition = Vector3.zero;
		this.Earth_Model.tr.localEulerAngles = new Vector3(-25f, 0f, 0f);
		if (!this.Earth_Camera)
		{
			GameObject gameObject = new GameObject("Earth_Camera_UI");
			gameObject.AddComponent<Camera>();
			gameObject.transform.parent = base.transform;
			gameObject.transform.localPosition = new Vector3(0f, 0f, -4f);
			gameObject.AddComponent<UICamera>();
			gameObject.GetComponent<UICamera>().eventType = UICamera.EventType.World_3D;
			gameObject.GetComponent<UICamera>().eventReceiverMask = 32;
			this.Earth_Camera = gameObject.GetComponent<Camera>();
			GameTools.GetCompentIfNoAddOne<AudioListener>(gameObject);
			this.Earth_Camera.fieldOfView = 40f;
			this.Earth_Camera.clearFlags = CameraClearFlags.Color;
			this.Earth_Camera.backgroundColor = Color.black;
			this.Earth_Camera.cullingMask = 32;
			this.Earth_Camera.clearFlags = CameraClearFlags.Depth;
			GameObject gameObject2 = new GameObject("Earth_Camera_Model");
			gameObject2.AddComponent<Camera>();
			gameObject2.transform.parent = gameObject.transform;
			gameObject2.transform.localPosition = new Vector3(0f, 0f, 0f);
			gameObject2.GetComponent<Camera>().fieldOfView = this.Earth_Camera.fieldOfView;
			gameObject2.GetComponent<Camera>().cullingMask = 1;
			gameObject2.GetComponent<Camera>().depth = -1f;
			this.Enlargement_Camera_Go = new GameObject(" Enlargement_Camera_Go");
			this.Enlargement_Camera_Go.transform.parent = base.transform;
			this.Earth_Model.renderer.material.shader = Shader.Find("Diffuse");
			this.Earth_Model.renderer.material.color = new Color(0.35f, 0.35f, 0.35f, 1f);
			GameObject gameObject3 = new GameObject("Earth_light");
			gameObject3.AddComponent<Light>();
			gameObject3.transform.parent = this.Earth_Model_Parent.transform;
			gameObject3.transform.localPosition = new Vector3(0f, 1.42f, 0f);
			gameObject3.GetComponent<Light>().type = LightType.Point;
			gameObject3.GetComponent<Light>().range = 5f;
			gameObject3.GetComponent<Light>().intensity = 0f;
			this.Space_Light = gameObject3.GetComponent<Light>();
			GameObject gameObject4 = new GameObject("Earth_light");
			gameObject4.AddComponent<Light>();
			gameObject4.transform.parent = this.Earth_Model_Parent.transform;
			gameObject4.transform.localPosition = new Vector3(0f, 1.42f, 0f);
			gameObject4.GetComponent<Light>().type = LightType.Point;
			gameObject4.GetComponent<Light>().range = 1.47f;
			gameObject4.GetComponent<Light>().intensity = 4f;
		}
		this.Earth_Model.tr.localPosition = new Vector3(1f, 1f, -1f);
		this.model_pos_y = 1f;
		this.model_pos_y = 0f;
		this.speed_x = -5f;
		this.First = true;
	}

	private void Start()
	{
		this.Material_Earth_Blue = (UnityEngine.Object.Instantiate(Resources.Load("EarthMaterial/Earth_Blue")) as Material);
		this.Material_Earth_Red = (UnityEngine.Object.Instantiate(Resources.Load("EarthMaterial/Earth_Red")) as Material);
		this.Material_Earth_Light_Blue = (UnityEngine.Object.Instantiate(Resources.Load("EarthMaterial/Earth_Light_Blue")) as Material);
		this.Material_Earth_Light_Red = (UnityEngine.Object.Instantiate(Resources.Load("EarthMaterial/Earth_Light_Red")) as Material);
	}

	private void AddGeographic(int id, string geo_name, string mission_name, float east, float west, float south, float north)
	{
		Geographic geographic = new Geographic();
		geographic.Geographic_id = id;
		geographic.Geographic_name = geo_name;
		geographic.Mission_name = mission_name;
		geographic.east = east;
		geographic.west = west;
		geographic.south = south;
		geographic.north = north;
		this.Geographic_List.Add(id, geographic);
	}

	private void Geo_Star_Btn_CallBack(GameObject ga)
	{
		ga.GetComponent<ButtonClick>().isSendLua = false;
		if (this.NowBattleType == LegionMapManager.BattleType.普通副本)
		{
			if (int.Parse(ga.transform.parent.name) > LegionMapManager._inst.PlayBattle)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("前置关卡未通过", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
		}
		else if (this.NowBattleType == LegionMapManager.BattleType.军团副本)
		{
			if (int.Parse(ga.transform.parent.name) > LegionMapManager._inst.PlayBattle)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("所选军团副本尚未解锁", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
			if (int.Parse(ga.transform.parent.name) < LegionMapManager._inst.PlayBattle)
			{
				HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("所选军团副本已被攻陷", "Battle"), HUDTextTool.TextUITypeEnum.Num5);
				return;
			}
		}
		this.SetEnlargementStar(ga.GetComponent<EarthStar>());
		base.StartCoroutine(this.OpenEarthStar(ga));
		UnityEngine.Debug.Log("地球点：" + ga.transform.parent.name);
	}

	[DebuggerHidden]
	private IEnumerator OpenEarthStar(GameObject ga)
	{
		LegionMapManager.<OpenEarthStar>c__Iterator1C <OpenEarthStar>c__Iterator1C = new LegionMapManager.<OpenEarthStar>c__Iterator1C();
		<OpenEarthStar>c__Iterator1C.ga = ga;
		<OpenEarthStar>c__Iterator1C.<$>ga = ga;
		<OpenEarthStar>c__Iterator1C.<>f__this = this;
		return <OpenEarthStar>c__Iterator1C;
	}

	public void OpenPlayBattleBySelf()
	{
		if (SenceInfo.battleResource == SenceInfo.BattleResource.NormalBattleFight || SenceInfo.battleResource == SenceInfo.BattleResource.LegionBattleFight)
		{
		}
		foreach (EarthStar current in this.EarthStarList)
		{
			if (current.transform.parent.name == this.PlayBattle.ToString())
			{
				this.SetEnlargementStar(current);
				this.Geo_Star_Btn_CallBack(current.gameObject);
				break;
			}
		}
	}

	public void Init(LegionMapManager.BattleType battleType = LegionMapManager.BattleType.普通副本)
	{
		if (!this.CSWarZoneList_request)
		{
			this.CSWarZoneList_request = true;
			CSWarZoneList cSWarZoneList = new CSWarZoneList();
			cSWarZoneList.zoneId = 1;
			ClientMgr.GetNet().SendHttp(5006, cSWarZoneList, null, null);
		}
		if (CameraControl.inst)
		{
			CameraControl.inst.gameObject.SetActive(false);
		}
		EventManager.Instance.AddEvent(EventManager.EventType.Geo_Star_Btn, new EventManager.VoidDelegate(this.Geo_Star_Btn_CallBack));
		this.CreateEarth_Model();
		this.ChangeBattleType(battleType, true);
		if (this.NowBattleType == LegionMapManager.BattleType.普通副本)
		{
			this.Earth_Model.tr.GetComponent<MeshRenderer>().material = this.Material_Earth_Blue;
		}
		else if (this.NowBattleType == LegionMapManager.BattleType.军团副本)
		{
			if (HeroInfo.GetInstance().playerGroupId != 0L)
			{
				ArmyGroupHandler.CG_CSLegionData(HeroInfo.GetInstance().playerGroupId, delegate(bool isError)
				{
					if (!isError)
					{
						LegionBattlePanel._inst.RefreshTitleDes();
					}
				});
			}
			this.Earth_Model.tr.GetComponent<MeshRenderer>().material = this.Material_Earth_Red;
		}
		this.CreateEarthStar();
	}

	private void CreateEarthStar()
	{
		this.Geographic_List.Clear();
		if (this.NowBattleType == LegionMapManager.BattleType.普通副本)
		{
			this.PlayBattle = 1;
			int num = 0;
			foreach (KeyValuePair<int, Battle> current in UnitConst.GetInstance().BattleConst)
			{
				if (current.Value.type == 1)
				{
					num++;
					float f = 0f;
					float f2 = 0f;
					float f3 = 0f;
					float f4 = 0f;
					if (current.Value.coord.Count >= 1)
					{
						if (Mathf.Abs(current.Value.coord[0]) > 180f)
						{
							continue;
						}
						if (current.Value.coord[0] >= 0f)
						{
							f = current.Value.coord[0];
						}
						else
						{
							f2 = current.Value.coord[0];
						}
					}
					if (current.Value.coord.Count >= 2)
					{
						if (Mathf.Abs(current.Value.coord[1]) > 90f)
						{
							continue;
						}
						if (current.Value.coord[1] <= 0f)
						{
							f3 = current.Value.coord[1];
						}
						else
						{
							f4 = current.Value.coord[1];
						}
					}
					this.AddGeographic(num, num.ToString(), current.Value.name, Mathf.Abs(f), Mathf.Abs(f2), Mathf.Abs(f3), Mathf.Abs(f4));
					if (current.Value.isCanSweep || current.Value.battleBox > 0)
					{
						this.PlayBattle++;
					}
				}
			}
		}
		else if (this.NowBattleType == LegionMapManager.BattleType.军团副本)
		{
			UnityEngine.Debug.Log("HeroInfo.GetInstance().MyLegionBattleData.NowBattleId:" + HeroInfo.GetInstance().MyLegionBattleData.NowBattleId);
			UnityEngine.Debug.Log("UnitConst.GetInstance().BattleConst[HeroInfo.GetInstance().MyLegionBattleData.NowBattleId].number:" + UnitConst.GetInstance().BattleConst[HeroInfo.GetInstance().MyLegionBattleData.NowBattleId].number);
			this.PlayBattle = UnitConst.GetInstance().BattleConst[HeroInfo.GetInstance().MyLegionBattleData.NowBattleId].number;
			int num2 = 0;
			foreach (KeyValuePair<int, Battle> current2 in UnitConst.GetInstance().BattleConst)
			{
				if (current2.Value.type == 2)
				{
					num2++;
					float f5 = 0f;
					float f6 = 0f;
					float f7 = 0f;
					float f8 = 0f;
					if (current2.Value.coord.Count >= 1)
					{
						if (Mathf.Abs(current2.Value.coord[0]) > 180f)
						{
							continue;
						}
						if (current2.Value.coord[0] >= 0f)
						{
							f5 = current2.Value.coord[0];
						}
						else
						{
							f6 = current2.Value.coord[0];
						}
					}
					if (current2.Value.coord.Count >= 2)
					{
						if (Mathf.Abs(current2.Value.coord[1]) > 90f)
						{
							continue;
						}
						if (current2.Value.coord[1] <= 0f)
						{
							f7 = current2.Value.coord[1];
						}
						else
						{
							f8 = current2.Value.coord[1];
						}
					}
					this.AddGeographic(num2, num2.ToString(), current2.Value.name, Mathf.Abs(f5), Mathf.Abs(f6), Mathf.Abs(f7), Mathf.Abs(f8));
					if (current2.Value.isCanSweep || current2.Value.battleBox > 0)
					{
						this.PlayBattle++;
					}
				}
			}
		}
		this.LastEarthStar = null;
		for (int i = 0; i < this.EarthStarList.Count; i++)
		{
			if (this.EarthStarList[i] && this.EarthStarList[i].transform.parent.parent.parent)
			{
				UnityEngine.Object.Destroy(this.EarthStarList[i].transform.parent.parent.parent.gameObject);
			}
		}
		this.EarthStarList.Clear();
		foreach (KeyValuePair<int, Geographic> current3 in this.Geographic_List)
		{
			this.CreateGeographicPosition(current3.Value);
		}
	}

	public void SetEnlargementStar(EarthStar star)
	{
		this.Enlargement = true;
		this.EnlargementStar = star;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F8))
		{
			this.Space_Model.tr.localScale = Vector3.zero;
			this.IsCompile = true;
		}
		if (this.Earth_Model)
		{
			this.Earth_JD = this.Earth_Model.tr.localEulerAngles.z;
			this.model_pos_y = Mathf.MoveTowards(this.model_pos_y, 0f, Mathf.Max(0.1f, this.model_pos_y * 0.6f) * Time.deltaTime);
			this.Earth_Model.tr.localPosition = new Vector3(this.model_pos_y, 1f * this.model_pos_y, this.model_pos_y * this.model_pos_y);
			this.Space_Light.intensity = (1f - this.model_pos_y * 1.5f) * 8f;
			bool flag = false;
			if (LegionBattlePanel._inst && (LegionBattlePanel._inst.AllBattlePanelSprite.gameObject.activeSelf || LegionBattlePanel._inst.ChooseBattlePanelSprite.gameObject.activeSelf || LegionBattlePanel._inst.NBattlePanel.gameObject.activeSelf))
			{
				flag = true;
			}
			if (!flag && this.model_pos_y <= 0f && Input.GetMouseButton(0))
			{
				if (this.xy.x == 0f && this.xy.y == 0f)
				{
					this.xy = new Vector2(Input.mousePosition.x / (float)Screen.width - 0.5f, Input.mousePosition.y / (float)Screen.height - 0.5f);
					this.time1 = 0f;
				}
				this.time0 += Time.deltaTime;
				if (this.time0 > 0.05f)
				{
					this.time0 = 0f;
					if (this.speed_x * 50f * (Input.mousePosition.x / (float)Screen.width - 0.5f - this.xy.x) < 0f)
					{
						this.speed_x = 0f;
					}
					this.speed_x += 50f * (Input.mousePosition.x / (float)Screen.width - 0.5f - this.xy.x);
					this.speed_z += 50f * (Input.mousePosition.y / (float)Screen.height - 0.5f - this.xy.y);
					if (Mathf.Abs(this.speed_x) >= 10f || Mathf.Abs(this.speed_z) >= 10f)
					{
						this.Enlargement = false;
					}
					this.xy = new Vector2(Input.mousePosition.x / (float)Screen.width - 0.5f, Input.mousePosition.y / (float)Screen.height - 0.5f);
				}
			}
			else
			{
				this.time0 = 0f;
				this.xy = Vector2.zero;
				this.time1 += Time.deltaTime;
			}
			if (this.time1 > 2f)
			{
				this.speed_x = Mathf.MoveTowards(this.speed_x, 0f, this.time1 * this.time1 * Time.deltaTime);
			}
			if (this.model_pos_y <= 0f)
			{
				if (this.First)
				{
					this.First = false;
					foreach (EarthStar current in this.EarthStarList)
					{
						if (current.transform.parent.name == this.PlayBattle.ToString())
						{
							this.SetEnlargementStar(current);
							break;
						}
					}
				}
			}
			else
			{
				this.First = true;
			}
			this.speed_x = Mathf.Max(-50f, Mathf.Min(50f, this.speed_x));
			if (!this.Enlargement)
			{
				float num = 20f;
				this.Earth_Model.tr.localEulerAngles = new Vector3(-25f, 0f, this.Earth_Model.tr.localEulerAngles.z + -this.speed_x * Time.deltaTime);
				this.Enlargement_Camera_Go.transform.position = this.Earth_Model.tr.position + new Vector3(0f, 0f, -1.6f);
				this.Earth_Camera.gameObject.transform.position = new Vector3(Mathf.MoveTowards(this.Earth_Camera.gameObject.transform.position.x, this.Enlargement_Camera_Go.transform.position.x, Mathf.Abs(this.Earth_Camera.gameObject.transform.position.x - this.Enlargement_Camera_Go.transform.position.x) / num), Mathf.MoveTowards(this.Earth_Camera.gameObject.transform.position.y, this.Enlargement_Camera_Go.transform.position.y, Mathf.Abs(this.Earth_Camera.gameObject.transform.position.y - this.Enlargement_Camera_Go.transform.position.y) / num), Mathf.MoveTowards(this.Earth_Camera.gameObject.transform.position.z, this.Enlargement_Camera_Go.transform.position.z, Mathf.Abs(this.Earth_Camera.gameObject.transform.position.z - this.Enlargement_Camera_Go.transform.position.z) / num));
			}
			else if (this.EnlargementStar)
			{
				if (this.EnlargementStar.transform.parent.parent.parent.localEulerAngles.z > 0f)
				{
					this.Enlargement_Earth_Y = 180f - this.EnlargementStar.transform.parent.parent.parent.localEulerAngles.z;
				}
				else
				{
					this.Enlargement_Earth_Y = 360f + (-180f - this.EnlargementStar.transform.parent.parent.parent.localEulerAngles.z);
				}
				float num2;
				if (this.EnlargementStar.WD > 0f)
				{
					num2 = -0.475f * Mathf.Sin(this.EnlargementStar.WD * 0.0174532924f);
				}
				else
				{
					num2 = 0.475f * Mathf.Sin(Mathf.Abs(this.EnlargementStar.WD * 0.0174532924f));
				}
				num2 -= 0.15f;
				this.Enlargement_Camera_Go.transform.position = this.Earth_Model.tr.position + new Vector3(0f, num2, -0.85f);
				float num3 = 20f;
				this.Earth_Model.tr.localEulerAngles = new Vector3(-25f, 0f, Mathf.MoveTowardsAngle(this.Earth_Model.tr.localEulerAngles.z, this.Enlargement_Earth_Y, Mathf.Abs((this.Earth_Model.tr.localEulerAngles.z - this.Enlargement_Earth_Y) / num3)));
				this.Earth_Camera.gameObject.transform.position = new Vector3(Mathf.MoveTowards(this.Earth_Camera.gameObject.transform.position.x, this.Enlargement_Camera_Go.transform.position.x, Mathf.Abs(this.Earth_Camera.gameObject.transform.position.x - this.Enlargement_Camera_Go.transform.position.x) / num3), Mathf.MoveTowards(this.Earth_Camera.gameObject.transform.position.y, this.Enlargement_Camera_Go.transform.position.y, Mathf.Abs(this.Earth_Camera.gameObject.transform.position.y - this.Enlargement_Camera_Go.transform.position.y) / num3), Mathf.MoveTowards(this.Earth_Camera.gameObject.transform.position.z, this.Enlargement_Camera_Go.transform.position.z, Mathf.Abs(this.Earth_Camera.gameObject.transform.position.z - this.Enlargement_Camera_Go.transform.position.z) / num3));
			}
			for (int i = 0; i < this.Geo_Star.Count; i++)
			{
			}
		}
		float num4 = 0.470588237f;
		if (this.Earth_Light1_Model)
		{
			this.Earth_Light1_Model.renderer.sharedMaterial.SetColor("_TintColor", new Color(num4, num4, num4, 0.75f * (1f - Mathf.Abs(this.Earth_Light1_Model.tr.localScale.x - 1f))));
		}
		if (this.Earth_Light2_Model)
		{
			this.Earth_Light2_Model.renderer.sharedMaterial.SetColor("_TintColor", new Color(num4, num4, num4, 0.75f * (1f - Mathf.Abs(this.Earth_Light1_Model.tr.localScale.x - 1f))));
		}
	}

	public void CreateGeographicPosition(Geographic geo)
	{
		GameObject gameObject = new GameObject("GeographicPosition_Parent_0");
		gameObject.transform.parent = this.Earth_Model.tr;
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		GameObject gameObject2 = new GameObject("GeographicPosition_Parent_1");
		gameObject2.transform.parent = gameObject.transform;
		gameObject2.transform.localScale = Vector3.one;
		gameObject2.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		gameObject2.transform.localPosition = new Vector3(0f, 0f, 0f);
		GameObject gameObject3 = new GameObject("GeographicPosition");
		gameObject3.transform.parent = gameObject2.transform;
		gameObject3.transform.localScale = Vector3.one * 0.02f;
		gameObject3.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		gameObject3.transform.localPosition = new Vector3(0f, -0.475f, 0f);
		if (geo.Mission_name == "太空")
		{
			gameObject3.transform.localPosition = new Vector3(0f, UnityEngine.Random.Range(1.5f, 3.5f), 0f);
		}
		float num = 0f;
		if (geo.east != 0f)
		{
			num = -geo.east;
		}
		else if (geo.west != 0f)
		{
			num = geo.west;
		}
		float num2 = 0f;
		if (geo.south != 0f)
		{
			num2 = geo.south;
		}
		else if (geo.north != 0f)
		{
			num2 = -geo.north;
		}
		gameObject.transform.localEulerAngles = new Vector3(0f, 0f, num);
		gameObject2.transform.localEulerAngles = new Vector3(num2, 0f, 0f);
		gameObject3.name = geo.Geographic_name;
		gameObject2.name = geo.Mission_name;
		GameObject gameObject4 = UnityEngine.Object.Instantiate(UIManager.inst.Geo_BMP.gameObject) as GameObject;
		gameObject4.transform.parent = gameObject3.transform;
		gameObject4.transform.localPosition = Vector3.zero;
		gameObject4.transform.localScale = Vector3.one * 1.5f;
		gameObject4.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
		gameObject4.AddComponent<EarthStar>();
		gameObject4.GetComponent<EarthStar>().JD = num;
		gameObject4.GetComponent<EarthStar>().WD = num2;
		if (this.LastEarthStar != null)
		{
			this.LastEarthStar.NextEarthStar = gameObject4.GetComponent<EarthStar>();
			this.CalculateMiniStar(this.LastEarthStar, gameObject4.GetComponent<EarthStar>());
		}
		this.LastEarthStar = gameObject4.GetComponent<EarthStar>();
		this.EarthStarList.Add(gameObject4.GetComponent<EarthStar>());
		this.Geo_Star.Add(gameObject4.GetComponent<UITexture>());
	}

	public void CalculateMiniStar(EarthStar fromStar, EarthStar toStar)
	{
		float num = 5f;
		float num2 = toStar.JD - fromStar.JD;
		float num3 = 1f;
		if (Mathf.Abs(num2) > 180f)
		{
			if (num2 < 0f)
			{
				num2 = 360f + num2;
			}
			else
			{
				num2 = 360f - num2;
			}
			num3 = -1f;
		}
		int a = (int)(Mathf.Abs(num2) / num);
		float num4 = toStar.WD - fromStar.WD;
		float num5 = 1f;
		if (Mathf.Abs(num4) > 90f)
		{
			if (num4 > 0f)
			{
				num4 = 180f - num4;
			}
			else
			{
				num4 = 180f + num4;
			}
			num5 = -1f;
		}
		int b = (int)(Mathf.Abs(num4) / num * 2f);
		int num6 = Mathf.Max(a, b);
		fromStar.EarthStarLine = new List<GameObject>(num6);
		for (int i = 0; i < num6; i++)
		{
			GameObject gameObject = new GameObject("GeographicPosition_Parent_0");
			gameObject.transform.parent = this.Earth_Model.tr;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
			GameObject gameObject2 = new GameObject("GeographicPosition_Parent_1");
			gameObject2.transform.parent = gameObject.transform;
			gameObject2.transform.localScale = Vector3.one;
			gameObject2.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			gameObject2.transform.localPosition = new Vector3(0f, 0f, 0f);
			GameObject gameObject3 = new GameObject("GeographicPosition");
			gameObject3.transform.parent = gameObject2.transform;
			gameObject3.transform.localScale = Vector3.one * 0.02f;
			gameObject3.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			gameObject3.transform.localPosition = new Vector3(0f, -0.475f, 0f);
			float z;
			if (num3 == 1f)
			{
				z = fromStar.JD + num3 * (float)i * (toStar.JD - fromStar.JD) / (float)num6;
			}
			else if (toStar.JD > fromStar.JD)
			{
				z = fromStar.JD + num3 * (float)i * (360f - toStar.JD + fromStar.JD) / (float)num6;
			}
			else
			{
				z = fromStar.JD + num3 * (float)i * (360f + toStar.JD - fromStar.JD) / (float)num6;
			}
			float x;
			if (num5 == 1f)
			{
				x = fromStar.WD + num5 * (float)i * (toStar.WD - fromStar.WD) / (float)num6;
			}
			else if (toStar.WD > fromStar.WD)
			{
				x = fromStar.WD + num5 * (float)i * (180f - toStar.WD + fromStar.WD) / (float)num6;
			}
			else
			{
				x = fromStar.WD + num5 * (float)i * (180f + toStar.WD - fromStar.WD) / (float)num6;
			}
			gameObject.transform.localEulerAngles = new Vector3(0f, 0f, z);
			gameObject2.transform.localEulerAngles = new Vector3(x, 0f, 0f);
			fromStar.EarthStarLine.Add(gameObject3);
		}
		fromStar.SetEarthStarLineRenderer();
	}

	public void ChangeBattleType(LegionMapManager.BattleType battleType, bool start = false)
	{
		if (this.NowBattleType == battleType)
		{
			return;
		}
		this.Enlargement = false;
		if (!start)
		{
			this.timeA = 0.85f;
			this.timeB = 3f;
			this.speed_x = 50f;
			LegionBattlePanel._inst.NormalBattleBtn.gameObject.transform.localScale = Vector3.zero;
			LegionBattlePanel._inst.LegionBattleBtn.gameObject.transform.localScale = Vector3.zero;
			this.Earth_Light1_Model.tr.localScale = Vector3.zero;
			this.Earth_Light2_Model.tr.localScale = Vector3.one * 2f;
			if (this.NowBattleType == LegionMapManager.BattleType.普通副本)
			{
				this.Earth_Light2_Model.tr.GetComponent<MeshRenderer>().material = this.Material_Earth_Light_Blue;
				this.Earth_Light1_Model.tr.GetComponent<MeshRenderer>().material = this.Material_Earth_Light_Red;
			}
			else if (this.NowBattleType == LegionMapManager.BattleType.军团副本)
			{
				this.Earth_Light1_Model.tr.GetComponent<MeshRenderer>().material = this.Material_Earth_Light_Blue;
				this.Earth_Light2_Model.tr.GetComponent<MeshRenderer>().material = this.Material_Earth_Light_Red;
			}
			this.Earth_Light1_Model.tr.DOScale(Vector3.one, this.timeA).OnComplete(delegate
			{
				this.Earth_Light1_Model.tr.DOScale(Vector3.one * 2f, this.timeA);
				this.NowBattleType = battleType;
				this.CreateEarthStar();
				if (this.NowBattleType == LegionMapManager.BattleType.普通副本)
				{
					this.Earth_Model.tr.GetComponent<MeshRenderer>().material = this.Material_Earth_Blue;
				}
				else if (this.NowBattleType == LegionMapManager.BattleType.军团副本)
				{
					this.Earth_Model.tr.GetComponent<MeshRenderer>().material = this.Material_Earth_Red;
				}
			});
			this.Earth_Light2_Model.tr.DOScale(Vector3.zero, this.timeB).OnComplete(delegate
			{
				LegionBattlePanel._inst.NormalBattleBtn.gameObject.transform.localScale = Vector3.one;
				LegionBattlePanel._inst.LegionBattleBtn.gameObject.transform.localScale = Vector3.one;
			});
		}
		else
		{
			this.NowBattleType = battleType;
			this.CreateEarthStar();
			if (this.NowBattleType == LegionMapManager.BattleType.普通副本)
			{
				this.Earth_Model.tr.GetComponent<MeshRenderer>().material = this.Material_Earth_Blue;
			}
			else if (this.NowBattleType == LegionMapManager.BattleType.军团副本)
			{
				this.Earth_Model.tr.GetComponent<MeshRenderer>().material = this.Material_Earth_Red;
			}
		}
	}
}
