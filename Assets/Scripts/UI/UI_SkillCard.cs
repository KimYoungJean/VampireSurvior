using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillCard : UI_Base
{
    // 몇레벨 스킬인지
    // 스킬 이름
    // 스킬 설명
    // 스킬 이미지

    int _templateID;
    Data.SkillData _skillData;

    public void SetInfo(int templateID)
    {
        _templateID = templateID;
        DataManager.Instance.SkillDic.TryGetValue(_templateID, out _skillData);

    }

    public void OnClick()
    {
        //스킬레벨 업그레이드 코드
        Debug.Log("스킬레벨 업그레이드");
        UImanager.instance.ClosePopup();
    }

}
