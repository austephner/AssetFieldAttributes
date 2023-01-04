using UnityEngine;

namespace ResourceAttributes.Samples
{
    [CreateAssetMenu(menuName = "Resource Attributes/Example Asset")]
    public class ExampleAsset : ScriptableObject
    {
        [AssetSelector(typeof(ExampleAsset), true, new string[] {"ResourceAttributes/Samples/"})] 
        public ExampleAsset[] otherAssets; 
    }
}