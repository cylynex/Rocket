using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPlatform : MonoBehaviour {

    GameObject dockTarget = null;
    float yOffset = 0.35f;

    private void OnCollisionEnter(Collision collision) {
        collision.gameObject.GetComponent<Engine>().isDocked = true;
        dockTarget = collision.gameObject;
    }

    private void Update() {
        if (dockTarget != null) {
            if (dockTarget.gameObject.GetComponent<Engine>().isDocked) {
                float yPos = transform.position.y + yOffset;
                Vector3 newPosition = new Vector3(transform.position.x, yPos, dockTarget.gameObject.transform.position.z);
                dockTarget.GetComponent<Engine>().DockPosition(newPosition, gameObject);
            } else {
                dockTarget = null;
            }
        }
    }


}
