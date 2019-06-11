using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is NULL!");
            }

            return _instance;
        }
    }

    public Text playerGemCountText;
    public Text gemCountText;
    public Image selectionImage;
    public Image[] healthBars;
    public GameObject HUD;
    public GameObject GameOverPanel;
    public GameObject WinPanel;
    public GameObject[] GotItemText;
    public GameObject NeedKeyText;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        selectionImage.enabled = false;
    }

    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = gemCount.ToString() + "G";
    }

    public void UpdateShopSelection(float yPos)
    {
        selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, yPos);
        if (!selectionImage.enabled)
        {
            selectionImage.enabled = true;
        }
    }

    public void UpdateGemCount(int count)
    {
        gemCountText.text = count.ToString();
    }

    public void UpdateLives(int livesRemaining)
    {
        healthBars[livesRemaining].enabled = false;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    public void Win()
    {
        StartCoroutine(WinRoutine());
    }

    public void GotItemTextEnable(int itemCode) // 0 to 2
    {
        GotItemText[itemCode].SetActive(true);
        StartCoroutine(GotItemWaitRoutine(itemCode));
    }

    public void NeedKey()
    {
        NeedKeyText.SetActive(true);
        StartCoroutine(NeedKeyRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(4.0f);
        HUD.SetActive(false);
        GameOverPanel.SetActive(true);
    }

    IEnumerator WinRoutine()
    {
        HUD.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        WinPanel.SetActive(true);
    }

    IEnumerator GotItemWaitRoutine(int itemCode)
    {
        yield return new WaitForSeconds(8f);
        GotItemText[itemCode].SetActive(false);
    }

    IEnumerator NeedKeyRoutine()
    {
        yield return new WaitForSeconds(8f);
        NeedKeyText.SetActive(false);
    }
}
