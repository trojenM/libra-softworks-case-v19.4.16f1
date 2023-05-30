using Game.Core.Enums;
using Game.Service;
using UnityEngine;

namespace Game.Core.ItemBase
{
    public class ItemFactory : MonoBehaviour, IProvidable
    {
        [Header("Items")]
        [SerializeField] private GameObject crossObj; // X

        private void Awake()
        {
            ServiceProvider.Register(this);
        }

        public Item CreateItem(ItemType type, Vector2Int itemPosition, Quaternion rotation, Transform parent = null)
        {
            GameObject prefab = null;

            switch (type)
            {
                case ItemType.Cross:
                    prefab = crossObj;
                    break;
                default:
                    Debug.LogWarning("Can not create item: " + type);
                    break;
            }

            if (prefab != null)
            {
                GameObject itemObj = Instantiate(prefab, new Vector3(itemPosition.x, itemPosition.y, 0), rotation, parent);
                Item item = itemObj.GetComponent<Item>();
            
                return item;
            }

            return null;
        }
    }
}
