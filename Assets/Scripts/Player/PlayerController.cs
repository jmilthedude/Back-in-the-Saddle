using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform externalBody;
    [SerializeField] private Transform internalBody;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Collider thisCollider;
    [SerializeField] private GameObject particles;
    [SerializeField] private float forceMultiplier = 5f;
    [SerializeField] private float cameraLerpFactor = .1f;
    [SerializeField] private float rotationLerpSpeed = .1f;

    private Camera mainCamera;

    private Vector2 moveInput;
    private bool hasMoved = false;
    private bool movementLocked;

    private void Awake()
    {
        mainCamera = Camera.main;
    }


    private void OnEnable()
    {
        InputManager.Instance.OnJump += Jump;
    }

    private void OnDisable()
    {
        if(InputManager.Instance != null)
        {
            InputManager.Instance.OnJump -= Jump;
        }
    }

    private void FixedUpdate()
    {
        if (!movementLocked)
        {
            rigidBody.AddForce(new Vector3(moveInput.x * forceMultiplier, 0, moveInput.y * forceMultiplier));
        }
    }

    private void Update()
    {
        if (!movementLocked)
        {
            UpdateMovement();
        }

        TranslateInternal();
        TranslateCamera();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag.Equals("Death"))
        {
            movementLocked = true;
            StartCoroutine(Die(this.thisCollider.ClosestPointOnBounds(other.contacts[0].point)));
        }

        if (other.collider.tag.Equals("Win"))
        {
            movementLocked = true;
            LevelTimer.Instance.Pause();
            LevelTimer.Instance.UpdateBestTime();
            StartCoroutine(Win());
        }
    }

    private IEnumerator Win()
    {
        moveInput = Vector2.zero;
        yield return new WaitForSeconds(1);

        LevelTimer.Instance.ResetTimer();
        GameManager.Instance.SetScene(SceneManager.GetActiveScene());
    }

    private IEnumerator Die(Vector3 collisionPoint)
    {
        moveInput = Vector2.zero;
        LevelTimer.Instance.Pause();
        particles.transform.position = collisionPoint;
        Instantiate(particles);

        yield return new WaitForSeconds(3);

        LevelTimer.Instance.ResetTimer();
        GameManager.Instance.SetScene(SceneManager.GetActiveScene());
    }

    private void TranslateCamera()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 playerPosition = externalBody.transform.position;
        mainCamera.transform.position = Vector3.Lerp(cameraPosition,
            new Vector3(playerPosition.x, cameraPosition.y, playerPosition.z), cameraLerpFactor * Time.deltaTime);
    }

    private void TranslateInternal()
    {
        Quaternion currentRotation = internalBody.rotation;

        Vector3 direction = rigidBody.velocity;
        direction.y = 0;
        if (direction != Vector3.zero && !movementLocked)
        {
            Quaternion to = Quaternion.LookRotation(direction);
            internalBody.rotation = Quaternion.Lerp(currentRotation, to, rotationLerpSpeed * Time.deltaTime);
        }

        internalBody.position = externalBody.position;
    }

    private void UpdateMovement()
    {
        moveInput = InputManager.Instance.GetMoveInput();
        if (moveInput.magnitude <= 0 || hasMoved) return;

        hasMoved = true;
        LevelTimer.Instance.StartTimer();
    }

    private void Jump()
    {
        Debug.Log("Jump");
    }
}