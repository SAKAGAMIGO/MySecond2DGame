using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{
    /// <summary>���ʑ��x</summary>
    public float DieVelocity = 20;

    [SerializeField] private GameObject m_effect = default;
    [SerializeField] private GameObject m_scoreTextPrefab = default; // �X�R�A�e�L�X�g�̃v���n�u
    [SerializeField] private Canvas m_canvas = default;             // �X�R�A�e�L�X�g��\������Canvas

    /// <summary>�Փ˃C�x���g</summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DieVelocity -= collision.relativeVelocity.sqrMagnitude;

        if (DieVelocity <= 8f)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

        if (collision.relativeVelocity.sqrMagnitude > DieVelocity)
        {
            Hit();
        }
    }

    public void OnDestroy()
    {
        ScoreDisplay.AddScore(500);
        ShowScoreText(500); // �X�R�A�e�L�X�g��\��
    }

    private void Hit()
    {
        if (m_effect)
        {
            Instantiate(m_effect, this.transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }

    private void ShowScoreText(int score)
    {
        if (m_scoreTextPrefab && m_canvas)
        {
            // �X�R�A�e�L�X�g�𐶐����ACanvas �̎q�I�u�W�F�N�g�ɐݒ�
            GameObject scoreText = Instantiate(m_scoreTextPrefab, m_canvas.transform);

            // ���������e�L�X�g�̈ʒu���u���b�N�̃��[���h���W����X�N���[�����W�֕ϊ�
            Vector3 worldPosition = this.transform.position;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            scoreText.transform.position = screenPosition;

            // �e�L�X�g�̓��e��ݒ�
            var textComponent = scoreText.GetComponent<Text>();
            if (textComponent != null)
            {
                textComponent.text = $"+{score}";
            }

            // ScoreTextController ��ǉ����Đ���
            scoreText.AddComponent<ScoreTextController>();
        }
    }
}
