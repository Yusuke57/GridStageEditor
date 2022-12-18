using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GridStageEditor
{
    public class ToolView : VisualElement
    {
        private readonly Dictionary<StageUnitType, ToolItem> itemButtons = new();
        
        public static StageUnitType CurrentStageUnitTool { get; private set; }

        public ToolView()
        {
            var baseTree = UIToolkitUtil.GetVisualTree("ToolView/ToolView");
            var baseElement = baseTree.Instantiate();
            Add(baseElement);
            
            InitList();
        }

        private void InitList()
        {
            var stageUnitDataHolder = UIToolkitUtil.GetAssetByType<StageUnitDataHolder>();
            var dataList = stageUnitDataHolder.stageUnitDataList.ToList();
            var eraserSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/GridStageEditor/Images/eraser.png");
            dataList.Insert(0, new StageUnitData { type = StageUnitType.Empty, sprite = eraserSprite });

            var itemContainer = this.Q<VisualElement>("tool-item-container");
            itemButtons.Clear();

            foreach (var data in dataList)
            {
                var itemButton = new ToolItem(data, OnClickToolItem);
                itemContainer.Add(itemButton);
                itemButtons.Add(data.type, itemButton);
            }
        }

        private void OnClickToolItem(StageUnitType unitType)
        {
            foreach (var (type, itemButton) in itemButtons)
            {
                itemButton.SetSelected(type == unitType);
            }
            CurrentStageUnitTool = unitType;
        }
    }
}