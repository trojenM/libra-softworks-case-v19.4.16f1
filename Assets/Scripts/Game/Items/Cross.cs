using System.Collections;
using Game.Core.ItemBase;
using UnityEngine;

namespace Game.Items
{
    public class Cross : Item
    {
        public override void DeleteSelfWithAnimation()
        {
            StartCoroutine(CrossDeleteAnimation());
        }

        private IEnumerator CrossDeleteAnimation()
        {
            Vector3 defaultSize = transform.localScale;
            Vector3 swelledSize = defaultSize * 1.75f;
        
            for (float i = 0f; i <= 1f; i += Time.deltaTime / 0.2f)
            {
                transform.localScale = Vector3.Slerp(swelledSize, defaultSize, i);
                yield return new WaitForEndOfFrame();
            }
        
            DeleteSelf();
        }
    }
}
