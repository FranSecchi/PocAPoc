using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeWord : Word
{
    private float zigzagFrequency = 2f; // Frequency of zigzag movement
    private float zigzagAmplitude = 0.5f; // Amplitude of zigzag movement
    private float elapsedTime = 0f; // Keeps track of time to create the wave effect
    private float timeToDisappear = 2f; // Keeps track of time to create the wave effect
    private Vector2 startPosition;
    private Vector2 targetPos;
    protected override void Init()
    {
        word.Type = WordType.FADE;
        startPosition = transform.position;
        targetPos = startPosition + Vector2.up * 2f;
        timeToDisappear = parameter.TimeToFadeWord;
        base.Init();
    }

    protected override void Step()
    {
        if (elapsedTime > timeToDisappear)
        {
            Remove(false);
        }
        else
        {
            // Calculate the upward and zigzag movement
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / timeToDisappear);

            float horizontalOffset = Mathf.Sin(elapsedTime * zigzagFrequency) * zigzagAmplitude;

            // Gradual upward movement reaching target y after timeToDisappear seconds
            float newY = Mathf.Lerp(startPosition.y, targetPos.y, t);

            // Set the new position with zigzagging and gradual vertical movement
            transform.position = new Vector3(startPosition.x + horizontalOffset, newY, transform.position.z);
        }
    }
}
