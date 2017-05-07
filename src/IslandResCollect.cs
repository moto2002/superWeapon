using DicForUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class IslandResCollect : MonoBehaviour
{
	public ResType resType;

	public double resNum;

	public static bool isRefresh;

	public int sendTime;

	public Vector3 startVector;

	public Vector3 endVector;

	private bool atHome;

	[HideInInspector]
	public bool isCanCollect;

	private static bool isFirstCoin = true;

	private static bool isFirstOil = true;

	private static bool isFirstSteel = true;

	private static bool isFirstRareEarth = true;

	public float duraTime = 16f;

	public float timeCD = 60f;

	public Transform GA;

	private Body_Model Effect;

	private TimeSpan time;

	private int num;

	private int limit;

	private float speed;

	private List<int> islandHomeLV = new List<int>();

	private Body_Model anima;

	private void Awake()
	{
		base.transform.localPosition = this.startVector;
		this.GA.gameObject.AddComponent<BoxCollider>();
		this.GA.gameObject.AddComponent<CollectRes>();
		switch (this.resType)
		{
		case ResType.金币:
		{
			Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName("xiangzi", this.GA);
			if (modelByBundleByName)
			{
				this.Effect = PoolManage.Ins.GetEffectByName("xiangzi_guang", modelByBundleByName.transform);
				if (this.Effect)
				{
					this.Effect.SetActive(false);
				}
			}
			break;
		}
		case ResType.石油:
		{
			Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName("xiangzi", this.GA);
			if (modelByBundleByName)
			{
				this.Effect = PoolManage.Ins.GetEffectByName("xiangzi_guang", modelByBundleByName.transform);
				if (this.Effect)
				{
					this.Effect.SetActive(false);
				}
			}
			break;
		}
		case ResType.钢铁:
		{
			Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName("xiangzi", this.GA);
			if (modelByBundleByName)
			{
				this.Effect = PoolManage.Ins.GetEffectByName("xiangzi_guang", modelByBundleByName.transform);
				if (this.Effect)
				{
					this.Effect.SetActive(false);
				}
			}
			break;
		}
		case ResType.稀矿:
		{
			Body_Model modelByBundleByName = PoolManage.Ins.GetModelByBundleByName("xiangzi", this.GA);
			if (modelByBundleByName)
			{
				this.Effect = PoolManage.Ins.GetEffectByName("xiangzi_guang", modelByBundleByName.transform);
				if (this.Effect)
				{
					this.Effect.SetActive(false);
				}
			}
			break;
		}
		}
	}

	private void Start()
	{
		this.GA.gameObject.SetActive(false);
		base.InvokeRepeating("RefreshData", 0f, 10f);
	}

	private void RefreshData()
	{
		this.resNum = 0.0;
		if (UIManager.curState != SenceState.Home)
		{
			return;
		}
		if (!HeroInfo.GetInstance().islandResData.IslandResTake.ContainsKey(this.resType))
		{
			return;
		}
		this.num = HeroInfo.GetInstance().islandResData.GetNumValue(this.resType);
		if (this.num <= 0)
		{
			return;
		}
		this.Reckon(ref this.resNum);
		if (!this.atHome)
		{
			this.sendTime += 10;
		}
	}

	private void Reckon(ref double _resNum)
	{
		this.time = TimeTools.GetNowTimeSyncServerToDateTime() - HeroInfo.GetInstance().islandResData.IslandResTake[this.resType];
		switch (this.resType)
		{
		case ResType.金币:
			this.speed = HeroInfo.GetInstance().islandResData.GetAllIslandSpeedValue(this.resType);
			this.limit = HeroInfo.GetInstance().islandResData.GetLimitValue(this.resType);
			this.num = HeroInfo.GetInstance().islandResData.GetNumValue(this.resType);
			_resNum += (double)(this.speed * (float)this.num) * this.time.TotalHours;
			_resNum = ((this.resNum < (double)this.limit) ? _resNum : ((double)this.limit));
			if (HeroInfo.GetInstance().islandResData.IslandResTmp.ContainsKey(this.resType))
			{
				_resNum += (double)HeroInfo.GetInstance().islandResData.IslandResTmp[this.resType];
			}
			if (((float)this.sendTime > this.timeCD && !this.atHome) || IslandResCollect.isFirstCoin)
			{
				this.GoHome();
			}
			break;
		case ResType.石油:
			DicForU.GetValues<long, int>(HeroInfo.GetInstance().islandResData.OilIslandes, this.islandHomeLV);
			for (int i = 0; i < this.islandHomeLV.Count; i++)
			{
				if (HeroInfo.GetInstance().islandResData.ResIslandOutputConst.ContainsKey(this.islandHomeLV[i]) && HeroInfo.GetInstance().islandResData.ResIslandOutputConst[this.islandHomeLV[i]].ContainsKey(this.resType))
				{
					this.speed = (float)HeroInfo.GetInstance().islandResData.ResIslandOutputConst[this.islandHomeLV[i]][this.resType].speendValue;
					this.limit = HeroInfo.GetInstance().islandResData.ResIslandOutputConst[this.islandHomeLV[i]][this.resType].limit;
					double num = (double)this.speed * this.time.TotalHours;
					_resNum += ((num < (double)this.limit) ? num : ((double)this.limit));
				}
			}
			if (HeroInfo.GetInstance().islandResData.IslandResTmp.ContainsKey(this.resType))
			{
				_resNum += (double)HeroInfo.GetInstance().islandResData.IslandResTmp[this.resType];
			}
			if (((float)this.sendTime > this.timeCD && !this.atHome) || IslandResCollect.isFirstOil)
			{
				this.GoHome();
			}
			break;
		case ResType.钢铁:
			DicForU.GetValues<long, int>(HeroInfo.GetInstance().islandResData.SteelIslandes, this.islandHomeLV);
			for (int j = 0; j < this.islandHomeLV.Count; j++)
			{
				if (HeroInfo.GetInstance().islandResData.ResIslandOutputConst.ContainsKey(this.islandHomeLV[j]) && HeroInfo.GetInstance().islandResData.ResIslandOutputConst[this.islandHomeLV[j]].ContainsKey(this.resType))
				{
					this.speed = (float)HeroInfo.GetInstance().islandResData.ResIslandOutputConst[this.islandHomeLV[j]][this.resType].speendValue;
					this.limit = HeroInfo.GetInstance().islandResData.ResIslandOutputConst[this.islandHomeLV[j]][this.resType].limit;
					double num2 = (double)this.speed * this.time.TotalHours;
					_resNum += ((num2 < (double)this.limit) ? num2 : ((double)this.limit));
				}
			}
			if (HeroInfo.GetInstance().islandResData.IslandResTmp.ContainsKey(this.resType))
			{
				_resNum += (double)HeroInfo.GetInstance().islandResData.IslandResTmp[this.resType];
			}
			if (((float)this.sendTime > this.timeCD && !this.atHome) || IslandResCollect.isFirstSteel)
			{
				this.GoHome();
			}
			break;
		case ResType.稀矿:
			DicForU.GetValues<long, int>(HeroInfo.GetInstance().islandResData.RareEarthIslandes, this.islandHomeLV);
			for (int k = 0; k < this.islandHomeLV.Count; k++)
			{
				if (HeroInfo.GetInstance().islandResData.ResIslandOutputConst.ContainsKey(this.islandHomeLV[k]) && HeroInfo.GetInstance().islandResData.ResIslandOutputConst[this.islandHomeLV[k]].ContainsKey(this.resType))
				{
					this.speed = (float)HeroInfo.GetInstance().islandResData.ResIslandOutputConst[this.islandHomeLV[k]][this.resType].speendValue;
					this.limit = HeroInfo.GetInstance().islandResData.ResIslandOutputConst[this.islandHomeLV[k]][this.resType].limit;
					double num3 = (double)this.speed * this.time.TotalHours;
					_resNum += ((num3 < (double)this.limit) ? num3 : ((double)this.limit));
				}
			}
			if (HeroInfo.GetInstance().islandResData.IslandResTmp.ContainsKey(this.resType))
			{
				_resNum += (double)HeroInfo.GetInstance().islandResData.IslandResTmp[this.resType];
			}
			if (((float)this.sendTime > this.timeCD && !this.atHome) || IslandResCollect.isFirstRareEarth)
			{
				this.GoHome();
			}
			break;
		}
	}

	private void GoHome()
	{
		if (this.resNum > 10.0)
		{
			this.atHome = true;
			switch (this.resType)
			{
			case ResType.金币:
				if (IslandResCollect.isFirstCoin)
				{
					this.ReSetJLS();
					IslandResCollect.isFirstCoin = false;
					base.gameObject.transform.position = this.endVector;
					this.anima = FuncUIManager.inst.ResourcePanelManage.AddChildByRes(base.gameObject, this.resType);
					CoroutineInstance.DoJob(this.JLS());
					this.isCanCollect = true;
				}
				else
				{
					this.ReSetJLS();
					TweenPosition tweenPosition = TweenPosition.Begin(base.gameObject, this.duraTime, this.endVector);
					tweenPosition.onFinished.Clear();
					tweenPosition.AddOnFinished(delegate
					{
						this.anima = FuncUIManager.inst.ResourcePanelManage.AddChildByRes(base.gameObject, this.resType);
						CoroutineInstance.DoJob(this.JLS());
						this.isCanCollect = true;
					});
				}
				break;
			case ResType.石油:
				if (IslandResCollect.isFirstOil)
				{
					this.ReSetJLS();
					IslandResCollect.isFirstOil = false;
					base.gameObject.transform.position = this.endVector;
					this.anima = FuncUIManager.inst.ResourcePanelManage.AddChildByRes(base.gameObject, this.resType);
					CoroutineInstance.DoJob(this.JLS());
					this.isCanCollect = true;
				}
				else
				{
					this.ReSetJLS();
					TweenPosition tweenPosition2 = TweenPosition.Begin(base.gameObject, this.duraTime, this.endVector);
					tweenPosition2.onFinished.Clear();
					tweenPosition2.AddOnFinished(delegate
					{
						this.anima = FuncUIManager.inst.ResourcePanelManage.AddChildByRes(base.gameObject, this.resType);
						CoroutineInstance.DoJob(this.JLS());
						this.isCanCollect = true;
					});
				}
				break;
			case ResType.钢铁:
				if (IslandResCollect.isFirstSteel)
				{
					this.ReSetJLS();
					IslandResCollect.isFirstSteel = false;
					base.gameObject.transform.position = this.endVector;
					this.anima = FuncUIManager.inst.ResourcePanelManage.AddChildByRes(base.gameObject, this.resType);
					CoroutineInstance.DoJob(this.JLS());
					this.isCanCollect = true;
				}
				else
				{
					this.ReSetJLS();
					TweenPosition tweenPosition3 = TweenPosition.Begin(base.gameObject, this.duraTime, this.endVector);
					tweenPosition3.onFinished.Clear();
					tweenPosition3.AddOnFinished(delegate
					{
						this.anima = FuncUIManager.inst.ResourcePanelManage.AddChildByRes(base.gameObject, this.resType);
						CoroutineInstance.DoJob(this.JLS());
						this.isCanCollect = true;
					});
				}
				break;
			case ResType.稀矿:
				if (IslandResCollect.isFirstRareEarth)
				{
					this.ReSetJLS();
					IslandResCollect.isFirstRareEarth = false;
					base.gameObject.transform.position = this.endVector;
					this.anima = FuncUIManager.inst.ResourcePanelManage.AddChildByRes(base.gameObject, this.resType);
					CoroutineInstance.DoJob(this.JLS());
					this.isCanCollect = true;
				}
				else
				{
					this.ReSetJLS();
					TweenPosition tweenPosition4 = TweenPosition.Begin(base.gameObject, this.duraTime, this.endVector);
					tweenPosition4.onFinished.Clear();
					tweenPosition4.AddOnFinished(delegate
					{
						this.anima = FuncUIManager.inst.ResourcePanelManage.AddChildByRes(base.gameObject, this.resType);
						CoroutineInstance.DoJob(this.JLS());
						this.isCanCollect = true;
					});
				}
				break;
			}
		}
	}

	private void ReSetJLS()
	{
		GameObject gameObject = base.transform.Find("GA/JLS").gameObject;
		gameObject.transform.localPosition = new Vector3(0f, 0.78f, 0f);
		Animation component = gameObject.GetComponent<Animation>();
		component.Play("idle");
		gameObject.SetActive(true);
		this.GA.gameObject.SetActive(true);
	}

	public void CollectIslandRes(Action CallBack)
	{
		if (this.isCanCollect)
		{
			double num = 0.0;
			this.Reckon(ref num);
			CollectResourcesHandler.TakeResource.buildingId = (long)this.resType;
			CollectResourcesHandler.TakeResource.resId = (int)this.resType;
			CollectResourcesHandler.TakeResource.takeTime = TimeTools.GetNowTimeSyncServerToLong();
			CollectResourcesHandler.TakeResource.takeNum = Mathf.RoundToInt((float)num);
			switch (this.resType)
			{
			case ResType.金币:
				if (HeroInfo.GetInstance().playerRes.resCoin >= HeroInfo.GetInstance().playerRes.maxCoin)
				{
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("金库已满，请先升级金库建筑等级", "ResIsland") + "!", HUDTextTool.TextUITypeEnum.Num5);
					return;
				}
				IslandResCollect.isFirstCoin = false;
				break;
			case ResType.石油:
				if (HeroInfo.GetInstance().playerRes.resOil >= HeroInfo.GetInstance().playerRes.maxOil)
				{
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("储油罐已满，请先升级储油罐建筑等级", "ResIsland") + "!", HUDTextTool.TextUITypeEnum.Num5);
					return;
				}
				IslandResCollect.isFirstOil = false;
				break;
			case ResType.钢铁:
				if (HeroInfo.GetInstance().playerRes.resSteel >= HeroInfo.GetInstance().playerRes.maxSteel)
				{
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("钢铁仓库已满，请先升级钢铁仓库等级", "ResIsland") + "!", HUDTextTool.TextUITypeEnum.Num5);
					return;
				}
				IslandResCollect.isFirstSteel = false;
				break;
			case ResType.稀矿:
				if (HeroInfo.GetInstance().playerRes.resRareEarth >= HeroInfo.GetInstance().playerRes.maxRareEarth)
				{
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("稀矿仓库已满，请先升级稀矿仓库建筑等级", "ResIsland") + "!", HUDTextTool.TextUITypeEnum.Num5);
					return;
				}
				IslandResCollect.isFirstRareEarth = false;
				break;
			}
			int takeNum = CollectResourcesHandler.TakeResource.takeNum;
			LogManage.LogError(string.Format("资源岛个数{0}上次收取时间是{1}  当前时间{6} 生产速率是{2}最大限制是{3}缓存资源{4} 时间差{5} 收取数目{7}", new object[]
			{
				HeroInfo.GetInstance().islandResData.GetNumValue(this.resType),
				TimeTools.GetTimeLongByDateTime(HeroInfo.GetInstance().islandResData.IslandResTake[this.resType]),
				HeroInfo.GetInstance().islandResData.GetAllIslandSpeedValue(this.resType),
				HeroInfo.GetInstance().islandResData.GetLimitValue(this.resType),
				(!HeroInfo.GetInstance().islandResData.IslandResTmp.ContainsKey(this.resType)) ? 0 : HeroInfo.GetInstance().islandResData.IslandResTmp[this.resType],
				(TimeTools.GetNowTimeSyncServerToDateTime() - HeroInfo.GetInstance().islandResData.IslandResTake[this.resType]).TotalMilliseconds,
				TimeTools.GetNowTimeSyncServerToLong(),
				num
			}));
			switch (this.resType)
			{
			case ResType.金币:
				takeNum = ((CollectResourcesHandler.TakeResource.takeNum + HeroInfo.GetInstance().playerRes.resCoin <= HeroInfo.GetInstance().playerRes.maxCoin) ? CollectResourcesHandler.TakeResource.takeNum : (HeroInfo.GetInstance().playerRes.maxCoin - HeroInfo.GetInstance().playerRes.resCoin));
				CollectResourcesHandler.TakeResource.takeNum = takeNum;
				CoroutineInstance.DoJob(ResFly2.CreateRes(base.transform, ResType.金币, takeNum, new Action(CollectResourcesHandler.CG_CollectResources), null));
				break;
			case ResType.石油:
				takeNum = ((CollectResourcesHandler.TakeResource.takeNum + HeroInfo.GetInstance().playerRes.resOil <= HeroInfo.GetInstance().playerRes.maxOil) ? CollectResourcesHandler.TakeResource.takeNum : (HeroInfo.GetInstance().playerRes.maxOil - HeroInfo.GetInstance().playerRes.resOil));
				CollectResourcesHandler.TakeResource.takeNum = takeNum;
				CoroutineInstance.DoJob(ResFly2.CreateRes(base.transform, ResType.石油, takeNum, new Action(CollectResourcesHandler.CG_CollectResources), null));
				break;
			case ResType.钢铁:
				takeNum = ((CollectResourcesHandler.TakeResource.takeNum + HeroInfo.GetInstance().playerRes.resSteel <= HeroInfo.GetInstance().playerRes.maxSteel) ? CollectResourcesHandler.TakeResource.takeNum : (HeroInfo.GetInstance().playerRes.maxSteel - HeroInfo.GetInstance().playerRes.resSteel));
				CollectResourcesHandler.TakeResource.takeNum = takeNum;
				CoroutineInstance.DoJob(ResFly2.CreateRes(base.transform, ResType.钢铁, takeNum, new Action(CollectResourcesHandler.CG_CollectResources), null));
				break;
			case ResType.稀矿:
				takeNum = ((CollectResourcesHandler.TakeResource.takeNum + HeroInfo.GetInstance().playerRes.resRareEarth <= HeroInfo.GetInstance().playerRes.maxRareEarth) ? CollectResourcesHandler.TakeResource.takeNum : (HeroInfo.GetInstance().playerRes.maxRareEarth - HeroInfo.GetInstance().playerRes.resRareEarth));
				CollectResourcesHandler.TakeResource.takeNum = takeNum;
				CoroutineInstance.DoJob(ResFly2.CreateRes(base.transform, ResType.稀矿, takeNum, new Action(CollectResourcesHandler.CG_CollectResources), null));
				break;
			}
			if (CallBack != null)
			{
				CallBack();
			}
			this.GoToIsland();
		}
	}

	public void GoToIsland()
	{
		this.isCanCollect = false;
		this.GA.gameObject.SetActive(false);
		if (this.Effect)
		{
			this.Effect.SetActive(false);
		}
		if (this.anima)
		{
			UnityEngine.Object.Destroy(this.anima.ga);
		}
		TweenPosition tweenPosition = TweenPosition.Begin(base.gameObject, this.duraTime, this.startVector);
		tweenPosition.onFinished.Clear();
		tweenPosition.AddOnFinished(delegate
		{
			this.atHome = false;
			this.sendTime = 0;
		});
	}

	[DebuggerHidden]
	private IEnumerator JLS()
	{
		IslandResCollect.<JLS>c__Iterator78 <JLS>c__Iterator = new IslandResCollect.<JLS>c__Iterator78();
		<JLS>c__Iterator.<>f__this = this;
		return <JLS>c__Iterator;
	}
}
