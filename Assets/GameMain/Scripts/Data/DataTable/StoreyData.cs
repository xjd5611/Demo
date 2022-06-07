using GameFramework;
using UnityEngine;

public class StoreyData
{
    public int typeId
    {
        get;
        private set;
    }

    public MstData[] msts;

    /// <summary>
    /// 获取楼层奖励。
    /// </summary>
    public string Treasure
    {
        get;
        private set;
    }

    public bool IsTop
    {
        get;
        private set;
    }

    public StoreyData(int typeId)
    {
        this.typeId = typeId;

        DRStorey dRStorey = GameEntry.DataTable.GetDataTable<DRStorey>().GetDataRow(typeId);
        MstData mst1 = dRStorey.Mst1 == 1000 ? null : new MstData(GameEntry.Entity.EntityId(), dRStorey.Mst1);
        MstData mst2 = dRStorey.Mst2 == 1000 ? null : new MstData(GameEntry.Entity.EntityId(), dRStorey.Mst2);
        MstData mst3 = dRStorey.Mst3 == 1000 ? null : new MstData(GameEntry.Entity.EntityId(), dRStorey.Mst3);
        if (mst2 == null)
        {
            mst1.Position = new Vector3(4.5f, 0.5f, 0);
            msts = new MstData[] { mst1 };
        }
        else
        {
            if (mst3 == null)
            {
                mst1.Position = new Vector3(3.5f, 0.5f, 0);
                mst2.Position = new Vector3(5.5f, 0.5f, 0);
                msts = new MstData[] { mst1, mst2 };
            }
            else
            {
                mst1.Position = new Vector3(2.5f, 0.5f, 0);
                mst2.Position = new Vector3(4.5f, 0.5f, 0);
                mst3.Position = new Vector3(6.5f, 0.5f, 0);
                msts = new MstData[] { mst1, mst2, mst3 };
            }
        }       
        this.Treasure = dRStorey.Treasure;

        IsTop = !GameEntry.DataTable.GetDataTable<DRStorey>().HasDataRow(typeId + 1);
    }
}

public class VarStoreyData : Variable<StoreyData>
{
    /// <summary>
    /// 初始化构建信息变量类的新实例。
    /// </summary>
    public VarStoreyData()
    {

    }

    /// <summary>
    /// 初始化构建信息变量类的新实例。
    /// </summary>
    /// <param name="value">值。</param>
    public VarStoreyData(StoreyData value)
    {
        this.Value = value;
    }

    /// <summary>
    /// 从构建信息类到构建信息变量类的隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator VarStoreyData(StoreyData value)
    {
        return new VarStoreyData(value);
    }

    /// <summary>
    /// 从构建信息变量类到构建信息类的隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator StoreyData(VarStoreyData value)
    {
        return value.Value;
    }
}
