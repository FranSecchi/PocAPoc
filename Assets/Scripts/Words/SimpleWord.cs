using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWord : Word
{
    private string seq = "";

    public override void OnCharPressed(char key)
    {
        //if (!word.Content.Contains(key)) return;
        seq += key;
        //Add check
        if (!word.Content.StartsWith(seq))
        {
            seq = "";
        }
        if (seq == word.Content)
        {
            Remove(true);
        }
        display.UpdateDisplay(gameObject, seq, word.Content);
    }

    protected override void Init()
    {
        display.Initialize(gameObject, word.Content);
        points = word.Content.Length;
        speed = GameManager.Parameters.SimpleSpeed;
    }

    protected override void Step()
    {
        if(Vector2.Distance(transform.position, goal.position) < GameManager.Parameters.GoalRadius)
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
