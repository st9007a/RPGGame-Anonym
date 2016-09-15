using UnityEngine;
using UnityEditor;
using System.Collections;

public class TernimalEditor : EditorWindow {

    [MenuItem("Editor/Terminal")]
    static void AddWindow()
    {

        Rect wr = new Rect(0, 0, 520, 650);
        TernimalEditor window = (TernimalEditor)EditorWindow.GetWindowWithRect(typeof(TernimalEditor), wr, true, "Terminal");
        window.Show();
    }

    void OnGUI() {
        Handles.BeginGUI();

        bool btn = Handles.Button(Vector3.zero, Quaternion.identity, 1, 1, delegate { Debug.Log("handle"); });
        Handles.EndGUI();
    }
}
