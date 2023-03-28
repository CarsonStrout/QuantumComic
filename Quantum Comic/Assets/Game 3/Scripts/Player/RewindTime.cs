using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RewindTime : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Volume pp; // urp post processing

    private ChromaticAberration chromaticAberration;
    private Bloom bloom;
    private ColorAdjustments colorAdjustments;

    [Space(5)]
    [Header("Distortion Stats")]
    [SerializeField] private float chromMax;
    [SerializeField] private float bloomMax;
    [SerializeField] private float colorMax;
    [SerializeField] private float transitionTime;

    [Space(5)]
    [SerializeField] private float recordTime = 5f; // amount of time in seconds that positions will be recorded
    public bool isRewinding = false;

    List<Vector3> positions;

    private void Start()
    {
        positions = new List<Vector3>();

        // gets access to post processing effects
        pp.profile.TryGet(out chromaticAberration);
        pp.profile.TryGet(out bloom);
        pp.profile.TryGet(out colorAdjustments);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            StartRewind();
        if (Input.GetKeyUp(KeyCode.LeftShift))
            StopRewind();

        ProcessChange();

        // only gives the player a trail when in their rewind
        if (isRewinding)
            tr.gameObject.SetActive(true);
        else
            tr.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    void Rewind()
    {
        // will rewind the player through previous positions as long as there are still positions in the list
        if (positions.Count > 0)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
        else
            StopRewind();
    }

    private void ProcessChange()
    {
        if (isRewinding) // distorts the post processing over the stated time
        {
            chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, chromMax, transitionTime * Time.deltaTime);
            bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, bloomMax, transitionTime * Time.deltaTime);
            colorAdjustments.hueShift.value = Mathf.Lerp(colorAdjustments.hueShift.value, colorMax, transitionTime * Time.deltaTime);
        }
        else // returns the post processing to its normal values over the stated time
        {
            chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, 0.2f, transitionTime * Time.deltaTime);
            bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 5, transitionTime * Time.deltaTime);
            colorAdjustments.hueShift.value = Mathf.Lerp(colorAdjustments.hueShift.value, 0, transitionTime * Time.deltaTime);
        }
    }

    void Record()
    {
        // records positions for a stated time, removing the last position as to not cause overflow
        if (positions.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
            positions.RemoveAt(positions.Count - 1);
        positions.Insert(0, transform.position);
    }

    public void StartRewind()
    {
        isRewinding = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
    }
}
