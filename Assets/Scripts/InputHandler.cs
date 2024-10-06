using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private List<IWordController> subscribers;
    // Start is called before the first frame update
    void Start()
    {
        subscribers = new List<IWordController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (!string.IsNullOrEmpty(Input.inputString))
            {
                char keyPressed = Input.inputString[0];

                if (char.IsLetterOrDigit(keyPressed) || char.IsPunctuation(keyPressed) || char.IsSymbol(keyPressed))
                {
                    notifyAll(keyPressed);
                }
            }
        }
    }

    public void subscribe(IWordController controller)
    {
        if (!subscribers.Contains(controller))
        {
            subscribers.Add(controller);
        }
    }
    public void unsubscribe(IWordController controller)
    {
        if (subscribers.Contains(controller))
        {
            subscribers.Remove(controller);
        }
    }

    public void notifyAll(char key)
    {
        List<IWordController> currentSubscribers = new List<IWordController>(subscribers);

        foreach (var subscriber in currentSubscribers)
        {
            subscriber.HandleInput(key);
        }
    }
}
