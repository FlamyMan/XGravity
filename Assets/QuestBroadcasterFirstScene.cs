using System.IO;
using TMPro;
using UnityEngine;

public class QuestBroadcasterFirstScene : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _path;

    private BroadcastMessages _parsedText;
    private float _nextTime;
    private int _currentNumber;

    private void Start()
    {
        _nextTime = Time.time;
        string rawJson = File.ReadAllText(_path);
        _parsedText = JsonUtility.FromJson<BroadcastMessages>(rawJson);
    }

    private void Update()
    {

        if (Time.time > _nextTime)
        {
            if (_currentNumber >= _parsedText.lines.Length)
            {
                _text.text = "";
                enabled = false;
                return;
            }
            _nextTime += _parsedText.durations[_currentNumber];
            _text.text = _parsedText.lines[_currentNumber];
            _currentNumber++;

        }
    }

}

public class BroadcastMessages
{
    public string[] lines;
    public float[] durations;
}
