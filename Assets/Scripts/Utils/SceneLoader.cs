using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private class LoadSceneTask
    {
        private AsyncOperation m_AsyncOperation = null;

        private string m_SceneName = null;

        public Scene LoadedScene;

        private Action<Scene> m_Callback = null;

        private bool m_SetActive = false;

        public LoadSceneTask(string sceneName, bool setActive, Action<Scene> callback)
        {
            m_Callback = callback;
            m_SceneName = sceneName;
            m_SetActive = setActive;
        }

        public void Start(LoadSceneMode mode)
        {
            m_AsyncOperation = SceneManager.LoadSceneAsync(m_SceneName, mode);
            m_AsyncOperation.completed += HandleAsyncOperationComplete;
        }

        private void HandleAsyncOperationComplete(AsyncOperation operation)
        {
            LoadedScene = SceneManager.GetSceneByName(m_SceneName);

            if (m_SetActive)
            {
                SceneManager.SetActiveScene(LoadedScene);
            }

            m_Callback?.Invoke(LoadedScene);
        }
    }

    private static Dictionary<string, Scene> m_LoadedScenes = new Dictionary<string, Scene>();

    private static Dictionary<string, LoadSceneTask> m_LoadingTasks = new Dictionary<string, LoadSceneTask>();

    static SceneLoader()
    {
        RegisterLoadedScenes();
    }

    private static void RegisterLoadedScenes()
    {
        m_LoadedScenes.Clear();
        int loadedScenesCount = SceneManager.sceneCount;

        for (int i = 0; i < loadedScenesCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (!m_LoadedScenes.ContainsKey(scene.name))
            {
                m_LoadedScenes.Add(scene.name, scene);
            }
        }
    }

    public static bool IsLoaded(string sceneName) => m_LoadedScenes.ContainsKey(sceneName);

    public static void LoadScene(
        string sceneName, 
        Action<Scene> onSceneLoadedCallback, 
        LoadSceneMode mode = LoadSceneMode.Single, 
        bool setSceneActive = true)
    {
        if (m_LoadingTasks.ContainsKey(sceneName))
        {
            Debug.LogError($"Scene {sceneName} is alreadyLoading!");
            return;
        }

        LoadSceneTask task = new LoadSceneTask(sceneName, setSceneActive, (loadedScene) =>
        {
            m_LoadedScenes[loadedScene.name] = loadedScene;
            m_LoadingTasks.Remove(loadedScene.name);

            onSceneLoadedCallback?.Invoke(loadedScene);
        });

        m_LoadingTasks.Add(sceneName, task);
        task.Start(mode);
    }
}
