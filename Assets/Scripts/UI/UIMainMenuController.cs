using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenuController : MonoBehaviour
{
    [SerializeField]
    private Slider m_DifficultySlider;

    
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void Awake()
    {
        m_DifficultySlider.onValueChanged.AddListener(ChangeDifficultyLevel);
    }

    public void StartGame()
    {
        SceneLoader.LoadScene(SceneNamesConsts.SPACE_SCENE_NAME, HandleSceneLaoded);
    }

    private void HandleSceneLaoded(Scene scene)
    {
        Debug.Log($"Loaded scene {scene.name}!");
    }

    public void ChangeDifficultyLevel(float level)
    {
        int newval = (int)(level + 0.5);

        GameplayManager.SetDifficultyLevel(newval);
    }
}   

