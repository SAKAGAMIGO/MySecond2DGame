    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    public Button stageButton; // �{�^��
    public CharacterMover characterMover; // �L�����N�^�[�̃X�N���v�g�Q��
    public Transform stagePosition; // �X�e�[�W�̈ʒu

    void Start()
    {
        // �{�^�����N���b�N���ꂽ�Ƃ��ɃL�����N�^�[���ړ�������
        stageButton.onClick.AddListener(() => characterMover.MoveTo(stagePosition.position));
    }
}
