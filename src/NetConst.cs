using System;

public class NetConst
{
	public static string ip
	{
		get
		{
			string[] array = User.GetIP().Split(new char[]
			{
				'/'
			}, StringSplitOptions.RemoveEmptyEntries);
			return array[array.Length - 1].Split(new char[]
			{
				':'
			})[0];
		}
	}

	public static int port
	{
		get
		{
			string[] array = User.GetIP().Split(new char[]
			{
				'/'
			}, StringSplitOptions.RemoveEmptyEntries);
			string s = array[array.Length - 1].Split(new char[]
			{
				':'
			})[1];
			return int.Parse(s) + 100;
		}
	}
}
