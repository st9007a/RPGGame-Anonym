using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TerminalProgress : MonoBehaviour {

    public string ExecuteAction { set; get; }
    public float Duration { set; get; }
    private int percent = 0;
    private float run = 0;

	void Update () {
        run += Time.deltaTime;
        if (percent < 100) {
            percent = (int)(run / Duration * 100);
            if (percent >= 100) {
                percent = 100;
                //find TerminalMachine
                GameObject.Find("Terminal").GetComponent<TerminalMachine>().ProgressComplete();

                //feed back
            }

            string output = "";
            output += ExecuteAction + "\n";
            output += "[";
            for (int i = 0; i <= 100; i = i + 2) {
                if (i <= percent) output += "-";
                else output += " ";
            }
            output += "] " + percent + "%\n";

            GetComponent<Text>().text = output;
        }
	}
}
