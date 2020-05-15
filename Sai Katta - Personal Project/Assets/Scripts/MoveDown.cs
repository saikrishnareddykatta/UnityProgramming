using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float speed = 30f;
    public float zDestroy = -24.0f;
    private Rigidbody objectRb;
    private PlayerController playerControllerScript;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * -speed); //objectRb.AddForce(Vector3.forward * -speed);
        }
        if(playerControllerScript.gameOver == true)
        {
            Destroy(gameObject);
            //Debug.Log("Entered update in movedown");
            // objectRb.AddForce(Vector3.forward * 0);
        }
        if (transform.position.z < zDestroy)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(1);
        }
    }
}
