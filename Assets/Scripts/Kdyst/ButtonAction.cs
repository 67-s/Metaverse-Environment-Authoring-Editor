using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Kdyst;

public class ButtonAction : MonoBehaviour
{
    [SerializeField]
    private GameObject InputNum;

    [SerializeField]
    private string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickButton()
    {
        var component = InputNum.GetComponent<TMPro.TMP_InputField>();
        try
        {
            int x = int.Parse(component.text);
            KdystContainer.Number = x;
            SceneManager.LoadScene(SceneName);
        }
        catch (System.FormatException)
        {
            Debug.Log("current text is = \"" + component.text + "\" . Only numbers can be written.");
        }
    }
}
