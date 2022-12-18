using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace GridStageEditor
{
    public static class UIToolkitUtil
    {
        private const string DIRECTORY_PATH = "Assets/GridStageEditor/Scripts/Editor/";

        public static VisualTreeAsset GetVisualTree(string path)
        {
            var fullPath = DIRECTORY_PATH + path + ".uxml";
            return AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(fullPath);
        }

        public static StyleSheet GetStyleSheet(string path)
        {
            var fullPath = DIRECTORY_PATH + path + ".uss";
            return AssetDatabase.LoadAssetAtPath<StyleSheet>(fullPath);
        }
        
        public static T GetAssetByType<T>(string[] searchInFolders = null) where T : UnityEngine.Object
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T)}", searchInFolders);
            if (guids == null || !guids.Any())
            {
                return null;
            }

            var guid = guids.First();
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var component = AssetDatabase.LoadAssetAtPath<T>(path);
            return component;
        }
    }
}