using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionData 
{
    public int curPower;
    public int powerMax;
    public int actMax;
    public List<Definition.Enum.ActionType> actList;


    public ActionData(int powerMax, int actMax)
    {
        this.powerMax = powerMax;
        this.actMax = actMax;
        actList = new List<Definition.Enum.ActionType>();    
        curPower = powerMax;
    }

    public void ResetPower()
    {
        curPower = powerMax;
    }

    public bool CanCost(int cost)
    {
        //Debug.Log(cost);
        //Debug.Log(curPower);
        return cost <= curPower;
    }

    public void CostPower(int cost)
    {
        curPower -= cost;
    }

    public void AddPower(int add)
    {
        curPower = Mathf.Clamp(curPower + add, 0, powerMax);
    }

    public void AddAct(Definition.Enum.ActionType actionType, int count)
    {
        for (int i = 0; i < count; i++)
        {
            actList.Add(actionType);
        }
        while(actList.Count > actMax)
        {
            actList.RemoveAt(0);
        }
    }
}

public class VarActionData : Variable<ActionData> 
{
    public VarActionData(int powerMax, int actMax)
    {
        Value = new ActionData(powerMax, actMax);
    }

    /// <summary>
    /// 初始化构建信息变量类的新实例。
    /// </summary>
    /// <param name="value">值。</param>
    public VarActionData(ActionData value)
    {
        Value = value;
    }

    /// <summary>
    /// 从构建信息类到构建信息变量类的隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator VarActionData(ActionData value)
    {
        return new VarActionData(value);
    }

    /// <summary>
    /// 从构建信息变量类到构建信息类的隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator ActionData(VarActionData value)
    {
        return value.Value;
    }
}
