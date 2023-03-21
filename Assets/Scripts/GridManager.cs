using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace PaintDots
{
    public class GridManager
    {

        private int Widht;
        private int Height;
        private float cellSize;
        private int[,] _gridArray;
        private Vector3 _originPos;
        

        public int[,] gridArray
        {
            get { return _gridArray; }
        }

        public void Initialize(int width, int height, float _cellSize,Vector3 originPos)
        {
            Widht = width;
            Height = height;
            cellSize = _cellSize;
            _originPos = originPos;
            _gridArray = new int[width, height];

        }
        public Vector3 GetCellWorldPosition(float x, float y)
        {
            return new Vector3(Mathf.FloorToInt(cellSize * x), 0, Mathf.FloorToInt(cellSize * y))+_originPos;
            
        }
        public Vector2Int GetCellXZbySwipe(int x , int z , Swipe swipe )
        {

            Vector2Int xz = new Vector2Int(-1, -1);
            switch (swipe)
            {
                case Swipe.Top :
                    if (z < (Height - 1))
                        xz = new Vector2Int(x, z + 1);
                    
                    break;
                case Swipe.Bottom :
                    if (z >0)
                        xz = new Vector2Int(x, z -1);
                    
                    break;
                case Swipe.Left :
                    if (x>0)
                        xz = new Vector2Int(x-1, z );
                    
                    break;
                case Swipe.Right :
                    if (x< (Widht-1))
                        xz = new Vector2Int(x+1 ,z );
                    
                    break;
                case Swipe.TopLeft :
                    if (x>0 && z<(Height-1))
                        xz = new Vector2Int(x-1, z + 1);
                    
                    break;
                case Swipe.TopRight :
                    if (x<(Widht-1) && z<(Height-1))
                        xz = new Vector2Int(x+1, z + 1);
                    
                    break;
                case Swipe.BottomLeft :
                    if (x>0 &&  z>0)
                        xz = new Vector2Int(x-1, z -1);
                    
                    break;
                case Swipe.BottomRight :
                    if (x<(Widht-1) && z>0)
                        xz = new Vector2Int(x+1, z - 1);
                    
                    break;
                
            }
            return xz;
        }
}
}
