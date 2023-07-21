using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerSettings playerSettings;
    [SerializeField] private GameObject fpsCounter;
    [SerializeField] private GameObject bestLabel;
    [SerializeField] private GameObject bestAmount;
    [SerializeField] private GameObject timer;

    private void Start()
    {
        EnableFpsCounter(GameManager.Instance.PlayerSettings.ShowFPS);
    }

    private void EnableFpsCounter(bool enable)
    {
        fpsCounter.SetActive(enable);
    }
}