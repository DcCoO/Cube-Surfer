using UnityEngine;

namespace CubeSurferClone
{
    public class EndLine : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) => EventController.Instance.OnGameWin();
    }
}
