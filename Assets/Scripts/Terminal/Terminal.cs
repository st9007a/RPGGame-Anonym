﻿using UnityEngine;
using System;
using System.Collections.Generic;


public class Terminal {

    public int Pointer { private set; get; }
    public List<Node> Tree { private set; get; }
    public Dictionary<string, Action<string, string>> InstructionSet { private set; get; }
    public Response Result { private set; get; }
    public int EnterNode { private set; get; }
    public Node Now { get { return Tree[Pointer]; } }

    private const string errorInstruction = "無此命令";
    private const string errorSubInstruction = "無此子命令";
    private const string errorObject = "無此對象";

    public Terminal() {

        Pointer = -1;
        Tree = new List<Node>();
        InstructionSet = new Dictionary<string, Action<string, string>>();
        EnterNode = -1;

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
            else SetTree(item); 
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

    public void Enter() {
        if (EnterNode != -1) {
            
            Pointer = EnterNode;
            EnterNode = -1;
            //capture all children node
            if (Pointer == 0) {
                foreach (Node child in Tree[Pointer].Each())
                    child.IsCapture = true;
            }
        }
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
                if (Tree[0].IsPwd) {
                    EnterNode = 0;
                    Result = new Response(Tree[0].Pwd, Response.type.PWD);
                }
                else {
                    Pointer = 0;
                    Result = new Response("取得最高權限");
                }
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
                    if (Tree[Pointer].Parent.IsCapture) {
                        Pointer = Tree.IndexOf(Tree[Pointer].Parent);
                        Result = new Response("現在位置 : " + Tree[Pointer].Name);
                    }
                    else {
                        EnterNode = Tree.IndexOf(Tree[Pointer].Parent);
                        Result = new Response(Tree[Pointer].Parent.Pwd, Response.type.PWD);
                    }
                }
                else Result = new Response("已在最上層");
                break;
            case "down":
                int c = Tree[Pointer].Children.Count;
                for (int i = 0; i < c; i++) {
                    if(obj == Tree[Pointer].Children[i].Name) {
                        if (Now.Children[i].IsCapture) {
                            Pointer = Tree.IndexOf(Tree[Pointer].Children[i]);
                            Result = new Response("現在位置 : " + Tree[Pointer].Name);
                        }
                        else {
                            EnterNode = Tree.IndexOf(Now.Children[i]);
                            Result = new Response(Now.Children[i].Pwd, Response.type.PWD);
                        }
                        return;
                        
                    }
                }
                Result = new Response(errorObject);
                break;
            default:
                Result = new Response(errorSubInstruction);
                break;
        }
    }
}
