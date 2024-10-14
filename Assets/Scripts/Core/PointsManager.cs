using UnityEngine;
public class PointsManager
{
    public int totalPoints = 0;
    public void sumPoints(Word word, bool completed)
    {
        int sum = completed ? word.word.Content.Length : 0;
        totalPoints += sum;
        if (completed)
        {

        }
        else
        {

        }
    }
}