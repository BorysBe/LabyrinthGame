using UnityEngine;

namespace Scripts
{
    public class Ball : MonoBehaviour
    {
       
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            Move(new Vector3(1,0,0));
        }

        public void Move(Vector3 vector)
        {
            transform.position += vector;
        }

    
    }
}
