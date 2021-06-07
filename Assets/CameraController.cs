using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineMixingCamera mc;
    [SerializeField] float transitionSpeed;

    public void SetActiveCamera(int cameraIndex) => StartCoroutine(SetCameraRoutine(cameraIndex));    

    IEnumerator SetCameraRoutine(float cameraIndex)
    {
        print("USING CAMERA " + cameraIndex);
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
            Player.Instance.StartGame();
        }
    }
}
