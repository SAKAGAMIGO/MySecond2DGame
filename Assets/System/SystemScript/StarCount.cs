using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCount : MonoBehaviour
{
    [SerializeField, Tooltip("スター表示のためのノルマスコア1")] int _quotaScoreOne;
    [SerializeField, Tooltip("スター表示のためのノルマスコア2")] int _quotaScoreTwo;
    [SerializeField, Tooltip("スター表示のためのノルマスコア3")] int _quotaScoreThree;

    [SerializeField] GameObject _starOne;
    [SerializeField] GameObject _starTwo;
    [SerializeField] GameObject _starThree;

    private void Start()
    {
        StartCoroutine(StarAnim());
    }

    IEnumerator StarAnim()
    {
        if (ScoreManager._score < _quotaScoreOne)
        {
            yield break;
        }
        _starOne.GetComponent<Animator>().Play("StarScore_1");
        yield return new WaitForSeconds(0.5f);
        if (ScoreManager._score < _quotaScoreTwo)
        {
            yield break;
        }
        _starTwo.GetComponent<Animator>().Play("StarScore_2");
        yield return new WaitForSeconds(0.5f);
        if (ScoreManager._score < _quotaScoreThree)
        {
            yield break;
        }
        _starThree.GetComponent<Animator>().Play("StarScore_3");
    }
}
