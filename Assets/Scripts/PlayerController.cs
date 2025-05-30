using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private int count;
    private float movementX;
    private float movementY;

    public float speed = 10f;
    private float baseSpeed; // для збереження початкової швидкості
    public float jumpForce = 5f;
    private bool isGrounded = true;



    public GameObject pickupEffectPrefab;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        baseSpeed = speed; // зберігаємо початкову швидкість
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp") || other.CompareTag("SpeedBoost") || other.CompareTag("SpeedPenalty"))
        {
            PlayPickupEffect(other.transform.position);

            if (other.CompareTag("PickUp"))
            {
                other.gameObject.SetActive(false);
                count++;
                SetCountText();
            }
            else if (other.CompareTag("SpeedBoost"))
            {
                other.gameObject.SetActive(false);
                StopAllCoroutines();
                StartCoroutine(SpeedChangeRoutine(baseSpeed + 5f, 5f));
            }
            else if (other.CompareTag("SpeedPenalty"))
            {
                other.gameObject.SetActive(false);
                StopAllCoroutines();
                StartCoroutine(SpeedChangeRoutine(baseSpeed - 5f, 5f));
            }
        }
    }
    private void PlayPickupEffect(Vector3 position)
    {
        if (pickupEffectPrefab != null)
        {
            GameObject effect = Instantiate(pickupEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 2f);
        }
    }
    private IEnumerator SpeedChangeRoutine(float newSpeed, float duration)
    {
        speed = newSpeed;
        yield return new WaitForSeconds(duration);
        speed = baseSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
        else if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 22)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }
}
