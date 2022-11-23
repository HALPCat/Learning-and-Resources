using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearningAndResources.HexMap
{
    public class HexTile : MonoBehaviour
    {
        private int _elevation = 0;
        public int Elevation
        {
            get
            {
                return _elevation;
            }
        }

        private Renderer _renderer;
        private MaterialPropertyBlock _propBlock;

        void Awake()
        {
            _renderer = GetComponentInChildren<Renderer>();
            _propBlock = new MaterialPropertyBlock();
        }

        public void SetElevation(int elevation, Color color)
        {
            Vector3 childPos = transform.GetChild(0).transform.position;
            childPos.y = elevation;
            transform.GetChild(0).transform.position = childPos;

            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetColor("_Color", color);
            _renderer.SetPropertyBlock(_propBlock);
        }
    }
}