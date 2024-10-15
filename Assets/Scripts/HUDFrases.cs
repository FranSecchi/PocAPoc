using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDFrases : MonoBehaviour
{
    public Transform bookPanel;
    public Button nextPageButton;
    public TextMeshProUGUI wordText;
    public TextMeshProUGUI descText;

    private List<WordStruct> frases;
    private int currentPage = 0;
    private int totalPages;
    internal void SetFrases(List<WordStruct> frases)
    {
        this.frases = frases;
        totalPages =  frases.Count;
        currentPage = 0;
    }
    // Called when the player clicks the UI button to open the book
    public void OpenBook()
    {
        if (bookPanel.gameObject.activeSelf)
        {
            bookPanel.gameObject.SetActive(false);
            return;
        }
        bookPanel.gameObject.SetActive(true);
        currentPage = 0;
        ShowPage(currentPage);
    }
    public void NextPage()
    {
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            ShowPage(currentPage);
        }
    }
    private void ShowPage(int pageIndex)
    {
        if(frases.Count == 0)
        {
            wordText.text = "Cap frase feta trovada";
            descText.text = "";
        }
        else
        {
            wordText.text = frases[pageIndex].Content;
            descText.text = frases[pageIndex].Description;
        }
        nextPageButton.interactable = (currentPage < totalPages - 1);
    }
}
