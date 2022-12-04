using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModelCamMove : MonoBehaviour
{
	static Camera cam;
	static int characterIdx = 0;

	private void Awake()
	{
		cam = GameObject.Find("CharacterCamera").GetComponent<Camera>();
	}
	public void NextBtnOnClick()
	{
		if (++characterIdx == 10)
		{
			characterIdx = 0;
			cam.transform.position = new Vector3(2000, 2000, 0);
		}
		else
			cam.transform.position += new Vector3(10, 0, 0);
	}

	public void PrevBtnOnClick()
	{
		if (--characterIdx < 0)
		{
			characterIdx = 9;
			cam.transform.position = new Vector3(2090, 2000, 0);
		}
		else
			cam.transform.position += new Vector3(-10, 0, 0);
	}

	static public int GetCharacterIdx()
	{
		return characterIdx;
	}
}
