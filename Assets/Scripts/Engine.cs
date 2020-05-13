using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Engine : MonoBehaviour {

    Rigidbody rb;
    AudioSource audioSource;

    [Header("Ship Values")]
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float thrustPower = 50f;
    [SerializeField] GameObject shipGraphic;

    [Header("Sounds")]
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip loseSound;
    [SerializeField] AudioClip winSound;

    [Header("Particles")]
    [SerializeField] ParticleSystem mainEngineEffects;
    [SerializeField] ParticleSystem loseEffect;
    [SerializeField] ParticleSystem winEffect;

    [Header("Game Variables")] // Imported from GC
    [SerializeField] Level levelData;
    [SerializeField] float currentFuel;
    [SerializeField] public bool isStarted;
    
    [Header("Dashboard")]
    [SerializeField] float velocity;

    [Header("UI")]
    [SerializeField] Text velocityLabel;

    enum State { Alive, Dying, Transcending, Testing };
    [SerializeField] State currentState = State.Alive;

    GameController gc;
    FuelBarController fbc;

    private void Awake() {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        fbc = GameObject.FindGameObjectWithTag("FuelController").GetComponent<FuelBarController>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        velocityLabel = GameObject.FindGameObjectWithTag("VelocityLabel").GetComponent<Text>();
    }

    // Setup local variables when sent over from the Game Controller
    public void SetupData(Level thisLevel) {
        isStarted = true;
        levelData = thisLevel;
        currentFuel = levelData.startingFuel;
        fbc.AdjustFuel(levelData.startingFuel);
    }

    private void Update() {
        if (isStarted) {
            if (currentState == State.Alive || currentState == State.Testing) {
                Thrust();
                Rotate();
            } 

            velocity = rb.velocity.magnitude;
            float vel = Mathf.Round(velocity);
            velocityLabel.text = vel.ToString();
        }
    }
    
    void Thrust() {
        if (currentState == State.Alive || currentState == State.Testing) {
            if (Input.GetKey(KeyCode.Space)) {
                ApplyThrust();

                // Only monitor fuel if this level is tracking fuel consumption
                if (levelData.fuelConsumption) {
                    UseFuel();
                }

            } else {
                audioSource.Stop();
                mainEngineEffects.Stop();
            }
        }
    }

    void ApplyThrust() {
        rb.AddRelativeForce(Vector2 .up * thrustPower * Time.deltaTime);
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineEffects.Play();
    }

    void UseFuel() {
        currentFuel -= levelData.fuelConsumptionRate * Time.deltaTime;
        if (currentFuel <= 0) {
            currentFuel = 0;
            RunOutOfGas();
        }

        UpdateFuel();
    }

    private void OnCollisionEnter(Collision collision) {
        if (currentState != State.Alive) { return; }

        switch (collision.gameObject.tag) {
            case "TakeOffPad":
                break;
            case "Fuel":
                print("got some free fuel");
                currentFuel += collision.gameObject.GetComponent<FuelPowerUp>().fuelAmount;
                UpdateFuel();
                break;
            case "LandingPad":
                if (velocity > levelData.maxLandVelocity) {
                    LoseLevel();
                } else {
                    WinLevel();
                }
                break;
            case "Obstacle":
                LoseLevel();
                break;
            default:
                LoseLevel();
                break;
        }
    }

    void UpdateFuel() {
        float barFuel = currentFuel / levelData.startingFuel;
        fbc.AdjustFuel(barFuel);
    }
    
    void SetSound(AudioClip soundToPlay) {
        audioSource.Stop();
        audioSource.PlayOneShot(soundToPlay);
    }


    void Rotate() {
        rb.freezeRotation = true;  // keep it from spinning out of control

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        rb.freezeRotation = false;
    }

    void WinLevel() {
        currentState = State.Transcending;
        SetSound(winSound);
        rb.freezeRotation = true;
        winEffect.Play();
        gc.LoadLevel("NextLevel");
    }

    void LoseLevel() {
        currentState = State.Dying;
        SetSound(loseSound);
        mainEngineEffects.Stop();
        loseEffect.Play();
        rb.freezeRotation = true;
        shipGraphic.SetActive(false);
        gc.LoadLevel("Reload");
    }

    void RunOutOfGas() {
        currentState = State.Dying;
        SetSound(loseSound);
        mainEngineEffects.Stop();
        loseEffect.Play();
        gc.LoadLevel("Reload");
    }
}
