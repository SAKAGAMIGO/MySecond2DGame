using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// <summary>���ʑ��x</summary>
    public float DieVelocity = 10;

    GameController gameController;

    [SerializeField] GameObject m_effect = default;
    [SerializeField] GameObject m_scoreTextPrefab = default; // �X�R�A�e�L�X�g�̃v���n�u
    [SerializeField] Canvas m_canvas = default; // �X�R�A�e�L�X�g��\������L�����o�X

    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    /// <summary>�Փ˃C�x���g</summary><param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DieVelocity -= collision.relativeVelocity.sqrMagnitude;

        if (collision.relativeVelocity.sqrMagnitude > DieVelocity)
        {
            SceneChenge.AddScore(1000);
            ShowScoreText(1000); // �X�R�A�e�L�X�g��\��
            Hit();
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        gameController.EnemyScore();
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

            // ���������e�L�X�g�̈ʒu�����[���h���W����X�N���[�����W�֕ϊ�
            Vector3 worldPosition = this.transform.position;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            scoreText.transform.position = screenPosition;

            // �e�L�X�g�̓��e��ݒ�
            var textComponent = scoreText.GetComponent<UnityEngine.UI.Text>();
            if (textComponent != null)
            {
                textComponent.text = $"+{score}";
            }

            // ScoreTextController ��ǉ����Đ���
            scoreText.AddComponent<ScoreTextController>();
        }
    }
}
