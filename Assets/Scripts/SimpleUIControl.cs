using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleUIControl : MonoBehaviour
{
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}