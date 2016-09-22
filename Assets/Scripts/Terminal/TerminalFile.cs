using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TerminalFile : MonoBehaviour {
    
    public void OpenFile(string content) {
        GetComponent<Text>().text = content;
    }
}
