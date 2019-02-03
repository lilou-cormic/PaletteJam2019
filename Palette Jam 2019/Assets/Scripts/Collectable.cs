using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer SpriteRenderer = null;

    [SerializeField]
    private Transform Display = null;

    [SerializeField]
    private AudioSource CollectAudioSource = null;

    [SerializeField]
    private UIPoints PointsDisplayPrefab = null;

    private static Color[] _colors = new Color[]
    {
            new Color(254 / 255f, 243 / 255f, 192 / 255f),
            new Color(255 / 255f, 252 / 255f, 064 / 255f),
            new Color(250 / 255f, 106 / 255f, 010 / 255f),
    };

    private CollectableData _CollectableData;
    public CollectableData CollectableData
    {
        get { return _CollectableData; }

        set
        {
            _CollectableData = value;

            if (_CollectableData.IsNone())
            {
                Destroy(gameObject);
                return;
            }

            SpriteRenderer.color = _colors[_CollectableData.Points - 1];

        }
    }

    private bool _isCollected = false;

    public void Collect()
    {
        if (_isCollected)
            return;

        CollectAudioSource.Play();

        var uiPoints = Instantiate(PointsDisplayPrefab, transform.position + Vector3.up * 0.3f, Quaternion.identity);
        uiPoints.SetPointsText(CollectableData);

        SpriteRenderer.enabled = false;

        _isCollected = true;
        ScoreManager.AddPoints(CollectableData.Points);
        Destroy(gameObject, 1f);
    }

    public void Update()
    {
        float t = (Mathf.Cos(Time.time * 10) + 1) / 2f;

        Display.localScale = new Vector3(Mathf.Lerp(0.5f, 0.4f, t), Display.localScale.y, Display.localScale.z);
        Display.localPosition = new Vector3(Display.localPosition.x, Mathf.Lerp(0f, 0.1f, t), Display.localPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collect();
    }
}
