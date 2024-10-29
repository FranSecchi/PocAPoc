using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MediumWord : Word
{
    protected override void Init()
    {
        word.Type = WordType.SIMPLE;
        base.Init();
    }

    protected override void Step()
    {
        if (Vector2.Distance(transform.position, goal) < GameManager.Parameter.GoalRadius)
        {
            Remove(false);
        }
        else
        {
            float step = speed * Time.deltaTime;  // Define how much to move in this step
            transform.position = Vector2.MoveTowards(transform.position, goal, step); //Move towards goal
        }
    }
}