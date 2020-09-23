using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	private int[,] level_map =
	{
		{1,2,2,2,2,2,2,2,2,2,2,2,2,7},
		{2,5,5,5,5,5,5,5,5,5,5,5,5,4},
		{2,5,3,4,4,3,5,3,4,4,4,3,5,4},
		{2,6,4,0,0,4,5,4,0,0,0,4,5,4},
		{2,5,3,4,4,3,5,3,4,4,4,3,5,3},
		{2,5,5,5,5,5,5,5,5,5,5,5,5,5},
		{2,5,3,4,4,3,5,3,3,5,3,4,4,4},
		{2,5,3,4,4,3,5,4,4,5,3,4,4,3},
		{2,5,5,5,5,5,5,4,4,5,5,5,5,4},
		{1,2,2,2,2,1,5,4,3,4,4,3,0,4},
		{0,0,0,0,0,2,5,4,3,4,4,3,0,3},
		{0,0,0,0,0,2,5,4,4,0,0,0,0,0},
		{0,0,0,0,0,2,5,4,4,0,3,4,4,0},
		{2,2,2,2,2,1,5,3,3,0,4,0,0,0},
		{0,0,0,0,0,0,5,0,0,0,4,0,0,0},
	};
	
	private GameObject create_map_element(string temp_path, Vector3 pos, float size_scale)
	{
		GameObject temp_obj = GameObject.Find(temp_path);
		temp_obj.transform.position = pos;
		temp_obj.transform.localScale = new Vector3(size_scale, size_scale, 1.0f);
		
		GameObject obj = Instantiate(temp_obj, temp_obj.transform);
		obj.gameObject.SetActive(true);
		obj.transform.parent = gameObject.transform;
		
		return obj;
	}
	
	private void create_quadrant_map(int[,] map_data, int quadrant_index, float view_scale, float size_scale)
	{
		int row = map_data.GetUpperBound(0) + 1, col = map_data.GetUpperBound(1) + 1;
		
		int start = (quadrant_index < 2)? 0 : 1;
		
		for(int i = start; i < row; ++i)
		{
			for(int j = 0; j < col; ++j)
			{
				Vector3 pos = new Vector3((j + (quadrant_index % 2) * col - col) * view_scale,
				(quadrant_index < 2)? (row - 0.5f - i) * view_scale : (-i + 0.5f) * view_scale,
										  0);
				
				int val = level_map[i, j];
	    		
				if(1 == val)
				{
					if((i < row - 1 && j < col - 1) && (2 == level_map[i, j + 1] && 2 == level_map[i + 1, j]))
					{
						create_map_element("templates/walls/outter_left_top", pos, size_scale);
					}
					else if((i < row - 1 && j > 0) && (2 == level_map[i, j - 1] && 2 == level_map[i + 1, j]))
					{
						create_map_element("templates/walls/outter_right_top", pos, size_scale);
					}
					else if((i > 0 && j < col - 1) && (2 == level_map[i, j + 1] && 2 == level_map[i - 1, j]))
					{
						create_map_element("templates/walls/outter_left_bottom", pos, size_scale);
					}
					else if((i > 0 && j > 0) && (2 == level_map[i, j - 1] && 2 == level_map[i - 1, j]))
					{
						create_map_element("templates/walls/outter_right_bottom", pos, size_scale);
					}
					else if((i < row - 1 && j < col - 1) &&
					((1 == level_map[i, j + 1] || 2 == level_map[i, j + 1]) && (1 == level_map[i + 1, j] || 2 == level_map[i + 1, j])))
					{
						create_map_element("templates/walls/outter_left_top", pos, size_scale);
					}
					else if((i < row - 1 && j > 0) &&
					((1 == level_map[i, j - 1] || 2 == level_map[i, j - 1]) && (1 == level_map[i + 1, j] || 2 == level_map[i + 1, j])))
					{
						create_map_element("templates/walls/outter_right_top", pos, size_scale);
					}
					else if((i > 0 && j < col - 1) &&
					((1 == level_map[i, j + 1] || 2 == level_map[i, j + 1]) && (1 == level_map[i - 1, j] || 2 == level_map[i - 1, j])))
					{
						create_map_element("templates/walls/outter_left_bottom", pos, size_scale);
					}
					else if((i > 0 && j > 0) &&
					((1 == level_map[i, j - 1] || 2 == level_map[i, j - 1]) && (1 == level_map[i - 1, j] || 2 == level_map[i - 1, j])))
					{
						create_map_element("templates/walls/outter_right_bottom", pos, size_scale);
					}
				}
				else if(2 == val)
				{
					int hn = 0, vn = 0;
					
					if((j > 0 && (1 == level_map[i, j - 1] || 2 == level_map[i, j - 1])))
					{
						++hn;
					}
					
					if(j < col - 1 && (1 == level_map[i, j + 1] || 2 == level_map[i, j + 1]))
					{
						++hn;
					}
					
					if((i > 0 && (1 == level_map[i - 1, j] || 2 == level_map[i - 1, j])))
					{
						++vn;
					}
					
					if(i < row - 1 && (1 == level_map[i + 1, j] || 2 == level_map[i + 1, j]))
					{
						++vn;
					}
					
					if(hn > vn)
					{
						create_map_element("templates/walls/outter_horizontal", pos, size_scale);
					}
					else if(vn > hn)
					{
						create_map_element("templates/walls/outter_vertical", pos, size_scale);
					}
				}
				else if(3 == val)
				{
					if(0 == quadrant_index % 2)
					{
						if((i < row - 1 && j < col - 1) && (4 == level_map[i, j + 1] && 4 == level_map[i + 1, j]))
						{
							create_map_element("templates/walls/inner_left_top", pos, size_scale);
						}
						else if((i < row - 1 && j > 0) && (4 == level_map[i, j - 1] && 4 == level_map[i + 1, j]))
						{
							create_map_element("templates/walls/inner_right_top", pos, size_scale);
						}
						else if((i > 0 && j < col - 1) && (4 == level_map[i, j + 1] && 4 == level_map[i - 1, j]))
						{
							create_map_element("templates/walls/inner_left_bottom", pos, size_scale);
						}
						else if((i > 0 && j > 0) && (4 == level_map[i, j - 1] && 4 == level_map[i - 1, j]))
						{
							create_map_element("templates/walls/inner_right_bottom", pos, size_scale);
						}
						else if((i < row - 1 && j < col - 1) &&
						((3 == level_map[i, j + 1] || 4 == level_map[i, j + 1]) && (3 == level_map[i + 1, j] || 4 == level_map[i + 1, j])))
						{
							create_map_element("templates/walls/inner_left_top", pos, size_scale);
						}
						else if((i < row - 1 && j > 0) &&
						((3 == level_map[i, j - 1] || 4 == level_map[i, j - 1]) && (3 == level_map[i + 1, j] || 4 == level_map[i + 1, j])))
						{
							create_map_element("templates/walls/inner_right_top", pos, size_scale);
						}
						else if((i > 0 && j < col - 1) &&
						((3 == level_map[i, j + 1] || 4 == level_map[i, j + 1]) && (3 == level_map[i - 1, j] || 4 == level_map[i - 1, j])))
						{
							create_map_element("templates/walls/inner_left_bottom", pos, size_scale);
						}
						else if((i > 0 && j > 0) &&
						((3 == level_map[i, j - 1] || 4 == level_map[i, j - 1]) && (3 == level_map[i - 1, j] || 4 == level_map[i - 1, j])))
						{
							create_map_element("templates/walls/inner_right_bottom", pos, size_scale);
						}
					}
					else
					{
						if((i < row - 1 && j > 0) && (4 == level_map[i, j - 1] && 4 == level_map[i + 1, j]))
						{
							create_map_element("templates/walls/inner_right_top", pos, size_scale);
						}
						else if((i < row - 1 && j < col - 1) && (4 == level_map[i, j + 1] && 4 == level_map[i + 1, j]))
						{
							create_map_element("templates/walls/inner_left_top", pos, size_scale);
						}
						else if((i > 0 && j > 0) && (4 == level_map[i, j - 1] && 4 == level_map[i - 1, j]))
						{
							create_map_element("templates/walls/inner_right_bottom", pos, size_scale);
						}
						else if((i > 0 && j < col - 1) && (4 == level_map[i, j + 1] && 4 == level_map[i - 1, j]))
						{
							create_map_element("templates/walls/inner_left_bottom", pos, size_scale);
						}
						else if((i < row - 1 && j > 0) &&
						((3 == level_map[i, j - 1] || 4 == level_map[i, j - 1]) && (3 == level_map[i + 1, j] || 4 == level_map[i + 1, j])))
						{
							create_map_element("templates/walls/inner_right_top", pos, size_scale);
						}
						else if((i < row - 1 && j < col - 1) &&
						((3 == level_map[i, j + 1] || 4 == level_map[i, j + 1]) && (3 == level_map[i + 1, j] || 4 == level_map[i + 1, j])))
						{
							create_map_element("templates/walls/inner_left_top", pos, size_scale);
						}
						else if((i > 0 && j > 0) &&
						((3 == level_map[i, j - 1] || 4 == level_map[i, j - 1]) && (3 == level_map[i - 1, j] || 4 == level_map[i - 1, j])))
						{
							create_map_element("templates/walls/inner_right_bottom", pos, size_scale);
						}
						else if((i > 0 && j < col - 1) &&
						((3 == level_map[i, j + 1] || 4 == level_map[i, j + 1]) && (3 == level_map[i - 1, j] || 4 == level_map[i - 1, j])))
						{
							create_map_element("templates/walls/inner_left_bottom", pos, size_scale);
						}
					}
				}
				else if(4 == val)
				{
					int hn = 0, vn = 0;
					
					if((j > 0 && (3 == level_map[i, j - 1] || 4 == level_map[i, j - 1])))
					{
						++hn;
					}
					
					if(j < col - 1 && (3 == level_map[i, j + 1] || 4 == level_map[i, j + 1]))
					{
						++hn;
					}
					
					if((i > 0 && (3 == level_map[i - 1, j] || 4 == level_map[i - 1, j])))
					{
						++vn;
					}
					
					if(i < row - 1 && (3 == level_map[i + 1, j] || 4 == level_map[i + 1, j]))
					{
						++vn;
					}
					
					if(hn > vn)
					{
						create_map_element("templates/walls/inner_horizontal", pos, size_scale);
					}
					else if(vn > hn)
					{
						create_map_element("templates/walls/inner_vertical", pos, size_scale);
					}
				}
				else if(5 == val)
				{
					create_map_element("templates/items/normal_pellet", pos, size_scale);
				}
				else if(6 == val)
				{
					create_map_element("templates/items/power_pellet", pos, size_scale);
				}
			}
		}
	}
	
    // Start is called before the first frame update
    void Start()
	{		
		int row = level_map.GetUpperBound(0) + 1, col = level_map.GetUpperBound(1) + 1;
		
		int full_row = row * 2 - 1, full_col = col * 2;
		
		int long_side = (full_row > full_col)? full_row : full_col;
		float view_scale = Camera.main.orthographicSize / long_side; 
    	
		float size_scale = 0.55f;
    	
		create_quadrant_map(level_map, 0, view_scale, size_scale);
		
		for(int i = 0; i < row; ++i)
		{
			for(int j = 0; j < col / 2; ++j)
			{
				int t = level_map[i, j];
				level_map[i, j] = level_map[i, col - 1 - j];
				level_map[i, col - 1 - j] = t;
			}
		}
		
		create_quadrant_map(level_map, 1, view_scale, size_scale);
		
		for(int i = 0; i < (row - 1) / 2; ++i)
		{
			for(int j = 0; j < col; ++j)
			{
				int t = level_map[i, j];
				level_map[i, j] = level_map[row - 1 - i, j];
				level_map[row - 1 - i, j] = t;
			}
		}
		
		create_quadrant_map(level_map, 3, view_scale, size_scale);
		
		for(int i = 0; i < row; ++i)
		{
			for(int j = 0; j < col / 2; ++j)
			{
				int t = level_map[i, j];
				level_map[i, j] = level_map[i, col - 1 - j];
				level_map[i, col - 1 - j] = t;
			}
		}
		
		create_quadrant_map(level_map, 2, view_scale, size_scale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
