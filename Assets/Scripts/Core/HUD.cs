using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Sprite lapiz;
    public Sprite lapiz_roto;
    public Image hp;
    public GameObject dialeg;
    public Notebook notebook;
    public GameObject hudPanel;
    public GameObject resumeButton;
    public Image[] lifesRend;
    public TextMeshProUGUI pointsTMP;
    public TextMeshProUGUI recordTMP;
    public GameObject newRecordDisplay;

    public void UpdateHp(float i, float x)
    {
        hp.fillAmount = i == 0f ? 0f : i / x;
    }
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
        lifesRend[0].transform.parent.gameObject.SetActive(lifes>0);
        for (int i = 0; i < lifesRend.Length; i++)
        {
            lifesRend[i].sprite = i < lifes ? lapiz : lapiz_roto;
        }
    }

    internal void SetFrases(List<WordStruct> frases)
    {
        notebook.SetFrases(frases);
    }

    public void OpenMenu()
    {
        hudPanel.SetActive(!hudPanel.activeSelf);
    }
    internal void DisplayNewRecord(int record)
    {
        newRecordDisplay.SetActive(true);
        recordTMP.text = record.ToString();
    }
    public void ShowDialeg()
    {
        dialeg.SetActive(true);
    }
    public void Pause(bool paused)
    {
        resumeButton.SetActive(paused);
        hudPanel.SetActive(paused);
        notebook.CloseBook();
    }
}
