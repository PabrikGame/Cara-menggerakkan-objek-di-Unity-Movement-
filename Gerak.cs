using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerak : MonoBehaviour
{
    public float kecepatan;
    public float loncat;
    public float gravitasi;
    public CharacterController Ch;
    Vector3 turun;

    public GameObject[] senjata;
    public float[] gantiSenjata;
    public float[] gantiSenjataAwal;
    public bool[] senjataReady;
    
    // Start is called before the first frame update
    void Start()
    {
        senjata[0].SetActive(true);
        senjata[1].SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        MK();
        tinggi();
        GantiSenjata();
    }

   

    void MK() 
    { 
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        Vector3 gerak = transform.right * x + transform.forward * y;

        if ((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)))
        {
            gerak = transform.forward * 0;
        }

        if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
        {
            gerak = transform.right * 0;
        }

        if (Ch.isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                gerak.z = gerak.z / 3;
                gerak.x = gerak.x / 3;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                gerak.z = gerak.z / 2;
                gerak.x = gerak.x / 2;
            }
        }

        

        Ch.Move(gerak * kecepatan * Time.deltaTime);
    }

    void tinggi()
    {
        turun.y -= gravitasi * Time.deltaTime;
        Ch.Move(turun * Time.deltaTime);

        if (Ch.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                turun.y = loncat;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                turun.y -= gravitasi * 1 / 2;
            }
        }


    }

    void GantiSenjata()
    {
        
        if (Input.GetKey("1") || Input.mouseScrollDelta.y < 0)
        {
            senjataReady[0] = true;
        }
        if (Input.GetKey("2") || Input.mouseScrollDelta.y > 0)
        {
            senjataReady[1] = true;
        }

        if (senjataReady[0] == true)
        {
            gantiSenjata[0] -= 1 * Time.deltaTime;
            senjata[1].SetActive(false);
        }
        if (senjataReady[1] == true)
        {
            gantiSenjata[1] -= 1 * Time.deltaTime;
            senjata[0].SetActive(false);
        }

        if (gantiSenjata[0] < 0) gantiSenjata[0] = 0;
        if (gantiSenjata[1] < 0) gantiSenjata[1] = 0;

        if (gantiSenjata[0] <= 0)
        {
            senjata[0].SetActive(true);
            senjata[1].SetActive(false);
            gantiSenjata[0] = gantiSenjataAwal[0];
            senjataReady[0] = false;
        }
        if (gantiSenjata[1] <= 0)
        {
            senjata[0].SetActive(false);
            senjata[1].SetActive(true);
            gantiSenjata[1] = gantiSenjataAwal[1];
            senjataReady[1] = false;
        }
    }

}
