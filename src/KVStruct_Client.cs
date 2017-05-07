using System;

public class KVStruct_Client
{
	private int key;

	private int value;

	public int Value
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	public int Key
	{
		get
		{
			return this.key;
		}
		set
		{
			this.key = value;
		}
	}

	public KVStruct_Client(int _key, int _value)
	{
		this.key = _key;
		this.value = _value;
	}
}
