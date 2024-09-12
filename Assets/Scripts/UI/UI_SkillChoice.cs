using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillChoice : UI_Base
{
    [SerializeField]
    Transform SkillChoiceBox;

    List<UI_SkillCard> Cards = new List<UI_SkillCard>();


    private void Start()
    {
        FillBox();
    }
    void FillBox()
    {
        foreach (Transform transform in SkillChoiceBox)
        {
            ResourceManager.Instance.Destroy(transform.gameObject);
        }

        for (int i = 0; i < 3; i++)
        {
            string key = "UI_SkillCard.prefab";
            var go = ResourceManager.Instance.Instantiate(key, SkillChoiceBox, pooling: false);
            UI_SkillCard card = go.GetOrAddComponent<UI_SkillCard>();

            Cards.Add(card);
        }
    }
}
