using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;
    public Slider loadingBar;
    public TextMeshProUGUI loadingText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Debug.Log("현재 씬 이름 : " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        // 현재씬이름이 AwakeScene이라면
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "AwakeScene")
        {
            StartCoroutine(LoadScene("LoginScene"));
        }

    }
    IEnumerator LoadScene(string sceneName)
    {
        Debug.Log("로딩 시작");
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;

            if (loadingBar != null && loadingBar.value < 1f)
            {
                loadingBar.value = asyncLoad.progress;
                loadingText.text = (asyncLoad.progress * 100).ToString("0") + "%";
            }

        }
        Debug.Log("로딩 완료");
    }

}
