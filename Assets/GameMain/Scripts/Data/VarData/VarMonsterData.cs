using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VarMonsterData : Variable<MonsterData>
{
    public VarMonsterData()
    {

    }

    public VarMonsterData(MonsterData value)
    {
        this.Value = value;
    }


    /// <summary>
    /// �ӹ�����Ϣ�ൽ������Ϣ���������ʽת����
    /// </summary>
    /// <param name="value">ֵ��</param>
    public static implicit operator VarMonsterData(MonsterData value)
    {
        VarMonsterData varValue = ReferencePool.Acquire<VarMonsterData>();
        varValue.Value = value;
        return varValue;
    }

    /// <summary>
    /// �ӹ�����Ϣ�����ൽ������Ϣ�����ʽת����
    /// </summary>
    /// <param name="value">ֵ��</param>
    public static implicit operator MonsterData(VarMonsterData value)
    {
        return value.Value;
    }
}
