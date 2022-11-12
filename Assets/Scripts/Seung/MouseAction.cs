using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseAction : MonoBehaviour
{
	TwoDiMap twoDiMap, miniMap;

	GameObject fstGameObject = null;
	GameObject sndGameObject = null;
	GraphicRaycaster gRay;
	BuildListScrollView buildListScrollView;

	TMP_Dropdown buildDropDown;
	TMP_Dropdown colorDropDown;

	private void Awake()
	{
		gRay = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
		buildListScrollView = FindObjectOfType<BuildListScrollView>();
		buildDropDown = GameObject.Find("BuildDropDown").GetComponent<TMP_Dropdown>();
		colorDropDown = GameObject.Find("ColorDropDown").GetComponent<TMP_Dropdown>();

		twoDiMap = GameObject.Find("TwoDiMapEditor").GetComponentInChildren<TwoDiMap>();
		miniMap = GameObject.Find("MiniMapViewer").GetComponentInChildren<TwoDiMap>();
	}
	void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			var ped = new PointerEventData(null);
			ped.position = Input.mousePosition;
			List<RaycastResult> results = new List<RaycastResult>();
			gRay.Raycast(ped, results);
			if (results.Count > 0)
				fstGameObject = results[0].gameObject.transform.parent.gameObject;
			if (fstGameObject != null && !fstGameObject.CompareTag("MiniMapTile"))
				fstGameObject = null;
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (fstGameObject != null)
			{
				var ped = new PointerEventData(null);
				ped.position = Input.mousePosition;
				List<RaycastResult> results = new List<RaycastResult>();
				gRay.Raycast(ped, results);
				if (results.Count > 0)
					sndGameObject = results[0].gameObject.transform.parent.gameObject;
				if (sndGameObject != null)
				{
					Area currArea = twoDiMap.ChangeObjToArea(fstGameObject, sndGameObject);
					if (currArea != null && twoDiMap.CheckArea(currArea))
					{
						twoDiMap.SetPrefapColor(currArea, BasicColor.basicColor[colorDropDown.value]);
						twoDiMap.AddInBuildArea(currArea, buildDropDown.value);
						miniMap.SetPrefapColor(currArea, BasicColor.basicColor[colorDropDown.value]);
						buildListScrollView.WhenBuildCreated(currArea, buildDropDown.captionText.text, BasicColor.basicColor[colorDropDown.value]);
					}
					else
						Debug.Log("Error");
				}
			}
			fstGameObject = null;
			sndGameObject = null;
		}

	}
}
