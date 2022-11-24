using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build.Content;
using UnityEngine;

// Color with indices
// singleton
public class BasicColor : MonoBehaviour
{
	public static BasicColor instance = null;
	public static Color[] basicColor = {
		Color.red, Color.green, Color.blue,
		new Color(1.0f, 0.752941f, 0.796078f, 1.0f), Color.black, Color.yellow,
		Color.cyan, Color.magenta, Color.gray,
		new Color(1.0f, 0.647059f, 0.0f, 1.0f), Color.white
	};
	public static int basicColorSize = 10;

	// Start is called before the first frame update
	public void awake()
    {
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			if (instance != this)
				Destroy(this.gameObject);
		}
	}

	
}

