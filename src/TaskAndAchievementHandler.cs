using msg;
using System;

public class TaskAndAchievementHandler
{
	private static Action<bool> RecieveTask;

	private static Action<bool> achievement;

	public static void CG_CSCompleteTask(int taskId, Action<bool> func = null)
	{
		TaskAndAchievementHandler.RecieveTask = func;
		CSCompleteTask cSCompleteTask = new CSCompleteTask();
		cSCompleteTask.taskId = taskId;
		ClientMgr.GetNet().SendHttp(1300, cSCompleteTask, new DataHandler.OpcodeHandler(TaskAndAchievementHandler.OnHttpShow), null);
	}

	private static void OnHttpShow(bool isError, Opcode opcode)
	{
		if (TaskAndAchievementHandler.RecieveTask != null)
		{
			TaskAndAchievementHandler.RecieveTask(isError);
		}
	}

	public static void CG_CSCompleteAchievement(int achievementId, Action<bool> func = null)
	{
		TaskAndAchievementHandler.achievement = func;
		CSCompleteAchievement cSCompleteAchievement = new CSCompleteAchievement();
		cSCompleteAchievement.achievementId = achievementId;
		ClientMgr.GetNet().SendHttp(1400, cSCompleteAchievement, new DataHandler.OpcodeHandler(TaskAndAchievementHandler.OnHttpShowTask), null);
	}

	private static void OnHttpShowTask(bool isError, Opcode opcode)
	{
		if (TaskAndAchievementHandler.achievement != null)
		{
			TaskAndAchievementHandler.achievement(isError);
		}
	}
}
