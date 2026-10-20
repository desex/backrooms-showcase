using UnityEngine;

namespace Editor.Tools.HoleGenerator
{
    public partial class HoleGeneratorEditor
    {
        private void CreateBottom(GameObject bottomPrefab, Transform parent, string tag)
        {
            var bottom = Instantiate(bottomPrefab, parent);
            
            _centerBottomPoint = bottom.GetComponent<Transform>();
            
            bottom.transform.localPosition = new Vector3(_widthHole, -_depthHole * 2f, _lengthHole);
            bottom.transform.localScale = new Vector3(_widthHole, 1, _lengthHole);
        
            SetTag(bottom, tag);
        
            bottom.AddComponent<MeshCollider>();
        
            GetCornersFromBounds(bottom.GetComponent<MeshCollider>());
        }
    
        private void CreateUpperCeiling()
        {
            _upperCeilingNorthWall = Instantiate(_parameters.UpHoleCelling, _root.transform);
            _upperCeilingSouthWall = Instantiate(_parameters.UpHoleCelling, _root.transform);
            
            _upperCeilingNorthWall.transform.localScale = Vector3.one;
            _upperCeilingSouthWall.transform.localScale = new (-1f, 1f , 1f);
        
            _upperCeilingNorthWall.transform.localPosition = _topMiddlePoint;
            
            _upperCeilingSouthWall.transform.localPosition = _topMiddlePoint;

            _upperCeilingNorthWall.transform.rotation = Quaternion.identity;
            _upperCeilingSouthWall.transform.rotation = Quaternion.identity;
        }
    }
}