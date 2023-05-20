using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerByScene : MonoBehaviour
{
    [SerializeField] private Button _pause;
    [SerializeField] private Button _resume;
    [SerializeField] private Button _menu;
    [SerializeField] private GameObject _gridButtonFinalGame;
    [SerializeField] private Image _loseImage;
    [SerializeField] private Image _winImage;
    private void Awake()
    {
        SetTargetFrameRate();
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneNames.MAIN_SCENE);
    }

    public void PlayerLose()
    {
        _gridButtonFinalGame.SetActive(true);
        _loseImage.gameObject.SetActive(true);
        _pause.gameObject.SetActive(false);
    }

    public void PlayerWin()
    {
        _gridButtonFinalGame.SetActive(true);
        _winImage.gameObject.SetActive(true);
        _pause.gameObject.SetActive(false);
    }

    public void GoToSceneWithLevels()
    {
        SceneManager.LoadScene(SceneNames.LEVELS_SCENE);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #region Пауза и Отмена ее
    public void Pause()
    {
        Time.timeScale = 0f;
        _pause.gameObject.SetActive(false);
        _resume.gameObject.SetActive(true);
        _menu.gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        _pause.gameObject.SetActive(true);
        _resume.gameObject.SetActive(false);
        _menu.gameObject.SetActive(false);
    }
    #endregion

    #region Уровни
    public void GoToLevel1()
    {
        SceneManager.LoadScene(SceneNames.LEVEL_1);
    }
    public void GoToLevel2()
    {
        SceneManager.LoadScene(SceneNames.LEVEL_2);
    }
    public void GoToLevel3()
    {
        SceneManager.LoadScene(SceneNames.LEVEL_3);
    }
    #endregion

    #region Системные настройки
    public void SetTargetFrameRate()
    {
        Application.targetFrameRate = 60;
    }
    #endregion

    public void Exit()
    {
        Application.Quit();
    }




}
