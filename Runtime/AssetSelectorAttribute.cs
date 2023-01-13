using System;
using UnityEngine;

namespace AssetAttributes
{
    public class AssetSelectorAttribute : PropertyAttribute
    {
        public readonly Type type;

        public readonly string[] paths = new string[0];

        public readonly bool showNoneOption = true;

        public readonly bool allowFolders = false;

        public readonly string assetFileType = ".asset"; 

        /// <summary>
        /// Creates a drop-down selector in the editor that lists assets of the given <see cref="type"/>.
        /// </summary>
        /// <param name="type">The <see cref="System.Type"/> type of asset to load.</param>
        /// <param name="paths">All top-level directory paths to use for pulling asset options from. If left null or
        /// empty, assets across the entire project will be loaded. This isn't recommended due to the poor performance.
        /// </param>
        public AssetSelectorAttribute(Type type, string[] paths)
        {
            this.type = type;
            this.paths = paths;
        }
        
        /// <summary>
        /// Creates a drop-down selector in the editor that lists assets of the given <see cref="type"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="assetFileType">The asset file type to use. The default value is ".asset"</param>
        /// <param name="paths">All top-level directory paths to use for pulling asset options from. If left null or
        /// empty, assets across the entire project will be loaded. This isn't recommended due to the poor performance.
        /// </param>
        public AssetSelectorAttribute(Type type, string assetFileType, string[] paths)
        {
            this.type = type;
            this.paths = paths;
            this.assetFileType = assetFileType;
        }
        
        /// <summary>
        /// Creates a drop-down selector in the editor that lists assets of the given <see cref="type"/>.
        /// </summary>
        /// <param name="type">The <see cref="System.Type"/> type of asset to load.</param>
        /// <param name="showNoneOption">When true, the "None" option will appear in the dropdown.</param>
        /// <param name="paths">All top-level directory paths to use for pulling asset options from. If left null or
        /// empty, assets across the entire project will be loaded. This isn't recommended due to the poor performance.
        /// </param>
        public AssetSelectorAttribute(Type type, bool showNoneOption, bool allowFolders, string[] paths)
        {
            this.type = type;
            this.paths = paths;
            this.showNoneOption = showNoneOption;
            this.allowFolders = allowFolders;
        }
        
        /// <summary>
        /// Creates a drop-down selector in the editor that lists assets of the given <see cref="type"/>.
        /// </summary>
        /// <param name="type">The <see cref="System.Type"/> type of asset to load.</param>
        /// <param name="showNoneOption">When true, the "None" option will appear in the dropdown.</param>
        /// <param name="assetFileType">The asset file type to use. The default value is ".asset"</param>
        /// <param name="paths">All top-level directory paths to use for pulling asset options from. If left null or
        /// empty, assets across the entire project will be loaded. This isn't recommended due to the poor performance.
        /// </param>
        public AssetSelectorAttribute(Type type, bool showNoneOption, bool allowFolders, string assetFileType, string[] paths)
        {
            this.type = type;
            this.paths = paths;
            this.showNoneOption = showNoneOption;
            this.allowFolders = allowFolders;
            this.assetFileType = assetFileType;
        }
    }
}