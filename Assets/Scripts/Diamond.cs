using UnityEngine;

namespace CubeSurferClone
{
    public class Diamond : MonoBehaviour, IReset
    {
        private void Start() => LevelBuilder.Instance.AddResetable(this);
        public void Reset() => gameObject.SetActive(true);

        private void OnTriggerEnter(Collider other)
        {
            ScoreController.Instance.AddScore();
            AudioController.Instance.PlayDiamond();
            HudController.Instance.UpdateHUD();
            gameObject.SetActive(false);
        }
    }
}
