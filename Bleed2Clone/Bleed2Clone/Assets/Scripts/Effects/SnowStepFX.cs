using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowStepFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem snowParticleSystem;
    [SerializeField] private ParticleSystem runningParticles;
    [SerializeField] private ParticleSystem jumpParticles;
    [SerializeField] private ParticleSystem landParticles;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform fxPos;
    [SerializeField] private Color frontParticleColor;
    [SerializeField] private Color backParticleColor;
    private bool particleColorSwitch; //A constantly changing bool that helps change the leg particle's color
    private bool isRunning;
    private bool isFlipped;
    private bool effectActive;
    private float lastXPos;
    private float currentXPos;
    private bool isGrounded;
    public bool IsGrounded
    {
        get { return isGrounded; }
        set
        {
            if (isGrounded != value)
            {
                GameEvents.OnPlayerGrounded(value);
                isGrounded = value;
            }
        }
    }

            // Start is called before the first frame update
    void Start()
    {
        snowParticleSystem.transform.position = fxPos.position;
        currentXPos = transform.position.x;


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            jumpParticles.Play();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentXPos = transform.position.x;
        isRunning = (lastXPos != currentXPos);
        isFlipped = (isRunning && lastXPos > currentXPos);
        var emission = runningParticles.emission;
        emission.enabled = isRunning;

        lastXPos = currentXPos;

        if (!effectActive && isRunning && IsGrounded
            ) StartCoroutine(PlaySnowStepFX());

        bool grounded = Physics2D.OverlapCircle(transform.position, 0.1f, whatIsGround);

        if (!IsGrounded && grounded)
        {
            IsGrounded = true;
            landParticles.Play();
            GameEvents.OnCameraShake(2, 2, 0.2f);
        }

        if(!grounded && isGrounded)
        {
            GameEvents.OnCameraShake(2, 2, 0.2f);
        }

        IsGrounded = grounded;
    }

    private IEnumerator PlaySnowStepFX()
    {
        effectActive = true;

        while (isRunning && isGrounded)
        {
            //Flip the effects if necessary (according to player direction)
            transform.rotation = isFlipped ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
            snowParticleSystem.Play();

            //Switch the colors of the particles for every leg
            particleColorSwitch = !particleColorSwitch;
            SwitchSnowParticleColor();

            //Wait the amount of time the animation takes between each leg
            yield return new WaitForSeconds(0.4f);
        }

        //Prepare the particle color for the next run
        particleColorSwitch = true;
        SwitchSnowParticleColor();

        effectActive = false;
    }

    private void SwitchSnowParticleColor()
    {
        var main = snowParticleSystem.main;
        if (particleColorSwitch) main.startColor = frontParticleColor;
        else main.startColor = backParticleColor;
    }
}
