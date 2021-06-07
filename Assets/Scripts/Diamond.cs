using UnityEngine;

public class Diamond : MonoBehaviour, IReset
{
    private void Start() => LevelBuilder.Instance.AddResetable(this);
    public void Reset() => gameObject.SetActive(true);    

    private void OnTriggerEnter(Collider other)
    {
        //TODO: score
        gameObject.SetActive(false);
    }
}
