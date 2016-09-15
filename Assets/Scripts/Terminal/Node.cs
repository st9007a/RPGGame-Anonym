using UnityEngine;
using System.Collections.Generic;

public class Node  {

    public int Depth;

    public int Id;
    public string Name;

    public bool IsPwd;
    public string Pwd;

    public Node Parent;
    public List<Node> Children;

    public List<string> Contains;

    public string show() {
        return "";
    }
      
	
}
