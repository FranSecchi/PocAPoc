using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject barra;
    public GameObject startText;
    public GameObject escriu;
    public TextMeshProUGUI textMeshPro; 
    public float typingSpeed = 0.05f;
    public float wait = 1f;

    public List<string> texts;
    private string fullText;            
    private string currentText = "";
    private bool skipTyping = false;
    private int idx = 0;
    private void Start()
    {
        StartCoroutine(TypeText());
    }

    private void Update()
    {
        // Check if spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!skipTyping)
                skipTyping = true;
            else if (idx < texts.Count)
                StartCoroutine(TypeText());
            else
            {
                barra.SetActive(false);
                startText.SetActive(true);
                escriu.SetActive(true);
            }
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
