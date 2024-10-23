using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Notebook notebook;
    public GameObject hudPanel;
    public GameObject resumeButton;
    public TextMeshProUGUI lifesTMP;
    public TextMeshProUGUI pointsTMP;
    public TextMeshProUGUI recordTMP;
    public GameObject newRecordDisplay;

    public void Restart()
    {
        resumeButton.SetActive(false);
        hudPanel.SetActive(false);
        newRecordDisplay.SetActive(false);
        GameManager.Instance.RestartGame();
    }
    internal void SetPoints(int points)
    {
        pointsTMP.text = points.ToString();
    }

    internal void SetWords(List<WordStruct> words)
    {
        notebook.SetWords(words);
    }

    internal void SetLifes(int lifes)
    {
        lifesTMP.text = lifes.ToString();
    }

    internal void SetFrases(List<WordStruct> frases)
    {
        notebook.SetFrases(frases);
    }
    internal void SetRegionals(List<WordStruct> words)
    {
        notebook.SetRegionals(words);
    }

    internal void OpenMenu()
    {
        hudPanel.SetActive(!hudPanel.activeSelf);
    }
    internal void DisplayNewRecord(int record)
    {
        newRecordDisplay.SetActive(true);
        recordTMP.text = record.ToString();
    }

    public void Pause(bool paused)
    {
        resumeButton.SetActive(paused);
        hudPanel.SetActive(paused);
    }
}
