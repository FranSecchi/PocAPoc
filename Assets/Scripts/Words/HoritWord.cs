using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoritWord : Word
{
    private Vector2 randomDirection;
    private float exitMargin = 0.05f; // Adjust this value for more or less margin

    protected override void Init()
    {
        word.Type = WordType.SIMPLE;
        base.Init();
        goal = Vector2.zero;

        // Calculate initial direction to goal
        Vector2 directionToGoal = (goal - (Vector2)transform.position).normalized;

        // Randomize the path by adding a small offset angle to the goal direction
        float angleOffset = Random.Range(-40f, 40f); // Small angle for variation
        float radians = angleOffset * Mathf.Deg2Rad;

        // Calculate random direction toward the goal
        randomDirection = new Vector2(
            directionToGoal.x * Mathf.Cos(radians) - directionToGoal.y * Mathf.Sin(radians),
            directionToGoal.x * Mathf.Sin(radians) + directionToGoal.y * Mathf.Cos(radians)
        ).normalized; // Normalize to ensure consistent speed
    }

    protected override void Step()
    {
        // Check if the word has exited the screen with a margin
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.x < -exitMargin || viewportPosition.x > 1 + exitMargin ||
            viewportPosition.y < -exitMargin || viewportPosition.y > 1 + exitMargin)
        {
            Remove(false); // Remove if out of screen bounds with margin
            return;
        }

        // Move in the direction of the randomized direction
        float step = speed * Time.deltaTime;
        transform.position += (Vector3)randomDirection * step;
    }
}
