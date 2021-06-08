using System.Collections;
using UnityEngine;

namespace CubeSurferClone
{    
    public class Poolable : MonoBehaviour
    {
        [SerializeField] string poolType;

        public void PoolAfterSeconds(float time) => StartCoroutine(PoolAfterSecondsRoutine(time));
        IEnumerator PoolAfterSecondsRoutine(float time)
        {
            yield return new WaitForSeconds(time);
            PoolController.Instance.AddToPool(gameObject, poolType);
        }
    }    
}
