using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeMode : MonoBehaviour
{
    [SerializeField] private Image panelImage; // パネルのImageコンポーネント
    [SerializeField] private float fadeDuration = 2.0f; // フェードにかける時間

    private void Start()
    {
        // 初期のアルファ値を0に設定
        Color color = panelImage.color;
        color.a = 0f;
        panelImage.color = color;


    }

    public void AA()
    {
        // フェードインを開始
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = panelImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // アルファ値を0から1まで徐々に増やす
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            panelImage.color = color;

            yield return null;
        }

        // 最終的にアルファ値を1に設定（確実に最大にするため）
        color.a = 1f;
        panelImage.color = color;
    }
}
