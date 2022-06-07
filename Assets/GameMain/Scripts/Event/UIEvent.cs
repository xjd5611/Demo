using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvent : MonoBehaviour
{

}

namespace UIEvents
{
    public class OnStateChangeEvent : EventArgs<OnStateChangeEvent> { }
    public class OnHPChangeEvent : EventArgs<OnHPChangeEvent> 
    {
        public int unitID;

        public int curHP;
        public int maxHP;
    }
}
