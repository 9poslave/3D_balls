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
        // ����� � ��� (������ ���� � �����)
        Application.Quit();

        // ���� �������� � �������� Unity, ��� ��������, �� ������:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
