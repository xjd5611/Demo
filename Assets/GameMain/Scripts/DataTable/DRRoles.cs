//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2022-05-05 17:11:32.869
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

    /// <summary>
    /// 角色配置表。
    /// </summary>
    public class DRRoles : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取角色ID。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取名称。
        /// </summary>
        public string NickName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取生命上限。
        /// </summary>
        public int HPMax
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取角色专属技能牌Id组合。
        /// </summary>
        public string SkillCardsId
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取终极技Id。
        /// </summary>
        public string UltimateSkillCardId
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
            NickName = columnStrings[index++];
            HPMax = int.Parse(columnStrings[index++]);
            SkillCardsId = columnStrings[index++];
            UltimateSkillCardId = columnStrings[index++];

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
                    NickName = binaryReader.ReadString();
                    HPMax = binaryReader.Read7BitEncodedInt32();
                    SkillCardsId = binaryReader.ReadString();
                    UltimateSkillCardId = binaryReader.ReadString();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }