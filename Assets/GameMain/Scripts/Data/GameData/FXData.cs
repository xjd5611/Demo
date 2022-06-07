using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXData : EntityData
{
    public bool FlipX = false;
    public bool FlipY = false;

    public FXData(int entityId, int typeId, Vector3 position, bool FlipX, bool FlipY)
    : base(entityId, typeId)
    {
        Positon = position;
        this.FlipX = FlipX;
        this.FlipY = FlipY;
    }
}
