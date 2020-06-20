using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Start is called before the first frame update
    void Start() {
        Invoke("LoadFirstScene", 2f);
    }

    void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
}
