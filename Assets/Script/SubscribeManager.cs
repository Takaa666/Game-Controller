using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SubscribeManager : MonoBehaviour
{
    public int coinCount;
    public Text coinText;
    public GameObject finishObject; // Reference to the GameObject of the finish object

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the finish object starts inactive
        finishObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = ": " + coinCount.ToString();

        // Check if coinCount has reached 3
        if (coinCount == 3)
        {
            coinText.text = "You Win!";
            finishObject.SetActive(true); // Activate the finish object
        }
        else
        {
            finishObject.SetActive(false); // Deactivate the finish object if coinCount is less than 3
        }
    }
}
