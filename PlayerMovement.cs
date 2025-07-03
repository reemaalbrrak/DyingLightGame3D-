using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float PlayerSpeed;
    public float JumpSpeed;

    public Rigidbody PlayerRB;
    public GameObject PlayerCamera;
    public bool isGrounded;

    void Update()
    {
        float horoizontal = Input.GetAxis("Horizontal") * PlayerSpeed;
        float vertical = Input.GetAxis("Vertical") * PlayerSpeed;

        Vector3 direction = transform.right * horoizontal + transform.forward * vertical;
        direction.y = PlayerRB.linearVelocity.y;
        PlayerRB.linearVelocity = direction;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            PlayerRB.AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
        }

        MoveCamera();
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && isGrounded)
        {
            if (!SoundManager.Instance.footstepSound.isPlaying)
                SoundManager.Instance.PlayFootstep();
        }
        else
        {
            SoundManager.Instance.footstepSound.Stop();
        }

    }

    float rotationCam = 0;
    void MoveCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX);

        float mouseY = Input.GetAxis("Mouse Y");
        rotationCam -= mouseY;

        PlayerCamera.transform.localRotation = Quaternion.Euler(rotationCam, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager1.Instance.TakeDamage();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
