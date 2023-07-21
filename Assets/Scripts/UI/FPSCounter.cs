using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private PlayerSettings playerSettings;
    [SerializeField] private TMP_Text label;
    [SerializeField] private float updateInterval = 1f;
    private float accum;
    private int frames;
    private float timeleft;
    private float fps;

    private void Start()
    {
        timeleft = updateInterval;
    }

    private void Update()
    {
        if (GameManager.Instance.PlayerSettings.ShowFPS)
        {
            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            ++frames;

            if (timeleft <= 0.0)
            {
                fps = (accum / frames);
                timeleft = updateInterval;
                accum = 0.0f;
                frames = 0;
            }

            label.text = fps.ToString("F2") + " fps";
        }
        else
        {
            label.text = "";
        }
    }
}