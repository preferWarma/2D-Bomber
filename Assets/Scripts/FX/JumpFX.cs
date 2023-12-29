using UnityEngine;

namespace FX
{
    public class JumpFX : MonoBehaviour
    {
        public void Finish()
        {
            gameObject.SetActive(false);
        }
    }
}
