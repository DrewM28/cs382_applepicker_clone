using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Basket : MonoBehaviour
{
    public ScoreCounter scoreCounter;
    // Start is called before the first frame update
    void Start()
    {
        //Find a game object named score counter in the scene hierarchy
        GameObject scoreGO = GameObject.Find( "ScoreCounter" );
        //Get the score counter (script) component of scoreGO
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();

    }

    // Update is called once per frame
    void Update()
    {
        //Get the current screen position of the mouse from Input
        Vector3 mousePos2D = Input.mousePosition;

        //The Camera's z position sets how far to push the mouse into 3D
        //If this line causes a NullReferenceException, select the Main Camera
        //in the hierarchy and set its tag to MainCamera in the Inspector
        mousePos2D.z = -Camera.main.transform.position.z;

        //Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( mousePos2D );

        //Move the x position of this Basket to the x position of the mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }


        void OnCollisionEnter( Collision coll ) {
        //Find out what hit the basket
        GameObject collideWith = coll.gameObject;
        if ( collideWith.CompareTag("Apple")) {
            Destroy( collideWith );
            //Increase the score
            scoreCounter.score += 100;
            HighScore.TRY_SET_HIGH_SCORE( scoreCounter.score );
        }
        else if ( collideWith.CompareTag("RottenApple")) {
            Destroy( collideWith );
            SceneManager.LoadScene( "Start_Scene" );
        }
    }
}
