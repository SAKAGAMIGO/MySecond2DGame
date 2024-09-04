using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeMode : MonoBehaviour
{
    [SerializeField] private Image panelImage; // �p�l����Image�R���|�[�l���g
    [SerializeField] private float fadeDuration = 2.0f; // �t�F�[�h�ɂ����鎞��

    private void Start()
    {
        // �����̃A���t�@�l��0�ɐݒ�
        Color color = panelImage.color;
        color.a = 0f;
        panelImage.color = color;


    }

    public void AA()
    {
        // �t�F�[�h�C�����J�n
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = panelImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // �A���t�@�l��0����1�܂ŏ��X�ɑ��₷
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            panelImage.color = color;

            yield return null;
        }

        // �ŏI�I�ɃA���t�@�l��1�ɐݒ�i�m���ɍő�ɂ��邽�߁j
        color.a = 1f;
        panelImage.color = color;
    }
}
