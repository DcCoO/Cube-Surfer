using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour, IReset
{
    [SerializeField] CinemachineMixingCamera mc;
    [SerializeField] float transitionSpeed;

    public void SetActiveCamera(int cameraIndex) => StartCoroutine(SetCameraRoutine(cameraIndex));    

    IEnumerator SetCameraRoutine(float cameraIndex)
    {
        for(float t = 0; t < 1; t += Time.deltaTime * transitionSpeed)
        {
            mc.m_Weight0 = (1 - t) * cameraIndex;
            mc.m_Weight1 = t * cameraIndex;
            yield return null;
        }
        mc.m_Weight0 = 1f - cameraIndex;
        mc.m_Weight1 = cameraIndex;

        if (cameraIndex == 1)
        {
            yield return new WaitForSeconds(0.2f);
            Player.Instance.SetStopped(false);
        }
    }

    public void Reset() => StartCoroutine(ResetRoutine());
    

    IEnumerator ResetRoutine()
    {
        foreach(CinemachineVirtualCamera vcam in mc.ChildCameras) vcam.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        foreach (CinemachineVirtualCamera vcam in mc.ChildCameras)
        {
            vcam.gameObject.SetActive(true);
            vcam.PreviousStateIsValid = false;
        }
    }
}
