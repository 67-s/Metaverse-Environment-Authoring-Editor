using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	const float kTurnSpeedX = 2.0f; // ���콺 ȸ�� �ӵ�
	const float kTurnSpeedY = 5.0f; // ���콺 ȸ�� �ӵ�
	const float kMoveSpeed = 2.0f; // �̵� �ӵ�
	public static bool IsCamearActive = false;
	void Update()
	{
		if (IsCamearActive)
		{
			MouseRotation();
			KeyboardMove();
		}
		if (Input.GetButtonDown("CameraActive"))
			IsCamearActive = !IsCamearActive;
	}

	// ī�޶� ȸ��
	void MouseRotation()
	{
		// ī�޶� �¿� ������
		// ȭ�鿡�� x������ ���콺�� �����̸� ī�޶� ��ǥ�迡���� ī�޶� y�� �������� ȸ���� ���̴�.
		float yRotateSize = Input.GetAxis("Mouse X") * kTurnSpeedY;

		// ī�޶� ���Ʒ� ������
		// ȭ�鿡�� y������ ���콺�� �����̸� ī�޶� ��ǥ�迡���� x�� �������� ȸ���� ���̴�.
		float xRotateSize = -Input.GetAxis("Mouse Y") * kTurnSpeedX;

		// ī�޶� ȸ������ ī�޶� �ݿ�(X, Y�ุ ȸ��)
		transform.eulerAngles += new Vector3(xRotateSize, yRotateSize, 0);
	}

	// Ű������ ������ ���� �̵�
	void KeyboardMove()
	{
		transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Up"), Input.GetAxis("Vertical")) * kMoveSpeed * Time.deltaTime);
	}
}
