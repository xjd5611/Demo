using GameFramework;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class ProcLoadDataTable : ProcedureBase
{
    public static readonly string[] DataTableNames = new string[]
    {
        "Roles","Msts","Effects","AtkCards","DefCards","SkiCards","Storey"
    };

    private Dictionary<string, bool> m_LoadedFlag;

    private IFsm<IProcedureManager> procedureOwner;
    private int loadDataUIId;

    protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnInit(procedureOwner);
        this.procedureOwner = procedureOwner;
    }

    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);
        GameEntry.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);

        GameEntry.UI.OpenLoadingUI();

        m_LoadedFlag = new Dictionary<string, bool>();

        PreloadResources();
       
    }

    protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        foreach (var flag in m_LoadedFlag)
        {
            if (flag.Value == false)
                return;
        }
        ChangeState<ProcLoadAsset>(procedureOwner);
    }

    protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);
        GameEntry.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);

    }

    private void PreloadResources()
    {
        foreach (string dataTableName in DataTableNames)
        {
            LoadDataTable(dataTableName);
        }

    }

    private void LoadDataTable(string dataTableName)
    {
        string dataTableAssetName = GameEntry.DataTable.GetDataTablePath(dataTableName);
        //string dataTableAssetName = AssetUtility.GetDataTableAsset(dataTableName, false);
        m_LoadedFlag.Add(dataTableName, false);
        GameEntry.DataTable.LoadDataTable(dataTableName, dataTableAssetName, true);
    }

    private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
    {
        LoadDataTableSuccessEventArgs eventArgs = e as LoadDataTableSuccessEventArgs;
        for (int i = 0; i < DataTableNames.Length; i++)
        {
            string dataTableAssetName = GameEntry.DataTable.GetDataTablePath(DataTableNames[i]);
            if(dataTableAssetName == eventArgs.DataTableAssetName && m_LoadedFlag.ContainsKey(DataTableNames[i]))
            {
                m_LoadedFlag[DataTableNames[i]] = true;
                
            }
        }
    }
}
