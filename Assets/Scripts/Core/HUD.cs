using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject wordTextPrefab;
    public GameObject hudPanel;
    public Transform contentPanel;
    public Transform bookPanel;
    public TextMeshProUGUI lifesTMP;
    public Button nextPageButton; 
    public int maxWordsPerPage = 10; 

    private List<WordStruct> words; 
    private int currentPage = 0; 
    private int totalPages; 

    public void Restart()
    {
        hudPanel.SetActive(false);
        GameManager.Instance.RestartGame();
    }
    internal void SetWords(List<WordStruct> words)
    {
        this.words = words;
        totalPages = Mathf.CeilToInt((float)words.Count / maxWordsPerPage);
        currentPage = 0; 
        hudPanel.SetActive(true);
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

    // Called when the player clicks the "Next Page" button
    public void NextPage()
    {
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            ShowPage(currentPage);
        }
    }

    internal void SetLifes(int lifes)
    {
        lifesTMP.text = lifes.ToString();
    }

    // Display the current page of words
    private void ShowPage(int pageIndex)
    {
        ClearContent(); // Clear any existing words in the panel

        int startWordIndex = pageIndex * maxWordsPerPage;
        int endWordIndex = Mathf.Min(startWordIndex + maxWordsPerPage, words.Count);

        for (int i = startWordIndex; i < endWordIndex; i++)
        {
            GameObject wordGO = Instantiate(wordTextPrefab, contentPanel);
            TMP_Text wordText = wordGO.GetComponent<TMP_Text>();
            wordText.text = words[i].Content;
            GameObject descGO = Instantiate(wordTextPrefab, contentPanel);
            TMP_Text descText = descGO.GetComponent<TMP_Text>();
            descText.text = words[i].Description;
        }

        // Enable/Disable next page button based on page status
        nextPageButton.interactable = (currentPage < totalPages - 1);
    }

    // Helper function to clear the content of the panel
    private void ClearContent()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject); // Destroy all existing word UI elements
        }
    }
}
