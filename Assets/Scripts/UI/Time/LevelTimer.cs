using System;
using UnityEngine;

public class LevelTimer : Singleton<LevelTimer>
{
    private static readonly float DefaultBestTime = 3599.999f;
    [SerializeField] private float currentTime;
    [SerializeField] private float bestTime = DefaultBestTime;
    [SerializeField] private bool running;

    public Action<string> OnUpdateTime;
    public Action<string> OnUpdateBestTime;

    public void Update()
    {
        if (!running)
        {
            return;
        }
        UpdateTime();
    }

    private void UpdateTime()
    {
        currentTime += Time.deltaTime;
        OnUpdateTime?.Invoke(GetCurrentTime());
    }

    private string GetAsText(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        return string.Format("{0:00}:{1:00}:{2:000}", timeSpan.Minutes, timeSpan.Seconds,
            timeSpan.Milliseconds).PadRight(9).Substring(0, 9);
    }

    public String GetBestTime()
    {
        return bestTime >= DefaultBestTime ? "00:00:000" : GetAsText(bestTime);
    }

    public String GetCurrentTime()
    {
        return GetAsText(currentTime);
    }

    public void UpdateBestTime()
    {
        if (currentTime < bestTime)
        {
            bestTime = currentTime;
            OnUpdateBestTime?.Invoke(GetBestTime());
        }
    }

    public void ResetTimer()
    {
        currentTime = 0;
    }

    public void ResetAll()
    {
        currentTime = 0;
        bestTime = DefaultBestTime;
    }

    public void Pause()
    {
        running = false;
    }

    public void UnPause()
    {
        running = true;
    }

    public void StartTimer()
    {
        running = true;
    }
}