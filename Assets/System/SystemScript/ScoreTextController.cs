using UnityEngine;
using UnityEngine.UI;

public class ScoreTextController : MonoBehaviour
{
    float duration = 1.0f; // 表示時間
    float moveSpeed = 100f; // 移動速度
    float elapsedTime = 0f;

    Text textComponent;

    private void Awake()
    {
        textComponent = GetComponent<Text>();
    }

    private void Update()
    {
        // 上方向に移動
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // 徐々に透明になる
        if (textComponent != null)
        {
            Color color = textComponent.color;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            textComponent.color = color;
        }

        // 時間を計測
        elapsedTime += Time.deltaTime;

        // 指定時間経過後に削除
        if (elapsedTime >= duration)
        {
            Destroy(gameObject);
        }
    }
}
