using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintDots
{


    public enum Swipe {None,Top,Bottom,Left,TopLeft,BottomLeft , Right,TopRight, BottomRight }
    
    public class SwipeController : MonoBehaviour
    {
        public Vector2 startPos, endPos;
        private LevelManager _levelManager;
        private Swipe direction;

        public void SetLevelManager(LevelManager manager)
        {

            _levelManager = manager;

        }

        public void OnUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
                endPos = startPos;
                
            }
            else if (Input.GetMouseButtonUp(0))

            {
                endPos = Input.mousePosition;
                if (Vector2.Distance(endPos, startPos) > 0.1f)
                {
                  
                    SwipeDirection();
                    
                }
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private Swipe SwipeDirection()
        {

            direction = Swipe.None;
            Vector2 currentSwipe = endPos - startPos;

            float angle = Mathf.Atan2(currentSwipe.y, currentSwipe.x) * (180 / Mathf.PI);
            
            
            if (angle > 67.5 && angle < 112.5)
            {
                direction = Swipe.Top;
                
            }
            else if (angle > 22.5 && angle < 67.5)
            {
                direction = Swipe.TopRight;
                
            }
            else if (angle > -22.5 && angle < 22.5)
            {
                direction = Swipe.Right;
                
            }
            else if (angle > -67.5 && angle < -22.5)
            {
                direction = Swipe.BottomRight;
                
            }
            else if (angle > -112.5 && angle < -67.5)
            {
                direction = Swipe.Bottom;
                

            }
            else if (angle >-157.5 && angle < -112.5)
            {
                direction = Swipe.BottomLeft;
                
            }
            else if (angle < -157.5 || angle > 157.5)
            {
                direction = Swipe.Left;
                
            }
            else if (angle > 112.5f && angle < 157.5f)
            {
                direction = Swipe.TopLeft;
                
            }

            if (direction != Swipe.None)
            {
               _levelManager.MoveBrush(direction);
                direction = Swipe.None;
                
            }
            

            return direction; 
            
        }
        


    }
}