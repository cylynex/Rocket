using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeOfBoardKiller : MonoBehaviour {

    private void OnCollisionEnter(Collision collision) {
        print("Something hit killer: " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Obstacle") {
            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "Player") {
            print("Player hit edge of world collider - do something");
        } else {
            print("unexpected object hit edge of world: " + collision.gameObject.name + " - " + collision.gameObject.tag);
        }
    }

}
