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
    public List<string> DocName { private set; get; }
    public List<string> DocContent { private set; get; }

    public Node() {
        Depth = 0;
        Id = 0;
        Name = "null";       
        Comment = "沒有說明";
        IsCapture = true;
        Parent = null;
        Children = new List<Node>();
        DocContent = new List<string>();
        DocName = new List<string>();
    }
    public Node(string name, bool isPwd = false, string pwd = "") : this() {
        Name = name;
        IsPwd = isPwd;
        IsCapture = !isPwd;
        Pwd = pwd;
    }

    public IEnumerable Each() {

        yield return this;

        foreach (Node n in Children) {
            if (n.Children.Count == 0) yield return n;
            else foreach (Node nc in n.Each()) yield return nc; 
        }

        yield break;
    }

    public void AddChild(Node n) {
        n.Depth = Depth + 1;
        n.Parent = this;

        Children.Add(n);
    }

    public void AddDoc(string name, string content) {
        DocName.Add(name);
        DocContent.Add(content);
    }

    public void AddComment(string comment) {
        Comment = comment;
    }
      
	
}
