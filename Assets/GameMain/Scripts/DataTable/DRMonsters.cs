//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2022-05-05 17:11:32.890
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

    /// <summary>
    /// 怪物配置表。
    /// </summary>
    public class DRMonsters : DataRowBase
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
        /// 获取攻击权重。
        /// </summary>
        public int AttackWeight
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取攻击力。
        /// </summary>
        public int Attack
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取技能权重。
        /// </summary>
        public int SkillWeight1
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取技能ID。
        /// </summary>
        public int SkillId1
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取技能权重。
        /// </summary>
        public int SkillWeight2
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取技能ID。
        /// </summary>
        public int SkillId2
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取技能权重。
        /// </summary>
        public int SkillWeight3
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取技能ID。
        /// </summary>
        public int SkillId3
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
            AttackWeight = int.Parse(columnStrings[index++]);
            Attack = int.Parse(columnStrings[index++]);
            SkillWeight1 = int.Parse(columnStrings[index++]);
            SkillId1 = int.Parse(columnStrings[index++]);
            SkillWeight2 = int.Parse(columnStrings[index++]);
            SkillId2 = int.Parse(columnStrings[index++]);
            SkillWeight3 = int.Parse(columnStrings[index++]);
            SkillId3 = int.Parse(columnStrings[index++]);

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
                    AttackWeight = binaryReader.Read7BitEncodedInt32();
                    Attack = binaryReader.Read7BitEncodedInt32();
                    SkillWeight1 = binaryReader.Read7BitEncodedInt32();
                    SkillId1 = binaryReader.Read7BitEncodedInt32();
                    SkillWeight2 = binaryReader.Read7BitEncodedInt32();
                    SkillId2 = binaryReader.Read7BitEncodedInt32();
                    SkillWeight3 = binaryReader.Read7BitEncodedInt32();
                    SkillId3 = binaryReader.Read7BitEncodedInt32();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private KeyValuePair<int, int>[] m_SkillWeight = null;

        public int SkillWeightCount
        {
            get
            {
                return m_SkillWeight.Length;
            }
        }

        public int GetSkillWeight(int id)
        {
            foreach (KeyValuePair<int, int> i in m_SkillWeight)
            {
                if (i.Key == id)
                {
                    return i.Value;
                }
            }

            throw new GameFrameworkException(Utility.Text.Format("GetSkillWeight with invalid id '{0}'.", id));
        }

        public int GetSkillWeightAt(int index)
        {
            if (index < 0 || index >= m_SkillWeight.Length)
            {
                throw new GameFrameworkException(Utility.Text.Format("GetSkillWeightAt with invalid index '{0}'.", index));
            }

            return m_SkillWeight[index].Value;
        }

        private KeyValuePair<int, int>[] m_SkillId = null;

        public int SkillIdCount
        {
            get
            {
                return m_SkillId.Length;
            }
        }

        public int GetSkillId(int id)
        {
            foreach (KeyValuePair<int, int> i in m_SkillId)
            {
                if (i.Key == id)
                {
                    return i.Value;
                }
            }

            throw new GameFrameworkException(Utility.Text.Format("GetSkillId with invalid id '{0}'.", id));
        }

        public int GetSkillIdAt(int index)
        {
            if (index < 0 || index >= m_SkillId.Length)
            {
                throw new GameFrameworkException(Utility.Text.Format("GetSkillIdAt with invalid index '{0}'.", index));
            }

            return m_SkillId[index].Value;
        }

        private void GeneratePropertyArray()
        {
            m_SkillWeight = new KeyValuePair<int, int>[]
            {
                new KeyValuePair<int, int>(1, SkillWeight1),
                new KeyValuePair<int, int>(2, SkillWeight2),
                new KeyValuePair<int, int>(3, SkillWeight3),
            };

            m_SkillId = new KeyValuePair<int, int>[]
            {
                new KeyValuePair<int, int>(1, SkillId1),
                new KeyValuePair<int, int>(2, SkillId2),
                new KeyValuePair<int, int>(3, SkillId3),
            };
        }
    }