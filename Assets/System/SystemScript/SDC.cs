using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindObjectOfType<ScoreDisplay>() != null)
        {
            Debug.Log("ScoreDisplay ����");
        }
        else
        {
            Debug.LogWarning("ScoreDisplay �Ȃ�");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
