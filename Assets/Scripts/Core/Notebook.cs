using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notebook : LocalizedText
{
    public HUD hud;
    public int maxWordsPerPage = 10;
    public int maxRegionalsPerPage = 10;
    public Transform bookPanel;
    public Transform contentPanel;
    public GameObject wordTextPrefab;
    public GameObject frasesContentPanel;
    public GameObject titleGO;
    public TextMeshProUGUI fraseText;
    public TextMeshProUGUI fraseDesc;
    public Button wordsButton;
    public Button frasesButton;
    public Button regionalsButton;
    public GameObject nextPageButton;
    public GameObject backPageButton;

    private List<WordStruct> words = new List<WordStruct>();
    private List<WordStruct> frases = new List<WordStruct>();
    private List<WordStruct> regionals = new List<WordStruct>();
    private int currentPage = 0;
    private int totalPages;

    internal void SetWords(List<WordStruct> inputWords)
    {
        words.Clear();
        regionals.Clear();
        foreach (var word in inputWords)
        {
            if (word.Dialect != "catala")
            {
                regionals.Add(word);
            }
            else
            {
                words.Add(word);
            }
        }
    }
    internal void SetFrases(List<WordStruct> frases)
    {
        this.frases = frases;
    }
    internal void SetRegionals(List<WordStruct> regionals)
    {
        this.regionals = regionals;
    }

    // Called when the player clicks the UI button to open the book
    public void OpenBook()
    {
        bookPanel.gameObject.SetActive(true);
        OpenWords();
    }
    public void CloseBook()
    {
        bookPanel.gameObject.SetActive(false);
        hud.OpenMenu();
    }
    public void OpenWords()
    {
        titleGO.SetActive(true);
        wordsButton.enabled = false;
        frasesButton.enabled = frases.Count > 0;
        regionalsButton.enabled = regionals.Count > 0;
        frasesContentPanel.SetActive(false);
        currentPage = 0;
        totalPages = Mathf.CeilToInt((float)words.Count / maxWordsPerPage);
        ShowPage(currentPage);
    }
    public void OpenFrases()
    {
        titleGO.SetActive(false);
        frasesButton.enabled = false;
        regionalsButton.enabled = regionals.Count > 0;
        wordsButton.enabled = true;
        frasesContentPanel.SetActive(true);
        currentPage = 0;
        totalPages = frases.Count;
        ShowFrasesPage(currentPage);
    }
    public void OpenRegionals()
    {
        titleGO.SetActive(true);
        frasesButton.enabled = frases.Count > 0;
        regionalsButton.enabled = false;
        wordsButton.enabled = true;
        frasesContentPanel.SetActive(false);
        currentPage = 0;
        totalPages = Mathf.CeilToInt((float)regionals.Count / maxRegionalsPerPage);
        ShowRegionalsPage(currentPage);
    }
    private void ShowPage(int pageIndex)
    {
        ClearContent();

        int startWordIndex = pageIndex * maxWordsPerPage;
        int endWordIndex = Mathf.Min(startWordIndex + maxWordsPerPage, words.Count);

        for (int i = startWordIndex; i < endWordIndex; i++)
        {
            GameObject wordGO = Instantiate(wordTextPrefab, contentPanel);
            TMP_Text wordText = wordGO.GetComponent<TMP_Text>();
            wordText.text = words[i].Content;
            GameObject descGO = Instantiate(wordTextPrefab, contentPanel);
            TMP_Text descText = descGO.GetComponent<TMP_Text>();
            descText.text = GetText(words[i].Description, Language.Spanish);
            GameObject go = Instantiate(wordTextPrefab, contentPanel);
            TMP_Text t = go.GetComponent<TMP_Text>();
            t.text = GetText(words[i].Description, Language.Catalan);
            t.enableAutoSizing = true;
        }
        nextPageButton.SetActive(currentPage < totalPages - 1);
    }
    private void ShowRegionalsPage(int pageIndex)
    {
        ClearContent();

        int startWordIndex = pageIndex * maxRegionalsPerPage;
        int endWordIndex = Mathf.Min(startWordIndex + maxRegionalsPerPage, regionals.Count);

        for (int i = startWordIndex; i < endWordIndex; i++)
        {
            GameObject wordGO = Instantiate(wordTextPrefab, contentPanel);
            TMP_Text wordText = wordGO.GetComponent<TMP_Text>();
            wordText.text = regionals[i].Content;
            GameObject descGO = Instantiate(wordTextPrefab, contentPanel);
            TMP_Text descText = descGO.GetComponent<TMP_Text>();
            descText.text = GetText(regionals[i].Description, Language.Spanish);
            GameObject go = Instantiate(wordTextPrefab, contentPanel);
            TMP_Text t = go.GetComponent<TMP_Text>();
            t.text = GetText(words[i].Description, Language.Catalan);
            t.enableAutoSizing = true;
        }
        nextPageButton.SetActive(currentPage < totalPages - 1);
    }
    private void ShowFrasesPage(int pageIndex)
    {
        ClearContent() ;
        if (totalPages == 0)
        {
            fraseText.text = "Cap frase feta trovada";
            fraseDesc.text = "";
        }
        else
        {
            fraseText.text = frases[pageIndex].Content;
            fraseDesc.text = frases[pageIndex].Description;
        }
        nextPageButton.SetActive(currentPage < totalPages - 1);
        backPageButton.SetActive(currentPage > 0);
    }
    // Helper function to clear the content of the panel
    private void ClearContent()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject); // Destroy all existing word UI elements
        }
    }

    protected override void OnLanguageChanged()
    {
        ShowPage(currentPage);
    }
    // Called when the player clicks the "Next Page" button
    public void NextPage()
    {
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            if (wordsButton.enabled) ShowRegionalsPage(currentPage);
            else ShowPage(currentPage);
        }
    }// Display the current page of words
    public void BackPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            if (wordsButton.enabled) ShowRegionalsPage(currentPage);
            else ShowPage(currentPage);
        }
        else CloseBook();
    }
}