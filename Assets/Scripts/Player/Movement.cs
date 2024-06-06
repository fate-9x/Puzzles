using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class Movement : MonoBehaviour
{
    public float speed = 1f;
    [SerializeField] private float gravity;
    public float jumpForce = 2f;
    private float timeJump = 1f;
    public Vector3 direction;
    private CharacterController characterController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private int rotation;
    [SerializeField] private TextMeshPro textPro;
    private Transform vrCamera;
    private Vector3 cameraForward;
    private Vector3 cameraRight;
    private bool rotateBool = true;

    [SerializeField] private Detector detector;

    private bool groundPlayer;

    public Transform[] playerHead; // Asigna el transform de la cámara del jugador
    public Transform fixedPosition; // La posición fija donde el jugador debe estar

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        groundPlayer = characterController.isGrounded;
    }

    // Update is called once per frame
    void Update()
    {

        foreach (Transform head in playerHead)
        {
            head.position = new Vector3(fixedPosition.position.x, head.position.y, fixedPosition.position.z);
        }

        textPro.text = "Detector: " + detector.inPlace;

        if (characterController.isGrounded && direction.y < 0)
        {
            direction.y = 0f;
        }

        if (OVRInput.GetDown(OVRInput.Button.One) && characterController.isGrounded)
        {
            direction.y = Mathf.Sqrt(jumpForce * timeJump * gravity);
        }
        else
        {
            direction.y -= gravity * Time.deltaTime;
        }

        Rotate();
        Move();
    }

    void Rotate()
    {
        // Rotar el personaje
        Vector2 secondaryThumbstick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        if (secondaryThumbstick.x > 0.5f) // Si el thumbstick se mueve a la derecha
        {
            transform.Rotate(0, rotation, 0); // Gira el personaje 45 grados a la derecha
        }
        else if (secondaryThumbstick.x < -0.5f) // Si el thumbstick se mueve a la izquierda
        {
            transform.Rotate(0, -rotation, 0); // Gira el personaje 45 grados a la izquierda
        }

        cameraForward = mainCamera.transform.forward.normalized;
        cameraRight = mainCamera.transform.right.normalized;
        cameraForward.y = 0;
        cameraRight.y = 0;
    }

    void Move()
    {
        Vector2 primaryThumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        
        direction.x = primaryThumbstick.x;
        direction.z = primaryThumbstick.y;

        Vector3 movePlayer = direction.x * cameraRight + direction.y * Vector3.up + direction.z * cameraForward;
        characterController.Move(movePlayer * speed * Time.deltaTime);
    }
    //void Move()
    //{
    //    Vector2 primaryThumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
    //    direction.x = primaryThumbstick.x;
    //    direction.z = primaryThumbstick.y;
    //    characterController.Move(direction * speed * Time.deltaTime);
    //}
}