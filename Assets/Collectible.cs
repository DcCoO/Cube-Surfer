using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] int numCollectibles;

    private void OnTriggerEnter(Collider other)
    {
        Player.Instance.Collect(numCollectibles);
        gameObject.SetActive(false);
    }
}
