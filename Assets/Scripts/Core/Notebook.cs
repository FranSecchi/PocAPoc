using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notebook : LocalizedText
{
    public HUD hud;
    public int maxWordsPerPage = 10;
    public int maxRegionalsPerPage = 10;
    public TextMeshProUGUI page1;
    public TextMeshProUGUI page2;
    public Transform bookPanel;
    public Transform contentPanel;
    public GameObject wordTextPrefab;
    public GameObject frasesContentPanel;
    public GameObject infoPanel;
    public GameObject titleGO;
    public TextMeshProUGUI fraseText;
    public TextMeshProUGUI fraseDesc;
    public Button wordsButton;
    public Button frasesButton;
    public Button regionalsButton;
    public GameObject nextPageButton;
    public GameObject backPageButton;
    public GameObject languageDpDwn;
    private Language language;
    List<WordStruct> totalWords;
    List<WordStruct> totalReg;
    List<WordStruct> totalFrases;
    private List<WordStruct> words = new List<WordStruct>();
    private List<WordStruct> frases = new List<WordStruct>();
    private List<WordStruct> regionals = new List<WordStruct>();
    private int currentPage = 0;
    private int totalPages;
    private int content = 0;
    internal void SetWords(List<WordStruct> inputWords)
    {
        totalWords = WordFactoryManager.total.OrderBy(word => word.Content.Length).ToList();
        totalReg = WordFactoryManager.regionals.OrderBy(word => word.Content.Length).ToList();
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
        totalFrases = WordFactoryManager.frases;
        this.frases = frases;
    }
    public void OpenInfo()
    {
        currentPage = 0;
        wordsButton.enabled = true;
        frasesButton.enabled = true;
        regionalsButton.enabled = true;
        frasesContentPanel.SetActive(false);
        languageDpDwn.SetActive(false);
        infoPanel.SetActive(true);
        titleGO.SetActive(false);
        contentPanel.gameObject.SetActive(false);
        nextPageButton.SetActive(false);
    }
    // Called when the player clicks the UI button to open the book
    public void OpenBook()
    {
        bookPanel.gameObject.SetActive(true);
        OpenWords();
    }
    public void CloseBook()
    {
        titleGO.SetActive(false);
        infoPanel.SetActive(false);
        frasesContentPanel.SetActive(false);
        bookPanel.gameObject.SetActive(false);
    }
    public void OpenWords()
    {
        content = 0;
        languageDpDwn.SetActive(true);
        titleGO.SetActive(true);
        contentPanel.gameObject.SetActive(true);
        infoPanel.SetActive(false);
        wordsButton.enabled = false;
        frasesButton.enabled = true;
        regionalsButton.enabled = true;
        frasesContentPanel.SetActive(false);
        currentPage = 0;
        totalPages = Mathf.CeilToInt((float)totalWords.Count / maxWordsPerPage);
        ShowPage(currentPage);
    }
    public void OpenFrases()
    {
        content = 1;
        languageDpDwn.SetActive(false);
        titleGO.SetActive(false);
        infoPanel.SetActive(false);
        frasesButton.enabled = false;
        regionalsButton.enabled = true;
        wordsButton.enabled = true;
        frasesContentPanel.SetActive(true);
        contentPanel.gameObject.SetActive(false);
        currentPage = 0;
        totalPages = totalFrases.Count;
        ShowFrasesPage(currentPage);
    }
    public void OpenRegionals()
    {
        content = 2;
        languageDpDwn.SetActive(true);
        titleGO.SetActive(true);
        infoPanel.SetActive(false);
        contentPanel.gameObject.SetActive(true);
        frasesButton.enabled = true;
        regionalsButton.enabled = false;
        wordsButton.enabled = true;
        frasesContentPanel.SetActive(false);
        currentPage = 0;
        totalPages = Mathf.CeilToInt((float)totalReg.Count / maxRegionalsPerPage);
        ShowRegionalsPage(currentPage);
    }
    private void ShowPage(int pageIndex)
    {
        ClearContent();

        int startWordIndex = pageIndex * maxWordsPerPage;
        int endWordIndex = Mathf.Min(startWordIndex + maxWordsPerPage, totalWords.Count);

        for (int i = startWordIndex; i < endWordIndex; i++)
        {
            var word = totalWords[i];
            bool has = words.Any(w => w.Content == word.Content);

            GameObject wordGO = Instantiate(wordTextPrefab, contentPanel);
            TMP_Text wordText = wordGO.GetComponent<TMP_Text>();
            wordText.text = has ? word.Content : "????";

            GameObject descGO = Instantiate(wordTextPrefab, contentPanel);
            TMP_Text descText = descGO.GetComponent<TMP_Text>();
            descText.text = has ? GetText(word.Description) : "????";

            GameObject go = Instantiate(wordTextPrefab, contentPanel);
            TMP_Text t = go.GetComponent<TMP_Text>();
            t.margin = new Vector4(0, 0, -80f, 0);
            t.text = has ? GetText(word.Description, Language.Catalan) : "????";
        }
        page1.text = currentPage.ToString();
        page2.text = (currentPage + 1).ToString();
        nextPageButton.SetActive(currentPage < totalPages - 1);
        backPageButton.SetActive(currentPage > 0);
    }
    private void ShowRegionalsPage(int pageIndex)
    {
        ClearContent();

        int startWordIndex = pageIndex * maxRegionalsPerPage;
        int endWordIndex = Mathf.Min(startWordIndex + maxRegionalsPerPage, totalReg.Count);

        for (int i = startWordIndex; i < endWordIndex; i++)
        {
            var regionalWord = totalReg[i];
            bool has = regionals.Any(w => w.Content == regionalWord.Content);

            GameObject wordGO = Instantiate(wordTextPrefab, contentPanel);
            TMP_Text wordText = wordGO.GetComponent<TMP_Text>();
            wordText.text = has ? regionalWord.Content : "????";

            GameObject descGO = Instantiate(wordTextPrefab, contentPanel);
            TMP_Text descText = descGO.GetComponent<TMP_Text>();
            descText.text = has ? GetText(regionalWord.Description) : "????";

            GameObject go = Instantiate(wordTextPrefab, contentPanel);
            TMP_Text t = go.GetComponent<TMP_Text>();
            t.margin = new Vector4(0, 0, -80f, 0);
            t.text = has ? GetText(regionalWord.Description, Language.Catalan) : "????";
        }
        page1.text = currentPage.ToString();
        page2.text = (currentPage + 1).ToString();
        nextPageButton.SetActive(currentPage < totalPages - 1);
        backPageButton.SetActive(currentPage > 0);
    }
    private void ShowFrasesPage(int pageIndex)
    {
        ClearContent() ;
        if (pageIndex < frases.Count)
        {
            fraseText.text = frases[pageIndex].Content;
            fraseDesc.text = frases[pageIndex].Description;
        }
        else
        {
            fraseText.text = "????";
            fraseDesc.text = "????";
        }
        page1.text = currentPage.ToString();
        page2.text = (currentPage + 1).ToString();
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
        switch (content)
        {
            case 0: ShowPage(currentPage); break;
            case 1: ShowFrasesPage(currentPage); break;
            case 2: ShowRegionalsPage(currentPage); break;
            default: break;
        }
    }
    // Called when the player clicks the "Next Page" button
    public void NextPage()
    {
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            switch (content)
            {
                case 0: ShowPage(currentPage); break;
                case 1: ShowFrasesPage(currentPage); break;
                case 2: ShowRegionalsPage(currentPage); break;
                    default:break;
            }
        }
    }// Display the current page of words
    public void BackPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            switch (content)
            {
                case 0: ShowPage(currentPage); break;
                case 1: ShowFrasesPage(currentPage); break;
                case 2: ShowRegionalsPage(currentPage); break;
                default: break;
            }
        }
    }
    public void Return()
    {
        CloseBook();
        hud.OpenMenu();
    }
}