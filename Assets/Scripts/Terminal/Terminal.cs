using UnityEngine;
using System;
using System.Collections.Generic;


public class Terminal {

    public int Pointer { private set; get; }
    public List<Node> Tree { private set; get; }
    public Dictionary<string, Action<string, string>> InstructionSet { private set; get; }
    public Response Result { private set; get; }

    private const string errorInstruction = "無此命令";
    private const string errorSubInstruction = "無此子命令";
    private const string errorObject = "無此對象";

    public Terminal() {

        Pointer = -1;
        Tree = new List<Node>();
        InstructionSet = new Dictionary<string, Action<string, string>>();

        initial();
    }

    public Terminal(Node root, string initialLocation) : this() {
        SetTree(root);
        SetPointer(initialLocation);
    }

    private void initial() {
        InstructionSet.Add("root", root);
        InstructionSet.Add("comment", comment);
        InstructionSet.Add("move", move);
    }

    public void SetTree(Node rootNode) {

        Tree.Add(rootNode);

        foreach (Node item in rootNode.Children) {
            if (item.Children.Count == 0) {
                Tree.Add(item);
                item.Id = Tree.IndexOf(item);
            }
            else {
                SetTree(item);
            }
        }

    }

    public bool SetPointer(string nodeName) {
        int c = Tree.Count;
        for (int i = 0; i < c; i++) {
            if (Tree[i].Name == nodeName) {
                Pointer = i;
                return true;
            }
        }
        return false;

    }

    public Node GetNode(string nodeName) {
        int c = Tree.Count;
        for (int i = 0; i < c; i++)
            if (nodeName == Tree[i].Name) return Tree[i];

        return new Node();
        
    }

    public void PrintTree() {
        for (int i = 0; i < Tree.Count; i++)
            Debug.Log(i + " " + Tree[i].Name + " " + (i == 0 ? "null" : Tree[i].Parent.Name));
    }

    public Response Instruct(string instr, string subInstr = "" , string obj = "") {

        if (!InstructionSet.ContainsKey(instr))
            Result = new Response(errorInstruction);
        else
        {
            Action<string, string> execute = InstructionSet[instr];
            execute(subInstr, obj);
        }

        return Result;
    }

    private void comment(string sub = "", string obj = "") {
        switch (sub) {
            case "":
                Result = new Response(Tree[Pointer].Comment);
                break;
            default:
                Result = new Response(errorSubInstruction);
                break;
        }
        
    }

    private void root(string sub = "", string obj = "") {
        switch (sub) {
            case "":
                Pointer = 0;
                Result = new Response("取得最高權限");
                break;
            default:
                Result = new Response(errorSubInstruction);
                break;
        }
        
    }

    private void move(string sub = "", string obj = "") {
        switch (sub) {
            case "up":
                if (Pointer != 0) {
                    Pointer = Tree.IndexOf(Tree[Pointer].Parent);
                    Result = new Response("現在位置 : " + Tree[Pointer].Name);
                }
                else Result = new Response("已在最上層");
                break;
            case "down":
                bool isExist = false;
                int c = Tree[Pointer].Children.Count;
                for (int i = 0; i < c; i++) {
                    if(obj == Tree[Pointer].Children[i].Name) {
                        Pointer = Tree.IndexOf(Tree[Pointer].Children[i]);
                        isExist = true;
                        break;
                    }
                }
                if (!isExist) Result = new Response(errorObject);
                else Result = new Response("現在位置 : " + Tree[Pointer].Name);
                break;
            default:
                Result = new Response(errorSubInstruction);
                break;
        }
    }
}
