using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallex : MonoBehaviour
{
    [SerializeField] private Vector2 effect;
    private Transform cameratransform;
    private Vector3 lastCameraPosition;
    private float textureUnitSizex;
    private float textureUnitSizey;

    // Start is called before the first frame update
    void Start()
    {
        cameratransform = Camera.main.transform;
        lastCameraPosition = cameratransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizex = texture.width / sprite.pixelsPerUnit;
        textureUnitSizey = texture.height / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = cameratransform.position - lastCameraPosition;
      //  float effect = .5f;
        transform.position += new Vector3(deltaMovement.x*effect.x, deltaMovement.y * effect.y) ;
        lastCameraPosition = cameratransform.position;

        if(Mathf.Abs(cameratransform.position.x-transform.position.x) >= textureUnitSizex)
        {
            float offsetx = (cameratransform.position.x - transform.position.x) % textureUnitSizex;
            transform.position = new Vector3(cameratransform.position.x + offsetx, transform.position.y);
        }
        
    }
}
