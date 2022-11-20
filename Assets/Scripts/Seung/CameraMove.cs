using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	const float kTurnSpeedX = 2.0f; // ���콺 ȸ�� �ӵ�
	const float kTurnSpeedY = 5.0f; // ���콺 ȸ�� �ӵ�
	const float kMoveSpeed = 2.0f; // �̵� �ӵ�
	const float kRotateLimit = 75.0f;
	static bool IsCameraActive = false;
	float tan20;
	float tan80;

	private void Awake()
	{
		tan20 = Mathf.Tan(20 * Mathf.PI / 180);
		tan80 = Mathf.Tan(80 * Mathf.PI / 180);
	}
	void Update()
	{
		if (IsCameraActive)
		{
			MouseRotation();
			KeyboardMove();
		}
		if (Input.GetButtonDown("CameraActive"))
		{
			IsCameraActive = !IsCameraActive;
			if (Cursor.lockState == CursorLockMode.Confined)
				Cursor.lockState = CursorLockMode.None;
			else
				Cursor.lockState = CursorLockMode.Confined;
		}
	}

	public void SetIsCameraActive(bool val)
	{
		IsCameraActive = val;
	}

	// ī�޶� ȸ��
	void MouseRotation()
	{
		// ī�޶� �¿� ������
		// ȭ�鿡�� x������ ���콺�� �����̸� ī�޶� ��ǥ�迡���� ī�޶� y�� �������� ȸ���� ���̴�.
		float yRotateSize = Input.GetAxis("Mouse X") * kTurnSpeedY + transform.eulerAngles.y;

		// ī�޶� ���Ʒ� ������
		// ȭ�鿡�� y������ ���콺�� �����̸� ī�޶� ��ǥ�迡���� x�� �������� ȸ���� ���̴�.
		float xRotateSize = Mathf.Clamp(-Input.GetAxis("Mouse Y") * kTurnSpeedX + transform.eulerAngles.x,-kRotateLimit,kRotateLimit);

		// ī�޶� ȸ������ ī�޶� �ݿ�(X, Y�ุ ȸ��)
		transform.eulerAngles = new Vector3(xRotateSize, yRotateSize, 0);
	}

	// Ű������ ������ ���� �̵�
	void KeyboardMove()
	{
		transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Up"), Input.GetAxis("Vertical")) * kMoveSpeed * Time.deltaTime);
	}
	public void CameraSetting(int row, int col)
	{
		float zp = tan20 * row * 5 / (tan80 - tan20);

		transform.position = new Vector3((float)col*5/2, zp * tan80, -zp *tan80 / tan20);
		transform.eulerAngles = new Vector3(50, 0, 0);
	}
}
