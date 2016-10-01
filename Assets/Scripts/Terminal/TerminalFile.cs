using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TerminalFile : MonoBehaviour {
    //9393ff
    public string OpenFile(string content) {
        string output = "";
        string[] parseContent = content.Split('\n');
        output += "----------------------------------------\n";
        output += "<color=#28ff28>檔案名稱 : </color>" + parseContent[0] + "\n";
        output += "----------------------------------------\n";
        output += "<color=#28ff28>檔案內容 : </color>\n";

        for (int i = 1; i < parseContent.Length; i++)
            output += parseContent[i] + "\n";

        output += "----------------------------------------";
        GetComponent<Text>().text = output;

        return output;
    }
}
