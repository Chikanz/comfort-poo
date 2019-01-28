using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCanvas : MonoBehaviour
{
    public float displayFor = 5;
    public float fadeDuration = 2;

    private Image img;
    
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        StartCoroutine(fade(fadeDuration));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator fade(float dur)
    {
        yield return new WaitForSeconds(displayFor);

        float elapsed = 0;
        while (elapsed < dur)
        {
            var color = img.color;
            color = new Color(color.r, color.g, color.b, 1 - (elapsed/dur));
            img.color = color;
            
            elapsed += Time.deltaTime;

            yield return null;
        }
    }
}
