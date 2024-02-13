using UnityEngine;
using Cinemachine;


public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera menuCamera;
    [SerializeField] private CinemachineVirtualCamera gameCamera;


    private void Awake()
    {
        menuCamera.enabled = true;
        gameCamera.enabled = false;
    }


    public void Switch()
    {
        menuCamera.enabled = !menuCamera.enabled;
        gameCamera.enabled = !gameCamera.enabled;
    }
}