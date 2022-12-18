using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GridStageEditor
{
    [CreateAssetMenu(fileName = "StageUnitDataHolder", menuName = "StageUnitDataHolder", order = 1)]
    public class StageUnitDataHolder : ScriptableObject
    {
        public List<StageUnitData> stageUnitDataList;

        public Sprite GetSprite(StageUnitType type)
        {
            return stageUnitDataList.FirstOrDefault(data => data.type == type)?.sprite;
        }
    }
    
    [Serializable]
    public class StageUnitData
    {
        public StageUnitType type;
        public Sprite sprite;
    }

    public enum StageUnitType
    {
        Empty = 0,
        Block = 1,
        Button = 2,
        Player = 3
    }
}