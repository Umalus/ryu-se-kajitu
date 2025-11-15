using System;

[Serializable]
public class RankingData{
    public string name;
    public int score;
    public string id { get; private set; }
    public DateTime dateTime;

    public RankingData(string _name,int _score, string _id,DateTime _date) {
        name = _name;
        if(_score > score)
        score = _score;
        id = _id;
        dateTime = _date;
    }

}
