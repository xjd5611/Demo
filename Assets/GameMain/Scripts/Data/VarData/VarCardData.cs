using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VarAttackCardData : Variable<AtkCardData>
{
    public VarAttackCardData(int entityId, int typeId)
    {
        Value = new AtkCardData(entityId, typeId);
    }
}

public class VarDefenseCardData : Variable<DefCardData>
{
    public VarDefenseCardData(int entityId, int typeId)
    {
        Value = new DefCardData(entityId, typeId);
    }
}

public class VarSkillCardData : Variable<SkiCardData>
{
    public VarSkillCardData(int entityId, int typeId)
    {
        Value = new SkiCardData(entityId, typeId);
    }
}
