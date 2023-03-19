using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BrushingLine
{
    [System.Serializable]
    public class Connection
    {
        [SerializeField]private Vector2Int _startPos;

       [SerializeField] private Vector2Int _endPos;

        public Connection(Vector2Int startCoords, Vector2Int endCoods)
        {
            _startPos = startCoords;
            _endPos = endCoods;
            
            
        }
        
        public Vector2Int StartCoords
        {
            get => _startPos;
        }

        public Vector2Int EndCoods
        {
            get => _endPos;
        }
        
        



    }

}
