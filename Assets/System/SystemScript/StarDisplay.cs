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
        // スコアが確定した後にアニメーションを開始する
        StartCoroutine(StarAnim());
    }

    IEnumerator StarAnim()
    {
        // クオタスコアを参照
        int quotaScore = _quotaScore[SceneChenge._stageNumber];
        Debug.Log(quotaScore);

        // スコアに基づいた星の獲得
        if (ScoreDisplay._resultScore > quotaScore / 3)
        {
            Debug.Log("Star1獲得");
            _starOne.GetComponent<Animator>().Play("StarScore_1");
        }
        yield return new WaitForSeconds(1f);

        if (ScoreDisplay._resultScore > quotaScore / 2)
        {
            Debug.Log("Star2獲得");
            _starTwo.GetComponent<Animator>().Play("StarScore_2");
        }
        yield return new WaitForSeconds(1f);

        if (ScoreDisplay._resultScore > quotaScore)
        {
            _starThree.GetComponent<Animator>().Play("StarScore_3");
            Debug.Log("Star3獲得");
        }
    }
}
