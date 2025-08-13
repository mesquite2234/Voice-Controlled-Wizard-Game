using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEditor;

public class SpellRecognizer : MonoBehaviour
{
    [SerializeField]
    private SpellCaster spellCaster;

    private KeywordRecognizer m_Recognizer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Recognizer = new KeywordRecognizer(spellCaster.spellNames.ToArray(), ConfidenceLevel.Low);
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
        m_Recognizer.Start();

    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        string spell = args.text.ToLower();

        foreach (string word in spellCaster.spellNames.ToArray())
        {
            if (spell.Contains(word))
            {
                Debug.Log(word);
                spellCaster.Cast(word, args.confidence);
            }
        }
    }

    public void ReloadRecognizer()
    {
        m_Recognizer.Stop();
        m_Recognizer.Dispose();

        m_Recognizer = new KeywordRecognizer(spellCaster.spellNames.ToArray(), ConfidenceLevel.Low);
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
        m_Recognizer.Start();
    }
}

[CustomEditor(typeof(SpellRecognizer))]
public class SpellRecognizerCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws default inspector for MyScript
        SpellRecognizer myScript = (SpellRecognizer)target;
        if (GUILayout.Button("Reload"))
        {
            myScript.ReloadRecognizer();
        }
    }
}