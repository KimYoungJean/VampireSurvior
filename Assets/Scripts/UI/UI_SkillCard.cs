using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillCard : UI_Base
{
    // ��� ��ų����
    // ��ų �̸�
    // ��ų ����
    // ��ų �̹���

    int _templateID;
    Data.SkillData _skillData;

    public void SetInfo(int templateID)
    {
        _templateID = templateID;
        DataManager.Instance.SkillDic.TryGetValue(_templateID, out _skillData);

    }

    public void OnClick()
    {
        //��ų���� ���׷��̵� �ڵ�
        Debug.Log("��ų���� ���׷��̵�");
        UImanager.instance.ClosePopup();
    }

}
