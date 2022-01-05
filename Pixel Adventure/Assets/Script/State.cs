using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class State : PlayerMove
{
    public void HpAbilityUp()       // 최대 HP업
    {
        if (Ability <= 0)
        {
            Debug.Log("포인트 없어요");
            return;
        }
        if (Ability > 0)
        {
            StartHealth = StartHealth + 10;
            Health = Health + 10;
            Ability = Ability - 1;
        }
    }

    public void MpAbilityUp()           //최대 MP 업
    {
        if (Ability <= 0)
        {
            Debug.Log("포인트 없어요");
            return;
        }
        if (Ability > 0)
        {
            StartMp = StartMp + 10;
            Mp = Mp + 10;
            Ability = Ability - 1;
        }
    }

    public void STRAbilityUp()      //공격력 업
    {
        if (Ability <= 0)
        {
            Debug.Log("포인트 없어요");
            return;
        }
        if (Ability > 0)
        {
            STR = STR + 1;
            Ability = Ability - 1;
        }
    }


    public void DEFAbilityUp()          //방어력 업
    {
        if (Ability <= 0)
        {
            Debug.Log("포인트 없어요");
            return;
        }
        if (Ability > 0)
        {
            DEF = DEF + 1;
            Ability = Ability - 1;
        }
    }

    public void ASAbilityUp()                  //공속 업
    {
        if (Ability <= 0)
        {
            Debug.Log("포인트 없어요");
            return;
        }
        if (Ability > 0)
        {
            if(AS < 2)
            {
                AS = AS + 0.1f;
                maxShotDelay = maxShotDelay - 0.03f;
                Ability = Ability - 1;
            }
            else
            {
                Vector3 vector = this.transform.position;
                vector.y += 3f;

                GameObject clone = Instantiate(prefabs_Floating_text, vector, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingText>().text.text = "더 이상 공속을 올릴 수 없습니다.";
                clone.GetComponent<FloatingText>().text.fontSize = 10;
                clone.transform.SetParent(parent.transform);
            }
        }
    }


    //IEnumerator Lv_up()
    //{
    //    Color color = GetComponent<SpriteRenderer>().color;
    //    color.a = 0f;
    //    GetComponent<SpriteRenderer>().color = color;
    //    yield return new WaitForSeconds(0.1f);
    //    color.a = 1f;
    //    GetComponent<SpriteRenderer>().color = color;
    //    yield return new WaitForSeconds(0.1f);
    //    color.a = 0f;
    //}

}
