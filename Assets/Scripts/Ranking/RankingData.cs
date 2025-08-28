using System;

[Serializable]
public class RankingData{
    public string name;
    public int score;
    public DateTime dateTime;

    public RankingData(string _name,int _score,DateTime _date) {
        name = _name;
        score = _score;
        dateTime = _date;
    }

}
