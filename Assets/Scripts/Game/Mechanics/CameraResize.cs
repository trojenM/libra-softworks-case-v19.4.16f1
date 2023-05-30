using Game.Core.Grid;
using UnityEngine;

namespace Game.Mechanics
{
    public class CameraResize : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Transform target;
        [SerializeField] public Camera cameraComponent;
        [SerializeField] private GridSystem grid;
        
        [Header("Properties")]
        [SerializeField] public Vector3 offset;

    
        private void Awake()
        {
            if (cameraComponent == null)
            {
                cameraComponent = GetComponent<Camera>();

                if (cameraComponent == null)
                    cameraComponent = GetComponentInChildren<Camera>();
            }
        }

        private void LateUpdate()
        {
            if (target == null)
                return;

            CenterTheGrid();
            FitToScreen();
        }
    
        private void CenterTheGrid()
        {
            float gridOrigin = grid.Size / 2f - 0.5f;
            Vector3 targetPosition = target.position + offset + new Vector3(gridOrigin, gridOrigin, 0f);

            transform.position = targetPosition;
        }
        
       private void FitToScreen()
       {
           float screenRatio = (float)Screen.width / Screen.height;
           float gridRatio = (float)grid.Size / grid.Size;
           float cameraSize;
       
           if (screenRatio >= gridRatio)
           {
               cameraSize = grid.Size / 2f;
           }
           else
           {
               float differenceInSize = gridRatio / screenRatio;
               cameraSize = grid.Size / 2f * differenceInSize;
           }
       
           cameraComponent.orthographicSize = cameraSize * offset.z;
       }

        
    }    
}
