using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class A_WMap_Road : MonoBehaviour
{
	public Transform Player;

	public Transform Target;

	public Transform Nodes;

	public List<Transform> NodeList0 = new List<Transform>();

	public List<Transform> NodeList1 = new List<Transform>();

	public List<Transform> NodeList2 = new List<Transform>();

	public List<Transform> NodeList3 = new List<Transform>();

	private string Car_Name;

	public Transform a;

	public Transform b;

	private Body_Model Car;

	private int random0;

	private float time0;

	private int random;

	private void Start()
	{
		this.Car_Name = "Basecar";
	}

	private void SetCar1()
	{
		this.a = (UnityEngine.Object.Instantiate(this.Player.transform, this.NodeList1[0].transform.position, this.Player.transform.rotation) as Transform);
		this.a.GetComponent<AIPath>().MapCar = true;
		this.a.transform.parent = base.transform;
		this.a.GetComponent<AIPath>().speed = 0.5f;
		this.Car = PoolManage.Ins.GetModelByBundleByName(this.Car_Name, null);
		this.Car.tr.localScale = Vector3.one * 0.05f;
		this.Car.tr.parent = this.a.transform;
		this.Car.tr.localEulerAngles = Vector3.zero;
		this.Car.tr.localPosition = new Vector3(0f, 1.2f, 0f);
		this.Car.ga.AddComponent<A_WMap_Car>();
		this.Car.ga.GetComponent<A_WMap_Car>().AW_Road = this;
		this.Car.ga.GetComponent<A_WMap_Car>().Road_No = 1;
		this.Car.gameObject.SetActive(false);
		base.StartCoroutine(this.Car_Show(this.Car.tr));
		this.b = (UnityEngine.Object.Instantiate(this.Target.transform, this.NodeList1[1].transform.position, this.Player.transform.rotation) as Transform);
		this.b.transform.parent = base.transform;
		this.a.GetComponent<AIPath>().target = this.b.transform;
	}

	private void SetCar2()
	{
		this.a = (UnityEngine.Object.Instantiate(this.Player.transform, this.NodeList2[0].transform.position, this.Player.transform.rotation) as Transform);
		this.a.GetComponent<AIPath>().MapCar = true;
		this.a.transform.parent = base.transform;
		this.a.GetComponent<AIPath>().speed = 0.5f;
		this.a.GetComponent<AIPath>().slowdownDistance = 0f;
		this.Car = PoolManage.Ins.GetModelByBundleByName(this.Car_Name, null);
		this.Car.tr.localScale = Vector3.one * 0.05f;
		this.Car.tr.parent = this.a.transform;
		this.Car.tr.localEulerAngles = Vector3.zero;
		this.Car.tr.localPosition = new Vector3(0f, 1.2f, 0f);
		this.Car.ga.AddComponent<A_WMap_Car>();
		this.Car.ga.GetComponent<A_WMap_Car>().WM_CarType = WMap_CarType.OnBridge;
		this.Car.ga.GetComponent<A_WMap_Car>().AW_Road = this;
		this.Car.ga.GetComponent<A_WMap_Car>().Road_No = 2;
		this.Car.gameObject.SetActive(false);
		base.StartCoroutine(this.Car_Show(this.Car.tr));
		this.b = (UnityEngine.Object.Instantiate(this.Target.transform, this.NodeList2[1].transform.position, this.Player.transform.rotation) as Transform);
		this.b.transform.parent = base.transform;
		this.a.GetComponent<AIPath>().target = this.b.transform;
	}

	private void SetCar3()
	{
		this.a = (UnityEngine.Object.Instantiate(this.Player.transform, this.NodeList3[0].transform.position, this.Player.transform.rotation) as Transform);
		this.a.GetComponent<AIPath>().MapCar = true;
		this.a.transform.parent = base.transform;
		this.a.GetComponent<AIPath>().speed = 0.5f;
		this.a.GetComponent<AIPath>().slowdownDistance = 0f;
		this.Car = PoolManage.Ins.GetModelByBundleByName(this.Car_Name, null);
		this.Car.tr.localScale = Vector3.one * 0.05f;
		this.Car.tr.parent = this.a.transform;
		this.Car.tr.localEulerAngles = Vector3.zero;
		this.Car.tr.localPosition = new Vector3(0f, 1.2f, 0f);
		this.Car.ga.AddComponent<A_WMap_Car>();
		this.Car.ga.GetComponent<A_WMap_Car>().WM_CarType = WMap_CarType.Rotate1;
		this.Car.ga.GetComponent<A_WMap_Car>().AW_Road = this;
		this.Car.ga.GetComponent<A_WMap_Car>().Road_No = 3;
		this.Car.gameObject.SetActive(false);
		base.StartCoroutine(this.Car_Show(this.Car.tr));
		this.b = (UnityEngine.Object.Instantiate(this.Target.transform, this.NodeList3[1].transform.position, this.Player.transform.rotation) as Transform);
		this.b.transform.parent = base.transform;
		this.a.GetComponent<AIPath>().target = this.b.transform;
	}

	private void SetCar0(int no)
	{
		this.a = (UnityEngine.Object.Instantiate(this.Player.transform, this.NodeList0[this.random0].transform.position, this.Player.transform.rotation) as Transform);
		this.a.GetComponent<AIPath>().MapCar = true;
		this.a.transform.parent = base.transform;
		no = UnityEngine.Random.Range(0, 10);
		if (no > 5)
		{
			no = 0;
		}
		no = 0;
		string name = string.Empty;
		switch (no)
		{
		case 0:
			name = "kirov";
			this.a.GetComponent<AIPath>().speed = 0.4f;
			break;
		case 1:
			name = "bomber";
			this.a.GetComponent<AIPath>().speed = 1f;
			break;
		case 2:
			name = "ari_mige";
			this.a.GetComponent<AIPath>().speed = 1.6f;
			break;
		case 3:
			name = "ari_appollo";
			this.a.GetComponent<AIPath>().speed = 1.2f;
			break;
		case 4:
			name = "ari_icehel";
			this.a.GetComponent<AIPath>().speed = 0.4f;
			break;
		case 5:
			name = "ari_apache";
			this.a.GetComponent<AIPath>().speed = 0.6f;
			break;
		}
		this.Car = PoolManage.Ins.GetModelByBundleByName(name, null);
		this.Car.tr.localScale = Vector3.one * 0.03f;
		this.Car.tr.parent = this.a.transform;
		this.Car.tr.localEulerAngles = Vector3.zero;
		this.Car.tr.localPosition = new Vector3(0f, 9f, 0f);
		this.Car.ga.AddComponent<A_WMap_Car>();
		this.Car.ga.GetComponent<A_WMap_Car>().AW_Road = this;
		this.Car.ga.GetComponent<A_WMap_Car>().Road_No = 1;
		this.Car.gameObject.SetActive(false);
		base.StartCoroutine(this.Car_Show(this.Car.tr));
		int num = this.random0 + 3;
		if (num >= 7)
		{
			num -= 7;
		}
		this.b = (UnityEngine.Object.Instantiate(this.Target.transform, this.NodeList0[num].transform.position, this.Player.transform.rotation) as Transform);
		this.b.transform.parent = base.transform;
		this.a.GetComponent<AIPath>().target = this.b.transform;
		this.random0++;
		if (this.random0 >= 7)
		{
			this.random0 = 0;
		}
	}

	[DebuggerHidden]
	private IEnumerator Car_Show(Transform car)
	{
		A_WMap_Road.<Car_Show>c__Iterator3D <Car_Show>c__Iterator3D = new A_WMap_Road.<Car_Show>c__Iterator3D();
		<Car_Show>c__Iterator3D.car = car;
		<Car_Show>c__Iterator3D.<$>car = car;
		<Car_Show>c__Iterator3D.<>f__this = this;
		return <Car_Show>c__Iterator3D;
	}

	private void Update()
	{
		if (Loading.senceType != SenceType.WorldMap)
		{
			return;
		}
		this.time0 += Time.deltaTime;
		if (this.time0 >= 5f)
		{
			this.time0 = 0f;
			this.random = UnityEngine.Random.Range(0, 10);
			switch (this.random)
			{
			case 0:
				this.Car_Name = "tank1_1";
				break;
			case 1:
				this.Car_Name = "tank2_1";
				break;
			case 2:
				this.Car_Name = "tank3_1";
				break;
			case 3:
				this.Car_Name = "tank4_1";
				break;
			case 4:
				this.Car_Name = "tank3_1";
				break;
			case 5:
				this.Car_Name = "tank3_1";
				break;
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
				this.Car_Name = "Basecar";
				break;
			}
			switch (this.random % 3)
			{
			case 0:
				this.SetCar1();
				break;
			case 1:
				this.SetCar2();
				this.SetCar0(0);
				break;
			case 2:
				this.SetCar3();
				break;
			}
		}
	}
}
