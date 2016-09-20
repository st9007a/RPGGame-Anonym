using System;

public struct Response {

    public enum type {
        TEXT,
        PWD,
        DOC,
    }

    public type Type { set; get; }
    public string Text { set; get; }
    public string Pwd { set; get; }
    public string Doc { set; get; }
    public Action Callback { set; get; }

    public Response(string s, type a = type.TEXT, Action act = null) {
        Type = a;
        Text = a == type.TEXT ? s : "";
        Pwd = a == type.PWD ? s : "";
        Doc = a == type.DOC ? s : "";

        Callback = act;
    }

}
