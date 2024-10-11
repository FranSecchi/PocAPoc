using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardWord : Word
{
    private string seq = "";

    public override void OnCharPressed(char key)
    {
        //if (!word.Content.Contains(key)) return;
        seq += key;
        //Add check
        if (!normalizedWord.StartsWith(seq))
        {
            seq = "";
        }
        display.UpdateDisplay(gameObject, seq, word.Content);
        if (seq == normalizedWord)
        {
            Remove(true);
        }
    }

    protected override void Init()
    {
        display.Initialize(gameObject, word.Content);
        points = normalizedWord.Length;
        speed = GameManager.Parameters.HardSpeed;
    }

    protected override void Step()
    {
        if (Vector2.Distance(transform.position, goal.position) < GameManager.Parameters.GoalRadius)
        {
            Remove(false);
        }
        else
        {
            float step = speed * Time.deltaTime;  // Define how much to move in this step
            transform.position = Vector2.MoveTowards(transform.position, goal.position, step); //Move towards goal
        }
    }
}
