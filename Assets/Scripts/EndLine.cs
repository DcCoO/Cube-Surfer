using UnityEngine;

public class EndLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) => EventController.Instance.OnGameWin();    
}
