using System.Collections.Generic;
using System.Linq;

namespace GridStageEditor
{
    public static class StageDataExtensions
    {
        private const int MIN_ROW = 2;
        private const int MIN_COL = 2;

        public static void AddRow(this StageData stageData, bool isAtFirst)
        {
            var additiveList = new List<StageUnitType>(new StageUnitType[stageData.size.x]);
            if (isAtFirst)
            {
                stageData.unitTypes.InsertRange(0, additiveList);
            }
            else
            {
                stageData.unitTypes.AddRange(additiveList);
            }
            stageData.size.y += 1;
        }

        public static void RemoveRow(this StageData stageData, bool isAtFirst)
        {
            if (stageData.size.y <= MIN_ROW)
            {
                return;
            }
            
            var removeCount = stageData.size.x;
            stageData.unitTypes = isAtFirst
                ? stageData.unitTypes.Skip(removeCount).ToList()
                : stageData.unitTypes.SkipLast(removeCount).ToList();
            stageData.size.y -= 1;
        }

        public static void AddCol(this StageData stageData, bool isAtFirst)
        {
            var list = new List<StageUnitType>();
            var chunk = stageData.unitTypes
                .Select((type, index) => new { type, index })
                .GroupBy(tuple => tuple.index / stageData.size.x)
                .Select(group => @group.Select(e => e.type));
            foreach (var types in chunk)
            {
                if (isAtFirst)
                {
                    list.Add(default);
                }
                list.AddRange(types);
                if (!isAtFirst)
                {
                    list.Add(default);
                }
            }
            
            stageData.size.x += 1;
            stageData.unitTypes = list;
        }

        public static void RemoveCol(this StageData stageData, bool isAtFirst)
        {
            if (stageData.size.x <= MIN_COL)
            {
                return;
            }

            var sizeX = stageData.size.x;
            stageData.unitTypes = stageData.unitTypes
                .Select((type, index) => new { type, index })
                .GroupBy(tuple => tuple.index / sizeX)
                .Select(group => @group
                    .Where(e => isAtFirst ? (e.index % sizeX > 0) : (e.index % sizeX < sizeX - 1))
                    .Select(e => e.type))
                .SelectMany(type => type)
                .ToList();
            
            stageData.size.x -= 1;
        }
    }
}