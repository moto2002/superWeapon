using System;
using System.IO;
using System.Text;

public class PacketBundle
{
	private static int[] sim;

	private static int[] spe;

	public static bool ToMsg(short nOpCode, object data, out byte[] msg)
	{
		MemoryStream memoryStream = new MemoryStream();
		Opcode.Serialize(memoryStream, data);
		byte[] array = memoryStream.ToArray();
		int num = array.Length;
		short num2 = (short)num;
		byte[] array2 = PacketBundle.ByteWriteShort(num2);
		byte[] array3 = PacketBundle.ByteWriteShort(nOpCode);
		msg = new byte[(int)(num2 + 4)];
		Buffer.BlockCopy(array2, 0, msg, 0, array2.Length);
		Buffer.BlockCopy(array3, 0, msg, array2.Length, array3.Length);
		Buffer.BlockCopy(array, 0, msg, array2.Length + array3.Length, array.Length);
		return true;
	}

	public static bool ToObject(byte[] msg, out int nOpCode, out object obj)
	{
		if (msg.Length < 8)
		{
			nOpCode = -1;
			obj = null;
			return false;
		}
		byte[] array = new byte[msg.Length];
		Buffer.BlockCopy(msg, 0, array, 0, msg.Length);
		int num = PacketBundle.ByteGetInt(array, 0);
		nOpCode = PacketBundle.ByteGetInt(array, 4);
		byte[] dst = new byte[num];
		Buffer.BlockCopy(array, 8, dst, 0, num);
		obj = null;
		return false;
	}

	public static int GetMsgLength(byte[] msg)
	{
		byte[] array = new byte[4];
		Buffer.BlockCopy(msg, 0, array, 0, 4);
		Array.Reverse(array, 0, 4);
		return BitConverter.ToInt32(array, 0);
	}

	private static byte[] ByteWriteInt(int n)
	{
		byte[] bytes = BitConverter.GetBytes(n);
		if (BitConverter.IsLittleEndian)
		{
			Array.Reverse(bytes);
		}
		return bytes;
	}

	private static byte[] ByteWriteShort(short n)
	{
		byte[] bytes = BitConverter.GetBytes(n);
		if (BitConverter.IsLittleEndian)
		{
			Array.Reverse(bytes);
		}
		return bytes;
	}

	public static ushort ByteReadShort(byte[] arrByte, int start)
	{
		byte[] array = new byte[2];
		Buffer.BlockCopy(arrByte, start, array, 0, 2);
		if (BitConverter.IsLittleEndian)
		{
			Array.Reverse(array);
		}
		return BitConverter.ToUInt16(array, 0);
	}

	private static byte[] ByteWriteLong(long l)
	{
		byte[] bytes = BitConverter.GetBytes(l);
		if (BitConverter.IsLittleEndian)
		{
			Array.Reverse(bytes);
		}
		return bytes;
	}

	private static byte[] ByteWriteString(string s)
	{
		return Encoding.ASCII.GetBytes(s);
	}

	private static int ByteGetInt(byte[] msg, int nBegin)
	{
		Array.Reverse(msg, nBegin, 4);
		return BitConverter.ToInt32(msg, nBegin);
	}

	private static bool IsSim(int nOpcode)
	{
		return false;
	}

	private static bool IsSpe(int nOpcode)
	{
		for (int i = 0; i < PacketBundle.spe.Length; i++)
		{
			if (PacketBundle.spe[i] == nOpcode)
			{
				return true;
			}
		}
		return false;
	}
}
