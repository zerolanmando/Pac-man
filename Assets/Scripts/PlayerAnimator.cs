using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
	public string sprite_name;
	public int frame_rate = 12;
	public SpriteRenderer sprite_renderer;
	
	private Sprite[] all_frames;
	private int frame_index = 0;
	
	private float elapsed = 0.0f;
	
	private float speed_x = 0.5f;
	private float speed_y = 0.0f;
	
    // Start is called before the first frame update
    void Start()
	{
		all_frames = Resources.LoadAll<Sprite>(sprite_name);
		
		if(all_frames.Length > 0)
		{
			sprite_renderer.sprite = all_frames[frame_index];
		}
    }

    // Update is called once per frame
    void Update()
	{	
	    elapsed += Time.deltaTime;
	    
	    if(elapsed >= 1.0f / frame_rate)
	    {
	    	elapsed = 0.0f;
	    	
	    	++frame_index;
	    	
	    	if(frame_index >= all_frames.Length)
	    	{
	    		frame_index = 0;
	    	}
	    	
	    	sprite_renderer.sprite = all_frames[frame_index];
	    }
	 
		Vector3 scale, position;
		
		scale = gameObject.transform.localScale;
		position = gameObject.transform.position;
		
		scale.x = (speed_x > 0.0f)? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
		position.x += speed_x * Time.deltaTime;
		position.y += speed_y * Time.deltaTime;
		
		gameObject.transform.localScale = scale;
		gameObject.transform.position = position;
		
		if(speed_x > 0.0f && position.x > -2.25f)
		{
			speed_x = 0.0f;
			speed_y = 0.5f;
		}
		else if(speed_y > 0.0f && position.y > 3.88f)
		{
			speed_x = -0.5f;
			speed_y = 0.0f;
		}
		else if(speed_x < 0.0f && position.x < -3.7f)
		{
			speed_x = 0.0f;
			speed_y = -0.5f;
		}
		else if(speed_y < 0.0f && position.y < 1.85f)
		{
			speed_x = 0.5f;
			speed_y = 0.0f;
		}
    }
}
