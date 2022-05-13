//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2022-05-05 17:11:32.895
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

    /// <summary>
    /// 技能卡牌。
    /// </summary>
    public class DRSkillCards : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取卡牌ID。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取卡牌名称。
        /// </summary>
        public string CardName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取时间消耗。
        /// </summary>
        public int TimeCost
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取卡牌种类。
        /// </summary>
        public int CardType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取招式消耗。
        /// </summary>
        public string ActionCost
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取附加效果Id。
        /// </summary>
        public string AdditionalEffectId
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
            CardName = columnStrings[index++];
            index++;
            TimeCost = int.Parse(columnStrings[index++]);
            CardType = int.Parse(columnStrings[index++]);
            ActionCost = columnStrings[index++];
            AdditionalEffectId = columnStrings[index++];

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
                    CardName = binaryReader.ReadString();
                    TimeCost = binaryReader.Read7BitEncodedInt32();
                    CardType = binaryReader.Read7BitEncodedInt32();
                    ActionCost = binaryReader.ReadString();
                    AdditionalEffectId = binaryReader.ReadString();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }