using System;

public class MapGridManager
{
	public static GridRows[] rows;

	public static void CreatMapGrid(int row, int num)
	{
		row++;
		num++;
		MapGridManager.rows = new GridRows[row];
		for (int i = 0; i < MapGridManager.rows.Length; i++)
		{
			MapGridManager.rows[i] = new GridRows();
			MapGridManager.rows[i].num = new int[num];
		}
	}

	public static void AddMapGrid(int x, int z)
	{
		MapGridManager.rows[x].num[z] = -2;
	}

	public static void RemoveMapGrid(int x, int z)
	{
		MapGridManager.rows[x].num[z] = 0;
	}

	public static bool VerifyTowerGrid(int towerIdx, int row, int num)
	{
		int size = UnitConst.GetInstance().buildingConst[towerIdx].size;
		for (int i = 0; i < UnitConst.GetInstance().buildingConst[towerIdx].towerGrids.Length; i++)
		{
			int num2 = row + UnitConst.GetInstance().buildingConst[towerIdx].towerGrids[i].numX;
			if (num2 < 0 || num2 >= SenceManager.inst.arrayX)
			{
				return false;
			}
			int num3 = num + UnitConst.GetInstance().buildingConst[towerIdx].towerGrids[i].numZ;
			if (num3 < 0 || num3 >= SenceManager.inst.arrayY)
			{
				return false;
			}
			if (!MapGridManager.VerifyMapGrid(num2, num3))
			{
				return false;
			}
		}
		return true;
	}

	public static bool VerifyMapGrid(int x, int z)
	{
		return MapGridManager.rows[x].num[z] == 0;
	}

	public static bool VerifyMapGrid2(int x, int z)
	{
		return x >= 0 && z >= 0 && x < 50 && z < 50 && MapGridManager.rows[x].num[z] == 0;
	}
}
