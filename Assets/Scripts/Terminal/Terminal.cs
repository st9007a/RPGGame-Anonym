using UnityEngine;
using System;
using System.Collections.Generic;

public class Terminal {

    public int Pointer { set; get; }
    public List<Node> Tree { set; get; }

    //instruction, sub-instruction, object
    public Dictionary<string, Action<string, string>> InstructionSet { set; get; }
    public string Result { private set; get; }

    public Terminal() {

        Pointer = -1;
        Tree = new List<Node>();
        InstructionSet = new Dictionary<string, Action<string, string>>();
        Result = "";

        Initial();
    }

    private void Initial() {
        InstructionSet.Add("show", show);
    }

    public string Instruct(string instr, string subInstr = "" , string obj = "") {

        if (!InstructionSet.ContainsKey(instr))
            Result = "無此命令";
        else {
            Action<string, string> execute = InstructionSet[instr];
            execute(subInstr, obj);
        }

        return Result;
    }

    private void show(string sub = "", string obj = "") {
        switch (sub) {
            case "":
                Result = "顯示所有結點";
                break;
            case "all":
                Result = "顯示隱藏結點";
                break;
            default:
                Result = "無此子命令";
                break;
        }
    }
}
