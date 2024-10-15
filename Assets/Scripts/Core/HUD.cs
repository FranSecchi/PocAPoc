using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public HUDFrases frasesHUD;
    public HUDWords wordsHUD;
    public GameObject hudPanel;
    public TextMeshProUGUI lifesTMP;


    public void Restart()
    {
        hudPanel.SetActive(false);
        GameManager.Instance.RestartGame();
    }
    internal void SetWords(List<WordStruct> words)
    {
        wordsHUD.SetWords(words);
    }

    internal void SetLifes(int lifes)
    {
        lifesTMP.text = lifes.ToString();
    }

    internal void SetFrases(List<WordStruct> frases)
    {
        frasesHUD.SetFrases(frases);
    }

    internal void OpenMenu()
    {
        hudPanel.SetActive(!hudPanel.activeSelf);
    }
}
