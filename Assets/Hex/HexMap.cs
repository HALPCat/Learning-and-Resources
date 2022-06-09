using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexMap;

namespace HexMap
{
    public class HexMap : MonoBehaviour
    {
        public Color[] ElevationColors = new Color[5];
        public HexTile TilePrefab;
        public Vector2Int Dimensions;
        HexTile[] tiles;

        void Awake()
        {
            tiles = new HexTile[Dimensions.x * Dimensions.y];
            Vector2Int thisPos = Vector2Int.zero;
            for(int y = 0, i = 0; y < Dimensions.y; y++)
            {
                for(int x = 0; x < Dimensions.x; x++)
                {
                    thisPos.x = x;
                    thisPos.y = y;
                    int elevation = (int)(GetNoise(x,y,10));
                    Debug.Log((int)(GetNoise(x,y,10f)));
                    CreateTile(thisPos, i++, elevation);
                }
            }
        }

        float GetNoise(float x, float y, float scale)
        {
            float maxHeight = 5;
            return Mathf.PerlinNoise(x/scale,y/scale)*maxHeight;

        }

        void CreateTile(Vector2Int tilePos, int i, int elevation)
        {
            Vector3 position;
            //position.x = tilePos.x * HexMetrics.outerRadius;
            position.x = (tilePos.x + tilePos.y * 0.5f - tilePos.y / 2) * (HexMetrics.innerRadius * 2f);
            position.y = 0f;
            position.z = tilePos.y * HexMetrics.outerRadius * 1.5f;

            HexTile tile = tiles[i] = Instantiate<HexTile>(TilePrefab);
            tile.transform.SetParent(transform, false);
            tile.transform.localPosition = position;
            tile.SetElevation(elevation, ElevationColors[elevation]);
        }
    }

    public static class HexMetrics
    {
        public const float EdgeLength = 1f;
        public const float outerRadius = 1f;
        public const float innerRadius = outerRadius * 0.866025404f;

        public static Vector3[] corners = {
            new Vector3(0f, 0f, outerRadius),
            new Vector3(innerRadius, 0f, 0.5f * outerRadius),
            new Vector3(innerRadius, 0f, -0.5f * outerRadius),
            new Vector3(0f, 0f, -outerRadius),
            new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
            new Vector3(-innerRadius, 0f, 0.5f * outerRadius)
        };
    }
}