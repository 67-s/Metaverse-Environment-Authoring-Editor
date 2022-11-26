using System;
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
	
	private GameObject roomObj;
	public TwoDiMap twoDiMap, miniMap;

	CameraMove cameraMove;

	private void Start()
	{
		defaultScale = buttonScale.localScale;
		cameraMove = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>();
		roomObj = GameObject.Find("Mediator").GetComponent<Mediator>().element;
	}
	public void BtnOnClick()
	{
		switch (currType)
		{
			case BtnType.Start:
			case BtnType.MakeRoom:
			case BtnType.AttendRoom:
			case BtnType.Finished:
			case BtnType.Back:
				CanvasGroupOn(nextGroup);
				CanvasGroupOff(currGroup);
				break;
			case BtnType.EditRoomBack:
				roomObj.SetActive(false);
				cameraMove.SetIsCameraActive(false);
				cameraMove.CameraSetting(0, 0);
				Cursor.lockState = CursorLockMode.None;
				CanvasGroupOn(nextGroup);
				CanvasGroupOff(currGroup);
				twoDiMap.DestroyMap();
				miniMap.DestroyMap();
				break;
			case BtnType.EditRoom:
				roomObj.GetComponent<Installer>().Resize(RoomData.rowCnt, RoomData.colCnt);
				roomObj.SetActive(true);
				//cameraMove.SetIsCameraActive(true);
				//Cursor.lockState = CursorLockMode.Confined;
				CanvasGroupOn(nextGroup);
				CanvasGroupOff(currGroup);
				twoDiMap.SetRowCnt(RoomData.rowCnt);
				twoDiMap.SetColCnt(RoomData.colCnt);
				twoDiMap.Make2dMap();
				miniMap.SetRowCnt(RoomData.rowCnt);
				miniMap.SetColCnt(RoomData.colCnt);
				miniMap.Make2dMap();
				cameraMove.CameraSetting(RoomData.rowCnt, RoomData.colCnt);
				break;
			case BtnType.MiniMapTog:
				CanvasGroupOn(nextGroup);
				CanvasGroupOff(currGroup);
				break;
			case BtnType.Create:
				twoDiMap.DebugBuildArea();
				Installer installer = roomObj.GetComponent<Installer>();
				foreach(var obj in twoDiMap.GetBuildArea())
				{
					int x = Math.Min(obj.Value.FstRow, obj.Value.SndRow);
					int z = Math.Min(obj.Value.FstCol, obj.Value.SndCol);
                    int xWidth = Math.Abs(obj.Value.FstRow - obj.Value.SndRow) + 1;
                    int zWidth = Math.Abs(obj.Value.FstCol - obj.Value.SndCol) + 1;
					installer.Build(x, z, xWidth, zWidth, 1, EBuildDirection.North);
                }
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
	public static void CanvasGroupOn(CanvasGroup cg)
	{
		cg.alpha = 1;
		cg.interactable = true;
		cg.blocksRaycasts = true;
	}
	public static void CanvasGroupOff(CanvasGroup cg)
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
