using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject startText;
    public TextMeshProUGUI textMeshPro; 
    public float typingSpeed = 0.05f;
    public float wait = 1f;

    private string fullText;            
    private string currentText = "";   

    private void Start()
    {
        fullText = textMeshPro.text;

        textMeshPro.text = "";

        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText += fullText[i];      
            textMeshPro.text = currentText;  

            yield return new WaitForSeconds(typingSpeed); 
        }
        yield return new WaitForSeconds(wait);
        startText.SetActive(true);
    }
}
