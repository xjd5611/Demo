using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData 
{
    [SerializeField]
    private int m_Id = 0;
    public int Id 
    {
        get
        {
            return m_Id;
        }
    }

    [SerializeField]
    private int m_TypeId = 0;
    public int TypeId
    {
        get
        {
            return m_TypeId;
        }
    }

    public EntityData(int entityId ,int typeId)
    {
        m_Id = entityId;
        m_TypeId = typeId;
    }

    public Vector3 Positon;
}
