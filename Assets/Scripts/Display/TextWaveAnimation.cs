using System.Collections;
using UnityEngine;
using TMPro;
public class TextWaveAnimation : MonoBehaviour
{
    private TextMeshPro textMeshPro; // Reference to the TextMesh Pro component
    public float waveFrequency = 2f; // Frequency of the wave
    public float waveAmplitude = 10f; // Amplitude of the wave

    private void Start()
    {
        waveAmplitude = GameManager.Parameters.WaveAmplitude;
        waveFrequency = GameManager.Parameters.WaveFrequency;
        textMeshPro = GetComponent<TextMeshPro>();
        // Start the wave animation coroutine
        StartCoroutine(AnimateTextWave());
    }

    private IEnumerator AnimateTextWave()
    {
        // Store the original text
        string originalText = textMeshPro.text;
        textMeshPro.text = ""; // Clear the text initially

        // Add each character to the TextMesh Pro component
        for (int i = 0; i < originalText.Length; i++)
        {
            textMeshPro.text += originalText[i];
            textMeshPro.ForceMeshUpdate(); // Force the mesh to update to get the correct text info
        }

        // Animate the letters
        while (true)
        {
            textMeshPro.ForceMeshUpdate();
            TMP_TextInfo textInfo = textMeshPro.textInfo;

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                // Get the character info
                TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

                // Calculate the wave offset
                float waveOffset = Mathf.Sin((Time.time + i * 0.1f) * waveFrequency) * waveAmplitude;

                // Modify the position of the character
                Vector3 charPosition = charInfo.bottomLeft;
                charPosition.y += waveOffset;

                // Update the character's vertex positions
                Vector3[] vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                // Update vertices to apply the wave effect
                vertices[charInfo.vertexIndex] += new Vector3(0, waveOffset, 0);
                vertices[charInfo.vertexIndex + 1] += new Vector3(0, waveOffset, 0);
                vertices[charInfo.vertexIndex + 2] += new Vector3(0, waveOffset, 0);
                vertices[charInfo.vertexIndex + 3] += new Vector3(0, waveOffset, 0);
            }

            // Update the mesh to apply the changes
            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                textInfo.meshInfo[i].mesh.RecalculateBounds();
                textInfo.meshInfo[i].mesh.RecalculateNormals();
            }

            // Wait for the next frame
            yield return null;
        }
    }
}