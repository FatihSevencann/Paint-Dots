using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace BrushingLine
{
    public class LinePaint : MonoBehaviour
    {
        [SerializeField]private LineRenderer _lineRenderer;
        
        private Vector2Int _startCoords;
        private Vector2Int _endCoords;


        public Vector2Int StartCoords
        {
            get => _startCoords;
    
        }

        public Vector2Int EndCoords
        {
            get => _endCoords;
            
        }

        public void SetConnectedCoords(Vector2Int startCoord ,Vector2Int endCoord)
        {
            _startCoords = startCoord;
            _endCoords = endCoord;
            

        }

        public void SetRendererPosition(Vector3 startPos, Vector3  endPos)
        {
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0,startPos);
            _lineRenderer.SetPosition(1,endPos);
            
        }
        
        
        
    }
}