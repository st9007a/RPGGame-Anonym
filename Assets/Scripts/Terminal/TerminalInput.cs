using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class TerminalInput : MonoBehaviour {

    public GameObject Machine;

    private InputField inputField;
    private Terminal t;

    private int keyRecordPointer = 0;
    private List<string> keyRecord = new List<string>();

    void Awake() {
        Node n = new Node("root");
        n.AddChild(new Node("home"));
        n.AddChild(new Node("config"));
        n.Children[0].AddChild(new Node("usr1"));

        t = new Terminal(n, "home");

        t.GetNode("home").AddComment("管理使用者");
        t.GetNode("config").AddComment("管理主機設定");

    }

	void Start(){
        inputField = GetComponent<InputField>();

        inputField.onEndEdit.AddListener(delegate { endEdit(); });
        inputField.Select();
        inputField.ActivateInputField();
    }

    void Update() {
        if (Input.GetKeyDown("down") && keyRecordPointer < keyRecord.Count - 1) {
            inputField.text = keyRecord[++keyRecordPointer];
        }
        if (Input.GetKeyDown("up") && keyRecordPointer > 0) {
            inputField.text = keyRecord[--keyRecordPointer];
        }
    }

    void endEdit() {
        if(inputField.text != "") { 

            //generate text 
            string text = inputField.text;
            RectTransform gTransform = GetComponent<RectTransform>();

            Machine.GetComponent<TerminalMachine>().PrintResultText("uno : " + text);

            //analyze input
            analyzeInput(text);

            //record input
            recordInput(text);

            //move input field
            transform.localPosition = new Vector3(0, 18 - transform.parent.GetComponent<RectTransform>().rect.height, 0);
            gTransform.offsetMax = new Vector2(0, gTransform.offsetMax.y);
            gTransform.offsetMin = new Vector2(0, gTransform.offsetMin.y);

            inputField.text = "";

        }

        inputField.Select();
        inputField.ActivateInputField();
        
    }

    void analyzeInput(string input) {
        string[] arg = input.Split(' ');
        List<string> argSet = new List<string>();
        string result = "";

        for (int i = 0; i < arg.Length; i++)        
            if (arg[i] != " " && arg[i] != "") argSet.Add(arg[i]);

        if (argSet.Count == 1) result = t.Instruct(argSet[0]);
        else if (argSet.Count == 2) result = t.Instruct(argSet[0], argSet[1]);
        else if (argSet.Count == 3) result = t.Instruct(argSet[0], argSet[1], argSet[2]);
        else result = "錯誤的輸入格式";

        //generate result text
        Machine.GetComponent<TerminalMachine>().PrintResultText(result);
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
