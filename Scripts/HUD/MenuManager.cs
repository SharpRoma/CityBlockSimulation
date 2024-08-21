using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject gamePanel;
    public Button continueButton;
    public Button quitButton;

    private bool isMenuActive = false;

    void Start()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        continueButton.onClick.AddListener(ContinueGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isMenuActive)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        isMenuActive = true;
        Time.timeScale = 0f;  // ��������� ������� � 0 ������������ ������� �����
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    void ResumeGame()
    {
        isMenuActive = false;
        Time.timeScale = 1f;  // �������������� �������� �������
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    void ContinueGame()
    {
        ResumeGame();
    }

    void QuitGame()
    {
        Application.Quit();  // ����� �� ����������
    }
}
