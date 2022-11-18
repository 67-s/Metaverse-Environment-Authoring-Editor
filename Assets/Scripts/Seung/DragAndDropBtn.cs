using Photon.Bolt;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DragAndDropBtnType
{
	None,
	WallSplitDoor,
}

public class DragAndDropBtn : MonoBehaviour
{
	public DragAndDropBtnType currType;
	public GameObject[] prefabs;
	// Start is called before the first frame update
	void Start()
    {
        
    }

	public void OnBtnClick()
	{
		CreatePrefab((int)currType);
	}

	public void CreatePrefab(int idx)
	{
		GameObject newGameObject = Instantiate(prefabs[idx], Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z)), new Quaternion(0, 0, 0, 1));

		newGameObject.transform.localScale *= 5;
	}
}
