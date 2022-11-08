using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildListScrollView : MonoBehaviour
{
	public GameObject buildContent;
	TwoDiMap twoDiMap, miniMap;

	private void Awake()
	{
		twoDiMap = GameObject.Find("TwoDiMapEditor").GetComponentInChildren<TwoDiMap>();
		miniMap = GameObject.Find("MiniMapViewer").GetComponentInChildren<TwoDiMap>();
	}
	public void WhenBuildCreated(Area area)
	{
		GameObject newObjContent = Instantiate(buildContent);
		Button delBtn = newObjContent.GetComponentInChildren<Button>();

		newObjContent.transform.SetParent(transform);
		delBtn.onClick.AddListener(DelBuild);
		newObjContent.GetComponent<BuildContent>().Area = area;

		newObjContent.SetActive(true);
	}

	public void DelBuild()
	{
		GameObject currObject = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
		BuildContent currArea = currObject.GetComponent<BuildContent>();

		twoDiMap.DelBuildArea(currArea.Area);
		miniMap.DelBuildArea(currArea.Area);
		Destroy(currObject);
	}
}
