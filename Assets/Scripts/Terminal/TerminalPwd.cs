using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TerminalPwd : MonoBehaviour {

    public string Pwd { set; get; }   
    public int EnterNodeId { set; get; } 

    public TerminalMachine Machine;
    public GameObject Input;

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
        RectTransform rect = Input.GetComponent<RectTransform>();
        Input.SetActive(true);
        //move input field
        Input.transform.localPosition = new Vector3(0, 18 - Input.transform.parent.GetComponent<RectTransform>().rect.height, 0);
        rect.offsetMax = new Vector2(0, rect.offsetMax.y);
        rect.offsetMin = new Vector2(0, rect.offsetMin.y);
        //select input field
        Input.GetComponent<InputField>().Select();
        Input.GetComponent<InputField>().ActivateInputField();

        
        
        GetComponent<InputField>().text = "";
        gameObject.SetActive(false);
    }

}
