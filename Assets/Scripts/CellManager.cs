using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintDots
{
    public class CellManager : MonoBehaviour
    {
        [SerializeField] private MeshRenderer cellCenter;
        [HideInInspector]public Vector2Int coords;

        public MeshRenderer CellCenter
        {
            get
            {
                return cellCenter;
            }
        }
        
        




    }

}
