using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PaintDots
{
    [System.Serializable]
    public class Connection
    { 
        [SerializeField]private Vector2Int _startPos;

       [SerializeField] private Vector2Int _endPos;

       [SerializeField] private Material _material;
       

        public Connection(Vector2Int startCoords, Vector2Int endCoods,Material material)
        {
            _startPos = startCoords;
            _endPos = endCoods;
            _material = material;
        }
        
        public Vector2Int StartCoords
        {
            get => _startPos;
        }

        public Vector2Int EndCoods
        {
            get => _endPos;
        }

        public Material MaterialChoose
        {
            get => _material;
        }
        
        



    }

}
