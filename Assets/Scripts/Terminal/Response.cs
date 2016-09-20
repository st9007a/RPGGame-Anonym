using UnityEngine;
using System.Collections;

public struct Response {

    public enum type {
        TEXT,
        PWD,
    }

    public type Type { set; get; }
    public string Text { set; get; }
    public string Pwd { set; get; }

    public Response(string s, type a = type.TEXT) {
        Type = a;
        Text = a == type.TEXT ? s : "";
        Pwd = a == type.PWD ? s : "";
    }

}
