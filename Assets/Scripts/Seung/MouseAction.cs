using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseAction : MonoBehaviour
{
	public TwoDiMap twoDiMap, miniMap;

	GameObject fstGameObject = null;
	GameObject sndGameObject = null;
	GraphicRaycaster gRay;
	private void Awake()
	{
		gRay = GetComponent<GraphicRaycaster>();
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
					twoDiMap.SetPrefapColorWithGameObj(fstGameObject, sndGameObject);
					miniMap.SetPrefapColorWithPos();
				}
			}
			fstGameObject = null;
			sndGameObject = null;
		}

	}
}
