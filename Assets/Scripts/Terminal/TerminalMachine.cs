using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TerminalMachine : MonoBehaviour {

    public GameObject ScrollViewContainer;
    public GameObject ResultText;

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
}
