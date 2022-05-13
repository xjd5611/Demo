//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;


/// <summary>
/// 游戏入口。
/// </summary>
public partial class GameEntry : MonoBehaviour
{
    //public static BuiltinDataComponent BuiltinData
    //{
    //    get;
    //    private set;
    //}

    //public static HPBarComponent HPBar
    //{
    //    get;
    //    private set;
    //}

    public static GameManager GameManager
    {
        get;
        private set;
    }

    private static void InitCustomComponents()
    {
        GameManager = UnityGameFramework.Runtime.GameEntry.GetComponent<GameManager>();
        //BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
        //HPBar = UnityGameFramework.Runtime.GameEntry.GetComponent<HPBarComponent>();
    }
}