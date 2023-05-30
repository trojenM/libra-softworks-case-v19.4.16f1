using UnityEngine;

namespace Game.Core.ItemBase
{
    public class Item : MonoBehaviour
    {
        public virtual void DeleteSelf()
        {
            DestroyImmediate(gameObject);
        }

        public virtual void DeleteSelfWithAnimation()
        {
            DeleteSelf();
        }
    }    
}
