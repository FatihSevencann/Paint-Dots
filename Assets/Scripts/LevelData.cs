using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrushingLine
{

    [CreateAssetMenu(fileName = "LevelData",menuName = "ScriptableObjects/Create LevelData",order = 1)]
    public class LevelData : ScriptableObject
    {
        public int width, height;
        public Vector2Int BrushStartCoords;
        public List<Connection> complatePattern = new List<Connection>();
        
        



    }
}
