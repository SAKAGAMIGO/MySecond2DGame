using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    // Inspectorから割り当てるGameObjectを保持する変数
    [SerializeField] GameObject stage1Button;
    [SerializeField] GameObject stage2Button;
    [SerializeField] GameObject stage3Button;
    [SerializeField] GameObject stage4Button;
    [SerializeField] GameObject stage5Button;
    [SerializeField] GameObject stage6Button;

    // 各オブジェクトをアクティブにするまでの遅延時間（秒）
    public float delayBetweenActivations = 1f;

    // ボタンのクリックでこのメソッドを呼び出す
    public void StartActivatingObjects()
    {
        // コルーチンを開始
        StartCoroutine(ActivateObjects());
    }

    private System.Collections.IEnumerator ActivateObjects()
    {   
        // 指定した時間だけ待つ
        yield return new WaitForSeconds(delayBetweenActivations);

    }

    public void Stage1Wakeup()
    {
        stage1Button.SetActive(true);
    }
    public void Stage2Wakeup()
    {
        stage2Button.SetActive(true);
    }
    public void Stage3Wakeup()
    {
        stage3Button.SetActive(true);
    }
    public void Stage4Wakeup()
    {
        stage4Button.SetActive(true);
    }
    public void Stage5Wakeup()
    {
        stage5Button.SetActive(true);
    }
    public void Stage6Wakeup()
    {
        stage6Button.SetActive(true);
    }
}
