using UnityEngine;

public class levelGen : MonoBehaviour
{
    public Vector2Int size;
    public Vector2 offset;
    public GameObject brickPrefab;
	public Color[] rowColors;

    private void Awake()
    {
        if (rowColors == null || rowColors.Length < size.y)
        {
            Debug.LogError($"Must assign {size.y} colors", this);
            return;
        }

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                GameObject newBrick = Instantiate(brickPrefab, transform);
                newBrick.transform.position = transform.position + new Vector3((float)((size.x-1)*.5f-i) * offset.x, j * offset.y, 0);
                BrickHealth brickHealth = newBrick.AddComponent<BrickHealth>();
                brickHealth.Initialize(rowColors, j);
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
