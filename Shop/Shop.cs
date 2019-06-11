using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public int currentSelectedItem;
    public int currentItemCost;

    private Player _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player = other.GetComponent<Player>();
            if (_player != null)
            {
                UIManager.Instance.OpenShop(_player.amountOfDiamond);
            }
            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        // 0 = flame sword
        // 1 = boots of flight
        // 2 = key to castle

        Debug.Log("Select() " + item);
        currentSelectedItem = item;
        switch (item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelection(82f);
                currentItemCost = 200;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelection(-32f);
                currentItemCost = 400;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelection(-140f);
                currentItemCost = 100;
                break;
        }
    }

    public void BuyItem()
    {
        if (_player.amountOfDiamond >= currentItemCost)
        {
            // award item
            if (currentSelectedItem == 0) //flame Sword dmg 2
            {
                GameManager.Instance.FlameEnabled = true;
                UIManager.Instance.GotItemTextEnable(0);
            }
            else if (currentSelectedItem == 1) //boots of flight: jump higher
            {
                _player.jumpForce = 11f;
                UIManager.Instance.GotItemTextEnable(1);
            }
            else if (currentSelectedItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
                UIManager.Instance.GotItemTextEnable(2);
            }

            _player.amountOfDiamond -= currentItemCost;
            Debug.Log("Purchased " + currentSelectedItem);
            Debug.Log("Gems remained " + _player.amountOfDiamond);
        }
        else
        {
            Debug.Log("You do not have enough gems. Closing shop.");
        }

        UIManager.Instance.UpdateGemCount(_player.amountOfDiamond);
        shopPanel.SetActive(false);
    }



}
