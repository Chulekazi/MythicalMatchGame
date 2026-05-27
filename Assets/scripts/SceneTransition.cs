using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour

{
    [Header("Fade Settings")]
    public Image fadeOverlay;          // Fullscreen UI Image
    public float fadeDuration = 1f;    // Seconds for fade
    public string mainMenuSceneName = "MainMenu";

    void Start()
    {
        if (fadeOverlay != null)
        {
            // Start fully transparent
            fadeOverlay.color = new Color(0, 0, 0, 0);
            fadeOverlay.raycastTarget = false; // Prevent blocking clicks
        }
    }

    public void FadeToMainMenu()
    {
        StartCoroutine(FadeOutAndLoad());
    }

    IEnumerator FadeOutAndLoad()
    {
        float elapsed = 0f;
        Color overlayColor = fadeOverlay.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            fadeOverlay.color = new Color(overlayColor.r, overlayColor.g, overlayColor.b, alpha);
            yield return null;
        }

        SceneManager.LoadScene(mainMenuSceneName);
    }
}
