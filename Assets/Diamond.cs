using UnityEngine;

public class Diamond : MonoBehaviour, IReset
{
    public void Reset() => gameObject.SetActive(true);    

    private void OnTriggerEnter(Collider other)
    {
        //TODO: score
        gameObject.SetActive(false);
    }
}
