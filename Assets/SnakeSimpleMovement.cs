using UnityEngine;

public class SnakeSimpleMovement : MonoBehaviour
{
    public float speed = 2;

    private Rigidbody componentRigidbody;
    private SnakeTail componentSnakeTail;
    private bool facingUp = true;
    private bool facingDown = false;
    private bool facingLeft = false;
    private bool facingRight = false;

    public SpawnFood foodController;
    public GameObject head;

    public GameObject endScreen;

    private void Start()
    {
        componentRigidbody = GetComponent<Rigidbody>();
        componentSnakeTail = GetComponent<SnakeTail>();

        componentSnakeTail.AddCircle();
        componentSnakeTail.AddCircle();
        componentSnakeTail.AddCircle();
        componentSnakeTail.AddCircle();


        
    }

    private void ReloadDirections()
    {
        facingUp = false;
        facingDown = false;
        facingRight = false;
        facingLeft = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {

               
            case "emptyApple":
               Destroy(other);
                break;

        }
    }


    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow) && facingDown == false)
        {
            ReloadDirections();
            facingUp = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            componentRigidbody.velocity = new Vector3(0, 0, 1 * speed);
        }

        if (Input.GetKey(KeyCode.DownArrow) && facingUp == false)
        {
            ReloadDirections();
            facingDown = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            componentRigidbody.velocity = new Vector3(0, 0, -1 * speed);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && facingRight == false)
        {
            ReloadDirections();
            facingLeft = true;
            transform.rotation = Quaternion.Euler(0, -90, 0);
            componentRigidbody.velocity = new Vector3(-1 * speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow) && facingLeft == false)
        {
            ReloadDirections();
            facingRight = true;
            transform.rotation = Quaternion.Euler(0, 90, 0);
            componentRigidbody.velocity = new Vector3(1 * speed, 0, 0);
        }
    }

    private void Update()
    {
        var startPos = head.transform.position + new Vector3(0, 0, 0.1f);
        var endPos = head.transform.position + new Vector3(0, 0, 0.5f);
        Ray ray = new Ray(head.transform.position, head.transform.position + head.transform.forward /2);
        RaycastHit hit;

        Debug.DrawLine(head.transform.position, head.transform.position + head.transform.forward/2, Color.red);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.tag);
            switch (hit.transform.tag)
            {
                default:
                    speed = 0;
                    endScreen.SetActive(true);
                    componentRigidbody.velocity = new Vector3(0, 0, 0);
                    break;
                case "wall":
                    speed = 0;
                    endScreen.SetActive(true);
                    componentRigidbody.velocity = new Vector3(0, 0, 0);
                    break;
                case "snake":
                    speed = 0;
                    break;
               
            }
        }


        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }
        }

    }

    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;




        


    void checkSwipe()
    {
     
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
          
            if (fingerDown.y - fingerUp.y > 0)
            {
                OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)
            {
                OnSwipeDown();
            }
            fingerUp = fingerDown;
        }

        
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            
            if (fingerDown.x - fingerUp.x > 0)
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    
    void OnSwipeUp()
    {
        ReloadDirections();
        facingUp = true;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        componentRigidbody.velocity = new Vector3(0, 0, 1 * speed);
    }

    void OnSwipeDown()
    {
        ReloadDirections();
        facingDown = true;
        transform.rotation = Quaternion.Euler(0, 180, 0);
        componentRigidbody.velocity = new Vector3(0, 0, -1 * speed);
    }

    void OnSwipeLeft()
    {
        ReloadDirections();
        facingLeft = true;
        transform.rotation = Quaternion.Euler(0, -90, 0);
        componentRigidbody.velocity = new Vector3(-1 * speed, 0, 0);
    }

    void OnSwipeRight()
    {
        ReloadDirections();
        facingLeft = true;
        transform.rotation = Quaternion.Euler(0, -90, 0);
        componentRigidbody.velocity = new Vector3(1 * speed, 0, 0);
    }
}
