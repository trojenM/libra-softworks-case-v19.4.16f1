using Game.Service;
using UnityEngine;
using Utils;

namespace Game.Core.Grid
{
    public class GridCellPool : MonoBehaviour, IProvidable
    {
        [SerializeField] private GridCell cellObj;
        private ObjectPool<GridCell> pool;

        public ObjectPool<GridCell> Pool => pool;
    
        private void Awake()
        {
            ServiceProvider.Register(this);
        }

        private void Start()
        {
            int poolSize = ServiceProvider.GetInterfaceManager.GetSliderMaxValue;
            poolSize *= poolSize;
        
            pool = new ObjectPool<GridCell>(cellObj, poolSize, transform);
        }
    }   
}
