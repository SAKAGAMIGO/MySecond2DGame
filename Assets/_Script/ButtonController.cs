using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    // Inspector���犄�蓖�Ă�GameObject��ێ�����ϐ�
    [SerializeField] GameObject stage1Button;
    [SerializeField] GameObject stage2Button;
    [SerializeField] GameObject stage3Button;
    [SerializeField] GameObject stage4Button;
    [SerializeField] GameObject stage5Button;
    [SerializeField] GameObject stage6Button;

    // �e�I�u�W�F�N�g���A�N�e�B�u�ɂ���܂ł̒x�����ԁi�b�j
    public float delayBetweenActivations = 1f;

    // �{�^���̃N���b�N�ł��̃��\�b�h���Ăяo��
    public void StartActivatingObjects()
    {
        // �R���[�`�����J�n
        StartCoroutine(ActivateObjects());
    }

    private System.Collections.IEnumerator ActivateObjects()
    {   
        // �w�肵�����Ԃ����҂�
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
