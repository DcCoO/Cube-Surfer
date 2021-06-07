using UnityEngine;

public class Collectible : MonoBehaviour, IReset
{
    [SerializeField] int numCollectibles;

    public void Reset() => gameObject.SetActive(true);
    

    private void OnTriggerEnter(Collider other)
    {
        Player.Instance.Collect(numCollectibles);
        gameObject.SetActive(false);
    }
}
