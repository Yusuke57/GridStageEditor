using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GridStageEditor
{
    public class EditView : VisualElement, IStageDataChangedNotifier, IStageDataApplier
    {
        private StageData currentStageData;

        public Action<StageData> NotifyStageDataChanged { private get; set; }

        public EditView()
        {
            var baseTree = UIToolkitUtil.GetVisualTree("EditView/EditView");
            var baseElement = baseTree.Instantiate();
            Add(baseElement);
            
            CreateGridSizeButtons();
        }

        public void ApplyStageData(StageData stageData)
        {
            currentStageData = stageData;
            CreateGrid(stageData);
        }

        private void CreateGridSizeButtons()
        {
            var list = new List<(bool isRow, bool isAtFirst)>
            {
                new(true, false), new(false, true), new(false, false), new(true, true)
            };
            var edges = this.Query(className: "edge");
            for (var i = 0; i < list.Count; i++)
            {
                var plusButton = new Button { text = "+" };
                var minusButton = new Button { text = "-" };

                var (isRow, isAtFirst) = list[i];
                plusButton.clicked += () => OnClickPlusButton(isRow, isAtFirst);
                minusButton.clicked += () => OnClickMinusButton(isRow, isAtFirst);

                var edge = edges.AtIndex(i);
                edge.Add(plusButton);
                edge.Add(minusButton);
            }
        }

        private void OnClickPlusButton(bool isRow, bool prepend)
        {
            if (currentStageData == null)
            {
                return;
            }
            
            if (isRow)
            {
                currentStageData.AddRow(prepend);
            }
            else
            {
                currentStageData.AddCol(prepend);
            }

            NotifyStageDataChanged?.Invoke(currentStageData);
        }

        private void OnClickMinusButton(bool isRow, bool prepend)
        {
            if (currentStageData == null)
            {
                return;
            }
            
            if (isRow)
            {
                currentStageData.RemoveRow(prepend);
            }
            else
            {
                currentStageData.RemoveCol(prepend);
            }

            NotifyStageDataChanged?.Invoke(currentStageData);
        }

        private void CreateGrid(StageData stageData)
        {
            var size = stageData.size;
            var gridContainer = this.Q<VisualElement>("grid-container");
            gridContainer.pickingMode = PickingMode.Ignore;
            var containerAreaSize = gridContainer.contentRect.size;
            var buttonPx = Mathf.Min(containerAreaSize.x / size.x, containerAreaSize.y / size.y);
            
            var stageUnitDataHolder = UIToolkitUtil.GetAssetByType<StageUnitDataHolder>();
            var contentAreaSize = (Vector2) size * buttonPx;
            var gridContent = this.Q<VisualElement>("grid-content");
            gridContent.style.width = contentAreaSize.x;
            gridContent.style.height = contentAreaSize.y;
            gridContent.Clear();
            
            for (var y = size.y - 1; y >= 0; y--)
            {
                for (var x = 0; x < size.x; x++)
                {
                    var index = y * size.x + x;
                    var stageUnitSprite = stageUnitDataHolder.GetSprite(stageData.unitTypes[index]);
                    var button = CreateGridButton(index, (int) buttonPx, stageUnitSprite);
                    gridContent.Add(button);
                }
            }
        }

        private Button CreateGridButton(int index, int px, Sprite sprite)
        {
            var button = new Button
            {
                style =
                {
                    width = px,
                    height = px,
                    backgroundImage = new StyleBackground(sprite)
                }
            };

            button.clicked += () =>
            {
                currentStageData.unitTypes[index] = ToolView.CurrentStageUnitTool;
                NotifyStageDataChanged?.Invoke(currentStageData);
            };

            return button;
        }
    }
}