using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimator_Dead : MonoBehaviour
{
	public string sprite_name;
	public int frame_rate = 12;
	public SpriteRenderer sprite_renderer;
	
	private Sprite[] all_frames;
	private int frame_index = 0;
	
	private float elapsed = 0.0f;
	
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
	}
}
