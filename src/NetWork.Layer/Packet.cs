using System;

namespace NetWork.Layer
{
	public class Packet
	{
		public int nOpCode;

		public Opcode kBody;

		public Packet(int code, Opcode buff)
		{
			this.nOpCode = code;
			this.kBody = buff;
		}
	}
}
