using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using DG.Tweening;
using GameFramework.Event;

public class CharacterUILogic : UIFormLogic
{
    private RoleData m_role;
    private MonsterData m_monster;

    private Image HPBar_Role;
    private Text HPText_Role;

    private Image HpBar_Monster;
    private Text HPText_Monster;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        HPBar_Role = transform.Find("HPBG_Role").Find("HPBar").GetComponent<Image>();
        HPText_Role = transform.Find("HPBG_Role").Find("HPText").GetComponent<Text>();
        HpBar_Monster = transform.Find("HPBG_Enemy").Find("HPBar").GetComponent<Image>();
        HPText_Monster = transform.Find("HPBG_Enemy").Find("HPText").GetComponent<Text>();
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData); 

        m_role = GameEntry.DataNode.GetData<VarRoleData>(Definition.Node.RoleNode).Value;
        m_monster = GameEntry.DataNode.GetData<VarMonsterData>(Definition.Node.MonsterNode).Value;

        GameEntry.Event.Subscribe(UpdateUIEvent.EventId, UpdateUI);
        GameEntry.Event.Fire(this, UpdateUIEvent.Create());

    }

    private void UpdateUI(object sender, GameEventArgs e)
    {
        HPBar_Role.fillAmount = m_role.CurHP * 1.0f / m_role.HPMax;
        HPText_Role.text = string.Format("{0}/{1}", m_role.CurHP, m_role.HPMax);
        HpBar_Monster.fillAmount = m_monster.CurHP * 1.0f / m_monster.HPMax;
        HPText_Monster.text = string.Format("{0}/{1}", m_monster.CurHP, m_monster.HPMax);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
        GameEntry.Event.Unsubscribe(UpdateUIEvent.EventId, UpdateUI);
    }

}
