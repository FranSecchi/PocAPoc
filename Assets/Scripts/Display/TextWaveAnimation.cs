using System.Collections;
using UnityEngine;
using TMPro;
public class TextWaveAnimation : MonoBehaviour
{
    private TextMeshPro textMeshPro; // Reference to the TextMesh Pro component
    public float waveFrequency = 2f; // Frequency of the wave
    public float waveAmplitude = 10f; // Amplitude of the wave
    public bool fade = false;
    private void Start()
    {
        waveAmplitude = GameManager.Parameter.WaveAmplitude;
        waveFrequency = GameManager.Parameter.WaveFrequency;
        textMeshPro = GetComponent<TextMeshPro>();
        // Start the wave animation coroutine
        StartCoroutine(AnimateTextWave());
        //if (fade) StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {
        float duration = GameManager.Parameter.TimeToFadeWord;
        float elapsedTime = 0f;
        Color startColor = textMeshPro.color;
        yield return new WaitForSeconds(1f);
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Color currentColor = startColor;
            currentColor.a = Mathf.Lerp(1f, 0.5f, t);
            textMeshPro.color = currentColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textMeshPro.text = ""; // Optionally clear the text after fading out
    }
    private IEnumerator AnimateTextWave()
    {
        string originalText = textMeshPro.text;
        textMeshPro.text = ""; // Clear the text initially

        // Add each character to the TextMesh Pro component
        for (int i = 0; i < originalText.Length; i++)
        {
            textMeshPro.text += originalText[i];
            textMeshPro.ForceMeshUpdate(); // Force the mesh to update to get the correct text info
        }

        float fadeStartTime = GameManager.Parameter.TimeToFadeWord / 2f; // Start fading after half of total duration
        float fadeDuration = GameManager.Parameter.TimeToFadeWord;       // Full duration for fade effect
        float elapsedTime = 0f;

        // Get the original color of the text (assuming all characters start with the same color)
        while (elapsedTime < fadeDuration)
        {
            textMeshPro.ForceMeshUpdate();
            TMP_TextInfo textInfo = textMeshPro.textInfo;

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
                if (!charInfo.isVisible) continue;

                // Calculate wave offset for animation
                float waveOffset = Mathf.Sin((Time.time + i * 0.1f) * waveFrequency) * waveAmplitude;

                // Modify the position of the character
                Vector3[] vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
                Vector3 offset = new Vector3(0, waveOffset, 0);
                vertices[charInfo.vertexIndex] += offset;
                vertices[charInfo.vertexIndex + 1] += offset;
                vertices[charInfo.vertexIndex + 2] += offset;
                vertices[charInfo.vertexIndex + 3] += offset;

                // Calculate fade effect after fade start time has passed
                Color32[] colors = textInfo.meshInfo[charInfo.materialReferenceIndex].colors32;
                if (elapsedTime >= fadeStartTime)
                {
                    float fadeT = Mathf.Clamp01((elapsedTime - fadeStartTime) / (fadeDuration - fadeStartTime));
                    byte alpha = (byte)Mathf.Lerp(255, 0, fadeT);

                    // Only change the alpha of each character's current color
                    colors[charInfo.vertexIndex].a = alpha;
                    colors[charInfo.vertexIndex + 1].a = alpha;
                    colors[charInfo.vertexIndex + 2].a = alpha;
                    colors[charInfo.vertexIndex + 3].a = alpha;
                }
            }

            // Update the mesh to apply changes
            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                textInfo.meshInfo[i].mesh.colors32 = textInfo.meshInfo[i].colors32;
                textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textMeshPro.text = ""; // Optionally clear the text after fading out completely
    }
}