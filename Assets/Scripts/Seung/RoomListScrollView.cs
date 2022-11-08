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

	public void WhenRoomCreated()
	{
		GameObject newRoomContent = Instantiate(roomContent);
		Button check = newRoomContent.GetComponentInChildren<Button>();

		newRoomContent.transform.SetParent(transform);
		check.onClick.AddListener(OnChkClick);

		newRoomContent.SetActive(true);
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
