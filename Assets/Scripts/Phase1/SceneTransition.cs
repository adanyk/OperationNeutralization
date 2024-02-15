using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public float fadeDuration = 1.5f;
    public int sceneToLoad = 1;
    private Image fadeImage;
    private GameObject canvasGameObject;

    private void Start()
    {
        InitializeFadeImage();
        StartChangeSceneWithLightingEffect();
    }

    private void InitializeFadeImage()
    {
        // Create a new GameObject for the Canvas
        canvasGameObject = new GameObject("Canvas");
        Canvas canvas = canvasGameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler canvasScaler = canvasGameObject.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        // Create a new GameObject for the Image
        GameObject imageGameObject = new GameObject("FadeImage");
        imageGameObject.transform.SetParent(canvasGameObject.transform);
        fadeImage = imageGameObject.AddComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0); // Start fully transparent

        // Set the image to cover the entire screen
        fadeImage.rectTransform.anchorMin = new Vector2(0, 0);
        fadeImage.rectTransform.anchorMax = new Vector2(1, 1);
        fadeImage.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        fadeImage.rectTransform.offsetMin = Vector2.zero; // Left-bottom
        fadeImage.rectTransform.offsetMax = Vector2.zero; // Right-top
    }

    private void StartChangeSceneWithLightingEffect()
    {
        StartCoroutine(ChangeSceneWithLightingEffect());
    }

    private IEnumerator ChangeSceneWithLightingEffect()
    {
        // Fade to black
        float timer = 0f;
        Color startColor = new Color(0, 0, 0, 0);
        Color endColor = new Color(0, 0, 0, 1);

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            yield return null;
        }

        // Once the screen is black, load the new scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
