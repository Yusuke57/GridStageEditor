using System;
using UnityEngine.UIElements;

namespace GridStageEditor
{
    public class ToolItem : VisualElement
    {
        public ToolItem(StageUnitData data, Action<StageUnitType> onClick)
        {
            var baseTree = UIToolkitUtil.GetVisualTree("ToolView/ToolItem");
            var baseElement = baseTree.Instantiate();
            Add(baseElement);

            InitItemButton(data, onClick);
        }

        private void InitItemButton(StageUnitData data, Action<StageUnitType> onClick)
        {
            var image = this.Q<Image>();
            image.sprite = data.sprite;

            var button = this.Q<Button>();
            button.clicked += () => onClick?.Invoke(data.type);
        }

        public void SetSelected(bool isSelected)
        {
            const string selectedClassName = "selected";
            this.Q<Button>().EnableInClassList(selectedClassName, isSelected);
        }
    }
}