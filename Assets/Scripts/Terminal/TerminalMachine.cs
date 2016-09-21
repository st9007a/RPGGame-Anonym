using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TerminalMachine : MonoBehaviour {

    public GameObject ScrollViewContainer;
    public GameObject ResultText;
    public GameObject PwdInputField;
    public Terminal t;

    void Awake() {
        Node n = new Node("root", true, "asd");
        n.AddChild(new Node("home", true, "123"));
        n.AddChild(new Node("config"));
        n.Children[0].AddChild(new Node("usr1"));

        t = new Terminal(n, "usr1");

        t.GetNode("home").AddComment("管理使用者");
        t.GetNode("config").AddComment("管理主機設定");

    }

    public void FixContainerSize(float componentHeight) {
        ScrollViewContainer.GetComponent<RectTransform>().offsetMax += new Vector2(0,componentHeight);        
    }

    public void PrintResultText(string text) {
        GameObject content = Instantiate(ResultText);
        RectTransform textTransform = content.GetComponent<RectTransform>();

        content.transform.SetParent(ScrollViewContainer.transform);
        content.transform.localPosition = new Vector3(0, 18 - ScrollViewContainer.GetComponent<RectTransform>().rect.height, 0);
        
        textTransform.offsetMax = new Vector2(5, textTransform.offsetMax.y);
        textTransform.offsetMin = new Vector2(5, textTransform.offsetMin.y);

        content.GetComponent<Text>().text = text;
        FixContainerSize(textTransform.rect.height);

    }

    public void PrintPwdInputField(string pwd) {

        PwdInputField.SetActive(true);

        RectTransform textTransform = PwdInputField.GetComponent<RectTransform>();
        TerminalPwd setting = PwdInputField.GetComponent<TerminalPwd>();

        PwdInputField.transform.SetParent(ScrollViewContainer.transform);
        PwdInputField.transform.localPosition = new Vector3(0, 18 - ScrollViewContainer.GetComponent<RectTransform>().rect.height, 0);
        textTransform.offsetMax = new Vector2(0, textTransform.offsetMax.y);
        textTransform.offsetMin = new Vector2(0, textTransform.offsetMin.y);

        setting.Pwd = pwd;
        setting.EnterNodeId = t.EnterNode;

        PwdInputField.GetComponent<InputField>().Select();
        PwdInputField.GetComponent<InputField>().ActivateInputField();
        
    }
}
