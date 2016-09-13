using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TernimalInput : MonoBehaviour {

    public GameObject TerminalText;

    private int lineCount = 0;

	void Start(){
        GetComponent<InputField>().onEndEdit.AddListener(delegate { endEdit(); });
    }

    void endEdit() {
        string text = "uno : " + GetComponent<InputField>().text;
        GameObject content = Instantiate(TerminalText);
        RectTransform textTransform = content.GetComponent<RectTransform>();
        RectTransform gTransform = GetComponent<RectTransform>();

        lineCount++;

        content.transform.parent = transform.parent;
        content.transform.localPosition = transform.localPosition;
        textTransform.offsetMax = new Vector2(5, textTransform.offsetMax.y);
        textTransform.offsetMin = new Vector2(5, textTransform.offsetMin.y);

        content.GetComponent<Text>().text = text;

        transform.localPosition = new Vector3(0, -18 - lineCount * textTransform.rect.height, 0);
        gTransform.offsetMax = new Vector2(0, gTransform.offsetMax.y);
        gTransform.offsetMin = new Vector2(0, gTransform.offsetMin.y);

        GetComponent<InputField>().text = "";
    }
}
