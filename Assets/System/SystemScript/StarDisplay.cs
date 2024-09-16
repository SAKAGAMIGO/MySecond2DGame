using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDisplay : MonoBehaviour
{ 
    [SerializeField] GameObject _starOne;
    [SerializeField] GameObject _starTwo;
    [SerializeField] GameObject _starThree;

    private void Start()
    {
        StartCoroutine(StarAnim());
    }

    IEnumerator StarAnim()
    {
        if (SceneChenge._score < Star._quotaScoreOne)
        {
            yield break;
        }
        _starOne.GetComponent<Animator>().Play("StarScore_1");
        yield return new WaitForSeconds(0.5f);
        if (SceneChenge._score < Star._quotaScoreTwo)
        {
            yield break;
        }
        _starTwo.GetComponent<Animator>().Play("StarScore_2");
        yield return new WaitForSeconds(0.5f);
        if (SceneChenge._score < Star._quotaScoreThree)
        {
            yield break;
        }
        _starThree.GetComponent<Animator>().Play("StarScore_3");
    }
}
