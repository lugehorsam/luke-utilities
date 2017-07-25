using System;

[Serializable]
public class ServerTime {

    public DateTime Time => DateTime.Parse (time);

    string time = "";
}
