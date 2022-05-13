using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VarAttackCardData : Variable<AttackCardData>
{
    public VarAttackCardData(int entityId, int typeId)
    {
        Value = new AttackCardData(entityId, typeId);
    }
}

public class VarDefenseCardData : Variable<DefenseCardData>
{
    public VarDefenseCardData(int entityId, int typeId)
    {
        Value = new DefenseCardData(entityId, typeId);
    }
}

public class VarSkillCardData : Variable<SkillCardData>
{
    public VarSkillCardData(int entityId, int typeId)
    {
        Value = new SkillCardData(entityId, typeId);
    }
}
