using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public KeyCode Reset = KeyCode.Return ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Reset))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
