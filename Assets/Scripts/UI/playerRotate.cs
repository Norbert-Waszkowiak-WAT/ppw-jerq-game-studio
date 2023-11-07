using UnityEngine;

public class playerRotate : MonoBehaviour
{
    public bool isRotating = false;
    public bool isRotatingArrowLeft = false;
    public bool isRotatingArrowRight = false;

    public float rotationSpeed = 1f;
    public float arrowRotationSpeed = 1000f;

    public GameObject player;

    public KeyCode rotationKey = KeyCode.Mouse0;

    private void Update()
    {
        if (isRotating && !isRotatingArrowRight && !isRotatingArrowLeft)
        {
            Rotate();
        }
        
        if (!Input.GetKey(rotationKey))
        {
            isRotating = false;
        }

        if (isRotatingArrowRight || isRotatingArrowLeft)
        {
            RotateArrows();
        }
    }

    void RotateArrows()
    {
        if (isRotatingArrowLeft)
        {
            player.transform.Rotate(0, arrowRotationSpeed * Time.deltaTime * 100f, 0);
        } else
        {
            player.transform.Rotate(0, -arrowRotationSpeed * Time.deltaTime * 100f, 0);
        }
    }

    void Rotate()
    {
        //mouse rotation
        float rotation = Input.GetAxis("Mouse X") * rotationSpeed * -1;

        player.transform.Rotate(0, rotation, 0);
    }

    public void StartRotation()
    {
        isRotating = true;
    }

    public void RotateArrowLeft()
    {
        isRotatingArrowLeft = true;
    }

    public void RotateArrowRight()
    {
        isRotatingArrowRight = true;
    }

    public void StopRotateArrowLeft()
    {
        isRotatingArrowLeft = false;
    }

    public void StopRotateArrowRight()
    {
        isRotatingArrowRight = false;
    }
}
