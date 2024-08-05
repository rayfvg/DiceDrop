using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
  public void SceneSwitchers(int idScene)
    {
        SceneManager.LoadScene(idScene);
    }
}
