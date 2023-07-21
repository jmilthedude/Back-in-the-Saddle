using TMPro;
using UnityEngine;

public class BestTimeUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpText;

    private void Update()
    {
       tmpText.SetText("best: " + LevelTimer.Instance.GetBestTime());
    }
}