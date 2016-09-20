using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TerminalPwd : MonoBehaviour {

    public string Pwd { set; get; }
    public string ResultText { set; get; }
    public string IncorrectText { set; get; }

    public TerminalMachine Machine;

    private InputField inputPwd;
    
	void Start () {
        inputPwd = GetComponent<InputField>();
        inputPwd.onEndEdit.AddListener(delegate { editEnd(); });
        inputPwd.Select();
        inputPwd.ActivateInputField();
	}

    void editEnd() {
        Machine.PrintResultText("Password : " + inputPwd.text);
        Machine.PrintResultText(inputPwd.text == Pwd ? ResultText : IncorrectText);
        if (inputPwd.text == Pwd) {
            //action of node
        }
        else {

        }

    }

}
