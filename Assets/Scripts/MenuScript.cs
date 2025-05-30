using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
   
    public void GoToMinigame()
    {
        SceneManager.LoadScene("Minigame");
    }

    public void ExitGame()
    {
        // Вихід з гри (працює лише в збірці)
        Application.Quit();

        // Якщо запускаєш в редакторі Unity, щоб побачити, що працює:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
