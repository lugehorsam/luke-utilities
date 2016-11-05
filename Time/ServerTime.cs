using System;

[Serializable]
public class ServerTime {

    public DateTime Time {
        get {
            return DateTime.Parse (time);
        }
    }

    string time = "";
}
