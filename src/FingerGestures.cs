using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[AddComponentMenu("FingerGestures/Finger Gestures Singleton")]
public class FingerGestures : MonoBehaviour
{
	public enum FingerPhase
	{
		None,
		Begin,
		Moving,
		Stationary
	}

	public class InputProviderEvent
	{
		public FGInputProvider inputProviderPrefab;
	}

	public class Finger
	{
		private int index;

		private FingerGestures.FingerPhase phase;

		private FingerGestures.FingerPhase prevPhase;

		private Vector2 pos = Vector2.zero;

		private Vector2 startPos = Vector2.zero;

		private Vector2 prevPos = Vector2.zero;

		private Vector2 deltaPos = Vector2.zero;

		private float startTime;

		private float lastMoveTime;

		private float distFromStart;

		private bool moved;

		private bool filteredOut = true;

		private Collider collider;

		private Collider prevCollider;

		private float elapsedTimeStationary;

		private List<GestureRecognizer> gestureRecognizers = new List<GestureRecognizer>();

		private Dictionary<string, object> extendedProperties = new Dictionary<string, object>();

		public int Index
		{
			get
			{
				return this.index;
			}
		}

		public bool IsDown
		{
			get
			{
				return this.phase != FingerGestures.FingerPhase.None;
			}
		}

		public FingerGestures.FingerPhase Phase
		{
			get
			{
				return this.phase;
			}
		}

		public FingerGestures.FingerPhase PreviousPhase
		{
			get
			{
				return this.prevPhase;
			}
		}

		public bool WasDown
		{
			get
			{
				return this.prevPhase != FingerGestures.FingerPhase.None;
			}
		}

		public bool IsMoving
		{
			get
			{
				return this.phase == FingerGestures.FingerPhase.Moving;
			}
		}

		public bool WasMoving
		{
			get
			{
				return this.prevPhase == FingerGestures.FingerPhase.Moving;
			}
		}

		public bool IsStationary
		{
			get
			{
				return this.phase == FingerGestures.FingerPhase.Stationary;
			}
		}

		public bool WasStationary
		{
			get
			{
				return this.prevPhase == FingerGestures.FingerPhase.Stationary;
			}
		}

		public bool Moved
		{
			get
			{
				return this.moved;
			}
		}

		public float StarTime
		{
			get
			{
				return this.startTime;
			}
		}

		public Vector2 StartPosition
		{
			get
			{
				return this.startPos;
			}
		}

		public Vector2 Position
		{
			get
			{
				return this.pos;
			}
		}

		public Vector2 PreviousPosition
		{
			get
			{
				return this.prevPos;
			}
		}

		public Vector2 DeltaPosition
		{
			get
			{
				return this.deltaPos;
			}
		}

		public float DistanceFromStart
		{
			get
			{
				return this.distFromStart;
			}
		}

		public bool IsFiltered
		{
			get
			{
				return this.filteredOut;
			}
		}

		public float TimeStationary
		{
			get
			{
				return this.elapsedTimeStationary;
			}
		}

		public List<GestureRecognizer> GestureRecognizers
		{
			get
			{
				return this.gestureRecognizers;
			}
		}

		public Dictionary<string, object> ExtendedProperties
		{
			get
			{
				return this.extendedProperties;
			}
		}

		public Finger(int index)
		{
			this.index = index;
		}

		public override string ToString()
		{
			return "Finger" + this.index;
		}

		internal void Update(bool newDownState, Vector2 newPos)
		{
			if (this.filteredOut && !newDownState)
			{
				this.filteredOut = false;
			}
			if (!this.IsDown && newDownState && !FingerGestures.instance.ShouldProcessTouch(this.index, newPos))
			{
				this.filteredOut = true;
				newDownState = false;
			}
			this.prevPhase = this.phase;
			if (newDownState)
			{
				if (!this.WasDown)
				{
					this.phase = FingerGestures.FingerPhase.Begin;
					this.pos = newPos;
					this.startPos = this.pos;
					this.prevPos = this.pos;
					this.deltaPos = Vector2.zero;
					this.moved = false;
					this.lastMoveTime = 0f;
					this.startTime = Time.time;
					this.elapsedTimeStationary = 0f;
				}
				else
				{
					this.prevPos = this.pos;
					this.pos = newPos;
					this.distFromStart = Vector3.Distance(this.startPos, this.pos);
					this.deltaPos = this.pos - this.prevPos;
					if (this.deltaPos.sqrMagnitude > 0f)
					{
						this.lastMoveTime = Time.time;
						this.phase = FingerGestures.FingerPhase.Moving;
					}
					else if (!this.IsMoving || Time.time - this.lastMoveTime > 0.05f)
					{
						this.phase = FingerGestures.FingerPhase.Stationary;
					}
					if (this.IsMoving)
					{
						this.moved = true;
					}
					else if (!this.WasStationary)
					{
						this.elapsedTimeStationary = 0f;
					}
					else
					{
						this.elapsedTimeStationary += Time.deltaTime;
					}
				}
			}
			else
			{
				this.phase = FingerGestures.FingerPhase.None;
			}
		}
	}

	public interface IFingerList : IEnumerable<FingerGestures.Finger>, IEnumerable
	{
		FingerGestures.Finger this[int index]
		{
			get;
		}

		int Count
		{
			get;
		}

		Vector2 GetAverageStartPosition();

		Vector2 GetAveragePosition();

		Vector2 GetAveragePreviousPosition();

		float GetAverageDistanceFromStart();

		FingerGestures.Finger GetOldest();

		bool AllMoving();

		bool MovingInSameDirection(float tolerance);
	}

	[Serializable]
	public class FingerList : IEnumerable<FingerGestures.Finger>, FingerGestures.IFingerList, IEnumerable
	{
		public delegate T FingerPropertyGetterDelegate<T>(FingerGestures.Finger finger);

		[SerializeField]
		private List<FingerGestures.Finger> list;

		public FingerGestures.Finger this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		public FingerList()
		{
			this.list = new List<FingerGestures.Finger>();
		}

		public FingerList(List<FingerGestures.Finger> list)
		{
			this.list = list;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public IEnumerator<FingerGestures.Finger> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		public void Add(FingerGestures.Finger touch)
		{
			this.list.Add(touch);
		}

		public bool Remove(FingerGestures.Finger touch)
		{
			return this.list.Remove(touch);
		}

		public bool Contains(FingerGestures.Finger touch)
		{
			return this.list.Contains(touch);
		}

		public void AddRange(IEnumerable<FingerGestures.Finger> touches)
		{
			this.list.AddRange(touches);
		}

		public void Clear()
		{
			this.list.Clear();
		}

		public Vector2 AverageVector(FingerGestures.FingerList.FingerPropertyGetterDelegate<Vector2> getProperty)
		{
			Vector2 vector = Vector2.zero;
			if (this.Count > 0)
			{
				foreach (FingerGestures.Finger current in this.list)
				{
					vector += getProperty(current);
				}
				vector /= (float)this.Count;
			}
			return vector;
		}

		public float AverageFloat(FingerGestures.FingerList.FingerPropertyGetterDelegate<float> getProperty)
		{
			float num = 0f;
			if (this.Count > 0)
			{
				foreach (FingerGestures.Finger current in this.list)
				{
					num += getProperty(current);
				}
				num /= (float)this.Count;
			}
			return num;
		}

		private static Vector2 GetFingerStartPosition(FingerGestures.Finger finger)
		{
			return finger.StartPosition;
		}

		private static Vector2 GetFingerPosition(FingerGestures.Finger finger)
		{
			return finger.Position;
		}

		private static Vector2 GetFingerPreviousPosition(FingerGestures.Finger finger)
		{
			return finger.PreviousPosition;
		}

		private static float GetFingerDistanceFromStart(FingerGestures.Finger finger)
		{
			return finger.DistanceFromStart;
		}

		public Vector2 GetAverageStartPosition()
		{
			return this.AverageVector(new FingerGestures.FingerList.FingerPropertyGetterDelegate<Vector2>(FingerGestures.FingerList.GetFingerStartPosition));
		}

		public Vector2 GetAveragePosition()
		{
			return this.AverageVector(new FingerGestures.FingerList.FingerPropertyGetterDelegate<Vector2>(FingerGestures.FingerList.GetFingerPosition));
		}

		public Vector2 GetAveragePreviousPosition()
		{
			return this.AverageVector(new FingerGestures.FingerList.FingerPropertyGetterDelegate<Vector2>(FingerGestures.FingerList.GetFingerPreviousPosition));
		}

		public float GetAverageDistanceFromStart()
		{
			return this.AverageFloat(new FingerGestures.FingerList.FingerPropertyGetterDelegate<float>(FingerGestures.FingerList.GetFingerDistanceFromStart));
		}

		public FingerGestures.Finger GetOldest()
		{
			FingerGestures.Finger finger = null;
			foreach (FingerGestures.Finger current in this.list)
			{
				if (finger == null || current.StarTime < finger.StarTime)
				{
					finger = current;
				}
			}
			return finger;
		}

		public bool MovingInSameDirection(float tolerance)
		{
			if (this.Count < 2)
			{
				return true;
			}
			float num = Mathf.Max(0.1f, 1f - tolerance);
			Vector2 lhs = this[0].Position - this[0].StartPosition;
			lhs.Normalize();
			for (int i = 1; i < this.Count; i++)
			{
				Vector2 rhs = this[i].Position - this[i].StartPosition;
				rhs.Normalize();
				if (Vector2.Dot(lhs, rhs) < num)
				{
					return false;
				}
			}
			return true;
		}

		public bool AllMoving()
		{
			if (this.Count == 0)
			{
				return false;
			}
			foreach (FingerGestures.Finger current in this.list)
			{
				if (!current.IsMoving)
				{
					return false;
				}
			}
			return true;
		}
	}

	[Flags]
	public enum SwipeDirection
	{
		Right = 1,
		Left = 2,
		Up = 4,
		Down = 8,
		UpperLeftDiagonal = 16,
		UpperRightDiagonal = 32,
		LowerRightDiagonal = 64,
		LowerLeftDiagonal = 128,
		None = 0,
		Vertical = 12,
		Horizontal = 3,
		Cross = 15,
		UpperDiagonals = 48,
		LowerDiagonals = 192,
		Diagonals = 240,
		All = 255
	}

	public delegate bool GlobalTouchFilterDelegate(int fingerIndex, Vector2 position);

	public bool makePersistent = true;

	public bool detectUnityRemote = true;

	public FGInputProvider mouseInputProviderPrefab;

	public FGInputProvider touchInputProviderPrefab;

	private FingerClusterManager fingerClusterManager;

	private FGInputProvider inputProvider;

	private static List<GestureRecognizer> gestureRecognizers = new List<GestureRecognizer>();

	private float pixelDistanceScale = 1f;

	public bool adjustPixelScaleForRetinaDisplay = true;

	public float retinaPixelScale = 0.5f;

	private static FingerGestures instance;

	private FingerGestures.Finger[] fingers;

	private FingerGestures.FingerList touches;

	private FingerGestures.GlobalTouchFilterDelegate globalTouchFilterFunc;

	private Transform[] fingerNodes;

	private static readonly FingerGestures.SwipeDirection[] AngleToDirectionMap = new FingerGestures.SwipeDirection[]
	{
		FingerGestures.SwipeDirection.Right,
		FingerGestures.SwipeDirection.UpperRightDiagonal,
		FingerGestures.SwipeDirection.Up,
		FingerGestures.SwipeDirection.UpperLeftDiagonal,
		FingerGestures.SwipeDirection.Left,
		FingerGestures.SwipeDirection.LowerLeftDiagonal,
		FingerGestures.SwipeDirection.Down,
		FingerGestures.SwipeDirection.LowerRightDiagonal
	};

	public static event Gesture.EventHandler OnGestureEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			FingerGestures.OnGestureEvent = (Gesture.EventHandler)Delegate.Combine(FingerGestures.OnGestureEvent, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			FingerGestures.OnGestureEvent = (Gesture.EventHandler)Delegate.Remove(FingerGestures.OnGestureEvent, value);
		}
	}

	public static event FingerEventDetector<FingerEvent>.FingerEventHandler OnFingerEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			FingerGestures.OnFingerEvent = (FingerEventDetector<FingerEvent>.FingerEventHandler)Delegate.Combine(FingerGestures.OnFingerEvent, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			FingerGestures.OnFingerEvent = (FingerEventDetector<FingerEvent>.FingerEventHandler)Delegate.Remove(FingerGestures.OnFingerEvent, value);
		}
	}

	public static FingerClusterManager DefaultClusterManager
	{
		get
		{
			return FingerGestures.Instance.fingerClusterManager;
		}
	}

	public static FingerGestures Instance
	{
		get
		{
			return FingerGestures.instance;
		}
	}

	public FGInputProvider InputProvider
	{
		get
		{
			return this.inputProvider;
		}
	}

	public int MaxFingers
	{
		get
		{
			return this.inputProvider.MaxSimultaneousFingers;
		}
	}

	public static FingerGestures.IFingerList Touches
	{
		get
		{
			return FingerGestures.instance.touches;
		}
	}

	public static List<GestureRecognizer> RegisteredGestureRecognizers
	{
		get
		{
			return FingerGestures.gestureRecognizers;
		}
	}

	public static float PixelDistanceScale
	{
		get
		{
			return FingerGestures.instance.pixelDistanceScale;
		}
		set
		{
			FingerGestures.instance.pixelDistanceScale = value;
		}
	}

	public static FingerGestures.GlobalTouchFilterDelegate GlobalTouchFilter
	{
		get
		{
			return FingerGestures.instance.globalTouchFilterFunc;
		}
		set
		{
			FingerGestures.instance.globalTouchFilterFunc = value;
		}
	}

	internal static void FireEvent(Gesture gesture)
	{
		if (FingerGestures.OnGestureEvent != null)
		{
			FingerGestures.OnGestureEvent(gesture);
		}
	}

	internal static void FireEvent(FingerEvent eventData)
	{
		if (FingerGestures.OnFingerEvent != null)
		{
			FingerGestures.OnFingerEvent(eventData);
		}
	}

	private void Init()
	{
		if (this.adjustPixelScaleForRetinaDisplay && this.IsRetinaDisplay())
		{
			FingerGestures.PixelDistanceScale = this.retinaPixelScale;
		}
		this.InitInputProvider();
		this.fingerClusterManager = base.GetComponent<FingerClusterManager>();
		if (!this.fingerClusterManager)
		{
			this.fingerClusterManager = base.gameObject.AddComponent<FingerClusterManager>();
		}
	}

	private void InitInputProvider()
	{
		FingerGestures.InputProviderEvent inputProviderEvent = new FingerGestures.InputProviderEvent();
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
		case RuntimePlatform.Android:
			inputProviderEvent.inputProviderPrefab = this.touchInputProviderPrefab;
			goto IL_4B;
		}
		inputProviderEvent.inputProviderPrefab = this.mouseInputProviderPrefab;
		IL_4B:
		base.gameObject.SendMessage("OnSelectInputProvider", inputProviderEvent, SendMessageOptions.DontRequireReceiver);
		this.InstallInputProvider(inputProviderEvent.inputProviderPrefab);
	}

	public void InstallInputProvider(FGInputProvider inputProviderPrefab)
	{
		if (!inputProviderPrefab)
		{
			Debug.LogError("Invalid InputProvider (null)");
			return;
		}
		Debug.Log("FingerGestures: using " + inputProviderPrefab.name);
		if (this.inputProvider)
		{
			UnityEngine.Object.Destroy(this.inputProvider.gameObject);
		}
		this.inputProvider = (UnityEngine.Object.Instantiate(inputProviderPrefab) as FGInputProvider);
		this.inputProvider.name = inputProviderPrefab.name;
		this.inputProvider.transform.parent = base.transform;
		this.InitFingers(this.MaxFingers);
	}

	public static FingerGestures.Finger GetFinger(int index)
	{
		return FingerGestures.instance.fingers[index];
	}

	public static void Register(GestureRecognizer recognizer)
	{
		if (FingerGestures.gestureRecognizers.Contains(recognizer))
		{
			return;
		}
		FingerGestures.gestureRecognizers.Add(recognizer);
	}

	public static void Unregister(GestureRecognizer recognizer)
	{
		FingerGestures.gestureRecognizers.Remove(recognizer);
	}

	public static float GetAdjustedPixelDistance(float rawPixelDistance)
	{
		return FingerGestures.PixelDistanceScale * rawPixelDistance;
	}

	private bool IsRetinaDisplay()
	{
		return false;
	}

	private void Awake()
	{
		this.CheckInit();
	}

	private void Start()
	{
		if (this.makePersistent)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
	}

	private void OnEnable()
	{
		this.CheckInit();
	}

	private void CheckInit()
	{
		if (FingerGestures.instance == null)
		{
			FingerGestures.instance = this;
			this.Init();
		}
		else if (FingerGestures.instance != this)
		{
			Debug.LogWarning("There is already an instance of FingerGestures created (" + FingerGestures.instance.name + "). Destroying new one.");
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
	}

	private void Update()
	{
		if (this.inputProvider)
		{
			this.UpdateFingers();
		}
	}

	private void InitFingers(int count)
	{
		this.fingers = new FingerGestures.Finger[count];
		for (int i = 0; i < count; i++)
		{
			this.fingers[i] = new FingerGestures.Finger(i);
		}
		this.touches = new FingerGestures.FingerList();
	}

	private void UpdateFingers()
	{
		this.touches.Clear();
		FingerGestures.Finger[] array = this.fingers;
		for (int i = 0; i < array.Length; i++)
		{
			FingerGestures.Finger finger = array[i];
			Vector2 zero = Vector2.zero;
			bool newDownState = false;
			this.inputProvider.GetInputState(finger.Index, out newDownState, out zero);
			finger.Update(newDownState, zero);
			if (finger.IsDown)
			{
				this.touches.Add(finger);
			}
		}
	}

	protected bool ShouldProcessTouch(int fingerIndex, Vector2 position)
	{
		return this.globalTouchFilterFunc == null || this.globalTouchFilterFunc(fingerIndex, position);
	}

	private Transform CreateNode(string name, Transform parent)
	{
		return new GameObject(name)
		{
			transform = 
			{
				parent = parent
			}
		}.transform;
	}

	private void InitNodes()
	{
		int num = this.fingers.Length;
		if (this.fingerNodes != null)
		{
			Transform[] array = this.fingerNodes;
			for (int i = 0; i < array.Length; i++)
			{
				Transform transform = array[i];
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
		this.fingerNodes = new Transform[num];
		for (int j = 0; j < this.fingerNodes.Length; j++)
		{
			this.fingerNodes[j] = this.CreateNode("Finger" + j, base.transform);
		}
	}

	public static FingerGestures.SwipeDirection GetSwipeDirection(Vector2 dir, float tolerance)
	{
		float num = Mathf.Max(Mathf.Clamp01(tolerance) * 22.5f, 0.0001f);
		float num2 = FingerGestures.NormalizeAngle360(57.29578f * Mathf.Atan2(dir.y, dir.x));
		if (num2 >= 337.5f)
		{
			num2 -= 360f;
		}
		int i = 0;
		while (i < 8)
		{
			float num3 = 45f * (float)i;
			if (num2 <= num3 + 22.5f)
			{
				float num4 = num3 - num;
				float num5 = num3 + num;
				if (num2 >= num4 && num2 <= num5)
				{
					return FingerGestures.AngleToDirectionMap[i];
				}
				break;
			}
			else
			{
				i++;
			}
		}
		return FingerGestures.SwipeDirection.None;
	}

	public static FingerGestures.SwipeDirection GetSwipeDirection(Vector2 dir)
	{
		return FingerGestures.GetSwipeDirection(dir, 1f);
	}

	public static bool UsingUnityRemote()
	{
		return false;
	}

	public static bool AllFingersMoving(params FingerGestures.Finger[] fingers)
	{
		if (fingers.Length == 0)
		{
			return false;
		}
		for (int i = 0; i < fingers.Length; i++)
		{
			FingerGestures.Finger finger = fingers[i];
			if (!finger.IsMoving)
			{
				return false;
			}
		}
		return true;
	}

	public static bool FingersMovedInOppositeDirections(FingerGestures.Finger finger0, FingerGestures.Finger finger1, float minDOT)
	{
		float num = Vector2.Dot(finger0.DeltaPosition.normalized, finger1.DeltaPosition.normalized);
		return num < minDOT;
	}

	public static float SignedAngle(Vector2 from, Vector2 to)
	{
		float y = from.x * to.y - from.y * to.x;
		return Mathf.Atan2(y, Vector2.Dot(from, to));
	}

	public static float NormalizeAngle360(float angleInDegrees)
	{
		angleInDegrees %= 360f;
		if (angleInDegrees < 0f)
		{
			angleInDegrees += 360f;
		}
		return angleInDegrees;
	}
}
