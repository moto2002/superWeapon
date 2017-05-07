using System;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
	public static InfoPanel inst;

	private GameObject infoRes;

	private GameObject infoProRes;

	private GameObject spyInfo;

	private T_Info infoCheck;

	private SpyInfo spyInfoClass;

	public List<T_Info> lifeInfos = new List<T_Info>();

	public List<T_InfoPro> proInfos = new List<T_InfoPro>();

	public List<SpyInfo> spyinfoList = new List<SpyInfo>();

	private void Awake()
	{
		InfoPanel.inst = this;
		this.infoRes = (GameObject)Resources.Load(ResManager.FuncUI_Path + "info");
		this.infoProRes = (GameObject)Resources.Load(ResManager.FuncUI_Path + "info_Pro");
		this.spyInfo = (GameObject)Resources.Load(ResManager.FuncUI_Path + "InfoBg");
	}

	public void ShowSimpleInfo(SenceState state, bool show, T_Tower tower)
	{
		if (show && tower.index != 12)
		{
			if (this.spyInfoClass == null)
			{
				this.spyInfoClass = this.CreateSpyInfo();
				this.spyInfoClass.Remove();
			}
			this.spyInfoClass.enabled = true;
			this.spyInfoClass.ShowInfo(tower, state, InfoType.info);
		}
		else if (this.spyInfoClass != null)
		{
			this.spyInfoClass.Remove();
		}
		if (show)
		{
			if (SpyPanelManager.inst && SpyPanelManager.inst.gameObject.activeSelf)
			{
				SpyPanelManager.inst.bottomLeft.SetActive(false);
				SpyPanelManager.inst.bottomRight.SetActive(false);
			}
		}
		else if (SpyPanelManager.inst && SpyPanelManager.inst.gameObject.activeSelf)
		{
			SpyPanelManager.inst.bottomLeft.SetActive(true);
			SpyPanelManager.inst.bottomRight.SetActive(true);
		}
	}

	public void ShowMVC(T_Tank tank, string name)
	{
	}

	public void ShowLife(int uniteType, T_Tower tower, T_Tank tank)
	{
	}

	public void ChangeSize(float n)
	{
		if (this.infoCheck != null)
		{
			this.infoCheck.transform.localScale = Vector3.one * (1f + n);
		}
		for (int i = 0; i < this.lifeInfos.Count; i++)
		{
			if (this.lifeInfos[i] != null)
			{
				this.lifeInfos[i].transform.localScale = Vector3.one * (1f + n);
			}
		}
	}

	public void RemoveInfo(T_Info info)
	{
		info.Remove();
		this.lifeInfos.Remove(info);
	}

	public void RevoveSpyInfo(SpyInfo info)
	{
		info.Remove();
		this.spyinfoList.Remove(info);
	}

	public T_Info CreatInfo()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(this.infoRes) as GameObject;
		if (gameObject)
		{
			gameObject.transform.parent = base.transform;
			gameObject.transform.localScale = Vector3.one;
			T_Info component = gameObject.GetComponent<T_Info>();
			gameObject.transform.localPosition = SenceManager.inst.hidePos;
			gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0f);
			return component;
		}
		return null;
	}

	public SpyInfo CreateSpyInfo()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(this.spyInfo) as GameObject;
		if (gameObject)
		{
			gameObject.transform.parent = base.transform;
			gameObject.transform.localScale = Vector3.one;
			return gameObject.GetComponent<SpyInfo>();
		}
		return null;
	}

	public T_Info CreatShieldInfo()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(this.infoRes) as GameObject;
		if (gameObject)
		{
			gameObject.transform.parent = base.transform;
			gameObject.transform.localScale = Vector3.one;
			T_Info component = gameObject.GetComponent<T_Info>();
			gameObject.transform.localPosition = SenceManager.inst.hidePos;
			gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + 5f, gameObject.transform.localPosition.y + 5f, 5f);
			return component;
		}
		return null;
	}

	public T_InfoPro CreatProInfo()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(this.infoProRes) as GameObject;
		gameObject.transform.parent = base.transform;
		gameObject.transform.localScale = Vector3.one;
		T_InfoPro component = gameObject.GetComponent<T_InfoPro>();
		gameObject.transform.localPosition = SenceManager.inst.hidePos;
		return component;
	}

	public void DelletAllProInfo()
	{
		for (int i = 0; i < this.proInfos.Count; i++)
		{
			if (this.proInfos[i] != null)
			{
				UnityEngine.Object.Destroy(this.proInfos[i].gameObject);
			}
		}
		this.proInfos.Clear();
	}
}
