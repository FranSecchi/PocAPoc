using System.Collections;
using UnityEngine;
using TMPro;
public class TextFallAnimation : MonoBehaviour
{
    public TextMeshPro textMeshPro; // Reference to the TextMeshPro component
    public float fallSpeed = 0.5f; // Speed at which the letters fall
    public float delayBetweenLetters = 0.15f; // Delay before each letter starts falling
    public float fallDistance = 30f; // Distance over which the letters fall
    public float lifetime = 2f; // Duration of the falling animation for each letter

    private Vector3[][] originalVerticesPerCharacter;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        StartCoroutine(AnimateTextFall());
    }

    private IEnumerator AnimateTextFall()
    {
        // Store the original text and clear the current text
        string originalText = textMeshPro.text;
        textMeshPro.text = ""; // Clear the text initially

        textMeshPro.ForceMeshUpdate(); // Force mesh update to get the correct text info
        TMP_TextInfo textInfo = textMeshPro.textInfo;

        // Pre-store the original vertices for each character
        originalVerticesPerCharacter = new Vector3[textInfo.characterCount][];

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            // Ensure the character is visible (skip spaces, etc.)
            if (!textInfo.characterInfo[i].isVisible)
                continue;

            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            // Store original vertices for this character (to restore later)
            originalVerticesPerCharacter[i] = new Vector3[4];
            originalVerticesPerCharacter[i][0] = textInfo.meshInfo[materialIndex].vertices[vertexIndex + 0];
            originalVerticesPerCharacter[i][1] = textInfo.meshInfo[materialIndex].vertices[vertexIndex + 1];
            originalVerticesPerCharacter[i][2] = textInfo.meshInfo[materialIndex].vertices[vertexIndex + 2];
            originalVerticesPerCharacter[i][3] = textInfo.meshInfo[materialIndex].vertices[vertexIndex + 3];
        }

        // Add each character to the TextMeshPro component, one by one
        for (int i = 0; i < originalText.Length; i++)
        {
            textMeshPro.text += originalText[i];
            textMeshPro.ForceMeshUpdate(); // Force the mesh to update to get the correct text info

            // Start falling animation for this character after delay
            StartCoroutine(AnimateLetterFall(i));

            // Delay before the next letter starts falling
            yield return new WaitForSeconds(delayBetweenLetters);
        }
    }

    private IEnumerator AnimateLetterFall(int charIndex)
    {
        TMP_TextInfo textInfo = textMeshPro.textInfo;

        // Ensure the character index is valid
        if (charIndex >= textInfo.characterCount)
            yield break;

        TMP_CharacterInfo charInfo = textInfo.characterInfo[charIndex];

        // Skip characters that are not visible (like spaces)
        if (!charInfo.isVisible)
            yield break;

        int materialIndex = charInfo.materialReferenceIndex;
        int vertexIndex = charInfo.vertexIndex;

        // Get the vertices for the specific character
        Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

        float elapsedTime = 0f;
        Vector3 startPosition = originalVerticesPerCharacter[charIndex][0];
        Vector3 endPosition = startPosition - new Vector3(0, fallDistance, 0); // Fall distance downwards

        while (elapsedTime < lifetime)
        {
            // Lerp the position of each vertex
            float t = elapsedTime / lifetime;
            Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, t);

            // Update all 4 vertices for the character
            Vector3 fallOffset = currentPosition - originalVerticesPerCharacter[charIndex][0];
            vertices[vertexIndex + 0] = originalVerticesPerCharacter[charIndex][0] + fallOffset;
            vertices[vertexIndex + 1] = originalVerticesPerCharacter[charIndex][1] + fallOffset;
            vertices[vertexIndex + 2] = originalVerticesPerCharacter[charIndex][2] + fallOffset;
            vertices[vertexIndex + 3] = originalVerticesPerCharacter[charIndex][3] + fallOffset;

            // Update the mesh with the new vertex positions
            textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Once the fall is complete, you can apply other effects here.
    }
}
