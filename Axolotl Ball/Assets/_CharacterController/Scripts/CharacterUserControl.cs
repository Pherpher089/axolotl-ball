using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUserControl : MonoBehaviour
{
    private Rigidbody2D m_RigidBody2D;
    [Range(1, 2)] public int m_PlayerNumber= 1;

    //mouse controls
    private Camera mainCamera;
    public float m_SpeedModifier = 450f;
    public BoxCollider2D courtBounds;
    private float m_Acceleration = 10.0f; // Adjust this value for desired smoothness
    Vector2 touchInput;
    private Touch? assignedTouch = null; // Remember the touch assigned to the player
    private bool startedMouseInBounds = false;
    void Awake()
    {
        touchInput = Vector2.zero;
        tag += m_PlayerNumber.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        courtBounds = GameObject.FindGameObjectWithTag($"Player{m_PlayerNumber}Court").GetComponent<BoxCollider2D>();
        mainCamera = Camera.main;
        m_RigidBody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenuController.isPaused) return;
        //KeyboardInput();
        TouchInput();
    }

    private void KeyboardInput()
    {
        Vector2 movementInput = new Vector2(Input.GetAxis(m_PlayerNumber.ToString() + "Horizontal"), Input.GetAxis(m_PlayerNumber.ToString() + "Vertical"));
        Vector2 targetVelocity = movementInput * m_SpeedModifier;

        m_RigidBody2D.velocity = Vector2.Lerp(m_RigidBody2D.velocity, targetVelocity, m_Acceleration * Time.deltaTime);
    }

    private void TouchInput()
    {
        Vector2 targetVelocity = GetInputDirection() * m_SpeedModifier;
        m_RigidBody2D.velocity = Vector2.Lerp(m_RigidBody2D.velocity, targetVelocity, m_Acceleration * Time.deltaTime);
    }
    private Vector2 GetInputDirection()
    {
        if (Input.touchCount > 0)
        {
            return GetTouchDragDirection();
        }
        else
        {
            return GetMouseDragDirection();
        }
    }

    private Vector3 touchStartPos;
    private Vector3 mouseStartPos;

    private Vector2 GetTouchDragDirection()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            Vector3 touchPos = new Vector3(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y, 0);

            if (!assignedTouch.HasValue && courtBounds.bounds.Contains(touchPos))
            {
                assignedTouch = touch;
            }

            if (assignedTouch.HasValue && touch.fingerId == assignedTouch.Value.fingerId)
            {
                if (touch.phase == UnityEngine.TouchPhase.Began)
                {
                    touchStartPos = touchPos;
                }
                else if (touch.phase == UnityEngine.TouchPhase.Moved)
                {
                    Vector2 direction = touchPos - touchStartPos;
                    touchStartPos = touchPos;
                    return new Vector2(direction.x, direction.y);
                }
                else if (touch.phase == UnityEngine.TouchPhase.Ended)
                {
                    assignedTouch = null; // Reset when the touch ends
                }
            }
        }
        return Vector2.zero;
    }

    private Vector2 GetMouseDragDirection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseStartPos.z = 0;
            if (courtBounds.bounds.Contains(mouseStartPos))
            {
                startedMouseInBounds = true;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (courtBounds.bounds.Contains(mouseStartPos) && startedMouseInBounds)
            {
                Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseStartPos;
                mouseStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseStartPos.z = 0;
                return new Vector2(direction.x, direction.y);
            }
            else
            {
                mouseStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseStartPos.z = 0;

                return Vector2.zero;
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            startedMouseInBounds = false;
        }

        return Vector2.zero;
    }
}
