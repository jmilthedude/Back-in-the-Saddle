using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public PlayerSettings PlayerSettings { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        PlayerSettings = PlayerSettings.Load();
    }

    public void SetScene(Scene scene)
    {
        SceneManager.LoadScene(scene.buildIndex);
    }
}