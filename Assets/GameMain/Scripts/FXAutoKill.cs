using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.Entity;

public class FXAutoKill : EntityLogic
{
    private SpriteRenderer m_sprite;
    private FXData m_FXData;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        m_sprite = GetComponent<SpriteRenderer>();

    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        if(userData != null)
        {
            m_FXData = userData as FXData;
            m_sprite.flipX = m_FXData.FlipX;
            m_sprite.flipY = m_FXData.FlipY;
            transform.position = m_FXData.Positon;
        }
    }    


    public void AutoKill()
    {
        GameEntry.Entity.HideEntity(Entity.Id);
    }
}
