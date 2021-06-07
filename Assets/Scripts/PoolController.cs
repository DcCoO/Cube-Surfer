using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : SingletonMonoBehaviour<PoolController>
{
    [Header("Explosion Particle")]
    [SerializeField] GameObject explosionParticle;
    Stack<GameObject> explosionPool = new Stack<GameObject>();
    [SerializeField] string explosionID;

    [Header("References")]
    [SerializeField] Transform levelParent;
    
    public void AddToPool(GameObject go, string objectType)
    {
        go.SetActive(false);
        if (objectType == explosionID) explosionPool.Push(go);
    }

    public void StopPooling() => StopAllCoroutines();

    public GameObject GetExplosion()
    {
        GameObject go;

        if (explosionPool.Count > 0) go = explosionPool.Pop();        
        else go = Instantiate(explosionParticle, levelParent);

        go.SetActive(true);
        Poolable poolable = go.GetComponent<Poolable>();
        poolable.Reset();
        poolable.PoolAfterSeconds(3);
        return go;
    }

    public void Reset()
    {
        while (explosionPool.Count > 0) Destroy(explosionPool.Pop());
    }
}