using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private PlayerMove Player;

    public GameObject HpPotion;
    public GameObject MpPotion;

    public int HpPotionAmount;
    public int MpPotionAmount;

    public Text HpPotionText;
    public Text MpPotionText;

    void Awake()
    {
        HpPotionText.text = " " + HpPotionAmount;
        MpPotionText.text = " " + MpPotionAmount;
        Player = FindObjectOfType<PlayerMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            //Debug.Log("z");
            ItemType items = collision.gameObject.GetComponent<ItemType>();
            switch(items.type)
            {
                case "HPpotion":
                    HpPotionEat();
                    Destroy(collision.gameObject);
                    break;
                case "MPpotion":
                    MpPotionEat();
                    Destroy(collision.gameObject);
                    break;
            }
        }

    }

    public void HpPotionEat()
    {
        HpPotionAmount = HpPotionAmount + 1;
        HpPotionText.text = " " + HpPotionAmount;
    }

    public void MpPotionEat()
    {
        MpPotionAmount = MpPotionAmount + 1;
        MpPotionText.text = " " + MpPotionAmount;
    }

    public void HpHeal()
    {
        if(HpPotionAmount >= 1)
        {
            if (Player.Health + 50 < Player.StartHealth)
            {
                Player.Health = Player.Health + 50;
                HpPotionAmount = HpPotionAmount - 1;
            }
            else
            {
                Player.Health = Player.StartHealth;
                HpPotionAmount = HpPotionAmount - 1;
            }
        }
        else
        {
            Debug.Log("HP포션없다");
        }
        Player.HealthBar.GetComponent<Image>().fillAmount = Player.Health / Player.StartHealth;
        Player.StateHealthBar.GetComponent<Image>().fillAmount = Player.Health / Player.StartHealth;
        Player.MainHpText.text = Player.Health + "/" + Player.StartHealth;
        Player.HpStateText.text = Player.Health + "/" + Player.StartHealth;
        HpPotionText.text = " " + HpPotionAmount;
    }

    public void MpHeal()
    {
        if (MpPotionAmount >= 1)
        {
            if (Player.Mp + 50 < Player.StartMp)
            {
                Player.Mp = Player.Mp + 50;
                MpPotionAmount = MpPotionAmount - 1;
            }
            else
            {
                Player.Mp = Player.StartMp;
                MpPotionAmount = MpPotionAmount - 1;
            }
        }
        else
        {
            Debug.Log("MP포션없다");
        }
        Player.MpBar.GetComponent<Image>().fillAmount = Player.Mp / Player.StartMp;
        Player.StateMpBar.GetComponent<Image>().fillAmount = Player.Mp / Player.StartMp;
        Player.MainMpText.text = Player.Mp + "/" + Player.StartMp;
        Player.MpStateText.text = Player.Mp + "/" + Player.StartMp;
        MpPotionText.text = " " + MpPotionAmount;
    }
}
