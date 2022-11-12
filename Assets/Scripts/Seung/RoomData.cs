using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomData : MonoBehaviour
{
	public IFType currType;
	public TMP_InputField inputField;
	static string roomName;
	static string intro;
	static string numberOfPeople;
	static string password;
	public static int rowCnt = 3;
	public static int colCnt = 3;
	public void OnEndEdit()
	{
		switch(currType)
		{
			case IFType.RmName:
				roomName = inputField.text.ToString();
				break;
			case IFType.RmIntro:
				intro = inputField.text.ToString();
				break;
			case IFType.RmLmtNum:
				numberOfPeople = inputField.text.ToString();
				break;
			case IFType.RmPwd:
				password = inputField.text.ToString();
				break;
			case IFType.RmRowCnt:
				rowCnt = int.Parse(inputField.text);
				break;
			case IFType.RmColCnt:
				colCnt = int.Parse(inputField.text);
				break;
		}
	}

	public string getRoomName()
    {
		return roomName;
    }

	public string getPassword()
    {
		return password;
    }

	public int getNumberOfPeople()
    {
		return int.Parse(numberOfPeople);
    }
}
