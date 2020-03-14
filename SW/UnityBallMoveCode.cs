using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Program currently moves ball upward. Must implment physics colliders and
//Have animated move path
public class NewBehaviourScript : MonoBehaviour
{
    public GameObject maincamera;
    public GameObject bubble;
    public EdgeCollider2D col;
    public CircleCollider2D coll;
    public float moveSpeed = 40f;
    bool touching = false;

    // Start is called before the first frame update
    void Start()
    {
        coll = bubble.GetComponent<CircleCollider2D>();
        col = maincamera.GetComponent<EdgeCollider2D>();
    }

    /* IN PROCESS - IMPLEMENTING EDGE DETECTION
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("GameObject1 collided with " + col.name);
        touching = true;
    }
    */

// Update is called once per frame
void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 10; i++)
            {
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                
            }
            /*
            while (touching == false)
            {
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                OnTriggerExit2D(col);
            }
            */
        }

    }
}
