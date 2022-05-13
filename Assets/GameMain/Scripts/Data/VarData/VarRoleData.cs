using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VarRoleData : Variable<RoleData>
{
    /// <summary>
    /// 初始化构建信息变量类的新实例。
    /// </summary>
    public VarRoleData()
    {

    }

    /// <summary>
    /// 初始化构建信息变量类的新实例。
    /// </summary>
    /// <param name="value">值。</param>
    public VarRoleData(RoleData value)
    {
        this.Value = value;
    }

    /// <summary>
    /// 从构建信息类到构建信息变量类的隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator VarRoleData(RoleData value)
    {
        return new VarRoleData(value);
    }

    /// <summary>
    /// 从构建信息变量类到构建信息类的隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator RoleData(VarRoleData value)
    {
        return value.Value;
    }
}
