using UnityEngine;

public class Look2d : MonoBehaviour
{
    private bool hasChanged = false;
    private float RotationY;
    private void Start() {
        RotationY = transform.eulerAngles.y;
    }
    private void Update() {

        if(Camera.main.transform.position.x >= transform.position.x && !hasChanged){
            hasChanged = true;
            transform.eulerAngles = new Vector3(0.0f,RotationY,0.0f);
        }
        else if(Camera.main.transform.position.x <= transform.position.x && hasChanged){
            hasChanged = false;
            transform.eulerAngles = new Vector3(0.0f,-RotationY,0.0f);
        }
    }
}
