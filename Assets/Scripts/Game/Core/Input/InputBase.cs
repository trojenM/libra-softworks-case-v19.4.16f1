using UnityEngine;

namespace Game.Core.Input
{
    public abstract class InputBase : MonoBehaviour 
    {
        public abstract bool ClickInput();
        public abstract Vector2Int GetGridPosition(Camera gridCamera);
    }
}
