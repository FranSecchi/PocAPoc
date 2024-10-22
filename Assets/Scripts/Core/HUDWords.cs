using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDWords : LocalizedText
{
    public int maxWordsPerPage = 10;
    public Transform bookPanel;
    public Transform contentPanel;
    public GameObject wordTextPrefab;
    public Button nextPageButton;
    public Button backPageButton;
    private List<WordStruct> words = new List<WordStruct>();
    private int currentPage = 0;
    private int totalPages;

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
    }// Display the current page of words
    public void BackPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage(currentPage);
        }
    }
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
            descText.text = GetText(words[i].Description, TextType.Simple);
        }
        nextPageButton.gameObject.SetActive(currentPage < totalPages - 1);
        backPageButton.gameObject.SetActive(currentPage > 0);
    }
    // Helper function to clear the content of the panel
    private void ClearContent()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject); // Destroy all existing word UI elements
        }
    }
    internal void SetWords(List<WordStruct> words)
    {
        this.words = words;
        totalPages = Mathf.CeilToInt((float)words.Count / maxWordsPerPage);
        currentPage = 0;
    }

    protected override void OnLanguageChanged()
    {
        ShowPage(currentPage);
    }
}