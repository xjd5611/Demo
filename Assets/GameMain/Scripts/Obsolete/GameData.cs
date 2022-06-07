using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GameData 
{
    public RoleData m_Role;
    public MstData m_Monster;

    public Dictionary<CharacterData, bool> m_actDic;
    public CharacterData actCharacter { get; private set; }

    public GameData(RoleData m_Role, MstData m_Monster)
    {
        this.m_Role = m_Role;
        this.m_Monster = m_Monster;
        
        actCharacter = m_Role;

        m_actDic = new Dictionary<CharacterData, bool>();
        m_actDic.Add(m_Role, false);
        m_actDic.Add(m_Monster, false);
    }

    public void SelectActor()
    {
        if (!m_actDic.ContainsValue(false))
        {
            m_actDic[m_Role] = false;
            m_actDic[m_Monster] = false;
        }

        foreach (var actor in m_actDic)
        {
            if(actor.Value == false)
            {
                actCharacter = actor.Key;
            }
        }                
        m_actDic[actCharacter] = true;

        GameEntry.Event.Fire(this, ActorSelectedEvent.Create());
    }

    public bool isRoleTurn()
    {
        return actCharacter == m_Role;
    }
}

public class VarGameData : Variable<GameData>
{
    public VarGameData(RoleData m_Role, MstData m_Monster)
    {
        Value = new GameData(m_Role, m_Monster);
    }

    /// <summary>
    /// 初始化构建信息变量类的新实例。
    /// </summary>
    /// <param name="value">值。</param>
    public VarGameData(GameData value)
    {
        Value = value;
    }

    /// <summary>
    /// 从构建信息类到构建信息变量类的隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator VarGameData(GameData value)
    {
        return new VarGameData(value);
    }

    /// <summary>
    /// 从构建信息变量类到构建信息类的隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator GameData(VarGameData value)
    {
        return value.Value;
    }
}