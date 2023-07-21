using TMPro;
using UnityEngine;

public class CurrentTimeUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpText;

    private void OnEnable()
    {
        LevelTimer.Instance.OnUpdateTime += UpdateTimeText;
    }

    private void OnDisable()
    {
        if (LevelTimer.Instance != null)
        {
            LevelTimer.Instance.OnUpdateTime -= UpdateTimeText;
        }
    }

    private void UpdateTimeText(string currentTime)
    {
        tmpText.text = currentTime;
    }
}