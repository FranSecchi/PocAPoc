using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeysManager : MonoBehaviour
{
    public static KeysManager Instance;
    public List<TextMeshProUGUI> keys;
    private Dictionary<int, TextMeshProUGUI> keyDictionary = new Dictionary<int, TextMeshProUGUI>();
    private Dictionary<int, List<char>> dynamicKeys = new Dictionary<int, List<char>>()
    {
        { 0, new List<char> { 'G', 'C', 'D', 'F' } },
        { 1, new List<char> { 'B', 'P', 'J', 'M' } },
        { 2, new List<char> { 'H', 'X', 'Q', 'V' } },
        { 3, new List<char> { 'Ç', 'Z' } }
    };
    private List<char> currentChars;
    private int idx = 0;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        currentChars = new List<char>();
        SetChar('D', 0);
        SetChar('B', 1);
        SetChar('H', 2);
        SetChar('Ç', 3);
    }
    public static bool Contains(char c)
    {
        return Instance.ContainsKey(c);
    }
    private void SetChar(char c, int key)
    {
        currentChars.Add(c);
        keys[key].text = "" + c;
    }
    public void Randomize()
    {
        currentChars.Clear(); // Clear current characters for new assignment

        for (int i = 0; i < keys.Count; i++)
        {
            // Select the character based on current index within each dynamic key list
            char nextChar = dynamicKeys[i][idx % dynamicKeys[i].Count];

            SetChar(nextChar, i); // Set the character in the key

            currentChars.Add(nextChar); // Add to active characters
        }

        // Increment the index for the next call, to cycle through the patterns
        idx++;

        // Update any necessary display or game state
        GameManager.Instance.UpdateDisplayWords();
    }
    public bool ContainsKey(char c)
    {
        if (currentChars.Contains(c))
        {
            return true;
        }
        bool b = true;
        foreach (var keyGroup in dynamicKeys.Values)
        {
            if (keyGroup.Contains(c))
            {
                b = false;
            }
        }

        return b;
    }
}
