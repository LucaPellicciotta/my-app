using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
    public void OnGoToMainScene() => SceneManager.LoadScene(0);
    public void OnGoToSettings() => SceneManager.LoadScene("SettingsScene");
    public void OnGoToSport() => SceneManager.LoadScene("SportScene");
    public void OnGoToBadHabits() => SceneManager.LoadScene("BadHabitsScene");
}
