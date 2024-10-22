using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    public float transitionDuration = 2f; // Duration of the transition
    private float elapsedTime = 0f;

    public Vector3 startPosition;
    public Vector3 targetPosition;
    public float startSize;
    public float targetSize;

    public void Transition()
    {
        StartCoroutine(TransitionC());
    }
    private IEnumerator TransitionC()
    {
        Camera.main.orthographicSize = startSize;
        Camera.main.transform.position = startPosition;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);

            Camera.main.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            Camera.main.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
            yield return null;
        }
        GameManager.Instance.FirstStart();
    }
}
