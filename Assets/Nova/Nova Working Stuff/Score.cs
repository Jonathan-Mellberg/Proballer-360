using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [HideInInspector] public class Rating
    {
        public int score = 100;
        public string customerName = "";
    }

    private List<Rating> ratings = new List<Rating>();

    public void DecreaseScore(int value)
    {
        int index = CustomerList.instance.customerIndex;
        ratings[index].score -= value;
    }

    public int FinalScore()
    {
        int scoreSum = 0;

        foreach (Rating rating in ratings)
            scoreSum += rating.score;

        return Mathf.RoundToInt(scoreSum / ratings.Count);
    }
}
