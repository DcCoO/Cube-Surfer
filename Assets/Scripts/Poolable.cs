using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour, IReset
{
    [SerializeField] string poolType;

    public void Reset() { }   

    public void PoolAfterSeconds(float time) => StartCoroutine(PoolAfterSecondsRoutine(time));
    IEnumerator PoolAfterSecondsRoutine(float time)
    {
        yield return new WaitForSeconds(time);
        PoolController.Instance.AddToPool(gameObject, poolType);
    }
}
