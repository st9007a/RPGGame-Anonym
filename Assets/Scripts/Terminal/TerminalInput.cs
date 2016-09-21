using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class TerminalInput : MonoBehaviour {

    public TerminalMachine Machine;

    private InputField inputField;

    private int keyRecordPointer = 0;
    private List<string> keyRecord = new List<string>();


	void Start(){
        inputField = GetComponent<InputField>();

        inputField.onEndEdit.AddListener(delegate { endEdit(); });
        inputField.Select();
        inputField.ActivateInputField();

    }

    void Update() {
        if (Input.GetKeyDown("down") && keyRecordPointer < keyRecord.Count - 1) {
            inputField.text = keyRecord[++keyRecordPointer];
            inputField.MoveTextEnd(false);
        }
        if (Input.GetKeyDown("up") && keyRecordPointer > 0) {
            inputField.text = keyRecord[--keyRecordPointer];
            inputField.MoveTextEnd(false);
        }
        if (Input.GetKeyDown("tab")) {
            char[] text = inputField.text.ToCharArray();
            string select = "";

            for (int i = text.Length - 1; i >= 0; i--) {
                if (text[i] != ' ') select += text[i];
                else break;
            }

            foreach (Node child in Machine.t.Now.Each()) {
                char[] childName = child.Name.ToCharArray();
                char[] parseSelect = select.ToCharArray();
                bool isMatch = true;

                for (int i = parseSelect.Length - 1; i >= 0; i--) {
                    if (parseSelect.Length > childName.Length || parseSelect[i] != childName[parseSelect.Length - 1 - i]) {
                        isMatch = false;
                        break;
                    }
                }

                if (isMatch) {
                    string[] parseText = inputField.text.Split(' ');
                    parseText[parseText.Length - 1] = child.Name;
                    string newText = parseText[0];
                    for (int i = 1; i < parseText.Length; i++) newText += " " + parseText[i];

                    inputField.text = newText;
                    inputField.MoveTextEnd(false);
                    break;
                }
            }
                


        }
        
    }

    void endEdit() {
        if(inputField.text != "") { 

            //generate text 
            string text = inputField.text;

            inputField.text = "";
            Machine.PrintResultText("uno : " + text);

            //analyze input
            analyzeInput(text);

            //record input
            recordInput(text);
        }

        inputField.Select();
        inputField.ActivateInputField();
        
    }

    void analyzeInput(string input) {
        string[] arg = input.Split(' ');
        List<string> argSet = new List<string>();
        //string result = "";
        Response result;

        for (int i = 0; i < arg.Length; i++)        
            if (arg[i] != " " && arg[i] != "") argSet.Add(arg[i]);

        if (argSet.Count == 1) result = Machine.t.Instruct(argSet[0]);
        else if (argSet.Count == 2) result = Machine.t.Instruct(argSet[0], argSet[1]);
        else if (argSet.Count == 3) result = Machine.t.Instruct(argSet[0], argSet[1], argSet[2]);
        else result = new Response("錯誤的輸入格式");

        //generate result text
        switch (result.Type) {
            case Response.type.TEXT:
                Machine.PrintResultText(result.Text);
                RectTransform gTransform = GetComponent<RectTransform>();
                
                //move input field
                transform.localPosition = new Vector3(0, 18 - transform.parent.GetComponent<RectTransform>().rect.height, 0);
                gTransform.offsetMax = new Vector2(0, gTransform.offsetMax.y);
                gTransform.offsetMin = new Vector2(0, gTransform.offsetMin.y);
                inputField.text = "";
                break;
            case Response.type.PWD:
                Machine.PrintPwdInputField(result.Pwd);
                gameObject.SetActive(false);
                break;
            default:
                break;
        }

    }

    void recordInput(string input) {

        if (keyRecord.Count < 50) {
            keyRecord.Add(input);
            keyRecordPointer = keyRecord.Count;
        }
        else {
            keyRecord.RemoveAt(0);
            keyRecord.Add(input);
            keyRecordPointer = 50;
        }
    }
}
