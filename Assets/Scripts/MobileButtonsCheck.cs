using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileButtonsCheck : MonoBehaviour
{
    public List<Button> buttons;
    // Start is called before the first frame update
    void Start()
    {
        bool a = Application.isMobilePlatform;
        foreach (var button in buttons)
        {
            button.interactable = a;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
