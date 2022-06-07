using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VarMstsData : Variable<MstData[]>
{
    public VarMstsData()
    {

    }

    public VarMstsData(MstData[] value)
    {
        this.Value = value;
    }


    /// <summary>
    /// �ӹ�����Ϣ�ൽ������Ϣ���������ʽת����
    /// </summary>
    /// <param name="value">ֵ��</param>
    public static implicit operator VarMstsData(MstData[] value)
    {
        VarMstsData varValue = ReferencePool.Acquire<VarMstsData>();
        varValue.Value = value;
        return varValue;
    }

    /// <summary>
    /// �ӹ�����Ϣ�����ൽ������Ϣ�����ʽת����
    /// </summary>
    /// <param name="value">ֵ��</param>
    public static implicit operator MstData[](VarMstsData value)
    {
        return value.Value;
    }
}
