using UnityEngine;
using UnityEngine.UI;

public class ScoreTextController : MonoBehaviour
{
    float duration = 1.0f; // �\������
    float moveSpeed = 100f; // �ړ����x
    float elapsedTime = 0f;

    Text textComponent;

    private void Awake()
    {
        textComponent = GetComponent<Text>();
    }

    private void Update()
    {
        // ������Ɉړ�
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // ���X�ɓ����ɂȂ�
        if (textComponent != null)
        {
            Color color = textComponent.color;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            textComponent.color = color;
        }

        // ���Ԃ��v��
        elapsedTime += Time.deltaTime;

        // �w�莞�Ԍo�ߌ�ɍ폜
        if (elapsedTime >= duration)
        {
            Destroy(gameObject);
        }
    }
}
