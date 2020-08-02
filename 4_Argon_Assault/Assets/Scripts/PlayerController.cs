using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    // todo work-out sometimes slow on first play of scene
    [Header("General")] [Tooltip("In ms^-1")] [SerializeField]
    float xSpeed = 4f;

    [Tooltip("In m")] [SerializeField] float xRange = 17f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 4f;
    [Tooltip("In m")] [SerializeField] float yMinRange = 17f;
    [Tooltip("In m")] [SerializeField] float yMaxRange = 17f;
    [SerializeField] private GameObject[] guns;

    [Header("Screen-position")] [SerializeField]
    private float positionPitchFactor = -5f;

    [SerializeField] private float positionYawFactor = 3f;

    [Header("Control throw based")] [SerializeField]
    private float controlPitchFactor = -20f;

    [SerializeField] private float controlRollFactor = -20f;

    private float xThrow, yThrow;
    private bool isControlEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    // Called by string reference
    public void OnPlayerDeath()
    {
        isControlEnabled = false;
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        float rawNewXPos = transform.localPosition.x + xOffset;
        float clampXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);

        float rawNewYPos = transform.localPosition.y + yOffset;
        float clampYPos = Mathf.Clamp(rawNewYPos, yMinRange, yMaxRange);

        transform.localPosition = new Vector3(clampXPos, clampYPos, transform.localPosition.z);
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            ActivateGuns();
        }
        else
        {
            DeactivateGuns();
        }
    }

    private void ActivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(true);
        }
    }

    private void DeactivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
    }
}