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
    private MstData[] m_msts;

    private GameObject m_RoleUI;
    private GameObject[] m_mstsUI;

    private GameObject tagetArrow;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        m_RoleUI = transform.Find("UnitUI").gameObject;
        tagetArrow = transform.Find("TargetArrow").gameObject;
        tagetArrow.SetActive(false);
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData); 

        m_role = GameEntry.DataNode.GetData<VarRoleData>(Definition.Node.RoleNode).Value;
        m_msts = GameEntry.DataNode.GetData<VarMstsData>(Definition.Node.MstsNode).Value;

        UnitUIInit();

        GameEntry.Event.Subscribe(MouseEnterMst.EventId, OnMouseEnterMst);
        GameEntry.Event.Subscribe(MouseExitMst.EventId, OnMouseExitMst);
        GameEntry.Event.Subscribe(MouseOverMst.EventId, OnMouseOverMst);
        GameEntry.Event.Subscribe(UpdateUnitUIEvent.EventId, UpdateUnitUI);
        GameEntry.Event.Fire(this, UpdateUnitUIEvent.Create());

    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);

        for (int i = 0; i < m_mstsUI.Length; i++)
        {
            Destroy(m_mstsUI[i].gameObject);
        }

        GameEntry.Event.Unsubscribe(MouseEnterMst.EventId, OnMouseEnterMst);
        GameEntry.Event.Unsubscribe(MouseExitMst.EventId, OnMouseExitMst);
        GameEntry.Event.Unsubscribe(MouseOverMst.EventId, OnMouseOverMst);
        GameEntry.Event.Unsubscribe(UpdateUnitUIEvent.EventId, UpdateUnitUI);
    }


    private void OnMouseExitMst(object sender, GameEventArgs e)
    {
        tagetArrow.SetActive(false);
    }

    private void OnMouseEnterMst(object sender, GameEventArgs e)
    {
        tagetArrow.SetActive(true);
    }

    private void OnMouseOverMst(object sender, GameEventArgs e)
    {
        Vector3 posInUI = Camera.main.WorldToScreenPoint(((GameObject)sender).transform.position + new Vector3(0,2,0));
        tagetArrow.transform.position = posInUI;
    }

    private void UnitUIInit()
    {
        m_RoleUI.transform.position = WorldToUGUIPosition(GetComponent<RectTransform>(), m_role.Position) - new Vector2(0, 150);
        m_mstsUI = new GameObject[m_msts.Length];
        for (int i = 0; i < m_msts.Length; i++)
        {
            m_mstsUI[i] = Instantiate(m_RoleUI, transform);
            m_mstsUI[i].transform.position = WorldToUGUIPosition(GetComponent<RectTransform>(), m_msts[i].Position) - new Vector2(0, 150);
        }
    }

    private Vector3 WorldPointToUILocalPoint(Vector3 point)
    {
        //将世界坐标转为屏幕坐标
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(point);

        //将屏幕坐标转换到RectTransform的局部坐标中
        Vector2 uiPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.GetComponent<RectTransform>(), screenPoint, Camera.main, out uiPosition);
        return uiPosition;
    }

    private Vector2 WorldToUGUIPosition(RectTransform rectTransform ,Vector3 worldPos)
    {
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);
        return new Vector2(rectTransform.rect.width * viewPos.x,
            rectTransform.rect.height * viewPos.y);
    }

    private void UpdateUnitUI(object sender, GameEventArgs e)
    {
        m_RoleUI.GetComponent<UnitUI>().SetUnitHP(m_role.CurHP, m_role.HPMax);
        for (int i = 0; i < m_mstsUI.Length; i++)
        {
            m_mstsUI[i].GetComponent<UnitUI>().SetUnitHP(m_msts[i].CurHP, m_msts[i].HPMax);
        }
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
    }



}
