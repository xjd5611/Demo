using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VarRoleData : Variable<RoleData>
{
    /// <summary>
    /// ��ʼ��������Ϣ���������ʵ����
    /// </summary>
    public VarRoleData()
    {

    }

    /// <summary>
    /// ��ʼ��������Ϣ���������ʵ����
    /// </summary>
    /// <param name="value">ֵ��</param>
    public VarRoleData(RoleData value)
    {
        this.Value = value;
    }

    /// <summary>
    /// �ӹ�����Ϣ�ൽ������Ϣ���������ʽת����
    /// </summary>
    /// <param name="value">ֵ��</param>
    public static implicit operator VarRoleData(RoleData value)
    {
        return new VarRoleData(value);
    }

    /// <summary>
    /// �ӹ�����Ϣ�����ൽ������Ϣ�����ʽת����
    /// </summary>
    /// <param name="value">ֵ��</param>
    public static implicit operator RoleData(VarRoleData value)
    {
        return value.Value;
    }
}
