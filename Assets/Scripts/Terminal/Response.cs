using System;

public struct Response {

    public enum type {
        TEXT,
        PWD,
        FILE,
        PROGRESS
    }

    public type Type { set; get; }
    public string Text { set; get; }
    public string Pwd { set; get; }
    public string File { set; get; }
    public string Progress { set; get; }

    public Response(string resContent, type resType = type.TEXT) {
        Type = resType;
        Text = resType == type.TEXT ? resContent : "";
        Pwd = resType == type.PWD ? resContent : "";
        File = resType == type.FILE ? resContent : "";
        Progress = resType == type.PROGRESS ? resContent : "";
    }

}
