using System;

public struct Response {

    public enum type {
        TEXT,
        PWD,
        FILE,
    }

    public type Type { set; get; }
    public string Text { set; get; }
    public string Pwd { set; get; }
    public string File { set; get; }

    public Response(string s, type a = type.TEXT) {
        Type = a;
        Text = a == type.TEXT ? s : "";
        Pwd = a == type.PWD ? s : "";
        File = a == type.FILE ? s : "";
    }

}
