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
    /// 从构建信息类到构建信息变量类的隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator VarMstsData(MstData[] value)
    {
        VarMstsData varValue = ReferencePool.Acquire<VarMstsData>();
        varValue.Value = value;
        return varValue;
    }

    /// <summary>
    /// 从构建信息变量类到构建信息类的隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator MstData[](VarMstsData value)
    {
        return value.Value;
    }
}
