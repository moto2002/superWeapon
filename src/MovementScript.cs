using System;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
	public float speed = 10f;

	private CharacterController cc;

	private void Awake()
	{
		this.cc = base.GetComponent<CharacterController>();
	}

	private void FixedUpdate()
	{
		Vector3 zero = Vector3.zero;
		zero.x = Input.GetAxis("Horizontal") * this.speed;
		zero.z = Input.GetAxis("Vertical") * this.speed;
		this.cc.SimpleMove(zero);
	}
}
