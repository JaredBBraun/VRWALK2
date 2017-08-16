using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FujiTeleporter : MonoBehaviour
{
    #region ENUM
    [System.Serializable]
    public enum STATE
    {
        IDLE,
        TRIGGERED,
        TELEPORTING,

    }
   

    STATE state = STATE.IDLE;
    #endregion

    private GameObject player, playerhead;
    private GameObject regionToTeleport;
    private GameObject centerToTeleport;
    private string teleporter;
    char ch, chs;
    int SectionNum;
    bool InsideSelf = false, lookingDown = false;

    private SteamVR_Fade fade;
    public float FadeTime;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fade = player.GetComponentInChildren<SteamVR_Fade>();
        playerhead = player.GetComponentInChildren<SteamVR_Fade>().gameObject;
    }
	
	
	void Update ()
    {
        if (lookingDown == true)
        {
            StartCoroutine(Transport(FadeTime));
        }
        else if (lookingDown == false)
        {
            NotTransport();

        }
        CheckSectionNum();

        switch (state)
        {
            case STATE.TRIGGERED:
                //do effect
                if (centerToTeleport != null && (InsideSelf == false) )
                {
                    if (playerhead.transform.localEulerAngles.x > 70f && playerhead.transform.localEulerAngles.x < 90f)
                    {
                        lookingDown = true;
                        
                    }
                    else if (playerhead.transform.localEulerAngles.x < 70f)
                    {
                        lookingDown = false;
                    }
                    

                }

                break;

            case STATE.TELEPORTING:
                //teleporting stuff

                if (lookingDown == true)
                {
                    player.transform.position = new Vector3(centerToTeleport.transform.position.x,
                   player.transform.position.y,
                   centerToTeleport.transform.position.z);
                }
               
                state = STATE.IDLE;

                break;

            case STATE.IDLE:
                //teleporting stuff
                fade.OnStartFade(Color.clear, .01f, true);
                        
                break;


        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("teleporter"))
        {
           
            teleporter = other.gameObject.name; //"5 - 6"

            ch = teleporter[4]; //'6'
            chs = teleporter[0]; //'5'
            
            centerToTeleport = GameObject.Find(ch + " - 5"); //"6 - 5" Center Cube for '6'
            if (centerToTeleport == GameObject.Find(ch + " - " + ch) && (SectionNum.ToString() == ch.ToString() )) //if we are in 6 - 6 dont teleport
            {
                InsideSelf = true;
            }
            else
            {
                InsideSelf = false;
            }

            if (InsideSelf == false)
            {
                state = STATE.TRIGGERED;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("teleporter"))
        {
            state = STATE.IDLE;
            teleporter = "";
            centerToTeleport = null;
        }
    }

    IEnumerator Transport(float time)
    {
        fade.OnStartFade(Color.black, time - .95f, true);
        yield return new WaitForSeconds(time);
        state = STATE.TELEPORTING;
    }

    void NotTransport()
    {
        fade.OnStartFade(Color.clear, .001f, true);
        //yield return new WaitForSeconds(.001f);
        //state = STATE.IDLE;
    }

    void CheckSectionNum()
    {
        if (chs == '1')
        {
            SectionNum = 1;
        }
        if (chs == '2')
        {
            SectionNum = 2;
        }
        if (chs == '3')
        {
            SectionNum = 3;
        }
        if (chs == '4')
        {
            SectionNum = 4;
        }
        if (chs == '5')
        {
            SectionNum = 5;
        }
        if (chs == '6')
        {
            SectionNum = 6;
        }
        if (chs == '7')
        {
            SectionNum = 7;
        }
        if (chs == '8')
        {
            SectionNum = 8;
        }
        if (chs == '9')
        {
            SectionNum = 9;
        }
    }
}
