using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridStageEditor
{
    [CreateAssetMenu(fileName = "StageDataHolder", menuName = "StageDataHolder", order = 0)]
    public class StageDataHolder : ScriptableObject
    {
        public List<StageData> stageDataList;
    }

    [Serializable]
    public class StageData
    {
        public int id;
        public Vector2Int size;
        public List<StageUnitType> unitTypes;
    }
}