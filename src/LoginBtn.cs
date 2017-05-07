using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LoginBtn : MonoBehaviour
{
	private Body_Model effect;

	public UILabel txtUName;

	public List<string> userList = new List<string>();

	private float last = -1000f;

	public static string tempUserName;

	public void OnEnable()
	{
	}

	private void Start()
	{
		string @string = PlayerPrefs.GetString("UserName");
		if (!string.IsNullOrEmpty(@string))
		{
			this.txtUName.text = @string;
		}
	}

	private void OnClick()
	{
		if (GameStartNotice._inst && GameStartNotice._inst.gameObject.activeSelf)
		{
			return;
		}
		if (!User.HasIP())
		{
			LoginPanelManager._instance.GetServer();
			return;
		}
		HUDTextTool.serverName = LoginEnterGame._instance.serverName.text;
		if (HDSDKInit.isLoginEnd)
		{
			this.txtUName.text = HeroInfo.GetInstance().platformId;
		}
		if (!this.Validate())
		{
			return;
		}
		string text = this.txtUName.text.Trim();
		LoginBtn.tempUserName = text;
		AudioManage.inst.PlayAuidoBySelf_2D("denglu", base.gameObject, false, 0uL);
		AudioManage.inst.PlayAuido("gohome", false);
		PlayerPrefs.SetString("loginServer", LoginEnterGame._instance.serverName.text);
		if (Time.time > this.last + 1.2f)
		{
			this.last = Time.time;
			base.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.04f);
			if (LoginEnterGame._instance)
			{
				UnityEngine.Object.Destroy(LoginEnterGame._instance.gameObject);
			}
			if (this.effect)
			{
				this.effect.DesInsInPool();
			}
			Init.inst.topLog.DOLocalMoveY(195f, 0.8f, false);
			Init.inst.downLog.DOLocalMoveY(-143f, 0.8f, false).OnComplete(delegate
			{
				DieBall dieBall = PoolManage.Ins.CreatEffect("kaishi_zhedang", Vector3.zero, Quaternion.identity, Init.inst.transform);
				dieBall.tr.localPosition = new Vector3(0f, 14.7f, 0f);
				dieBall.tr.localScale = Vector3.one;
				GameTools.GetCompentIfNoAddOne<RenderQueueEdit>(dieBall.ga);
				TweenAlpha tweenAlpha = UITweener.Begin<TweenAlpha>(Init.inst.closeSprit, 0.3f);
				tweenAlpha.delay = 1.5f;
				tweenAlpha.from = 0f;
				tweenAlpha.to = 1f;
				tweenAlpha.PlayForward();
				TweenAlpha tweenAlpha2 = UITweener.Begin<TweenAlpha>(Init.inst.startSprite, 0.3f);
				tweenAlpha2.delay = 1.5f;
				tweenAlpha2.from = 1f;
				tweenAlpha2.to = 0f;
				tweenAlpha2.PlayForward();
				Init.inst.transform.DOLocalMoveZ(0f, 2.6f, false).OnComplete(new TweenCallback(this.show));
			});
		}
	}

	public void show()
	{
		LogManage.LogError("LoginGame");
		string text = this.txtUName.text.Trim();
		LoginHandler.CG_H_Connect();
		this.last = Time.time;
	}

	public bool Validate()
	{
		return !(this.txtUName.text == "请输入用户名") && !string.IsNullOrEmpty(this.txtUName.text.Trim());
	}
}
