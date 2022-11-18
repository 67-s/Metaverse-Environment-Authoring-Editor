using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoomListScrollView : MonoBehaviour
{
	public GameObject roomContent;
	private static int roomCount = 0;
	private Dictionary<string, GameObject> roomLists = new Dictionary<string, GameObject>();

	public void WhenRoomCreated(string roomName, string roomIntro)
	{
		GameObject newRoomContent = Instantiate(roomContent);
		Button check = newRoomContent.GetComponentInChildren<Button>();
		TMP_Text text = newRoomContent.GetComponentInChildren<TMP_Text>();

		newRoomContent.transform.SetParent(transform);
		check.onClick.AddListener(OnChkClick);
		text.text = roomName + " : " + roomIntro;

		roomLists.Add(roomName, newRoomContent);
		newRoomContent.SetActive(true);
	}

	public void ClearList()
	{
		foreach(var roomList in roomLists)
		{
			Destroy(roomList.Value);
		}
		roomLists.Clear();
	}

	private void OnChkClick()
	{
		var currObject = EventSystem.current.currentSelectedGameObject;
		TMP_Text btnText = currObject.GetComponentInChildren<TMP_Text>();
		if (btnText.text.Equals("O"))
		{
			roomCount--;
			btnText.text = "";
		}
		else if (roomCount == 0)
		{
			btnText.text = "O";
			roomCount++;
		}
	}
}
