using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LearningAndResources.MeshGeneration;

namespace LearningAndResources.GridGame
{
    public class WorldGrid : MonoBehaviour
    {
        [SerializeField]
        Vector2Int _size;
        [SerializeField]
        MeshGenerator MeshGenerator;
        [SerializeField]
        GridTile[] _gridTiles = new GridTile[0];

        void Start()
        {
            _gridTiles = new GridTile[_size.x * _size.y];
            Vector3[] worldPositions = new Vector3[_gridTiles.Length];

            int index = 0;
            for(int y = 0; y < _size.y; y++)
            {
                for(int x = 0; x < _size.x; x++)
                {
                    _gridTiles[index] = new GridTile(new Vector2Int(x, y));
                    worldPositions[index] = _gridTiles[index].WorldPos;
                    index++;
                }
            }

            Debug.Log("worldPositions length: " + worldPositions.Length);
            MeshGenerator.CreateQuads(worldPositions);
        }

        /*
        void OnDrawGizmos()
        {
            if(_gridTiles != null && _gridTiles.Length <= 0)
            {
                return;
            }

            foreach(GridTile gt in _gridTiles)
            {
                Vector3 gizmoPos = new Vector3(gt.Position.x, 0, gt.Position.y);
                Gizmos.DrawCube(gizmoPos, Vector3.one * .1f);
            }
        }
        */
    }

    [System.Serializable]
    public class GridTile
    {
        public Vector2Int Position;
        public Vector3 WorldPos;

        public GridTile(Vector2Int position)
        {
            Position = position;
            WorldPos = new Vector3(position.x, 0, position.y);
        }
    }
}