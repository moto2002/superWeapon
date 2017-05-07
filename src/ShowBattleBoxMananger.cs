using System;
using UnityEngine;

public class ShowBattleBoxMananger : MonoBehaviour
{
	public UIGrid grid;

	public GameObject prefab;

	private void Start()
	{
	}

	public void OnEnable()
	{
		this.Init();
		this.ShowBox();
	}

	private void Init()
	{
		EventManager.Instance.AddEvent(EventManager.EventType.BattelBoxClosePanel, new EventManager.VoidDelegate(this.CloseShowBox));
	}

	public void ShowBox()
	{
		for (int i = 0; i < HeroInfo.GetInstance().addBox.Count; i++)
		{
			if (HeroInfo.GetInstance().addBox[i] != null)
			{
				GameObject gameObject = NGUITools.AddChild(this.grid.gameObject, this.prefab);
				BoxPrefab component = gameObject.GetComponent<BoxPrefab>();
				component.name_Client.text = LanguageManage.GetTextByKey("恭喜您获得了", "Battle") + "：" + LanguageManage.GetTextByKey(BattleFieldBox.BattleFieldBox_PlannerData[HeroInfo.GetInstance().addBox[i].Key].name, "Battle");
				component.model = PoolManage.Ins.GetModelByBundleByName("case", component.name_Client.transform);
				component.model.tr.parent = component.name_Client.transform;
				component.model.tr.localPosition = new Vector3(0f, 123f, -65f);
				component.model.tr.localScale = new Vector3(100f, 100f, 100f);
				component.model.tr.localRotation = Quaternion.Euler(0f, 90f, -38f);
				Transform transform = component.model.tr.FindChild("case_w");
				Transform transform2 = component.model.tr.FindChild("case_y");
				Transform transform3 = component.model.tr.FindChild("case_g");
				transform3.gameObject.SetActive(BattleFieldBox.BattleFieldBox_PlannerData[HeroInfo.GetInstance().addBox[i].Key].quility == 1);
				transform2.gameObject.SetActive(BattleFieldBox.BattleFieldBox_PlannerData[HeroInfo.GetInstance().addBox[i].Key].quility == 3);
				transform.gameObject.SetActive(BattleFieldBox.BattleFieldBox_PlannerData[HeroInfo.GetInstance().addBox[i].Key].quility == 2);
				Transform[] componentsInChildren = component.model.GetComponentsInChildren<Transform>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					componentsInChildren[j].gameObject.layer = 5;
				}
				component.effect = PoolManage.Ins.GetEffectByName("huodebaoxiang", null);
				component.effect.tr.parent = component.name_Client.transform;
				component.effect.ga.AddComponent<RenderQueueEdit>();
				component.effect.tr.localScale = Vector3.one;
				component.effect.tr.localPosition = new Vector3(0f, 170f, 0f);
			}
		}
	}

	public void CloseShowBox(GameObject ga)
	{
		UIManager.curState = SenceState.Home;
		Loading.IsRefreshSence = true;
		SenceHandler.CG_GetMapData(HeroInfo.GetInstance().homeInWMapIdx, 1, 0, null);
	}
}
