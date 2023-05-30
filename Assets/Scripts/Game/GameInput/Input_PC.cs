using Game.Core.Input;
using UnityEngine;

namespace Game.GameInput
{
    public class Input_PC : InputBase
    {
        public override bool ClickInput()
        {
            return Input.GetMouseButtonUp(0);
        }

        public override Vector2Int GetGridPosition(Camera gridCamera)
        {
            Vector3 mousePosition = gridCamera.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.RoundToInt(mousePosition.x);
            int y = Mathf.RoundToInt(mousePosition.y);
        
            return new Vector2Int(x, y);
        }
    }
}
