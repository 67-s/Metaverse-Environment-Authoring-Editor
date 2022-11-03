using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Bolt;


public class Chatting : GlobalEventListener
{
    public List<string> chatList = new List<string>();
    public Button sendBtn;
    public Text chatLog;
    public Text chattingList;
    public InputField input;
    ScrollRect scroll_rect = null;
    string chatters;
    // Start is called before the first frame update
    void Start()
    {
        /*
        PhotonNetwork.IsMessageQueueRunning = true;
        scroll_rect = GameObject.FindObjectOfType<ScrollRect>();
        */
        //input = GameObject.FindObjectOfType<InputField>();
        scroll_rect = GameObject.FindObjectOfType<ScrollRect>();
        input = gameObject.GetComponentInChildren<InputField>();
        chatLog = gameObject.GetComponentInChildren<Text>();
        //input.text = "Hello!!!";
        chatLog.text = "hi";
        input.ActivateInputField();
    }
    public void SendButtonOnClicked()
    {
        /*
        if (input.text.Equals("")) { Debug.Log("Empty"); return; }
        string msg = string.Format("[{0}] {1}", PhotonNetwork.LocalPlayer.NickName, input.text);
        photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, msg);
        ReceiveMsg(msg);
        input.ActivateInputField(); // 반대는 input.select(); (반대로 토글)
        input.text = "";
        */
    }
    void Update()
    {
        //
        //chatterUpdate();
        //if (Input.GetKeyDown(KeyCode.Return) && !input.isFocused) SendButtonOnClicked();
        //
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var chat = ChatEvent.Create();
            chat.Message = input.text;
            chat.Send();
            input.Select();
            input.text = "";
			input.ActivateInputField();
		}
    }
    void chatterUpdate()
    {
        chatters = "Player List\n";
        /*
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            chatters += p.NickName + "\n";
        }
        */
        chattingList.text = chatters;
    }

    public override void OnEvent(ChatEvent evnt)
    {
        base.OnEvent(evnt);
        chatLog.text += "\n" + evnt.Message;
        //scroll_rect.verticalNormalizedPosition = 0.0f;
    }
}
