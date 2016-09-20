using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TerminalPwd : MonoBehaviour {

    private InputField inputPwd;

	void Start () {
        inputPwd = GetComponent<InputField>();
        inputPwd.onEndEdit.AddListener(delegate { editEnd(); });
        inputPwd.Select();
        inputPwd.ActivateInputField();
	}

    void editEnd() {

    }
}
