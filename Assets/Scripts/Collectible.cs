using UnityEngine;

namespace CubeSurferClone
{
    public class Collectible : MonoBehaviour, IReset
    {
        [SerializeField] int numCollectibles;

        private void Start() => LevelBuilder.Instance.AddResetable(this);
        public void Reset() => gameObject.SetActive(true);

        private void OnTriggerEnter(Collider other)
        {
            Player.Instance.Collect(numCollectibles);
            AudioController.Instance.PlayCollectible();
            gameObject.SetActive(false);
        }
    }
}
