using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;



public class BossHealth : MonoBehaviour
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
    public AudioSource bossMusic;
    public AudioSource bossHurtSound;
    public AudioSource bossHitSound;



    // Trigger for the RainbowManager. When the boss is hurt, the rainbow shard appears.
    public delegate void OnBossHurt();
    public static OnBossHurt onBossHurt;

    public void Start()
    {
        timelineObj = GameObject.FindGameObjectWithTag("timeline");
        _playableDirector = timelineObj.GetComponent<PlayableDirector>();
        parentAnim.speed = bossSpeed;
        bossMusic.Play();
    }


    //Take damage every time the star hits the boss
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "star")
        {
            if (currentHealth > 1 && !ishurt)
            {
                currentHealth -= damage;
                heathBar.value = currentHealth / maxHealth;
                bossHitSound.Play();
            }
            else
            {
                if (!ishurt)
                {
                    currentHealth = 0;
                    StartCoroutine(BossHurt());
                }



            }
        }
    }

    IEnumerator BossHurt()
    {
        //Stop him from moving, play hurt animation, restart timeline and then destroy the boss gameobject.
        parentAnim.speed = 0;
        bossMusic.Stop();
        bossHurtSound.Play();
        ishurt = true;
        TimelineControl.StartTimeline(_playableDirector);
        anim.Play("boss-hurting");
        yield return new WaitForSeconds(3f);
        onBossHurt?.Invoke();
        Destroy(bossHolder);

    }
}
