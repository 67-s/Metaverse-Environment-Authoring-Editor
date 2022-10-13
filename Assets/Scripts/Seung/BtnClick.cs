using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public BtnType currType;
	public Transform buttonScale;
	Vector3 defaultScale;
	public CanvasGroup currGroup;
	public CanvasGroup nextGroup;
	
	public GameObject roomObj;

	CameraMove cameraMove;

	private void Start()
	{
		defaultScale = buttonScale.localScale;
		cameraMove = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>();
	}
	public void BtnOnClick()
	{
		switch (currType)
		{
			case BtnType.Start:
			case BtnType.MakeRoom:
			case BtnType.AttendRoom:
			case BtnType.Finished:
				CanvasGroupOn(nextGroup);
				CanvasGroupOff(currGroup);
				break;
			case BtnType.Back:
				roomObj.SetActive(false);
				cameraMove.SetIsCameraActive(false);
				cameraMove.CameraReset();
				Cursor.lockState = CursorLockMode.None;
				CanvasGroupOn(nextGroup);
				CanvasGroupOff(currGroup);
				break;
			case BtnType.EditRoom:
				roomObj.SetActive(true);
				cameraMove.SetIsCameraActive(true);
				Cursor.lockState = CursorLockMode.Confined;
				CanvasGroupOn(nextGroup);
				CanvasGroupOff(currGroup);
				break;
			case BtnType.GotoMetaverse:
				SceneLoader.LoadSceneHandle("Scene2");
				break;
			case BtnType.Quit:
#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
				break;
		}
	}
	public void CanvasGroupOn(CanvasGroup cg)
	{
		cg.alpha = 1;
		cg.interactable = true;
		cg.blocksRaycasts = true;
	}
	public void CanvasGroupOff(CanvasGroup cg)
	{
		cg.alpha = 0;
		cg.interactable = false;
		cg.blocksRaycasts = false;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		buttonScale.localScale = defaultScale * 1.2f;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		buttonScale.localScale = defaultScale;
	}
}
