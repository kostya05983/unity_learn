using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 4f;
    [Tooltip("In m")] [SerializeField] float xRange = 17f;
    [Tooltip("In ms^-1")] [SerializeField] private float ySpeed = 4f;
    [Tooltip("In m")] [SerializeField] float yMinRange = 17f;
    [Tooltip("In m")] [SerializeField] float yMaxRange = 17f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;
        float clampXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);

        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffset;
        float clampYPos = Mathf.Clamp(rawNewYPos, yMinRange, yMaxRange);
        
        transform.localPosition = new Vector3(clampXPos, clampYPos, transform.localPosition.z);
    }
}