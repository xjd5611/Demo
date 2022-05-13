using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

public static class UIFormExtention
{
    private static int loadingUIId;
    public static void OpenLoadingUI(this UIComponent uIComponent)
    {
        loadingUIId = GameEntry.UI.OpenUIForm(Definition.UIPath.LoadData, Definition.Group.UI);
    }

    public static void CloseLoadingUI(this UIComponent uIComponent)
    {
        if(GameEntry.UI.HasUIForm(loadingUIId))
            GameEntry.UI.CloseUIForm(loadingUIId);
    }

    private static int gameOverUIId;
    public static void OpenGameOverUI(this UIComponent uIComponent, object isWin)
    {
        gameOverUIId = GameEntry.UI.OpenUIForm(Definition.UIPath.GameOverUI, Definition.Group.UI, isWin);
    }

    public static void CloseGameOverUI(this UIComponent uIComponent)
    {
        if (GameEntry.UI.HasUIForm(gameOverUIId))
            GameEntry.UI.CloseUIForm(gameOverUIId);
    }

    public static void OpenCharacterUI(this UIComponent uIComponent)
    {
        GameEntry.UI.OpenUIForm(Definition.UIPath.CharacterUI, Definition.Group.UI);
    }

    public static void OpenPlayerInteractUI(this UIComponent uIComponent)
    {
        GameEntry.UI.OpenUIForm(Definition.UIPath.PlayerInteractUI, Definition.Group.UI);
    }
}
