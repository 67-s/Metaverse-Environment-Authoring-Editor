using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public BtnType currentType;
	public Transform buttonScale;
	Vector3 defaultScale;
	public CanvasGroup currGroup;
	public CanvasGroup nextGroup;

	private void Start()
	{
		defaultScale = buttonScale.localScale;
	}
	public void BtnOnClick()
	{
		switch (currentType)
		{
			case BtnType.Start:
				CanvasGroupOn(nextGroup);
				CanvasGroupOff(currGroup);
				break;
			case BtnType.Back:
				CanvasGroupOn(nextGroup);
				CanvasGroupOff(currGroup);
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
