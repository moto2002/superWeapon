using msg;
using System;

public class ItemHandler
{
	private static Action func;

	public static void CG_ItemList(Action _func)
	{
		CSItemList cSItemList = new CSItemList();
		cSItemList.typeId = 22;
		ItemHandler.func = _func;
		ClientMgr.GetNet().SendHttp(1100, cSItemList, new DataHandler.OpcodeHandler(ItemHandler.GC_ItemList), null);
	}

	public static void GC_ItemList(bool isError, Opcode opcode)
	{
		if (ItemHandler.func != null)
		{
			ItemHandler.func();
		}
	}

	public static void CG_MixItem(int itemId, int needNum, Action _func)
	{
		CSItemMix cSItemMix = new CSItemMix();
		cSItemMix.itemId = 22;
		cSItemMix.itemNum = needNum;
		ItemHandler.func = _func;
		ClientMgr.GetNet().SendHttp(1100, cSItemMix, null, null);
	}

	public static void GC_MixItem(Opcode opcode)
	{
		if (ItemHandler.func != null)
		{
			ItemHandler.func();
		}
	}
}
