using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    private static FadeController instance = default;
    [Tooltip("フェードスピード")]
    [SerializeField]
    private float _fadeSpeed = 1f;
    [Tooltip("フェードする画像")]
    [SerializeField]
    private Image _fadeImage = default;
    [Tooltip("開始時の色")]
    [SerializeField]
    private Color _startColor = Color.black;
    private void Awake()
    {
        instance = this;
        _fadeImage.gameObject.SetActive(false);
    }
    /// <summary>
    /// フェードインする
    /// </summary>
    public static void StartFadeIn()
    {
        instance?.StartCoroutine(instance.FadeIn(() => { }));
    }
    /// <summary>
    /// フェードアウトする
    /// </summary>
    public static void StartFadeOut()
    {
        instance?.StartCoroutine(instance.FadeOut(() => { }));
    }
    /// <summary>
    /// フェードイン後にアクションする
    /// </summary>
    /// <param name="action"></param>
    public static void StartFadeIn(Action action)
    {
        if (instance == null)
        {
            action?.Invoke();
            return;
        }
        instance.StartCoroutine(instance.FadeIn(action));
    }
    /// <summary>
    /// フェードアウト後にアクションする
    /// </summary>
    /// <param name="action"></param>
    public static void StartFadeOut(Action action)
    {
        if (instance == null)
        {
            action?.Invoke();
            return;
        }
        instance.StartCoroutine(instance.FadeOut(action));
    }
    IEnumerator FadeIn(Action action)
    {
        _fadeImage.gameObject.SetActive(true);
        yield return FadeIn();
        action?.Invoke();
        _fadeImage.gameObject.SetActive(false);
    }
    IEnumerator FadeOut(Action action)
    {
        _fadeImage.gameObject.SetActive(true);
        yield return FadeOut();
        action?.Invoke();
    }
    IEnumerator FadeIn()
    {
        float clearScale = 1f;
        Color currentColor = _startColor;
        while (clearScale > 0f)
        {
            clearScale -= _fadeSpeed * Time.deltaTime;
            if (clearScale <= 0f)
            {
                clearScale = 0f;
            }
            currentColor.a = clearScale;
            _fadeImage.color = currentColor;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float clearScale = 0f;
        Color currentColor = _startColor;
        while (clearScale < 1f)
        {
            clearScale += _fadeSpeed * Time.deltaTime;
            if (clearScale >= 1f)
            {
                clearScale = 1f;
            }
            currentColor.a = clearScale;
            _fadeImage.color = currentColor;
            yield return null;
        }
    }
}
