using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class DamagedTile : MonoBehaviour
{

    private PlayerMove Player;
    public bool iswalking = false;

    private void Awake()
    {
        Player = FindObjectOfType<PlayerMove>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            iswalking = true;
            StartCoroutine(burn());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.spriteRenderer.color = new Color(1, 1f, 1f, 1f);
            iswalking = false;
        }
    }


    IEnumerator burn()
    {
        while (iswalking == true)
        {
            if (Player.Health <= 0)
            {
                Destroy(gameObject);            //죽는 모션 없시 먼저 죽을시 따로 빼서 함수 만들고 iNVOKE사용해서 디스트로이해줘야됨
            }
            Player.spriteRenderer.color = new Color(1, 0.7f, 0.7f, 1f);
            Player.Health = Player.Health - 2;        //출혈데미지(지속데미지)
            Player.HealthBar.GetComponent<Image>().fillAmount = Player.Health / Player.StartHealth;
            Player.StateHealthBar.GetComponent<Image>().fillAmount = Player.Health / Player.StartHealth;
            Player.MainHpText.text = Player.Health + "/" + Player.StartHealth;
            Player.HpStateText.text = Player.Health + "/" + Player.StartHealth;

            yield return new WaitForSeconds(0.5f);
        }
    }

}
