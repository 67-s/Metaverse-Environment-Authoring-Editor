using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomData : MonoBehaviour
{
	public IFType currType;
	public TMP_InputField inputField;
	static string Name;
	static string Intro;
	static string NumberOfPeople;
	static string Password;
	public void OnEndEdit()
	{
		switch(currType)
		{
			case IFType.RmName:
				Name = inputField.text.ToString();
				break;
			case IFType.RmIntro:
				Intro = inputField.text.ToString();
				break;
			case IFType.RmLmtNum:
				NumberOfPeople = inputField.text.ToString();
				break;
			case IFType.RmPwd:
				Password = inputField.text.ToString();
				break;
		}
		Debug.Log(Name);
		Debug.Log(Intro);
		Debug.Log(NumberOfPeople);
		Debug.Log(Password);
	}
}
