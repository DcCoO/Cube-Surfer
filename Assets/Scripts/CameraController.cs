using Cinemachine;
using System.Collections;
using UnityEngine;

namespace CubeSurferClone
{
    public class CameraController : MonoBehaviour, IReset
    {
        [SerializeField] CinemachineMixingCamera mc;
        [SerializeField] float transitionSpeed;

        public void SetGameCamera() => StartCoroutine(GameCameraRoutine());

        IEnumerator GameCameraRoutine()
        {

            foreach (CinemachineVirtualCamera vcam in mc.ChildCameras) vcam.gameObject.SetActive(false);
            yield return new WaitForEndOfFrame();
            mc.m_Weight0 = mc.m_Weight1 = 1;
            mc.m_Weight2 = 0;
            foreach (CinemachineVirtualCamera vcam in mc.ChildCameras)
            {
                vcam.gameObject.SetActive(true);
                vcam.PreviousStateIsValid = false;
            }

            for (float t = 0; t < 1; t += Time.deltaTime * transitionSpeed)
            {
                mc.m_Weight0 = (1 - t);
                mc.m_Weight1 = t;
                yield return null;
            }
            mc.m_Weight0 = 0;
            mc.m_Weight1 = 1;

            yield return new WaitForSeconds(0.2f);
            Player.Instance.SetStopped(false);
        }

        public void MenuCamera() => StartCoroutine(MenuCameraRoutine());

        IEnumerator MenuCameraRoutine()
        {
            for (float t = 0; t < 1; t += Time.deltaTime * transitionSpeed * 2)
            {
                mc.m_Weight0 = t;
                mc.m_Weight1 = Mathf.Min(mc.m_Weight1, 1 - t);
                mc.m_Weight2 = Mathf.Min(mc.m_Weight0, 1 - t);
                yield return null;
            }
            mc.m_Weight1 = mc.m_Weight2 = 0;
            mc.m_Weight0 = 1;
        }


        public void Reset() => StartCoroutine(ResetRoutine());

        IEnumerator ResetRoutine()
        {
            foreach (CinemachineVirtualCamera vcam in mc.ChildCameras) vcam.gameObject.SetActive(false);
            yield return new WaitForEndOfFrame();
            mc.m_Weight0 = mc.m_Weight2 = 0;
            mc.m_Weight1 = 1;
            foreach (CinemachineVirtualCamera vcam in mc.ChildCameras)
            {
                vcam.gameObject.SetActive(true);
                vcam.PreviousStateIsValid = false;
            }
        }

        public void WinCamera() => StartCoroutine(WinCameraRoutine());

        IEnumerator WinCameraRoutine()
        {
            for (float t = 0; t < 1; t += Time.deltaTime * transitionSpeed)
            {
                mc.m_Weight0 = Mathf.Min(mc.m_Weight0, 1 - t);
                mc.m_Weight1 = Mathf.Min(mc.m_Weight1, 1 - t);
                mc.m_Weight2 = t;
                yield return null;
            }
            mc.m_Weight0 = mc.m_Weight1 = 0;
            mc.m_Weight2 = 1;

            PoolController.Instance.Confetti();
        }
    }
}
