using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
	public SpriteRenderer sprite_renderer;
	
	private float elapsed = 0.0f;
	private int frame_rate = 12;
	
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
	    elapsed += Time.deltaTime;
	    
	    if(elapsed >= 1.0f / frame_rate)
	    {
	    	elapsed = 0.0f;
	    }
    }
}
