using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class TerminalInput : MonoBehaviour {

    public GameObject TerminalText;

    private int lineCount = 0;
    private InputField inputField;
    private Terminal t;

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

    void endEdit() {
        if(inputField.text != "") { 
            string text = inputField.text;
            GameObject content = Instantiate(TerminalText);
            RectTransform textTransform = content.GetComponent<RectTransform>();
            RectTransform gTransform = GetComponent<RectTransform>();

            lineCount++;

            content.transform.SetParent(transform.parent);
            content.transform.localPosition = transform.localPosition;
            textTransform.offsetMax = new Vector2(5, textTransform.offsetMax.y);
            textTransform.offsetMin = new Vector2(5, textTransform.offsetMin.y);

            content.GetComponent<Text>().text = "uno : " + text;
            analyzeInput(text);

            transform.localPosition = new Vector3(0, -18 - lineCount * textTransform.rect.height, 0);
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

        for (int i = 0; i < arg.Length; i++) 
            if (arg[i] != " ")  argSet.Add(arg[i]);

        if (argSet.Count <= 3) {

            string result;

            switch (argSet.Count) {
                case 1:
                    result = t.Instruct(argSet[0]);
                    break;
                case 2:
                    result = t.Instruct(argSet[0], argSet[1]);
                    break;
                case 3:
                    result = t.Instruct(argSet[0], argSet[1], argSet[2]);
                    break;
                default:
                    result = "";
                    break;
            }

            Debug.Log(result);
        }           
        
    }
}
