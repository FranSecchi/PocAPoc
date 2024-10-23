using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDFrases : MonoBehaviour
{
    public Transform bookPanel;
    public Button nextPageButton;
    public Button backPageButton;
    public TextMeshProUGUI fraseText;
    public TextMeshProUGUI fraseDesc;

    private List<WordStruct> frases = new List<WordStruct>();
    private int currentPage = 0;
    private int totalPages;
}
