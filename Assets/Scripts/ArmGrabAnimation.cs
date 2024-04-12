using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Get the Animator component attached to the object
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the "E" key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("test");
            // Trigger the animation
            animator.SetTrigger("Active");
        }
    }
}
