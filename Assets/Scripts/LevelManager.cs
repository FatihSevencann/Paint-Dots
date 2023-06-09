
using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace PaintDots
{
    public class LevelManager : MonoBehaviour
    {
        public  CameraManager myCamera;
        [SerializeField] public  CellManager blockPrefab;
        [SerializeField] private float cellSize;
       [SerializeField]private Vector3 _gridOriginPos;
        [SerializeField] private BrushController _brushController;
        [SerializeField] private CameraManager solutionCamera;
        [SerializeField] private List<LevelData> _levelDatas;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private Material[] Materials;
        public LineRenderer[] _lineRenderers;
        
        private int width;
        private int height;
        private CellManager[,] _cellManagers;
        
        private GridManager _gridManager;
        private SwipeController _swipeController;
        private BrushController currentBrush;
       [SerializeField] private LinePaint _linePaintPrefab,solutionPrefab;
       private List<Connection> inProgress = new List<Connection>();
        private List<LinePaint> _connectedLinePaints = new List<LinePaint>();
        private int IndexColor=0;
        public  void ChangeColor(int colorIndex)
        {
            IndexColor = colorIndex;
            // Seçilen renk index'ine göre kalem prefabının tüm child objelerinin rengini değiştirme
            switch (colorIndex)
            {
                case 0:
                    SetChildColor(currentBrush.transform, Color.red);
                    break;
                case 1:
                    SetChildColor(currentBrush.transform, Color.yellow);
                    break;
                case 2:
                    SetChildColor(currentBrush.transform, Color.magenta);
                    break;
                case 3:
                    SetChildColor(currentBrush.transform, Color.green);
                    break;
                case 4:
                    SetChildColor(currentBrush.transform, Color.blue);
                    break;
            }
        }
        void SetChildColor(Transform parent, Color color)
        {
            // Parent objenin tüm child objelerinin rengini değiştirme
            foreach (Transform child in parent)
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = color;
                }
            }
        }
        private void Start()
        {
            GameManager.currentLevel = 0;
            GameManager.gameStatus = GameStatus.Playing;
            GameManager.currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
            uiManager.LevelText.text = "Level" +(GameManager.currentLevel + 1);
            _swipeController = new SwipeController();
            _swipeController.SetLevelManager(this);

            width = _levelDatas[GameManager.currentLevel].width;
            height= _levelDatas[GameManager.currentLevel].height;
            
            _gridManager = new GridManager();
            ComplateBoard();
            _gridManager.Initialize(width,height,cellSize,Vector3.zero);
            _cellManagers = new CellManager[width, height];
            
            CreateGrid(Vector3.zero);
            currentBrush = Instantiate(_brushController, _gridManager.GetCellWorldPosition(_levelDatas[GameManager.currentLevel].BrushStartCoords.x,_levelDatas[GameManager.currentLevel].BrushStartCoords.y), Quaternion.Euler(-7f,9,19));
            currentBrush.coords = new Vector2Int(0, 0);
            
            myCamera.ZoomPerspectiveCamera(width,height);
            ChangeColor(IndexColor);
        }

        private void CreateGrid( Vector3 originPos)
        {

            for (int i = 0; i < _gridManager.gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < _gridManager.gridArray.GetLength(1); j++)
                {
                    _cellManagers[i, j] = CreateCell(i, j, originPos);
                }
            }
        }
        private CellManager CreateCell(int x, int y, Vector3 originPos)
        {
            CellManager cell = Instantiate(blockPrefab);
            cell.coords = new Vector2Int(x, y);
            cell.transform.localScale = new Vector3(cellSize, 0.25f, cellSize);
            cell.transform.position=originPos+_gridManager.GetCellWorldPosition(x,y);
            return cell;
        }
        public void MoveBrush(Swipe directions)
        { 
            _lineRenderers[0].sharedMaterial = Materials[IndexColor];
            Material newMaterial = _lineRenderers[0].sharedMaterial;
            Vector2Int newCoords =
                _gridManager.GetCellXZbySwipe(currentBrush.coords.x, currentBrush.coords.y, directions);
            
            if (newCoords != new Vector2Int(-1, -1))
            {
                Vector3 finalPos=_gridManager.GetCellWorldPosition(newCoords.x, newCoords.y);
                
                if (ConnectedAlreadyDone(currentBrush.coords, newCoords,newMaterial,true) == false)
                {
                    inProgress.Add(new Connection(currentBrush.coords, newCoords,newMaterial));
                    _cellManagers[currentBrush.coords.x, currentBrush.coords.y].CellCenter.gameObject.SetActive(true);
                    LinePaint linePaint = Instantiate(_linePaintPrefab, new Vector3(0, 0.2f, 0), Quaternion.identity);
                   
                    linePaint.SetRendererPosition(currentBrush.transform.position + new Vector3(0, 0.2f, 0),
                        finalPos + new Vector3(0, 0.2f, 0),newMaterial);
                   
                    linePaint.SetConnectedCoords(currentBrush.coords, newCoords);
                    _connectedLinePaints.Add(linePaint);
                }
                else
                {
                    RemoveConnectedPaint(currentBrush.coords,newCoords) ;
                }
                if (_levelDatas[GameManager.currentLevel].complatePattern.Count<= inProgress.Count)
                {
                    if (IsCompleteLevel())
                    {
                        GameManager.gameStatus = GameStatus.Complete;
                        GameManager.currentLevel++;
                        if (GameManager.currentLevel > _levelDatas.Count - 1)
                        {
                            GameManager.currentLevel = 0;

                        }
                        PlayerPrefs.SetInt("CurrentLevel", GameManager.currentLevel);
                        uiManager.LevelComplete();
                    }
                    
                }
                currentBrush.transform.position = finalPos;
                currentBrush.coords = newCoords;
            }
        }
        bool ConnectedAlreadyDone(Vector2Int startCoord, Vector2Int endCoords,Material material, bool removeConnection)
        {
            bool connected = false;

            for (int i = 0; i < inProgress.Count; i++)
            {
                if (inProgress[i].StartCoords == startCoord && inProgress[i].EndCoods == endCoords ||
                    inProgress[i].StartCoords == endCoords && inProgress[i].EndCoods == startCoord && inProgress[i].MaterialChoose==material)
                {
                    if (removeConnection)
                    {
                        inProgress.RemoveAt(i); 
                    }
                 
                    connected=true;
                    break;
                    
                }
            }
            return connected;
        }
        public void  RemoveConnectedPaint(Vector2Int startCoord, Vector2Int endCoords)
        {
            for (int i = 0; i < _connectedLinePaints.Count; i++)
            {
                if (_connectedLinePaints[i].StartCoords == startCoord && _connectedLinePaints[i].EndCoords == endCoords ||
                    _connectedLinePaints[i].StartCoords == endCoords && _connectedLinePaints[i].EndCoords == startCoord)
                {
                    LinePaint line = _connectedLinePaints[i];
                    _connectedLinePaints.RemoveAt(i);
                    Destroy(line.gameObject);
                    _cellManagers[endCoords.x,endCoords.y].CellCenter.gameObject.SetActive(false);
                }
            }
            
        }
        bool IsCompleteLevel()
        {
            if (_levelDatas[GameManager.currentLevel].complatePattern.Count != inProgress.Count)
            {
                return false;
            }
            for (int i = 0; i < _levelDatas[GameManager.currentLevel].complatePattern.Count; i++)
            {
                if (!ConnectedAlreadyDone(_levelDatas[GameManager.currentLevel].complatePattern[i].StartCoords,
                        _levelDatas[GameManager.currentLevel].complatePattern[i].EndCoods,_levelDatas[GameManager.currentLevel].complatePattern[i].MaterialChoose,false))
                {
                    return false;
                }
            }
            return true;
        }
        public void ComplateBoard()
        {
            _gridManager.Initialize(width,height,cellSize,_gridOriginPos);
            Vector3 Offset = new Vector3((_levelDatas[GameManager.currentLevel].width - cellSize) / 2,0,
                (_levelDatas[GameManager.currentLevel].height - cellSize) / 2);
            solutionCamera.transform.position += Offset;
            
            solutionCamera.ZoomOrthograpichSizeCamera((_levelDatas[GameManager.currentLevel].width),_levelDatas[GameManager.currentLevel].height);
            
            for (int i = 0; i < _levelDatas[GameManager.currentLevel].complatePattern.Count; i++)
            {
                Vector3 startPos = _gridManager.GetCellWorldPosition(_levelDatas[GameManager.currentLevel].complatePattern[i].StartCoords.x,
                    _levelDatas[GameManager.currentLevel].complatePattern[i].StartCoords.y);
                Vector3 endPos = _gridManager.GetCellWorldPosition(_levelDatas[GameManager.currentLevel].complatePattern[i].EndCoods.x,
                    _levelDatas[GameManager.currentLevel].complatePattern[i].EndCoods.y);
                Material materialObject = _levelDatas[GameManager.currentLevel].complatePattern[i].MaterialChoose;

                LinePaint linePaint = Instantiate(solutionPrefab, new Vector3(-29.5f, 0.2f, 3.2f), Quaternion.identity);
                linePaint.SetRendererPosition(startPos ,endPos,materialObject);
            }
            
            
        }



        private void Update()
        {
            if ( GameManager.gameStatus== GameStatus.Playing )
            {
                _swipeController.OnUpdate();
                
            }
            
            
        }
        
  
            
           
            
        
    }
}