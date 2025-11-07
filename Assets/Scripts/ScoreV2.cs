using System.Collections.Generic;
using UnityEngine;

public class ScoreV2 : MonoBehaviour
{
    public int killThreshold = 20;
    public int angryThreshold = 30;
    public int happyThreshold = 70;

    [HideInInspector]
    public class Rating
    {
        public int score = 100;
        public string customerName = "";
    }

    public List<Rating> ratings = new List<Rating>();

    public void DecreaseScore(int value)
    {
        int index = CustomerList.instance.customerIndex;
        ratings[index].score -= value;
        if (ratings[index].score <= 0) ratings[index].score = 0;
    }

    public int FinalScore()
    {
        int scoreSum = 0;

        foreach (Rating rating in ratings)
            scoreSum += rating.score;

        return Mathf.RoundToInt(scoreSum / ratings.Count);
    }
}
