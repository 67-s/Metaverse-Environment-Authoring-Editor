using System;
using TMPro;
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
	public void WhenBuildCreated(Area area, string buildName, Color color)
	{
		GameObject newObjContent = Instantiate(buildContent);
		Button delBtn = newObjContent.GetComponentInChildren<Button>();

		newObjContent.transform.SetParent(transform);
		delBtn.onClick.AddListener(DelBuild);
		newObjContent.GetComponent<BuildContent>().Area = area;
		newObjContent.GetComponentInChildren<TMP_Text>().text = buildName;
		newObjContent.transform.Find("Color").GetComponent<Image>().color = color;

		newObjContent.SetActive(true);
	}

	public void DelBuild()
	{
		GameObject currObject = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
		Area currArea = currObject.GetComponent<BuildContent>().Area;

		{
			int x = Math.Min(currArea.FstRow, currArea.SndRow);
			int z = Math.Min(currArea.FstCol, currArea.SndCol);
			int xWidth = Math.Abs(currArea.FstRow - currArea.SndRow) + 1;
			int zWidth = Math.Abs(currArea.FstCol - currArea.SndCol) + 1;
			GameObject.Find("Mediator")
				.GetComponent<Mediator>().element
				.GetComponent<Installer>()
				.Remove(x, z, xWidth, zWidth);
		}
		twoDiMap.DelBuildArea(currArea, true);
		miniMap.DelBuildArea(currArea, false);
		Destroy(currObject);
	}
}
