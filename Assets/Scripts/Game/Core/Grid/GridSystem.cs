using System;
using System.Collections.Generic;
using Game.Core.Enums;
using Game.Core.Input;
using Game.Items;
using Game.Service;
using UnityEngine;

namespace Game.Core.Grid
{
     [DefaultExecutionOrder(1)]
    public class GridSystem : MonoBehaviour
    {
        
        [Header("References")]
        [SerializeField] private GridCell cellObj;
        [SerializeField] private InputBase input;
        
        [Header("Properties")]
        [SerializeField] private int gridSize;
        [SerializeField] private bool checkDiagonal = true;
        [SerializeField] private bool playAnimationOnItemDelete = true;
        
        private GridCell[,] gridCells;
        private Dictionary<Cross, Vector2Int> neighbourCrosses = new Dictionary<Cross, Vector2Int>();
        private Camera gridCamera;
        
        private const int NeighbourCrossLimit = 3;

        public int Size { get => gridSize; 
                            set => gridSize = value; }

        private void Awake()
        {
            gridCamera = Camera.main;
        }

        private void Start()
        {
            CreateGrid();
        }

        private void Update()
        {
            if (input.ClickInput())
            {
                Vector2Int gridPos = input.GetGridPosition(gridCamera);
                
                if (IsOnGrid(gridPos.x, gridPos.y))
                {
                    GridCell selectedCell = gridCells[gridPos.x, gridPos.y];

                    if (!selectedCell.item)
                    {
                        GridCell cell = gridCells[gridPos.x, gridPos.y];
                        cell.item = ServiceProvider.GetItemFactory.CreateItem(ItemType.Cross, gridPos, Quaternion.Euler(0f, 0f, 45f), cell.transform);
                    
                        CheckAndDestroyConnectedX(gridPos.x, gridPos.y, checkDiagonal);   
                    }
                }
            }
        }

        private void CheckAndDestroyConnectedX(int x, int y, bool considerCorners)
        {
            if (neighbourCrosses.Count >= NeighbourCrossLimit)
            {
                foreach (var cross in neighbourCrosses)
                {
                    Vector2Int crossPos = cross.Value;
                    
                    gridCells[crossPos.x, crossPos.y].item = null;
                    
                    if (playAnimationOnItemDelete)
                        cross.Key.DeleteSelfWithAnimation();
                    else 
                        cross.Key.DeleteSelf();
                }
                        
                neighbourCrosses.Clear();
                return;
            }
            
            for (int xx = -1; xx <= 1; xx++) 
            {
                for (int yy = -1; yy <= 1; yy++) 
                {
                    if (xx == 0 && yy == 0)
                        continue; // self cell.
                    
                    if (!considerCorners && Math.Abs(xx) + Math.Abs(yy) > 1)
                        continue;
                    
                    if (IsOnGrid(x + xx, y + yy))
                    {
                        GridCell neighbourCell = gridCells[x + xx, y + yy];
                        
                        if ((Cross) neighbourCell.item && !neighbourCrosses.ContainsKey((Cross) neighbourCell.item))
                        {
                            neighbourCrosses.Add((Cross) neighbourCell.item, new Vector2Int(x + xx, y+ yy));
                            CheckAndDestroyConnectedX(x + xx, y + yy, considerCorners);
                        }
                    }
                }
            }
            
            neighbourCrosses.Clear();
        }

        private void CreateGrid()
        {
            gridCells = new GridCell[gridSize, gridSize];

            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    GridCell cell = ServiceProvider.GetGridCellPool.Pool.GetFromPool(new Vector3(x, y, 0), Quaternion.identity, transform);
                    gridCells[x, y] = cell;
                }
            }
        }

        private void RemoveGrid()
        {
            foreach (var grid in gridCells)
            {
                ServiceProvider.GetGridCellPool.Pool.AddToPool(grid);
            }

            gridCells = null;
        }

        public void UpdateGrid()
        {
            RemoveGrid();
            CreateGrid();
        }

        public bool IsOnGrid(int x, int y) => (x >= 0 && y >= 0 && x < gridSize && y < gridSize);
        
    }
}