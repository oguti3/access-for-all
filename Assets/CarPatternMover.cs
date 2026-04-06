using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarPatternMover : MonoBehaviour
{
    [Header("Timer Reference")]
    public Timer timer;

    [Header("Pattern Settings")]
    public bool loopPattern = false;

    [Header("Honk")]
    [Tooltip("Drag your honk audio file here")]
    public AudioClip honkClip;
    [Range(0f, 1f)]
    public float honkVolume = 1f;

    [Header("Pattern — define this car's steps below")]
    public List<MoveStep> pattern1 = new List<MoveStep>();
    public List<MoveStep> pattern2 = new List<MoveStep>();

    private CarController car;
    private AudioSource audioSource;
    private bool isRunning = false;
    private Vector3 startPos;

    [System.Serializable]
    public struct MoveStep
    {
        public MoveType type;
        [Tooltip("Distance in units (Forward/Backward/Left/Right), degrees (TurnLeft/TurnRight), or seconds (Wait)")]
        public float amount;
    }

    public enum MoveType { Forward, Backward, Left, Right, TurnLeft, TurnRight, Wait }

    void Awake()
    {
        car = GetComponent<CarController>();

        // Grab existing AudioSource or add one automatically
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        startPos = car.transform.position;
    }

    void Start()
    {
        if (timer != null)
            timer.OnTimerEnd.AddListener(TriggerPattern);
        else
            Debug.LogWarning($"[CarPatternMover] '{name}': No Timer assigned!");
    }

    void OnDestroy()
    {
        if (timer != null)
            timer.OnTimerEnd.RemoveListener(TriggerPattern);
    }

    private IEnumerator RunPattern(List<MoveStep> cPattern)
    {
        do
        {
            foreach (MoveStep step in cPattern)
                yield return StartCoroutine(ExecuteStep(step));
        }
        while (loopPattern);

        isRunning = false;
    }

    private IEnumerator ExecuteStep(MoveStep step)
    {
        switch (step.type)
        {
            case MoveType.Forward:   yield return MoveByAmount(step.amount, () => car.DriveForward());  break;
            case MoveType.Backward:  yield return MoveByAmount(step.amount, () => car.DriveBackward()); break;
            case MoveType.Right:     yield return MoveByAmount(step.amount, () => car.DriveRight());    break;
            case MoveType.Left:      yield return MoveByAmount(step.amount, () => car.DriveLeft());     break;
            case MoveType.TurnRight: yield return TurnByDegrees(step.amount, right: true);              break;
            case MoveType.TurnLeft:  yield return TurnByDegrees(step.amount, right: false);             break;
            case MoveType.Wait:      yield return new WaitForSeconds(step.amount);                      break;
        }
    }

    private IEnumerator MoveByAmount(float targetDistance, System.Action driveAction)
    {
        float travelled = 0f;
        Vector3 lastPos = transform.position;

        while (travelled < targetDistance)
        {
            driveAction();
            travelled += Vector3.Distance(transform.position, lastPos);
            lastPos = transform.position;
            yield return null;
        }
    }

    private IEnumerator TurnByDegrees(float targetDegrees, bool right)
    {
        float turned = 0f;
        float lastY = transform.eulerAngles.y;

        while (turned < targetDegrees)
        {
            if (right) car.TurnRightContinuous();
            else       car.TurnLeftContinuous();

            float currentY = transform.eulerAngles.y;
            turned += Mathf.Abs(Mathf.DeltaAngle(lastY, currentY));
            lastY = currentY;
            yield return null;
        }
    }

    public void TriggerPattern()
    {
        if (isRunning) return;
        isRunning = true;
        Honk();
        if (car.transform.position == startPos)
            StartCoroutine(RunPattern(pattern1));
        else
            StartCoroutine(RunPattern(pattern2));
    }

    public void Honk()
    {
        if (honkClip != null)
            audioSource.PlayOneShot(honkClip, honkVolume);
        else
            Debug.LogWarning($"[CarPatternMover] '{name}': No honk clip assigned!");
    }

    public void StopPattern()
    {
        StopAllCoroutines();
        isRunning = false;
    }
}