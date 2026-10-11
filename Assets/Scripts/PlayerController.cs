using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerController : MonoBehaviour
{
    //speed at which the ball moves 
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    //rigibody of the ball
    private Rigidbody rb;
    private int sugarCount;
    private int waterCount;
    private int iceCount;
    
    //movement along the x and y axes
    private float movementX;
    private float movementY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get and store the rigidbodt component attached to the ball
        rb = GetComponent<Rigidbody>();
        sugarCount = 0;
        waterCount = 0;
        iceCount = 0;

SetCountText();
        winTextObject.SetActive(false);
    }

    //called when a move input is detected
    void OnMove( InputValue movementValue)
        {
            //converts the input value into a vector2 for movement
            Vector2 movementVector = movementValue.Get<Vector2>();
            //stores the x and y components of the movement
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
    
    void SetCountText()
{
    countText.text =
        "Sugar: " + sugarCount + "/4\n" +
        "Water: " + waterCount + "/3\n" +
        "Ice: " + iceCount + "/3";

    if (sugarCount >= 4 &&
        waterCount >= 3 &&
        iceCount >= 3)
    {
        winTextObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text =
            "Lemonade Complete!";

        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
    }
}

    void FixedUpdate()
    {
    
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

  
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sugar"))
            {
            other.gameObject.SetActive(false);
            sugarCount++;
            SetCountText();
            }
        else if (other.gameObject.CompareTag("Water"))
            {
            other.gameObject.SetActive(false);
            waterCount++;
            SetCountText();
            }
        else if (other.gameObject.CompareTag("Ice"))
            {
            other.gameObject.SetActive(false);
            iceCount++;
            SetCountText();
    }
}

    private void OnCollisionEnter(Collision collision)
{
   if (collision.gameObject.CompareTag("Enemy"))
   {
       // Destroy the current object
       Destroy(gameObject); 
       // Update the winText to display "You Lose!"
       winTextObject.gameObject.SetActive(true);
       winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
   }
}

}
