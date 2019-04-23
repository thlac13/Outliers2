using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{

    public CharacterController charcontrol;

    float horizontalmove = 0f;

    float verticalmove = 0f;

    public float charspeed = 20f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 horiz = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);

        Vector2 vert = new Vector2(Input.GetAxisRaw("Vertical"), 0f);

        transform.position = (horiz * Time.deltaTime * charspeed) + (vert * Time.deltaTime * charspeed);


    }
}
