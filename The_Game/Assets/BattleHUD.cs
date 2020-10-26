using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;
    public Text hpIndicator;


    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl" + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        SetHP(unit.currentHP, unit.maxHP);
    }

    public void SetHP(float hp, float maxHP)
    {
        hpSlider.value = hp;
        hpIndicator.text = "HP: " + Mathf.Ceil(hp) + "/" + (int)maxHP;
    }

}
 