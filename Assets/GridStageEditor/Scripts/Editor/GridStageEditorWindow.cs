using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GridStageEditor
{
    public class GridStageEditorWindow : EditorWindow
    {
        [MenuItem("Window/GridStageEditorWindow")]
        public static void Open()
        {
            var window = GetWindow<GridStageEditorWindow>();
            window.titleContent = new GUIContent("GridStageEditorWindow");
        }

        private StageDataHolder cachedStageDataHolder;
        private StageData selectedStageData;

        public void CreateGUI()
        {
            var rootElement = rootVisualElement;

            // UXMLを元にUI要素を作成
            var windowTree = UIToolkitUtil.GetVisualTree("GridStageEditorWindow");
            var windowElement = windowTree.Instantiate();
            rootElement.Add(windowElement);

            // 各containerにViewを生成
            var listView = new ListView();
            var toolView = new ToolView();
            var editView = new EditView();
            rootElement.Q<VisualElement>("list-view-container").Add(listView);
            rootElement.Q<VisualElement>("tool-view-container").Add(toolView);
            rootElement.Q<VisualElement>("edit-view-container").Add(editView);

            var stageDataChangedNotifiers = new List<IStageDataChangedNotifier> { listView, editView };
            var stageDataAppliers = new List<IStageDataApplier> { editView };
            InitViewsDependedOnStageData(stageDataChangedNotifiers, stageDataAppliers);
        }

        private void InitViewsDependedOnStageData(List<IStageDataChangedNotifier> notifiers, List<IStageDataApplier> appliers)
        {
            foreach (var notifier in notifiers)
            {
                notifier.NotifyStageDataChanged = OnStageDataChanged;
            }

            void OnStageDataChanged(StageData stageData)
            {
                // ステージデータの変更を保存
                var stageDataHolder = UIToolkitUtil.GetAssetByType<StageDataHolder>();
                EditorUtility.SetDirty(stageDataHolder);
                AssetDatabase.SaveAssets();

                foreach (var applier in appliers)
                {
                    applier.ApplyStageData(stageData);
                }
            }
        }
    }
}