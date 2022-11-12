
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildContent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Area Area { get; set; }

	static TwoDiMap twoDiMap;
	void Awake()
	{
		twoDiMap = twoDiMap = GameObject.Find("TwoDiMapEditor").GetComponentInChildren<TwoDiMap>();
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		twoDiMap.AreaHighlight(Area);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		twoDiMap.AreaHighlightReset(Area);
	}
}
