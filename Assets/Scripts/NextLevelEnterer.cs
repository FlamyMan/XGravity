using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.code
{
    public class NextLevelEnterer : MonoBehaviour
    {
        public void NextLevel()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}
