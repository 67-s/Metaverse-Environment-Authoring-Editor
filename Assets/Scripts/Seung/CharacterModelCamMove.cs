using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModelCamMove : MonoBehaviour
{
	Camera cam;
	int idx = 0;

	private void Awake()
	{
		cam = GameObject.Find("CharacterCamera").GetComponent<Camera>();
	}
	public void NextBtnOnClick()
	{
		if (++idx == 10)
		{
			idx = 0;
			cam.transform.position = new Vector3(2000, 2000, 0);
		}
		else
			cam.transform.position += new Vector3(10, 0, 0);
	}

	public void PrevBtnOnClick()
	{
		if (--idx < 0)
		{
			idx = 9;
			cam.transform.position = new Vector3(2090, 2000, 0);
		}
		else
			cam.transform.position += new Vector3(-10, 0, 0);
	}
}
