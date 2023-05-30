using Game.Core.Grid;
using UnityEngine;

namespace Game.Mechanics
{
    public class FitGridToScreen : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] public Vector3 offset;
        [SerializeField] public Camera cameraComponent;
        [SerializeField] private GridSystem grid;
    
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
            float targetAspect = cameraComponent.aspect * offset.z;

            float currentAspect = (float)Screen.width / Screen.height;
            float scaleFactor = currentAspect / targetAspect;

            float defaultSize = grid.Size;
            float newSize = defaultSize * scaleFactor;

            cameraComponent.orthographicSize = newSize;
        }
    }    
}
