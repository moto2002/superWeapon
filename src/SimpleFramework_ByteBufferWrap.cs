using LuaInterface;
using SimpleFramework;
using System;

public class SimpleFramework_ByteBufferWrap
{
	private static Type classType = typeof(ByteBuffer);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Close", new LuaCSFunction(SimpleFramework_ByteBufferWrap.Close)),
			new LuaMethod("WriteByte", new LuaCSFunction(SimpleFramework_ByteBufferWrap.WriteByte)),
			new LuaMethod("WriteInt", new LuaCSFunction(SimpleFramework_ByteBufferWrap.WriteInt)),
			new LuaMethod("WriteShort", new LuaCSFunction(SimpleFramework_ByteBufferWrap.WriteShort)),
			new LuaMethod("WriteLong", new LuaCSFunction(SimpleFramework_ByteBufferWrap.WriteLong)),
			new LuaMethod("WriteFloat", new LuaCSFunction(SimpleFramework_ByteBufferWrap.WriteFloat)),
			new LuaMethod("WriteDouble", new LuaCSFunction(SimpleFramework_ByteBufferWrap.WriteDouble)),
			new LuaMethod("WriteString", new LuaCSFunction(SimpleFramework_ByteBufferWrap.WriteString)),
			new LuaMethod("WriteBytes", new LuaCSFunction(SimpleFramework_ByteBufferWrap.WriteBytes)),
			new LuaMethod("WriteBuffer", new LuaCSFunction(SimpleFramework_ByteBufferWrap.WriteBuffer)),
			new LuaMethod("ReadByte", new LuaCSFunction(SimpleFramework_ByteBufferWrap.ReadByte)),
			new LuaMethod("ReadInt", new LuaCSFunction(SimpleFramework_ByteBufferWrap.ReadInt)),
			new LuaMethod("ReadShort", new LuaCSFunction(SimpleFramework_ByteBufferWrap.ReadShort)),
			new LuaMethod("ReadLong", new LuaCSFunction(SimpleFramework_ByteBufferWrap.ReadLong)),
			new LuaMethod("ReadFloat", new LuaCSFunction(SimpleFramework_ByteBufferWrap.ReadFloat)),
			new LuaMethod("ReadDouble", new LuaCSFunction(SimpleFramework_ByteBufferWrap.ReadDouble)),
			new LuaMethod("ReadString", new LuaCSFunction(SimpleFramework_ByteBufferWrap.ReadString)),
			new LuaMethod("ReadBytes", new LuaCSFunction(SimpleFramework_ByteBufferWrap.ReadBytes)),
			new LuaMethod("ReadBuffer", new LuaCSFunction(SimpleFramework_ByteBufferWrap.ReadBuffer)),
			new LuaMethod("ToBytes", new LuaCSFunction(SimpleFramework_ByteBufferWrap.ToBytes)),
			new LuaMethod("Flush", new LuaCSFunction(SimpleFramework_ByteBufferWrap.Flush)),
			new LuaMethod("New", new LuaCSFunction(SimpleFramework_ByteBufferWrap._CreateSimpleFramework_ByteBuffer)),
			new LuaMethod("GetClassType", new LuaCSFunction(SimpleFramework_ByteBufferWrap.GetClassType))
		};
		LuaField[] fields = new LuaField[0];
		LuaScriptMgr.RegisterLib(L, "SimpleFramework.ByteBuffer", typeof(ByteBuffer), regs, fields, typeof(object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateSimpleFramework_ByteBuffer(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 0)
		{
			ByteBuffer o = new ByteBuffer();
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		if (num == 1)
		{
			byte[] arrayNumber = LuaScriptMgr.GetArrayNumber<byte>(L, 1);
			ByteBuffer o2 = new ByteBuffer(arrayNumber);
			LuaScriptMgr.PushObject(L, o2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: SimpleFramework.ByteBuffer.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, SimpleFramework_ByteBufferWrap.classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Close(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		byteBuffer.Close();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteByte(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		byte v = (byte)LuaScriptMgr.GetNumber(L, 2);
		byteBuffer.WriteByte(v);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteInt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		int v = (int)LuaScriptMgr.GetNumber(L, 2);
		byteBuffer.WriteInt(v);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteShort(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		ushort v = (ushort)LuaScriptMgr.GetNumber(L, 2);
		byteBuffer.WriteShort(v);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteLong(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		long v = (long)LuaScriptMgr.GetNumber(L, 2);
		byteBuffer.WriteLong(v);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteFloat(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		float v = (float)LuaScriptMgr.GetNumber(L, 2);
		byteBuffer.WriteFloat(v);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteDouble(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		double v = LuaScriptMgr.GetNumber(L, 2);
		byteBuffer.WriteDouble(v);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteString(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		byteBuffer.WriteString(luaString);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteBytes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		byte[] arrayNumber = LuaScriptMgr.GetArrayNumber<byte>(L, 2);
		byteBuffer.WriteBytes(arrayNumber);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteBuffer(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		LuaStringBuffer stringBuffer = LuaScriptMgr.GetStringBuffer(L, 2);
		byteBuffer.WriteBuffer(stringBuffer);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadByte(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		byte d = byteBuffer.ReadByte();
		LuaScriptMgr.Push(L, d);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadInt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		int d = byteBuffer.ReadInt();
		LuaScriptMgr.Push(L, d);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadShort(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		ushort d = byteBuffer.ReadShort();
		LuaScriptMgr.Push(L, d);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadLong(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		long d = byteBuffer.ReadLong();
		LuaScriptMgr.Push(L, d);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadFloat(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		float d = byteBuffer.ReadFloat();
		LuaScriptMgr.Push(L, d);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadDouble(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		double d = byteBuffer.ReadDouble();
		LuaScriptMgr.Push(L, d);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadString(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		string str = byteBuffer.ReadString();
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadBytes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		byte[] o = byteBuffer.ReadBytes();
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadBuffer(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		LuaStringBuffer lsb = byteBuffer.ReadBuffer();
		LuaScriptMgr.Push(L, lsb);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToBytes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		byte[] o = byteBuffer.ToBytes();
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Flush(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ByteBuffer byteBuffer = (ByteBuffer)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleFramework.ByteBuffer");
		byteBuffer.Flush();
		return 0;
	}
}
