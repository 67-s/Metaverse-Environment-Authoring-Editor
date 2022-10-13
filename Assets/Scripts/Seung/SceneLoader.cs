using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
	public Slider progressbar;
	public TMP_Text loadtext;
	public static string loadScene;
	public static int loadType;
	private void Start()
	{
		// method를 파라메타로 넣어서 코루틴을 반복호출하지 않고 스스로 작동하게 한다.
		StartCoroutine(LoadScene());
	}
	public static void LoadSceneHandle(string _name)
	{
		loadScene = _name;
		SceneManager.LoadScene("LoadingScene");
	}
	IEnumerator LoadScene()
	{
		// 1프레임을 대기하고 함수를 이어서 실행한다.
		yield return null;

		AsyncOperation operation = SceneManager.LoadSceneAsync(loadScene);
		operation.allowSceneActivation = false;

		while(!operation.isDone)
		{
			yield return null;

			if (progressbar.value < 0.9f)
			{
				progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
			}
			else if(operation.progress >= 0.9f)
			{
				progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
			}

			if (progressbar.value >= 1f)
			{
				loadtext.text = "Press spacebar to go to metaverse!!!";
			}
			if (Input.GetKeyDown(KeyCode.Space) && progressbar.value >= 1f && operation.progress >= 0.9f)
			{
				operation.allowSceneActivation = true;
			}
		}
	}
}
