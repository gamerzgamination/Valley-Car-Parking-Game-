using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// attach to UI Text component (with the full text already there)

public class UITextTypeWriter: MonoBehaviour
{

	Text txt;
	public string story;
	public float StartTyping_Delay;
	public float Typing_Delay;
	

	void OnEnable()
	{
		txt = GetComponent<Text>();
		story = txt.text;
		txt.text = "";

		// TODO: add optional delay when to start
		StartCoroutine("PlayText");
	}

	public IEnumerator PlayText()
	{

		yield return new WaitForSeconds(StartTyping_Delay);
		foreach (char c in story)
		{
			txt.text += c;
			yield return new WaitForSeconds(Typing_Delay);
		}
    }

  //  public void ClearText()
  //  {
        
  //     // txt.text = "";
		//story = "";
  //  }
}