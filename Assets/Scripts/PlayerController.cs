using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerPrefab;
    public Manager manager;

    private GameObject currentPlayer;
    private Vector2 moveInput;

    [SerializeField] int speed = 10;

    private void Start()
    {
        SaveScript.isInMainMenu = false;
        SaveScript.isGameOver = false;

        currentPlayer = Instantiate(playerPrefab, transform);
        currentPlayer.GetComponent<Player>().manager = manager;
        Camera.main.GetComponent<AudioSource>().enabled = true;
    }

    private void Update()
    {
        if (!manager.isLost)
        {
            GetMoveInput();
            FollowPlayer();
        }
    }

    private void GetMoveInput()
    {
        // Keyboard/mouse controls
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(horizontal, vertical);

        // Gamepad controls
        //if (moveInput.magnitude < 0.5f)
        //{
        //    float gamepadHorizontal = Input.GetAxisRaw("GamepadHorizontal");
        //    float gamepadVertical = Input.GetAxisRaw("GamepadVertical");
        //    moveInput = new Vector2(gamepadHorizontal, gamepadVertical);
        //}

        // Apply dead zone
        if (moveInput.magnitude < 0.5f)
        {
            moveInput = Vector2.zero;
        }
        else
        {
            moveInput = moveInput.normalized * ((moveInput.magnitude - 0.5f) / (1 - 0.5f)) * speed * Time.deltaTime;
        }
    }

    private void FollowPlayer()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 newPos = currentPlayer.transform.position + new Vector3(moveInput.x, moveInput.y, 0f);
        newPos.x = Mathf.Clamp(newPos.x, -7f, 7f);
        newPos.y = Mathf.Clamp(newPos.y, -4f, 4f);
        currentPlayer.transform.position = newPos;
    }
}
