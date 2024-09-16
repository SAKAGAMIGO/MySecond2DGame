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
        StartCoroutine(StarAnim());
    }

    IEnumerator StarAnim()
    {
        int quotaScore = _quotaScore[SceneChenge._stageNumber];
        Debug.Log(quotaScore);
        if (SceneChenge._score > quotaScore / 3)
        {
            Debug.Log("Star1Šl“¾");
        _starOne.GetComponent<Animator>().Play("StarScore_1");
        }
        yield return new WaitForSeconds(0.5f);
        if (SceneChenge._score > quotaScore / 2)
        {
            Debug.Log("Star2Šl“¾");
        _starTwo.GetComponent<Animator>().Play("StarScore_2");
            
        }
        yield return new WaitForSeconds(0.5f);
        if (SceneChenge._score > quotaScore / 1)
        {
        _starThree.GetComponent<Animator>().Play("StarScore_3");
            Debug.Log("Star3Šl“¾");
        }
    }
}
