﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCam : MonoBehaviour{

	
	[SerializeField] float mouseLookSensitivity = 2;
	[Range( 45f , 90f )] [SerializeField] float m_pitchMaxAngle = 80f;

	[SerializeField] float speed = 4;
	

	Vector3 currentVelocity;

	float m_pitch;

	void Start () 
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		//anglePitch = 0;

	}


	void Update()
	{
		if(Cursor.lockState == CursorLockMode.Locked) {
			float deltaPitch = -Input.GetAxis("Mouse Y") * mouseLookSensitivity;
			float deltaYaw = Input.GetAxis("Mouse X") * mouseLookSensitivity;

			if (m_pitch + deltaPitch > m_pitchMaxAngle) {
				deltaPitch = m_pitchMaxAngle - m_pitch;
			} else if (m_pitch + deltaPitch < -m_pitchMaxAngle) {
				deltaPitch = -m_pitchMaxAngle - m_pitch;
			}

			m_pitch += deltaPitch;


			transform.Rotate(Vector3.right, deltaPitch, Space.Self);
			transform.Rotate(Vector3.up, deltaYaw, Space.World);



			float rightMove = Input.GetAxisRaw("Horizontal");
			float forwardMove = Input.GetAxisRaw("Vertical");
			float upMove = 0;

			if (Input.GetKey(KeyCode.E))
				upMove = 1;
			else if (Input.GetKey(KeyCode.Q))
				upMove = -1;

			Vector3 targetVelocity = (new Vector3(rightMove, upMove, forwardMove)).normalized * speed;

			if (Input.GetKey(KeyCode.LeftControl))
				targetVelocity *= 0.5f;
			else if (Input.GetKey(KeyCode.LeftShift))
				targetVelocity *= 2f;


			transform.Translate(targetVelocity * Time.deltaTime);
		}
	}
}
