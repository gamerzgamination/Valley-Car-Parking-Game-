using UnityEngine;
using System.Collections.Generic;

namespace UnityStandardAssets.Water
{
    //[ExecuteInEditMode]
    public class TextureNoise : MonoBehaviour
    {
        public string TextureName = "_MainTex";
        public bool scollx=false;
        public bool scrolly=false;
        public bool scrollxy=true;

        public float scrollSpeed = -0.1f;
        public Renderer r;
        public Material[] Materials;
        public int SelectedMaterialNo;
        Vector2 textureOffset;
        private void Start()
        {
            if (!r)
                r = GetComponent<Renderer>();
            Materials = r.materials;
        }
        void FixedUpdate()
        {
            if(scollx)
                  textureOffset = new Vector2((Time.time * scrollSpeed),0);
            else if(scrolly)
                textureOffset = new Vector2(0, (Time.time * scrollSpeed));
            else if(scrollxy)
                textureOffset = new Vector2((Time.time * scrollSpeed), (Time.time * scrollSpeed));



           // textureOffset = new Vector2((Time.time * scrollSpeed), (Time.time * scrollSpeed));
            Materials[SelectedMaterialNo].SetTextureOffset(TextureName, textureOffset);




        }
    }
}