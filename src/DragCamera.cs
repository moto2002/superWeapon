using System;
using UnityEngine;

public class DragCamera : MonoBehaviour
{
	public enum DragCameraAround
	{
		Middle,
		Left,
		Right
	}

	public Transform Parent;

	private float speed;

	private Camera camera_Client;

	private Rect rect = default(Rect);

	private bool Drag_Back;

	private bool Drag_End = true;

	private float camera_y;

	private GameObject CameraTexturePoint;

	private UIWidget CTUW;

	public UITexture CameraTexture;

	public DragCamera.DragCameraAround dragCameraAround;

	public bool SetBuildCamera;

	public float Screen_Width;

	public float Screen_Heigth;

	private bool BK;

	public float BK_PosX;

	public float BK_PosY;

	public float BK_Width;

	public float BK_Heigth;

	private void Start()
	{
		this.camera_Client = base.GetComponent<Camera>();
		this.rect = new Rect(0f, 0f, 0f, 0f);
		this.speed = 0.8f;
		this.Drag_Back = false;
		this.Drag_End = false;
		base.transform.eulerAngles = CameraControl.inst.MainCamera.transform.eulerAngles;
		this.camera_Client.fieldOfView = 25f;
		this.camera_Client.depth = 2f;
		if (CameraControl.inst.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.BuildingMovingOrInBuilding)
		{
			this.SetBuildCamera = true;
			base.transform.eulerAngles = new Vector3(34f, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
			this.camera_Client.fieldOfView = 30f;
		}
		this.dragCameraAround = DragCamera.DragCameraAround.Middle;
		this.CameraTexture = UIManager.inst.DragCameraTexture;
		this.CTUW = this.CameraTexture.GetComponent<UIWidget>();
		this.CameraTexturePoint = new GameObject("CameraTexturePoint");
		this.CameraTexturePoint.transform.parent = base.transform;
		this.CameraTexturePoint.transform.localEulerAngles = Vector3.zero;
		this.CameraTexturePoint.transform.localPosition = new Vector3(0f, 0f, 5f);
	}

	public void SetDragState(bool drag_back, bool drag_end)
	{
		this.Drag_Back = drag_back;
		this.Drag_End = drag_end;
	}

	private void Update()
	{
		this.Screen_Width = (float)Screen.width;
		this.Screen_Heigth = (float)Screen.height;
		if (this.SetBuildCamera)
		{
			if (CameraControl.inst.cameraBuildingStateEnum == CameraControl.CameraBuildingStateEnum.Normal)
			{
				this.Drag_End = true;
			}
		}
		else
		{
			base.transform.eulerAngles = new Vector3(40f, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
			this.CameraTexturePoint.transform.localPosition = new Vector3(0f, 0f, 6f);
			if (FightHundler.FightEnd)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		if (!this.Drag_Back && !this.Drag_End)
		{
			this.BK = true;
			if (this.Parent != null)
			{
				base.transform.position = this.Parent.position + new Vector3(10f, 11f, 10f);
			}
			else
			{
				this.Drag_End = true;
			}
			this.camera_y = Mathf.Min(0.3f, this.camera_y + this.speed * Time.deltaTime);
			this.rect = new Rect(this.rect.x, 1f - (float)((int)(this.camera_y * 1000f)) * 0.001f, Mathf.Min(0.3f, (float)((int)((this.rect.width + this.speed * Time.deltaTime) * 1000f)) * 0.001f), (float)((int)(this.camera_y * 1000f)) * 0.001f);
			float num = Input.mousePosition.x / (float)Screen.width;
			if (this.dragCameraAround == DragCamera.DragCameraAround.Middle)
			{
				if (num >= 0.5f)
				{
					this.dragCameraAround = DragCamera.DragCameraAround.Left;
					this.rect = new Rect(0f, this.rect.y, this.rect.width, this.rect.height);
				}
				else
				{
					this.dragCameraAround = DragCamera.DragCameraAround.Right;
					this.rect = new Rect(1f - this.rect.width, this.rect.y, this.rect.width, this.rect.height);
				}
			}
			else if (this.dragCameraAround == DragCamera.DragCameraAround.Left)
			{
				if (num <= 0.7f)
				{
					this.rect = new Rect(1f - this.rect.width, this.rect.y, this.rect.width, this.rect.height);
				}
				else
				{
					this.dragCameraAround = DragCamera.DragCameraAround.Right;
					this.rect = new Rect(0f, this.rect.y, this.rect.width, this.rect.height);
				}
			}
			else if (this.dragCameraAround == DragCamera.DragCameraAround.Right)
			{
				if (num >= 0.3f)
				{
					this.rect = new Rect(0f, this.rect.y, this.rect.width, this.rect.height);
				}
				else
				{
					this.dragCameraAround = DragCamera.DragCameraAround.Left;
					this.rect = new Rect(1f - this.rect.width, this.rect.y, this.rect.width, this.rect.height);
				}
			}
			this.camera_Client.rect = this.rect;
		}
		else if (this.Drag_Back)
		{
			this.BK = false;
			this.camera_y = Mathf.Max(0f, this.camera_y - this.speed * Time.deltaTime);
			this.rect = new Rect(0f, 1f - (float)((int)(this.camera_y * 1000f)) * 0.001f, Mathf.Max(0f, (float)((int)((this.rect.width - this.speed * Time.deltaTime) * 1000f)) * 0.001f), (float)((int)(this.camera_y * 1000f)) * 0.001f);
			this.camera_Client.rect = this.rect;
		}
		else if (this.Drag_End)
		{
			this.BK = false;
			this.camera_y = Mathf.Max(0f, this.camera_y - this.speed * Time.deltaTime);
			this.rect = new Rect(0f, 1f - (float)((int)(this.camera_y * 1000f)) * 0.001f, Mathf.Max(0f, (float)((int)((this.rect.width - this.speed * Time.deltaTime) * 1000f)) * 0.001f), (float)((int)(this.camera_y * 1000f)) * 0.001f);
			this.camera_Client.rect = this.rect;
			if (this.camera_y <= 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		this.CTUW.transform.localScale = Vector3.zero;
		this.BK_PosX = this.rect.x * this.Screen_Width;
		this.BK_PosY = 0f;
		this.BK_Width = this.rect.width * this.Screen_Width;
		this.BK_Heigth = this.rect.height * this.Screen_Heigth;
		this.CameraTexture.transform.eulerAngles = this.CameraTexturePoint.transform.eulerAngles;
		this.CameraTexture.transform.position = this.CameraTexturePoint.transform.position;
	}

	private void OnGUI()
	{
		if (this.BK)
		{
			GUI.DrawTexture(new Rect(this.BK_PosX, this.BK_PosY, this.BK_Width, this.BK_Heigth), this.CTUW.mainTexture);
		}
	}
}
