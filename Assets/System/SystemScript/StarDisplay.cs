using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDisplay : MonoBehaviour
{
    [SerializeField] GameObject _starOne;
    [SerializeField] GameObject _starTwo;
    [SerializeField] GameObject _starThree;

    [SerializeField] public int[] _quotaScore;

    private void Start()
    {
        // �X�R�A���m�肵����ɃA�j���[�V�������J�n����
        StartCoroutine(StarAnim());
    }

    IEnumerator StarAnim()
    {
        // �N�I�^�X�R�A���Q��
        int quotaScore = _quotaScore[SceneChenge._stageNumber];
        Debug.Log(quotaScore);

        // �X�R�A�Ɋ�Â������̊l��
        if (ScoreDisplay._resultScore > quotaScore / 3)
        {
            Debug.Log("Star1�l��");
            _starOne.GetComponent<Animator>().Play("StarScore_1");
        }
        yield return new WaitForSeconds(1f);

        if (ScoreDisplay._resultScore > quotaScore / 2)
        {
            Debug.Log("Star2�l��");
            _starTwo.GetComponent<Animator>().Play("StarScore_2");
        }
        yield return new WaitForSeconds(1f);

        if (ScoreDisplay._resultScore > quotaScore)
        {
            _starThree.GetComponent<Animator>().Play("StarScore_3");
            Debug.Log("Star3�l��");
        }
    }
}
