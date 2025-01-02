using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class storyScript : MonoBehaviour
{

    public TextMeshProUGUI textMeshPro; 
    public AudioSource audioSource; 
    public AudioClip letterSound; 
    public float wordDelay = 0.2f; 
    public int maxLines = 5;
    public bool playSoundPerLetter = true; 

    [TextArea]
    [SerializeField] string fullText;

    void Start()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        textMeshPro.text = ""; 
        int lineCount = 0; 

        if (playSoundPerLetter)
        {
            foreach (char letter in fullText)
            {
                textMeshPro.text += letter; 
                if (letter == '\n')
                {
                    lineCount++; 
                    if (lineCount > maxLines)
                    {
                        RemoveFirstLine(); 
                    }
                }
                PlaySound(); 
                yield return new WaitForSeconds(wordDelay); 
            }
        }
        else
        {
            string[] words = fullText.Split(' '); 
            foreach (string word in words)
            {
                textMeshPro.text += word + " "; 
                lineCount = CountLines(textMeshPro.text); 

               
                if (word.Contains("\n"))
                {
                    lineCount++;
                }

                if (lineCount > maxLines)
                {
                    RemoveFirstLine(); 
                }

                PlaySound(); 
                yield return new WaitForSeconds(wordDelay); 
            }
        }

        print("complete");
        Invoke(nameof(loadscene), .5f);
    }

    
    int CountLines(string text)
    {
        return text.Split('\n').Length;
    }

   
    void RemoveFirstLine()
    {
        string[] lines = textMeshPro.text.Split('\n');
        if (lines.Length > 1)
        {
            textMeshPro.text = string.Join("\n", lines, 1, lines.Length - 1); 
        }
    }

   
    void PlaySound()
    {
        if (audioSource != null && letterSound != null)
        {
            audioSource.PlayOneShot(letterSound); 
        }
    }

    void loadscene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
