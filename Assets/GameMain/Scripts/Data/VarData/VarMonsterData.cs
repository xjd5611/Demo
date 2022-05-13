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
    /// 从构建信息类到构建信息变量类的隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator VarMonsterData(MonsterData value)
    {
        VarMonsterData varValue = ReferencePool.Acquire<VarMonsterData>();
        varValue.Value = value;
        return varValue;
    }

    /// <summary>
    /// 从构建信息变量类到构建信息类的隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator MonsterData(VarMonsterData value)
    {
        return value.Value;
    }
}
