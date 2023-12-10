using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Playables;



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
    private GameObject timelineObj;
    private PlayableDirector _playableDirector;
    bool ishurt = false;
    public float bossSpeed = 1;
    public AudioSource bossTheme;
    public AudioSource bossCurse;




    public delegate void OnBossHurt();
    public static OnBossHurt onBossHurt;

    public void Start()
    {
        timelineObj = GameObject.FindGameObjectWithTag("timeline");
        _playableDirector = timelineObj.GetComponent<PlayableDirector>();
        parentAnim.speed= bossSpeed;
        bossTheme.Play();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "star")
        {
            if (currentHealth > 1 && !ishurt)
            {

                currentHealth -= damage;
          
             //   counter.text = currentHealth.ToString();
                heathBar.value = currentHealth / maxHealth;
            }
            else
            {
                if (!ishurt)
                {
                    currentHealth = 0;
                    StartCoroutine(bossHurt()); 
                }
               
             
               
            }
        }
    }

     IEnumerator bossHurt()
    {
       //Stop him from moving;
        parentAnim.speed = 0;
      bossTheme.Stop();
       bossCurse.Play();
        ishurt = true;
        TimelineControl.StartTimeline(_playableDirector);
        anim.Play("boss-hurting");
        yield return new WaitForSeconds(3f);
        onBossHurt?.Invoke();
        Destroy(bossHolder);
       
    }
}
