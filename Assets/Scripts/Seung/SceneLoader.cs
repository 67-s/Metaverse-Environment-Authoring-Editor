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
		// method�� �Ķ��Ÿ�� �־ �ڷ�ƾ�� �ݺ�ȣ������ �ʰ� ������ �۵��ϰ� �Ѵ�.
		StartCoroutine(LoadScene());
	}
	public static void LoadSceneHandle(string _name)
	{
		loadScene = _name;
		SceneManager.LoadScene("LoadingScene");
	}
	IEnumerator LoadScene()
	{
		// 1�������� ����ϰ� �Լ��� �̾ �����Ѵ�.
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
