using System;
using System.Linq;
using UnityEngine.UIElements;

namespace GridStageEditor
{
    public class ListView : VisualElement, IStageDataChangedNotifier
    {
        public Action<StageData> NotifyStageDataChanged { private get; set; }

        public ListView()
        {
            var baseTree = UIToolkitUtil.GetVisualTree("ListView/ListView");
            var baseElement = baseTree.Instantiate();
            Add(baseElement);

            InitList();
        }

        private void InitList()
        {
            var stageDataHolder = UIToolkitUtil.GetAssetByType<StageDataHolder>();
            var dataList = stageDataHolder.stageDataList;

            var listView = this.Q<UnityEngine.UIElements.ListView>();
            listView.makeItem = () => new Label();
            listView.bindItem = (element, index) => ((Label) element).text = dataList[index].id.ToString();
            listView.itemsSource = dataList;
            listView.selectionType = SelectionType.Single;
            listView.onSelectionChange += selection => NotifyStageDataChanged?.Invoke((StageData) selection.First());
        }
    }
}