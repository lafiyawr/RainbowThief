using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;




public class health : MonoBehaviour
{
    // Start is called before the first frame update

    public float maxHealth;
    public float currentHealth;
    public int damage;
    public TMP_Text counter;
    public Slider heathBar;
    public Animator anim;
    public Animator parentAnim;
    public GameObject bossHolder;
    



    public delegate void OnBossHurt();
    public static OnBossHurt onBossHurt;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "star")
        {
            if (currentHealth > 1)
            {

                currentHealth -= damage;
          
             //   counter.text = currentHealth.ToString();
                heathBar.value = currentHealth / maxHealth;
            }
            else
            {
                currentHealth = 0;
                StartCoroutine(bossHurt());
              //  print("penguin hurt!");
              //  onBossHurt?.Invoke();
              //  Destroy(gameObject);
               
            }
        }
    }

     IEnumerator bossHurt()
    {
        parentAnim.speed = 0;
        anim.Play("boss-hurting");
        yield return new WaitForSeconds(1f);
        Destroy(bossHolder);
        yield return new WaitForSeconds(.5f);
        onBossHurt?.Invoke();
    }
}
