using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.UI;
using GameFramework.Event;
using System;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    private Transform StateTF;
    private Image BloodIcon;
    private Image ComaIcon;
    private Image ElectrocutedIcon;
    private Image FrezonIcon;
    private Image PoisoningIcon;

    private Image HPBar;
    private Text HPText;

    private int unitID;

    private void Awake()
    {
        StateTF = transform.Find("State");
        BloodIcon = StateTF.Find("blood").GetComponent<Image>();
        ComaIcon = StateTF.Find("coma").GetComponent<Image>();
        ElectrocutedIcon = StateTF.Find("electrocuted").GetComponent<Image>();
        FrezonIcon = StateTF.Find("frezon").GetComponent<Image>();
        PoisoningIcon = StateTF.Find("poisoning").GetComponent<Image>();

        HPBar = transform.Find("HPBar").Find("HPBar").GetComponent<Image>();
        HPText = transform.Find("HPBar").Find("HPText").GetComponent<Text>();
    }

    public void SetUnitHP(int curHP ,int maxHP)
    {
        HPBar.fillAmount = curHP * 1.0f / maxHP;
        HPText.text = string.Format("{0}/{1}", curHP, maxHP);
    }
}
