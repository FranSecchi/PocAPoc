using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject barra;
    public GameObject startText;
    public GameObject escriu;
    public GameObject keys;
    public GameObject tip;
    public GameObject tip1;
    public TextMeshProUGUI textMeshPro; 
    public float typingSpeed = 0.05f;
    public float wait = 1f;

    public List<string> texts;
    private string fullText;            
    private string currentText = "";
    private bool skipTyping = false;
    private bool check = true;
    private int idx = 0;
    private void Start()
    {
        StartCoroutine(TypeText());
    }

    private void Update()
    {
        // Check if spacebar is pressed
        if (check && Input.GetKeyDown(KeyCode.Space))
        {
            BarPress();
        }
    }

    private void OnDisable()
    {
        tip.SetActive(false);
        tip1.SetActive(false);
        keys.SetActive(false);
    }
    public void BarPress()
    {
        if (!skipTyping)
            skipTyping = true;
        else if (idx < texts.Count)
            StartCoroutine(TypeText());
        else
        {
            barra.SetActive(false);
            startText.SetActive(true);
            escriu.SetActive(true);
            tip.SetActive(true);
            tip1.SetActive(true);
            keys.SetActive(true);
            check = false;
        }
    }
    
    IEnumerator TypeText()
    {
        fullText = texts[idx];
        skipTyping = false;
        textMeshPro.text = "";
        currentText = "";
        ++idx;
        for (int i = 0; i < fullText.Length; i++)
        {
            // Check if typing should be skipped
            if (skipTyping)
            {
                textMeshPro.text = fullText;
                break;
            }
            currentText += fullText[i];      
            textMeshPro.text = currentText;  

            yield return new WaitForSeconds(typingSpeed); 
        }
        skipTyping = true;
    }
}
