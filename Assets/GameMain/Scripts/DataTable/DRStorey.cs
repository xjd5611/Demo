//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2022-06-02 13:49:35.746
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

    /// <summary>
    /// 怪物楼层配置表。
    /// </summary>
    public class DRStorey : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取楼层。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取怪物1。
        /// </summary>
        public int Mst1
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取怪物2。
        /// </summary>
        public int Mst2
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取怪物3。
        /// </summary>
        public int Mst3
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取楼层奖励。
        /// </summary>
        public string Treasure
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            Mst1 = int.Parse(columnStrings[index++]);
            Mst2 = int.Parse(columnStrings[index++]);
            Mst3 = int.Parse(columnStrings[index++]);
            Treasure = columnStrings[index++];

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    Mst1 = binaryReader.Read7BitEncodedInt32();
                    Mst2 = binaryReader.Read7BitEncodedInt32();
                    Mst3 = binaryReader.Read7BitEncodedInt32();
                    Treasure = binaryReader.ReadString();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private KeyValuePair<int, int>[] m_Mst = null;

        public int MstCount
        {
            get
            {
                return m_Mst.Length;
            }
        }

        public int GetMst(int id)
        {
            foreach (KeyValuePair<int, int> i in m_Mst)
            {
                if (i.Key == id)
                {
                    return i.Value;
                }
            }

            throw new GameFrameworkException(Utility.Text.Format("GetMst with invalid id '{0}'.", id));
        }

        public int GetMstAt(int index)
        {
            if (index < 0 || index >= m_Mst.Length)
            {
                throw new GameFrameworkException(Utility.Text.Format("GetMstAt with invalid index '{0}'.", index));
            }

            return m_Mst[index].Value;
        }

        private void GeneratePropertyArray()
        {
            m_Mst = new KeyValuePair<int, int>[]
            {
                new KeyValuePair<int, int>(1, Mst1),
                new KeyValuePair<int, int>(2, Mst2),
                new KeyValuePair<int, int>(3, Mst3),
            };
        }
    }