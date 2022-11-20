using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	const float kTurnSpeedX = 2.0f; // 마우스 회전 속도
	const float kTurnSpeedY = 5.0f; // 마우스 회전 속도
	const float kMoveSpeed = 2.0f; // 이동 속도
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

	// 카메라 회전
	void MouseRotation()
	{
		// 카메라 좌우 움직임
		// 화면에서 x축으로 마우스를 움직이면 카메라 좌표계에서는 카메라를 y축 기준으로 회전한 것이다.
		float yRotateSize = Input.GetAxis("Mouse X") * kTurnSpeedY + transform.eulerAngles.y;

		// 카메라 위아래 움직임
		// 화면에서 y축으로 마우스를 움직이면 카메라 좌표계에서는 x축 기준으로 회전한 것이다.
		float xRotateSize = Mathf.Clamp(-Input.GetAxis("Mouse Y") * kTurnSpeedX + transform.eulerAngles.x,-kRotateLimit,kRotateLimit);

		// 카메라 회전량을 카메라에 반영(X, Y축만 회전)
		transform.eulerAngles = new Vector3(xRotateSize, yRotateSize, 0);
	}

	// 키보드의 눌림에 따라 이동
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
