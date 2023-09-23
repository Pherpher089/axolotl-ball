using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUserControl : MonoBehaviour
{
    private Rigidbody2D m_RigidBody2D;
    [Range(1, 2)] public int m_PlayerNumber= 1;

    //mouse controls
    public float m_SpeedModifierTouch = 50f;
    public float m_SpeedModifierMouse = 375f;
    public float m_SpeedModifierAxis = 450f;

    BoxCollider2D courtBounds;
    public float m_Acceleration = 10.0f; // Adjust this value for desired smoothness
    private Touch? assignedTouch = null; // Remember the touch assigned to the player
    private bool startedMouseInBounds = false;
    void Awake()
    {
        tag += m_PlayerNumber.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        courtBounds = GameObject.FindGameObjectWithTag($"Player{m_PlayerNumber}Court").GetComponent<BoxCollider2D>();
        m_RigidBody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenuController.isPaused || !GameController.Instance.allowCharacterMovement) return;
        //KeyboardInput();
        TouchInput();
        if(GameController.Instance.allowMouseInput) MouseInput();
    }

    private void KeyboardInput()
    {
        Vector2 movementInput = new Vector2(Input.GetAxis(m_PlayerNumber.ToString() + "Horizontal"), Input.GetAxis(m_PlayerNumber.ToString() + "Vertical"));
        Vector2 targetVelocity = movementInput * m_SpeedModifierAxis;

        m_RigidBody2D.velocity = Vector2.Lerp(m_RigidBody2D.velocity, targetVelocity, m_Acceleration * Time.deltaTime);
    }

    public void MouseInput()
    {
        Vector2 targetVelocity = GetMouseDragDirection() * m_SpeedModifierMouse;
        m_RigidBody2D.velocity = Vector2.Lerp(m_RigidBody2D.velocity, targetVelocity, m_Acceleration * Time.deltaTime);
    }

    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Vector2 targetVelocity = GetTouchDragDirection() * m_SpeedModifierTouch;
            m_RigidBody2D.velocity = Vector2.Lerp(m_RigidBody2D.velocity, targetVelocity, m_Acceleration * Time.deltaTime);
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
