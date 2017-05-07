using System;
using UnityEngine;

public class AchievementItem : MonoBehaviour
{
	public int id;

	public GameObject Btn;

	public GameObject Process;

	public GameObject Content;

	public UISprite PrizeUISprite;

	public UISprite ProcessUISprite;

	public UISprite itemTypeIcon;

	public UILabel PrizeUILabel;

	public UILabel ProcessUILabel;

	public UILabel tittleLabel;

	public UILabel descriptionUILabel;

	public GameObject CompleteBG;

	public UISprite[] Stars;

	private void Awake()
	{
	}

	public void InitData()
	{
		Achievement achievement = UnitConst.GetInstance().AllAchievementConst[this.id];
		this.tittleLabel.text = LanguageManage.GetTextByKey(achievement.name, "achieve");
		this.tittleLabel.gameObject.SetActive(true);
		this.Process.gameObject.SetActive(true);
		if (achievement.lastStar < 5)
		{
			if (achievement.stepRecord < achievement.step[0] || achievement.lastStar == 0)
			{
				this.PrizeUILabel.text = achievement.prizes[0].ToString();
				this.descriptionUILabel.text = LanguageManage.GetTextByKey(achievement.description, "achieve") + achievement.step[0];
				if (achievement.stepRecord < achievement.step[0])
				{
					this.IsCanReceive(false);
					this.ProcessUILabel.text = achievement.stepRecord + "/" + achievement.step[0];
					this.ProcessUISprite.fillAmount = (float)achievement.stepRecord / (float)achievement.step[0];
				}
				else
				{
					this.ProcessUILabel.text = achievement.step[0] + "/" + achievement.step[0];
					this.ProcessUISprite.fillAmount = 1f;
					this.IsCanReceive(true);
				}
				this.SetStar(0, this.Stars);
			}
			else if (achievement.stepRecord < achievement.step[1] || achievement.lastStar == 1)
			{
				this.descriptionUILabel.text = LanguageManage.GetTextByKey(achievement.description, "achieve") + achievement.step[1];
				this.PrizeUILabel.text = achievement.prizes[1].ToString();
				if (achievement.stepRecord < achievement.step[1])
				{
					this.IsCanReceive(false);
					this.ProcessUILabel.text = achievement.stepRecord + "/" + achievement.step[1];
					this.ProcessUISprite.fillAmount = (float)achievement.stepRecord / (float)achievement.step[1];
				}
				else
				{
					this.IsCanReceive(true);
					this.ProcessUILabel.text = achievement.step[1] + "/" + achievement.step[1];
					this.ProcessUISprite.fillAmount = (float)achievement.step[1] / (float)achievement.step[1];
				}
				this.SetStar(1, this.Stars);
			}
			else if (achievement.stepRecord < achievement.step[2] || achievement.lastStar == 2)
			{
				this.descriptionUILabel.text = LanguageManage.GetTextByKey(achievement.description, "achieve") + achievement.step[2];
				this.PrizeUILabel.text = achievement.prizes[2].ToString();
				if (achievement.stepRecord < achievement.step[2])
				{
					this.IsCanReceive(false);
					this.ProcessUILabel.text = achievement.stepRecord + "/" + achievement.step[2];
					this.ProcessUISprite.fillAmount = (float)achievement.stepRecord / (float)achievement.step[2];
				}
				else
				{
					this.IsCanReceive(true);
					this.ProcessUILabel.text = achievement.step[2] + "/" + achievement.step[2];
					this.ProcessUISprite.fillAmount = (float)achievement.step[2] / (float)achievement.step[2];
				}
				this.SetStar(2, this.Stars);
			}
			else if (achievement.stepRecord < achievement.step[3] || achievement.lastStar == 3)
			{
				this.descriptionUILabel.text = LanguageManage.GetTextByKey(achievement.description, "achieve") + achievement.step[3];
				this.PrizeUILabel.text = achievement.prizes[3].ToString();
				if (achievement.stepRecord < achievement.step[3])
				{
					this.IsCanReceive(false);
					this.ProcessUILabel.text = achievement.stepRecord + "/" + achievement.step[3];
					this.ProcessUISprite.fillAmount = (float)achievement.stepRecord / (float)achievement.step[3];
				}
				else
				{
					this.IsCanReceive(true);
					this.ProcessUILabel.text = achievement.step[3] + "/" + achievement.step[3];
					this.ProcessUISprite.fillAmount = (float)achievement.step[3] / (float)achievement.step[3];
				}
				this.SetStar(3, this.Stars);
			}
			else if (achievement.stepRecord < achievement.step[4] || achievement.lastStar == 4)
			{
				this.descriptionUILabel.text = achievement.description + achievement.step[4];
				this.PrizeUILabel.text = achievement.prizes[4].ToString();
				if (achievement.stepRecord < achievement.step[4])
				{
					this.IsCanReceive(false);
					this.ProcessUILabel.text = achievement.stepRecord + "/" + achievement.step[4];
					this.ProcessUISprite.fillAmount = (float)achievement.stepRecord / (float)achievement.step[4];
				}
				else
				{
					this.IsCanReceive(true);
					this.ProcessUILabel.text = achievement.step[4] + "/" + achievement.step[4];
					this.ProcessUISprite.fillAmount = (float)achievement.step[4] / (float)achievement.step[4];
				}
				this.SetStar(4, this.Stars);
			}
		}
		else
		{
			this.descriptionUILabel.text = achievement.description + achievement.step[4];
			this.SetStar(5, this.Stars);
			this.Content.SetActive(false);
			this.CompleteBG.SetActive(true);
			this.Process.SetActive(false);
			this.descriptionUILabel.gameObject.SetActive(false);
			this.Btn.SetActive(false);
			this.PrizeUISprite.gameObject.SetActive(false);
			this.Content.gameObject.SetActive(true);
			this.itemTypeIcon.gameObject.SetActive(true);
			this.itemTypeIcon.spriteName = "成就完成字体";
		}
	}

	private void IsCanReceive(bool isCan)
	{
		this.Process.SetActive(true);
		if (!isCan)
		{
			this.itemTypeIcon.gameObject.SetActive(true);
		}
		this.Btn.SetActive(isCan);
	}

	private void SetStar(int num, UISprite[] stars)
	{
		for (int i = 1; i <= stars.Length; i++)
		{
			if (i <= num)
			{
				stars[i - 1].spriteName = "星2";
			}
			else
			{
				stars[i - 1].spriteName = "空星";
			}
			if (i == num)
			{
			}
		}
	}
}
