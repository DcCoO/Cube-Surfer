using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineMixingCamera mc;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveCamera(int cameraIndex)
    {
        mc.m_Weight0 = 1 - cameraIndex;
        mc.m_Weight1 = cameraIndex;
    }
}
