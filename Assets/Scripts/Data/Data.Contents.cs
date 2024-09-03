using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;
using UnityEngine.UIElements;
namespace Data
{
    #region json 방식
    //
    //    #region PlayerData
    //    [Serializable]
    //    public class  PlayerData
    //    {
    //        public int level;
    //        public int maxHp;
    //        public int attack;
    //        public int totalExp;

    //    }

    //    [Serializable]
    //    public class PlayerDataLoader: ILoader<int,PlayerData>
    //    {
    //        public List<PlayerData> stats = new List<PlayerData>();

    //        public Dictionary<int, PlayerData> MakeDict()
    //        {
    //            Dictionary<int, PlayerData> dict = new Dictionary<int, PlayerData>();

    //            foreach( PlayerData stat in stats)
    //            {
    //                dict.Add(stat.level, stat);
    //            }

    //            return dict;
    //        }
    //    }
    //    #endregion
    //
    #endregion
    #region xml 방식
    public class PlayerData
    {
        [XmlAttribute]
        public int level;
        [XmlAttribute]
        public int maxHp;
        [XmlAttribute]
        public int attack;
        [XmlAttribute]
        public int totalExp;
    }

    [Serializable, XmlRoot("PlayerDatas")]
    public class PlayerDataLoader : ILoader<int, PlayerData>
    {
        [XmlElement("PlayerData")]
        public List<PlayerData> stats = new List<PlayerData>();

        public Dictionary<int, PlayerData> MakeDict()
        {
            Dictionary<int, PlayerData> dict = new Dictionary<int, PlayerData>();

            foreach (PlayerData stat in stats)
            {
                dict.Add(stat.level, stat);
            }

            return dict;
        }
    }
    #endregion

    public class MonsterData
    {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public string prefab;
        [XmlAttribute]
        public int level;
        [XmlAttribute]
        public int maxHp;
        [XmlAttribute]
        public int attack;
        [XmlAttribute]
        public float speed;

        //DropData
        // - 일정확률로
        // - 어떤 아이템을
        // - 몇개를 드랍할지?
    }

}
