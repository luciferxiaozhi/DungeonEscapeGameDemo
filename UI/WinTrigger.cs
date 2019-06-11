using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (GameManager.Instance.HasKeyToCastle)
            {
                UIManager.Instance.Win();
            }
            else
            {
                UIManager.Instance.NeedKey();
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

}
