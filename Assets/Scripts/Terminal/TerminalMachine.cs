using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TerminalMachine : MonoBehaviour {

    public GameObject ScrollViewContainer;
    public GameObject ResultText;
    public GameObject PwdInputField;
    public GameObject FileText;
    public GameObject TerminalInputField;

    public Terminal t;

    void Awake() {
        Node n = new Node("root", true, "asd");
        n.AddChild(new Node("home"));
        n.AddChild(new Node("config"));
        n.Children[0].AddChild(new Node("usr1", true, "456"));

        t = new Terminal(n, "home");

        t.GetNode("home").AddComment("管理使用者");
        t.GetNode("home").AddFile("test_file", "usr1 pwd=456");

        t.GetNode("config").AddComment("管理主機設定");

    }

    public void FixContainerSize(float componentHeight) {
        ScrollViewContainer.GetComponent<RectTransform>().offsetMax += new Vector2(0,componentHeight);        
    }

    public void FixInputFieldPosition() {

        TerminalInputField.SetActive(true);

        RectTransform gTransform = TerminalInputField.GetComponent<RectTransform>();

        TerminalInputField.transform.localPosition = new Vector3(0, 18 - TerminalInputField.transform.parent.GetComponent<RectTransform>().rect.height, 0);
        gTransform.offsetMax = new Vector2(0, gTransform.offsetMax.y);
        gTransform.offsetMin = new Vector2(0, gTransform.offsetMin.y);
        TerminalInputField.GetComponent<InputField>().text = "";

        TerminalInputField.GetComponent<InputField>().Select();
        TerminalInputField.GetComponent<InputField>().ActivateInputField();
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

    public void PrintFileContent(string content) {
        string[] lines = content.Split('\n');
        GameObject fileText = Instantiate(FileText);
        
        fileText.transform.SetParent(ScrollViewContainer.transform);
        fileText.GetComponent<TerminalFile>().OpenFile(content);
        
        fileText.transform.localPosition = new Vector3(0, 18 - ScrollViewContainer.GetComponent<RectTransform>().rect.height, 0);
        fileText.GetComponent<RectTransform>().offsetMax = new Vector2(-5, fileText.GetComponent<RectTransform>().offsetMin.y);
        fileText.GetComponent<RectTransform>().offsetMin = new Vector2(5, fileText.GetComponent<RectTransform>().offsetMin.y);
        FixContainerSize(21 * lines.Length + 10);
    }
}
