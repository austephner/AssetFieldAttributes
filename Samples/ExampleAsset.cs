using UnityEngine;

namespace AssetAttributes.Samples
{
    [CreateAssetMenu(menuName = "Resource Attributes/Example Asset")]
    public class ExampleAsset : ScriptableObject
    {
        [AssetSelector(typeof(ExampleAsset), true, false, new string[] {"AssetAttributes/Samples/"})] 
        public ExampleAsset[] exampleAssets; 
    }
}