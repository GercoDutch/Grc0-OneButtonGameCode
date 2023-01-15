using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notes : MonoBehaviour
{
    [SerializeField] public List<GameObject> notesList;

    [SerializeField] private GameObject hitZone;
    [SerializeField] private GameObject hitZoneRing;

    [Header("Music system")]
    public AudioSource musicSegment;
    bool hasPlayed = false;
    public GameObject musicTrigger;

    public float speed;

    float combo;
    float bestCombo;
    float hitDamage;
    float comboDamage;
    float totalDamage;

    [Header("Quality Sprites")]
    public GameObject perfect;
    public GameObject good;
    public GameObject okay;
    public GameObject bad;
    public GameObject miss;
    public TextMeshProUGUI dmgCounter;

    private void Update()
    {
        if (notesList.Count != 0 && !transform.parent.GetComponent<CombatManager>().gameEnded)
        {
            musicTrigger.transform.position += new Vector3(-speed, 0) * Time.deltaTime;

            notesList.ForEach(note => 
            {
                note.transform.position += new Vector3(-speed, 0) * Time.deltaTime;
            });

            if (transform.parent.GetComponent<CombatManager>().comboList[0] == gameObject)
            {
                float r = notesList[0].transform.position.x - hitZone.transform.position.x;
                Color ringColor = hitZoneRing.GetComponent<SpriteRenderer>().color;
                ringColor.a = 1f - Mathf.Abs(r) * 0.5f;
                hitZoneRing.GetComponent<SpriteRenderer>().color = ringColor;
            }


                if (musicTrigger.transform.position.x - hitZone.transform.position.x <= 0 && !musicSegment.isPlaying && !hasPlayed)
            {
                musicSegment.Play();

                hasPlayed = true;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space) || notesList[0].transform.position.x - hitZone.transform.position.x <= -1.6f)
            {
                if (transform.parent.GetComponent<CombatManager>().comboList[0] == gameObject && transform.parent.GetComponent<CombatManager>().playerHP > 0 && notesList[0].transform.position.x - hitZone.transform.position.x <= 3f)
                {
                    SpaceHit();
                }
            }
        }
/*        if (notesList.Count == 0)
        {
            gameObject.SetActive(false);
        }*/

    }

    void SpaceHit()
    {

        float hit = notesList[0].transform.position.x - hitZone.transform.position.x;
        hit = Mathf.Abs(hit) - 1.5f;
        
        if (hit > 0)
        {
            Debug.Log("Miss");
            transform.parent.GetComponent<CombatManager>().AnimationPlayer(1);

            transform.parent.GetComponent<CombatManager>().playerHP -= 20;
            combo = 0;


            StartCoroutine(FadeIn(miss.GetComponent<SpriteRenderer>()));
        }

        else if(hit < 0) {
            hit = hit * -10f;
            hitDamage = hitDamage + hit;

            if (notesList.Count > 1)
            {
                transform.parent.GetComponent<CombatManager>().AnimationPlayer(0);
            }

            else { transform.parent.GetComponent<CombatManager>().AnimationPlayer(2); }

            if (hit > 0 && hit < 4)
            {
                Debug.Log("bad");
                combo = 0;
                transform.parent.GetComponent<CombatManager>().playerHP -= 10;

                StartCoroutine(FadeIn(bad.GetComponent<SpriteRenderer>()));
            }


            if (hit >= 4 && hit < 8)
            {
                Debug.Log("Okay");
                transform.parent.GetComponent<CombatManager>().playerHP -= 5;

                StartCoroutine(FadeIn(okay.GetComponent<SpriteRenderer>()));
            }


            if (hit >= 8 && hit < 12)
            {
                Debug.Log("Good");
                combo = combo + 1;

                StartCoroutine(FadeIn(good.GetComponent<SpriteRenderer>()));
            }


            if (hit >= 12)
            {
                Debug.Log("Pefect");
                combo = combo + 1;

                StartCoroutine(FadeIn(perfect.GetComponent<SpriteRenderer>()));
            }
        }


        if (combo > bestCombo) { bestCombo = combo; }

        comboDamage = bestCombo / 10f + 1f;
        totalDamage = hitDamage * comboDamage;

        if (notesList.Count == 1)
        {
            transform.parent.GetComponent<CombatManager>().enemyHP -= totalDamage;
            combo = 0;
            bestCombo = 0;
            StartCoroutine(DamageCounter(totalDamage));
            Debug.Log("You just dealt " + totalDamage + " Damage");
        }

        Destroy(notesList[0]); 
        notesList.RemoveAt(0);
    }

    IEnumerator FadeAway(SpriteRenderer _sprite)
    {
        Color tmpColor = _sprite.color;
        while (tmpColor.a > 0f)
        {
            tmpColor.a -= 1f * Time.deltaTime / 0.25f;
            _sprite.color = tmpColor;

            if (tmpColor.a <= 0f)
                tmpColor.a = 0f;

            yield return null;
        }
            yield return null;        
    }
    IEnumerator FadeIn(SpriteRenderer _sprite)
    {

        if(_sprite != perfect.GetComponent<SpriteRenderer>())
        {
            StartCoroutine(FadeAway(perfect.GetComponent<SpriteRenderer>()));
        }
        if (_sprite != good.GetComponent<SpriteRenderer>())
        {
            StartCoroutine(FadeAway(good.GetComponent<SpriteRenderer>()));
        }
        if (_sprite != okay.GetComponent<SpriteRenderer>())
        {
            StartCoroutine(FadeAway(okay.GetComponent<SpriteRenderer>()));
        }
        if (_sprite != bad.GetComponent<SpriteRenderer>())
        {
            StartCoroutine(FadeAway(bad.GetComponent<SpriteRenderer>()));
        }
        if (_sprite != miss.GetComponent<SpriteRenderer>())
        {
            StartCoroutine(FadeAway(miss.GetComponent<SpriteRenderer>()));
        }

        Color tmpColor = _sprite.color;
        while (tmpColor.a < 1f)
        {
            tmpColor.a += 1f * Time.deltaTime / 0.25f;
            _sprite.color = tmpColor;

            if (tmpColor.a >= 1f)
            {
                tmpColor.a = 1f;
            }
            yield return null;
        }
        yield return null;   
        
        
        
        
    }

    IEnumerator DamageCounter(float damage)
    {
        dmgCounter.gameObject.SetActive(true);

        int dmgCurrent = 0;
        dmgCounter.text = $"{dmgCurrent}";
        while (dmgCurrent < damage)
        {
            dmgCurrent++;
            dmgCounter.text = "-" + $"{dmgCurrent}";
            yield return new WaitForSeconds(0.01f);
        }

        StartCoroutine(FadeAway(miss.GetComponent<SpriteRenderer>()));
        StartCoroutine(FadeAway(bad.GetComponent<SpriteRenderer>()));
        StartCoroutine(FadeAway(okay.GetComponent<SpriteRenderer>()));
        StartCoroutine(FadeAway(good.GetComponent<SpriteRenderer>()));
        StartCoroutine(FadeAway(perfect.GetComponent<SpriteRenderer>()));

        yield return new WaitForSeconds(1f);

        dmgCounter.gameObject.SetActive(false);

        yield return null;
    }


}