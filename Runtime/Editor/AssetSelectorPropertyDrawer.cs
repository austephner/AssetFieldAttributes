#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AssetAttributes.Editor
{
    [CustomPropertyDrawer(typeof(AssetSelectorAttribute))]
    public class AssetSelectorPropertyDrawer : PropertyDrawer
    {
        private List<Object> _assets = new List<Object>();

        private List<string> _paths = new List<string>();

        private bool _doubleHeightForErrorMessage = false;

        #region Unity Events

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * (_doubleHeightForErrorMessage ? 2 : 1);
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var assetSelectorAttribute = (AssetSelectorAttribute)attribute;
            
            if (_assets == null || _assets.Count == 0 || _assets.Any(r => r == null))
            {
                _assets = new List<Object>();
                _paths = new List<string>();

                if (assetSelectorAttribute.showNoneOption)
                {
                    _assets.Add(null);
                    _paths.Add("None");
                }
                
                if (assetSelectorAttribute.paths != null && assetSelectorAttribute.paths.Length > 0)
                {
                    foreach (var path in assetSelectorAttribute.paths)
                    {
                        var modifiedDirectoryPath = path.StartsWith("Assets/") 
                            ? path.Substring(7, path.Length - 7) 
                            : path;
                        
                        var files = Directory.GetFiles(Path.Combine(Application.dataPath, modifiedDirectoryPath));

                        foreach (var file in files)
                        {
                            TryAddAssetAtPath(
                                file, 
                                _assets, 
                                _paths, 
                                assetSelectorAttribute.type, 
                                assetSelectorAttribute.assetFileType,
                                assetSelectorAttribute.allowFolders);
                        }
                    }
                }
                else
                {
                    var files = Directory.GetFiles(
                        Application.dataPath, 
                        $"*{assetSelectorAttribute.assetFileType}", 
                        SearchOption.AllDirectories);
                    
                    for (var fileIndex = 0; fileIndex < files.Length; fileIndex++)
                    {
                        TryAddAssetAtPath(
                            files[fileIndex], 
                            _assets, 
                            _paths, 
                            assetSelectorAttribute.type, 
                            assetSelectorAttribute.assetFileType,
                            assetSelectorAttribute.allowFolders);
                    }
                }
            }

            var currentSelectionIndex = -1;

            for (int i = 0; i < _assets.Count; i++)
            {
                if (_assets[i] == property.objectReferenceValue)
                {
                    currentSelectionIndex = i;
                    break;
                }
            }

            if (currentSelectionIndex == -1)
            {
                _assets.Add(property.objectReferenceValue);
                _paths.Add(FormatPathStringForDisplay(AssetDatabase.GetAssetPath(property.objectReferenceValue), assetSelectorAttribute.allowFolders));
                currentSelectionIndex = _assets.Count - 1;
            }

            if (!assetSelectorAttribute.showNoneOption && _assets.Count == 0 || 
                assetSelectorAttribute.showNoneOption && _assets.Count == 1)
            {
                _doubleHeightForErrorMessage = true;
                EditorGUI.PropertyField(position, property, label);
                position.y += position.height;
                EditorGUI.LabelField(position, label, $"No assets of type \"{assetSelectorAttribute.type}\" were found!");
            }
            else
            {
                _doubleHeightForErrorMessage = false;

                var fieldPosition = position;
                fieldPosition.width -= 30;

                var buttonPosition = position;
                buttonPosition.x = fieldPosition.x + fieldPosition.width;
                buttonPosition.width = 30;
                
                var nextSelectionIndex = EditorGUI.Popup(
                    fieldPosition,
                    label.text,
                    currentSelectionIndex,
                    _paths.ToArray());
                
                if (nextSelectionIndex != currentSelectionIndex)
                {
                    property.objectReferenceValue = _assets[nextSelectionIndex];
                }

                GUI.enabled = property.objectReferenceValue;
                
                var buttonContent = EditorGUIUtility.IconContent("d_Button Icon");
                buttonContent.tooltip = "Edit"; 
                
                if (GUI.Button(
                        buttonPosition,
                        buttonContent) && 
                    property.objectReferenceValue)
                {
                    Selection.activeObject = property.objectReferenceValue;
                }

                GUI.enabled = true;
            }
        }

        #endregion

        #region Utilities
        
        private void TryAddAssetAtPath(string filePath, List<Object> assets, List<string> paths, Type type, string assetFileType, bool allowFolders)
        {
            if (!filePath.EndsWith(assetFileType))
            {
                return;
            }
            
            var modifiedFilePath = filePath.Replace(Application.dataPath, "Assets");
            var foundAsset = AssetDatabase.LoadAssetAtPath(modifiedFilePath, type);

            if (foundAsset)
            {
                paths.Add(FormatPathStringForDisplay(modifiedFilePath, allowFolders));
                assets.Add(foundAsset);
            }
        }

        private string FormatPathStringForDisplay(string pathString, bool allowFolders)
        {
            var result = pathString.Replace("\\", "/");

            if (!allowFolders)
            {
                result = result.Split('/').LastOrDefault();
            }

            return result;
        }

        #endregion
    }
}
#endif