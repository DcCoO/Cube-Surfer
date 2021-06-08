using UnityEngine;

namespace CubeSurferClone
{
    public class Lava : MonoBehaviour
    {
        [SerializeField] float damageTime;
        float count;

        private void Update() => count += Time.deltaTime;

        private void OnTriggerStay(Collider other)
        {
            if (count >= damageTime)
            {
                Player.Instance.FallInLava();
                count = 0;
            }
        }
    }
}
