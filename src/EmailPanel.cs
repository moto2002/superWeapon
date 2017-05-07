using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

public class EmailPanel : FuncUIPanel
{
	private int sublength = 10;

	[HideInInspector]
	public GameObject obj;

	public UITable itemTable;

	public UIScrollView contentView;

	public GameObject emailItempre;

	public GameObject resPrefab;

	public GameObject itemPrefab;

	public static EmailPanel ins;

	[HideInInspector]
	public bool isClick;

	public List<long> emaiObj = new List<long>();

	private bool isFirst = true;

	public bool isCoin;

	public bool isOil;

	public bool isSteel;

	public bool isRay;

	public List<GameObject> golist = new List<GameObject>();

	[HideInInspector]
	public string cionCount;

	[HideInInspector]
	public string oilCount;

	[HideInInspector]
	public string stellCount;

	[HideInInspector]
	public string rayCount;

	[HideInInspector]
	public string moneyCounts;

	public List<GameObject> resObj = new List<GameObject>();

	public List<GameObject> emailRece = new List<GameObject>();

	public List<UITable> resTable = new List<UITable>();

	public GameObject closeEmail;

	public List<GameObject> eobj = new List<GameObject>();

	public GameObject resTop;

	public GameObject noEmailTip;

	private float time;

	public void OnDestroy()
	{
		EmailPanel.ins = null;
	}

	public override void Awake()
	{
		EmailPanel.ins = this;
		this.itemTable = base.transform.FindChild("Container/Content/View/table").GetComponent<UITable>();
		this.contentView = base.transform.FindChild("Container/Content/View").GetComponent<UIScrollView>();
		this.emailItempre = base.transform.FindChild("emailItem").gameObject;
		this.resTop = base.transform.FindChild("resource").gameObject;
		this.resPrefab = base.transform.FindChild(string.Empty).gameObject;
		this.resPrefab = base.transform.FindChild("ResPrefab").gameObject;
		this.resPrefab.AddComponent<ResourcesPrefab>();
		this.itemPrefab = base.transform.FindChild("Itemitem").gameObject;
		this.itemPrefab.AddComponent<ItemPrefabScript>();
		this.closeEmail = base.transform.FindChild("Container/close").gameObject;
		this.closeEmail.AddComponent<EmailBtn>();
		EmailBtn component = this.closeEmail.GetComponent<EmailBtn>();
		component.type = EmailBtnType.close;
		this.noEmailTip = base.transform.FindChild("NoEmail").gameObject;
	}

	public override void OnEnable()
	{
		base.StartCoroutine(this.Reposition());
		EmailManager.GetIns().SortEmail();
		this.isFirst = false;
		this.Refresh();
		base.OnEnable();
	}

	public void Refresh()
	{
		this.isCoin = true;
		if (EmailManager.GetIns().emailList.Count <= 0)
		{
			this.noEmailTip.gameObject.SetActive(true);
		}
		else
		{
			this.noEmailTip.gameObject.SetActive(false);
		}
		for (int i = 0; i < EmailManager.GetIns().emailList.Count; i++)
		{
			EmailInfo emailInfo = EmailManager.GetIns().emailList[i];
			if (!this.emaiObj.Contains(emailInfo.id))
			{
				this.emaiObj.Add(emailInfo.id);
				GameObject gameObject = NGUITools.AddChild(this.itemTable.gameObject, this.emailItempre);
				gameObject.name = emailInfo.id.ToString();
				gameObject.AddComponent<EmailItem>();
				EmailItem component = gameObject.GetComponent<EmailItem>();
				component.emailID = emailInfo.id;
				this.emailRece.Add(gameObject);
				this.SetUITexture(component.emailBG, "wangyuan/Email/Texture/", "新邮件背景");
				component.emailBG.GetComponent<UITexture>().height = 134;
				if (emailInfo.isOpened)
				{
					component.emailFlag.spriteName = "邮件打开";
				}
				else
				{
					component.emailFlag.spriteName = "邮件未打开";
				}
				if (emailInfo.title.Equals("fight_prize_1") || emailInfo.title.Equals("fight_prize_2"))
				{
					StringBuilder stringBuilder = new StringBuilder();
					emailInfo.title = LanguageManage.GetTextByKey(emailInfo.title, "BattleNotice");
					string[] array = emailInfo.content.Split(new char[]
					{
						'|'
					});
					for (int j = 0; j < array.Length; j++)
					{
						string text = array[j];
						string[] array2 = text.Split(new char[]
						{
							'='
						});
						if (array2[0].Equals("奖励资源") && !string.IsNullOrEmpty(array2[1].Trim()))
						{
							string textByKey = LanguageManage.GetTextByKey(array2[0], "BattleNotice");
							string[] array3 = array2[1].Split(new char[]
							{
								','
							});
							StringBuilder stringBuilder2 = new StringBuilder();
							for (int k = 0; k < array3.Length; k++)
							{
								if (array3[k].Contains(":"))
								{
									switch (int.Parse(array3[k].Split(new char[]
									{
										':'
									})[0]))
									{
									case 1:
										stringBuilder2.Append(string.Format(" {0}:{1}", LanguageManage.GetTextByKey("获取金币", "ResIsland"), array3[k].Split(new char[]
										{
											':'
										})[1]));
										break;
									case 2:
										stringBuilder2.Append(string.Format(" {0}:{1}", LanguageManage.GetTextByKey("获取石油", "ResIsland"), array3[k].Split(new char[]
										{
											':'
										})[1]));
										break;
									case 3:
										stringBuilder2.Append(string.Format(" {0}:{1}", LanguageManage.GetTextByKey("获取钢铁", "ResIsland"), array3[k].Split(new char[]
										{
											':'
										})[1]));
										break;
									case 4:
										stringBuilder2.Append(string.Format(" {0}:{1}", LanguageManage.GetTextByKey("获取稀矿", "ResIsland"), array3[k].Split(new char[]
										{
											':'
										})[1]));
										break;
									case 7:
										stringBuilder2.Append(string.Format(" {0}:{1}", LanguageManage.GetTextByKey("获取钻石", "ResIsland"), array3[k].Split(new char[]
										{
											':'
										})[1]));
										break;
									}
								}
							}
							stringBuilder.Append(string.Format(textByKey, stringBuilder2.ToString()));
						}
						else if (array2[0].Equals("奖励道具") && !string.IsNullOrEmpty(array2[1].Trim()))
						{
							string textByKey2 = LanguageManage.GetTextByKey(array2[0], "BattleNotice");
							string[] array4 = array2[1].Split(new char[]
							{
								','
							});
							StringBuilder stringBuilder3 = new StringBuilder();
							for (int l = 0; l < array4.Length; l++)
							{
								if (array4[l].Contains(":"))
								{
									stringBuilder3.Append(string.Format("  {0}:{1}", LanguageManage.GetTextByKey(UnitConst.GetInstance().ItemConst[int.Parse(array4[l].Split(new char[]
									{
										':'
									})[0])].Name, "item"), array4[l].Split(new char[]
									{
										':'
									})[1]));
								}
							}
							stringBuilder.Append(string.Format(textByKey2, stringBuilder3.ToString()));
						}
						else
						{
							string textByKey3 = LanguageManage.GetTextByKey(array2[0], "BattleNotice");
							string[] array5 = array2[1].Split(new char[]
							{
								';'
							});
							try
							{
								stringBuilder.Append(string.Format(textByKey3, array5));
							}
							catch (Exception var_19_505)
							{
								string text2 = string.Empty;
								string[] array6 = array5;
								for (int m = 0; m < array6.Length; m++)
								{
									string str = array6[m];
									text2 += str;
								}
								UnityEngine.Debug.LogError(string.Format("跑马灯的文字内容是{0} 参数是{1})", textByKey3, text2));
							}
						}
					}
					emailInfo.content = stringBuilder.ToString();
				}
				component.title.text = emailInfo.title;
				if (emailInfo.content.Length > this.sublength)
				{
					component.content.text = emailInfo.content.Substring(0, this.sublength) + "...";
				}
				else
				{
					component.content.text = emailInfo.content;
				}
				if (this.isFirst)
				{
					if (emailInfo.content.Length > this.sublength)
					{
						component.content.text = emailInfo.content.Substring(0, this.sublength) + "...";
					}
					else
					{
						component.content.text = emailInfo.content;
						component.bottom.SetActive(false);
					}
				}
				int count = emailInfo.resources.Count;
				int count2 = emailInfo.items.Count;
				int zuanshiNum = emailInfo.zuanshiNum;
				if (count2 > 0)
				{
					for (int n = 0; n < emailInfo.items.Count; n++)
					{
						GameObject gameObject2 = NGUITools.AddChild(EmailItem._instance.itemTablePrf.gameObject, this.itemPrefab);
						if (!gameObject2.GetComponent<ItemPrefabScript>())
						{
							gameObject2.AddComponent<ItemPrefabScript>();
						}
						ItemPrefabScript component2 = gameObject2.GetComponent<ItemPrefabScript>();
						Item item = UnitConst.GetInstance().ItemConst[(int)emailInfo.items[n].key];
						AtlasManage.SetUiItemAtlas(component2.itemIcon, item.IconId);
						component2.itemIcon.GetComponent<ItemTipsShow2>().Index = (int)emailInfo.items[n].key;
						component2.itemIcon.GetComponent<ItemTipsShow2>().JianTouPostion = 1;
						component2.itemIcon.GetComponent<ItemTipsShow2>().Type = 1;
						component2.itemBG.spriteName = this.SetQuSprite((int)item.Quality);
						component2.itemLabel.text = emailInfo.items[n].value.ToString();
					}
				}
				if (count > 0)
				{
					int num = 0;
					while (num < emailInfo.resources.Count)
					{
						GameObject gameObject3 = NGUITools.AddChild(EmailItem._instance.resTablePrf.gameObject, this.resPrefab);
						if (!this.resPrefab.GetComponent<ResourcesPrefab>())
						{
							this.resPrefab.AddComponent<ResourcesPrefab>();
						}
						ResourcesPrefab component3 = gameObject3.GetComponent<ResourcesPrefab>();
						gameObject3.transform.parent.name = emailInfo.id.ToString();
						this.resTable.Add(EmailItem._instance.resTablePrf);
						this.resObj.Add(gameObject3);
						component3.recNum.text = emailInfo.resources[num].value.ToString();
						switch ((int)emailInfo.resources[num].key)
						{
						case 1:
							component3.resIcon.spriteName = "新金矿";
							this.cionCount = emailInfo.resources[num].value.ToString();
							gameObject3.name = emailInfo.resources[num].key.ToString();
							break;
						case 2:
							component3.resIcon.spriteName = "新石油";
							this.oilCount = emailInfo.resources[num].value.ToString();
							gameObject3.name = emailInfo.resources[num].key.ToString();
							break;
						case 3:
							component3.resIcon.spriteName = "新钢铁";
							this.stellCount = emailInfo.resources[num].value.ToString();
							gameObject3.name = emailInfo.resources[num].key.ToString();
							break;
						case 4:
							component3.resIcon.spriteName = "新稀矿";
							this.rayCount = emailInfo.resources[num].value.ToString();
							gameObject3.name = emailInfo.resources[num].key.ToString();
							break;
						case 7:
							component3.resIcon.spriteName = "新钻石";
							this.moneyCounts = emailInfo.resources[num].value.ToString();
							gameObject3.name = emailInfo.resources[num].key.ToString();
							break;
						}
						IL_A47:
						num++;
						continue;
						goto IL_A47;
					}
				}
				if (zuanshiNum > 0)
				{
					GameObject gameObject4 = NGUITools.AddChild(EmailItem._instance.resTablePrf.gameObject, this.resPrefab);
					ResourcesPrefab component4 = gameObject4.GetComponent<ResourcesPrefab>();
					gameObject4.name = "7";
					component4.resIcon.spriteName = "新钻石";
					component4.resIcon.GetComponent<UISprite>().width = 32;
					component4.resIcon.GetComponent<UISprite>().height = 33;
					component4.recNum.text = zuanshiNum.ToString();
				}
				component.lblTime.text = TimeTools.ConvertLongDateTime(emailInfo.time).ToString("yyyy-MM-dd");
				component.obj.name = emailInfo.id.ToString();
				component.btnReward.name = emailInfo.id.ToString();
				if (count2 > 0 || count > 0 || zuanshiNum > 0)
				{
					component.helpInfo.gameObject.SetActive(true);
					if (!emailInfo.isGetReward)
					{
						component.helpInfo.text = LanguageManage.GetTextByKey("可领取奖励", "others");
						component.helpInfo.color = new Color(0.294117659f, 0.8980392f, 0.972549f);
						component.btnReward.GetComponent<UISprite>().spriteName = "blue";
						component.btnReward.GetComponent<UIButton>().normalSprite = "blue";
						component.btnReward.GetComponent<UIButton>().hoverSprite = "blued";
						component.btnReward.GetComponent<UIButton>().pressedSprite = "blued";
						component.btnReward.GetComponent<UIButton>().disabledSprite = "blue";
						component.xulz.SetActive(true);
						component.btnReward.GetComponent<BoxCollider>().enabled = true;
						component.btnReward.GetComponent<EmailBtn>().enabled = true;
						component.showState.text = LanguageManage.GetTextByKey("领取奖励", "others");
					}
					else
					{
						component.helpInfo.text = LanguageManage.GetTextByKey("已领取", "others");
						component.helpInfo.color = new Color(0.0784313753f, 0.8980392f, 0.0784313753f);
						component.btnReward.GetComponent<UISprite>().spriteName = "hui";
						component.btnReward.GetComponent<UIButton>().normalSprite = "hui";
						component.btnReward.GetComponent<UIButton>().hoverSprite = "hui";
						component.btnReward.GetComponent<UIButton>().pressedSprite = "hui";
						component.btnReward.GetComponent<UIButton>().disabledSprite = "hui";
						component.xulz.SetActive(false);
						component.btnReward.GetComponent<BoxCollider>().enabled = false;
						component.btnReward.GetComponent<EmailBtn>().enabled = false;
						component.showState.text = LanguageManage.GetTextByKey("已领取", "others");
					}
				}
				if (count2 <= 0 && count <= 0 && zuanshiNum <= 0)
				{
					component.btnReward.SetActive(false);
				}
			}
		}
		this.isFirst = false;
		this.itemTable.Reposition();
		this.contentView.ResetPosition();
	}

	public string SetQuSprite(int quaty)
	{
		switch (quaty)
		{
		case 1:
			return "白";
		case 2:
			return "绿";
		case 3:
			return "蓝";
		case 4:
			return "紫";
		case 5:
			return "红";
		default:
			return string.Empty;
		}
	}

	public void ButtonClick(EmailBtnType type, GameObject go)
	{
		switch (type)
		{
		case EmailBtnType.close:
			for (int i = 0; i < this.emailRece.Count; i++)
			{
				this.emailRece[i].GetComponent<EmailItem>().bottom.gameObject.SetActive(false);
				this.emailRece[i].GetComponent<EmailItem>().emailBG.GetComponent<UITexture>().height = 134;
				this.SetUITexture(this.emailRece[i].GetComponent<EmailItem>().emailBG, "wangyuan/Email/Texture/", "新邮件背景");
				this.emailRece[i].GetComponent<EmailItem>().expend.localEulerAngles = Vector3.zero;
				this.emailRece[i].GetComponent<EmailItem>().expend.localPosition = new Vector3(651f, -39.1f, 0f);
				if (this.emailRece[i].GetComponent<EmailItem>().resTablePrf.transform.childCount > 0 || this.emailRece[i].GetComponent<EmailItem>().itemTablePrf.transform.childCount > 0)
				{
					this.emailRece[i].GetComponent<EmailItem>().helpInfo.gameObject.SetActive(true);
				}
				else
				{
					this.emailRece[i].GetComponent<EmailItem>().helpInfo.gameObject.SetActive(false);
				}
			}
			base.StartCoroutine(this.Reposition());
			FuncUIManager.inst.HideFuncUI("EmailPanel");
			break;
		case EmailBtnType.getReward:
			this.isCoin = true;
			if (Time.time - this.time >= 0.5f)
			{
				long num = long.Parse(go.name);
				this.time = Time.time;
				for (int j = 0; j < this.emailRece.Count; j++)
				{
					for (int k = 0; k < this.resTable.Count; k++)
					{
						if (long.Parse(this.emailRece[j].name) == num && long.Parse(this.resTable[k].name) == num)
						{
							int l = 0;
							while (l < this.resTable[k].transform.childCount)
							{
								switch (int.Parse(this.resTable[k].transform.GetChild(l).name))
								{
								case 1:
									if (int.Parse(this.resTable[k].transform.GetChild(l).GetChild(0).GetComponent<UILabel>().text.ToString()) > HeroInfo.GetInstance().playerRes.maxCoin - HeroInfo.GetInstance().playerRes.resCoin)
									{
										this.isCoin = false;
									}
									break;
								case 2:
									if (int.Parse(this.resTable[k].transform.GetChild(l).GetChild(0).GetComponent<UILabel>().text.ToString()) > HeroInfo.GetInstance().playerRes.maxOil - HeroInfo.GetInstance().playerRes.resOil)
									{
										this.isCoin = false;
									}
									break;
								case 3:
									if (int.Parse(this.resTable[k].transform.GetChild(l).GetChild(0).GetComponent<UILabel>().text.ToString()) > HeroInfo.GetInstance().playerRes.maxSteel - HeroInfo.GetInstance().playerRes.resSteel)
									{
										this.isCoin = false;
									}
									break;
								case 4:
									if (int.Parse(this.resTable[k].transform.GetChild(l).GetChild(0).GetComponent<UILabel>().text.ToString()) > HeroInfo.GetInstance().playerRes.maxRareEarth - HeroInfo.GetInstance().playerRes.resRareEarth)
									{
										this.isCoin = false;
									}
									break;
								}
								IL_44E:
								l++;
								continue;
								goto IL_44E;
							}
						}
					}
				}
				if (!this.isCoin)
				{
					HUDTextTool.inst.SetText(LanguageManage.GetTextByKey("资源已达上限", "others"), HUDTextTool.TextUITypeEnum.Num5);
					return;
				}
				EmailItem._instance.emailFlag.spriteName = "邮件打开";
				EmailManager.GetIns().ChangeMailState(num, true);
				this.RefreshAttachMentState(num);
				EmailHandler.CSReceiveMailAttachment(num, delegate(bool isError)
				{
					if (!isError)
					{
						ShowAwardPanelManger.showAwardList();
					}
				});
				go.transform.GetComponent<UISprite>().spriteName = "hui";
				go.transform.GetComponent<UIButton>().normalSprite = "hui";
				go.transform.GetComponent<UIButton>().hoverSprite = "hui";
				go.transform.GetComponent<UIButton>().pressedSprite = "hui";
				go.transform.GetComponent<UIButton>().disabledSprite = "hui";
				go.transform.GetChild(0).GetComponent<UILabel>().text = LanguageManage.GetTextByKey("已领取", "others");
				go.transform.GetComponent<EmailBtn>().enabled = false;
				go.transform.GetComponent<BoxCollider>().enabled = false;
				this.isClick = true;
			}
			else
			{
				MessageBox.GetMessagePanel().ShowBtn(LanguageManage.GetTextByKey("神手速", "others"), LanguageManage.GetTextByKey("手速太快了", "others"), LanguageManage.GetTextByKey("好吧", "others"), null);
			}
			break;
		case EmailBtnType.expend:
		{
			long num2 = long.Parse(go.transform.parent.name);
			this.Expend(num2, go.GetComponentInParent<EmailItem>());
			EmailHandler.CSReadMail(num2);
			break;
		}
		case EmailBtnType.clearEmail:
			this.itemTable.DestoryChildren(true);
			break;
		}
	}

	public void SetUITexture(UITexture texture, string path, string name)
	{
		texture.mainTexture = (Resources.Load(path + name) as Texture);
	}

	private void Expend(long id, EmailItem thisItem)
	{
		EmailInfo emailById = EmailManager.GetIns().GetEmailById(id);
		if (!emailById.isOpened)
		{
			emailById.isOpened = true;
			EmailHandler.CSReadMail(id);
		}
		if (!thisItem.IsExpend)
		{
			thisItem.content.text = emailById.content;
			thisItem.expend.localEulerAngles = new Vector3(0f, 0f, 180f);
			thisItem.expend.localPosition = new Vector3(651f, -270f, 0f);
			thisItem.emailBG.GetComponent<UITexture>().height = 367;
			this.SetUITexture(thisItem.emailBG, "wangyuan/Email/Texture/", "新邮件背景打开");
			thisItem.helpInfo.gameObject.SetActive(false);
			thisItem.emailFlag.spriteName = "邮件打开";
			thisItem.bottom.SetActive(true);
			emailById.isOpened = true;
			if (emailById.items.Count > 0 || emailById.resources.Count > 0 || emailById.zuanshiNum > 0)
			{
				thisItem.helpInfo.gameObject.SetActive(false);
				if (!emailById.isGetReward)
				{
					LogManage.LogError("info.isGetReward   可领取");
					thisItem.helpInfo.text = LanguageManage.GetTextByKey("可领取奖励", "others");
					thisItem.helpInfo.color = new Color(0.294117659f, 0.8980392f, 0.972549f);
					thisItem.btnReward.GetComponent<UISprite>().spriteName = "blue";
					thisItem.btnReward.GetComponent<UIButton>().normalSprite = "blue";
					thisItem.btnReward.GetComponent<UIButton>().hoverSprite = "blued";
					thisItem.btnReward.GetComponent<UIButton>().pressedSprite = "blued";
					thisItem.btnReward.GetComponent<UIButton>().disabledSprite = "blue";
					thisItem.xulz.SetActive(true);
					thisItem.btnReward.GetComponent<BoxCollider>().enabled = true;
					thisItem.btnReward.GetComponent<EmailBtn>().enabled = true;
					thisItem.showState.text = LanguageManage.GetTextByKey("领取奖励", "others");
				}
				else
				{
					LogManage.LogError("info.isGetReward   已领取");
					thisItem.helpInfo.text = LanguageManage.GetTextByKey("已领取", "others");
					thisItem.helpInfo.color = new Color(0.0784313753f, 0.8980392f, 0.0784313753f);
					thisItem.btnReward.GetComponent<UISprite>().spriteName = "hui";
					thisItem.btnReward.GetComponent<UIButton>().normalSprite = "hui";
					thisItem.btnReward.GetComponent<UIButton>().hoverSprite = "hui";
					thisItem.btnReward.GetComponent<UIButton>().pressedSprite = "hui";
					thisItem.btnReward.GetComponent<UIButton>().disabledSprite = "hui";
					thisItem.xulz.SetActive(false);
					thisItem.btnReward.GetComponent<BoxCollider>().enabled = false;
					thisItem.btnReward.GetComponent<EmailBtn>().enabled = false;
				}
			}
		}
		else
		{
			emailById.isOpened = true;
			if (emailById.content.Length > this.sublength)
			{
				thisItem.content.text = emailById.content.Substring(0, this.sublength) + "...";
			}
			else
			{
				thisItem.content.text = emailById.content;
			}
			if (emailById.items.Count > 0 || emailById.resources.Count > 0 || emailById.zuanshiNum > 0)
			{
				thisItem.helpInfo.gameObject.SetActive(true);
				if (!emailById.isGetReward)
				{
					thisItem.helpInfo.text = LanguageManage.GetTextByKey("可领取奖励", "others");
					thisItem.helpInfo.color = new Color(0.294117659f, 0.8980392f, 0.972549f);
				}
				if (emailById.isGetReward)
				{
					thisItem.helpInfo.text = LanguageManage.GetTextByKey("已领取", "others");
					thisItem.helpInfo.color = new Color(0.0784313753f, 0.8980392f, 0.0784313753f);
					thisItem.btnReward.GetComponent<UISprite>().spriteName = "hui";
					thisItem.btnReward.GetComponent<UIButton>().normalSprite = "hui";
					thisItem.btnReward.GetComponent<UIButton>().pressedSprite = "hui";
					thisItem.btnReward.GetComponent<UIButton>().hoverSprite = "hui";
					thisItem.btnReward.GetComponent<UIButton>().disabledSprite = "hui";
					thisItem.xulz.SetActive(false);
					thisItem.showState.text = LanguageManage.GetTextByKey("已领取", "others");
					thisItem.btnReward.GetComponent<EmailBtn>().enabled = false;
				}
			}
			else
			{
				thisItem.helpInfo.gameObject.SetActive(false);
				thisItem.btnReward.SetActive(false);
			}
			thisItem.expend.localEulerAngles = Vector3.zero;
			thisItem.expend.localPosition = new Vector3(651f, -39.1f, 0f);
			this.SetUITexture(thisItem.emailBG, "wangyuan/Email/Texture/", "新邮件背景");
			thisItem.emailBG.GetComponent<UITexture>().height = 134;
			if (emailById.isGetReward)
			{
				thisItem.btnReward.GetComponent<UISprite>().spriteName = "hui";
				thisItem.btnReward.GetComponent<UIButton>().normalSprite = "hui";
				thisItem.btnReward.GetComponent<UIButton>().pressedSprite = "hui";
				thisItem.btnReward.GetComponent<UIButton>().hoverSprite = "hui";
				thisItem.btnReward.GetComponent<UIButton>().disabledSprite = "hui";
				thisItem.xulz.SetActive(false);
				thisItem.btnReward.GetComponent<EmailBtn>().enabled = false;
				thisItem.showState.text = LanguageManage.GetTextByKey("已领取", "others");
				thisItem.btnReward.GetComponent<BoxCollider>().enabled = false;
			}
			else
			{
				thisItem.btnReward.GetComponent<UISprite>().spriteName = "升级、确定按钮";
				thisItem.btnReward.GetComponent<UIButton>().normalSprite = "升级、确定按钮";
				thisItem.btnReward.GetComponent<UIButton>().hoverSprite = "升级、确定按钮选中";
				thisItem.btnReward.GetComponent<UIButton>().pressedSprite = "升级、确定按钮选中";
				thisItem.btnReward.GetComponent<UIButton>().disabledSprite = "升级、确定按钮";
				thisItem.xulz.SetActive(true);
				thisItem.showState.text = LanguageManage.GetTextByKey("领取奖励", "others");
			}
			thisItem.helpInfo.gameObject.SetActive(true);
			thisItem.bottom.SetActive(false);
		}
		thisItem.IsExpend = !thisItem.IsExpend;
		NGUITools.UpdateWidgetCollider(thisItem.obj);
		base.StartCoroutine(this.Reposition());
	}

	[DebuggerHidden]
	private IEnumerator Reposition()
	{
		EmailPanel.<Reposition>c__Iterator68 <Reposition>c__Iterator = new EmailPanel.<Reposition>c__Iterator68();
		<Reposition>c__Iterator.<>f__this = this;
		return <Reposition>c__Iterator;
	}

	public void RefreshAttachMentState(long emailId)
	{
		string text = emailId.ToString();
		EmailInfo emailById = EmailManager.GetIns().GetEmailById(emailId);
		if (emailById == null)
		{
			return;
		}
		for (int i = 0; i < this.itemTable.children.Count; i++)
		{
			if (text.Equals(this.itemTable.children[i].gameObject.name))
			{
				EmailItem component = this.itemTable.children[i].GetComponent<EmailItem>();
				emailById.isGetReward = true;
				component.helpInfo.text = LanguageManage.GetTextByKey("已领取", "others");
				component.btnReward.GetComponent<UISprite>().spriteName = "hui";
				component.btnReward.GetComponent<UIButton>().normalSprite = "hui";
				component.btnReward.GetComponent<UIButton>().pressedSprite = "hui";
				component.btnReward.GetComponent<UIButton>().hoverSprite = "hui";
				component.btnReward.GetComponent<UIButton>().disabledSprite = "hui";
				component.xulz.SetActive(false);
				component.showState.text = LanguageManage.GetTextByKey("已领取", "others");
				component.helpInfo.color = new Color(0.06666667f, 0.7411765f, 0f);
				if (!emailById.isOpened)
				{
					emailById.isOpened = true;
				}
				break;
			}
		}
	}
}
