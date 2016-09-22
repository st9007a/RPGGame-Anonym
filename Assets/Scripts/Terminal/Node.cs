using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Node {

    public int Depth { private set; get; }
    public int Id { set; get; }

    public bool IsPwd { private set; get; }
    public bool IsCapture { set; get; }

    public string Name { private set; get; }
    public string Pwd { private set; get; }
    public string Comment { private set; get; }

    public Node Parent { private set; get; }

    public List<Node> Children { set; get; }
    public List<string> FileName { private set; get; }
    public List<string> FileContent { private set; get; }

    public Node() {
        Depth = 0;
        Id = 0;
        Name = "null";       
        Comment = "沒有說明";
        IsCapture = true;
        Parent = null;
        Children = new List<Node>();
        FileContent = new List<string>();
        FileName = new List<string>();
    }
    public Node(string name, bool isPwd = false, string pwd = "") : this() {
        Name = name;
        IsPwd = isPwd;
        IsCapture = !isPwd;
        Pwd = pwd;
    }

    /// <summary>
    /// return all children node (include self)
    /// </summary>
    /// <returns></returns>
    public IEnumerable Each() {

        yield return this;

        foreach (Node n in Children) {
            if (n.Children.Count == 0) yield return n;
            else foreach (Node nc in n.Each()) yield return nc; 
        }

        yield break;
    }

    /// <summary>
    /// return all children node (exclude self)
    /// </summary>
    /// <returns></returns>
    public IEnumerable EachChild() {
        foreach (Node n in Children) {
            if (n.Children.Count == 0) yield return n;
            else foreach (Node nc in n.Each()) yield return nc;
        }

        yield break;
    }

    /// <summary>
    /// compare input and return node name or file name 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public string SearchItemName(string str) {

        List<string> nameSet = new List<string>();
        char[] parseSelect = str.ToCharArray();

        int c = FileName.Count;
        for (int i = 0; i < c; i++) nameSet.Add(FileName[i]);

        c = Children.Count;
        for (int i = 0; i < c; i++) nameSet.Add(Children[i].Name);

        c = nameSet.Count;
        for (int i = 0; i < c; i++) {
            char[] childName = nameSet[i].ToCharArray();

            bool isMatch = true;

            for (int j = parseSelect.Length - 1; j >= 0; j--) {
                if (parseSelect.Length > childName.Length || parseSelect[j] != childName[parseSelect.Length - 1 - j]) {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch) {
                return nameSet[i];
            }
        }
        return str;
    }

    public void AddChild(Node n) {
        n.Depth = Depth + 1;
        n.Parent = this;

        Children.Add(n);
    }

    public void AddFile(string name, string content) {
        FileName.Add(name);
        FileContent.Add(content);
    }

    public void AddComment(string comment) {
        Comment = comment;
    }
      
	
}
