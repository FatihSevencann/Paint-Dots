using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrushingLine
{
    public class GameManager : MonoBehaviour
    {
        public static GameStatus gameStatus = GameStatus.Playing;
        public static int currentLevel = 0;
     
        
    }

    public enum GameStatus
    {
        Playing,
        Complete
        
    }
}