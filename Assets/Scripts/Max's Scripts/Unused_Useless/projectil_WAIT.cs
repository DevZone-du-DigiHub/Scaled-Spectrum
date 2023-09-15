using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class projectil_WAIT : MonoBehaviour
{
    [SerializeField] private float waitDuration1 = 2f;
    [SerializeField] private UnityEvent onTimerComplete1;
    private Coroutine timerCoroutine1;

    [SerializeField] private float waitDuration2 = 3f;
    [SerializeField] private UnityEvent onTimerComplete2;
    private Coroutine timerCoroutine2;

    [SerializeField] private float waitDuration3 = 4f;
    [SerializeField] private UnityEvent onTimerComplete3;
    private Coroutine timerCoroutine3;

    public void StartTimer1Externally()
    {
        if (timerCoroutine1 != null)
        {
            StopCoroutine(timerCoroutine1);
        }

        timerCoroutine1 = StartCoroutine(TimerCoroutine1());
    }

    public void StartTimer2Externally()
    {
        if (timerCoroutine2 != null)
        {
            StopCoroutine(timerCoroutine2);
        }

        timerCoroutine2 = StartCoroutine(TimerCoroutine2());
    }

    public void StartTimer3Externally()
    {
        if (timerCoroutine3 != null)
        {
            StopCoroutine(timerCoroutine3);
        }

        timerCoroutine3 = StartCoroutine(TimerCoroutine3());
    }

    private IEnumerator TimerCoroutine1()
    {
        yield return new WaitForSeconds(waitDuration1);
        onTimerComplete1.Invoke();
    }

    private IEnumerator TimerCoroutine2()
    {
        yield return new WaitForSeconds(waitDuration2);
        onTimerComplete2.Invoke();
    }

    private IEnumerator TimerCoroutine3()
    {
        yield return new WaitForSeconds(waitDuration3);
        onTimerComplete3.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
