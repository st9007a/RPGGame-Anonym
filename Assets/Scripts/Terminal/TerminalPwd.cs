using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TerminalPwd : MonoBehaviour {

    public string Pwd { set; get; }   
    public int EnterNodeId { set; get; } 

    public TerminalMachine Machine;

    private const string CorrectPwdText = "密碼正確 , ";
    private const string IncorrectPwdText = "輸入密碼錯誤";

    private InputField inputPwd;
    
	void Start () {
        inputPwd = GetComponent<InputField>();
        inputPwd.onEndEdit.AddListener(delegate { editEnd(); });
        inputPwd.Select();
        inputPwd.ActivateInputField();
	}

    void editEnd() {

        if (inputPwd.text == Pwd) Machine.t.Enter();

        string pwdStr = "";
        for (int i = 0; i < inputPwd.text.Length; i++) pwdStr += "*";

        Machine.PrintResultText("Password : " + pwdStr);
        Machine.PrintResultText(inputPwd.text == Pwd ? EnterNodeId == 0 ? "取得最高權限" : CorrectPwdText + "進入" + Machine.t.Now.Name : IncorrectPwdText);
        
        //move input field
        Machine.FixInputFieldPosition();
     
        GetComponent<InputField>().text = "";
        gameObject.SetActive(false);
    }

}
