using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Movement/Custom")]
    [Description("Moves the agent towards the target per frame without pathfinding, plays walking animation, and flips sprite based on direction.")]
    public class CustomMoveTowards : ActionTask<Transform>
    {
        [RequiredField]
        public BBParameter<GameObject> target;
        public BBParameter<float> speed = 2f;
        public BBParameter<float> stopDistance = 0.1f;
        public bool waitActionFinish;

        // Add a reference to the Animator
        private Animator animator;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking"); // Name of the animation parameter
        private Transform agentTransform;

        protected override void OnExecute()
        {
            // Get the Animator component attached to the agent
            animator = agent.GetComponent<Animator>();
            agentTransform = agent;

            // Ensure the animator starts in the idle state
            if (animator != null)
            {
                animator.SetBool(IsWalking, false);
            }
        }

        protected override void OnUpdate()
        {
            float distance = (agent.position - target.value.transform.position).magnitude;

            // Check if the agent is within the stop distance
            if (distance <= stopDistance.value)
            {
                // Stop the walking animation when reached the target
                if (animator != null)
                {
                    animator.SetBool(IsWalking, false);
                }
                EndAction();
                return;
            }

            // Move the agent towards the target
            agent.position = Vector3.MoveTowards(agent.position, target.value.transform.position, speed.value * Time.deltaTime);

            // Play the walking animation
            if (animator != null)
            {
                animator.SetBool(IsWalking, true);
            }

            // Flip the sprite based on movement direction
            FlipSprite();

            // Check if the action should wait to finish
            if (!waitActionFinish)
            {
                EndAction();
            }
        }

        private void FlipSprite()
        {
            // Calculate direction to target
            Vector3 direction = (target.value.transform.position - agent.position).normalized;

            // Check if the target is to the right of the agent
            if (direction.x > 0)
            {
                // Face right
                agentTransform.localScale = new Vector3(1, 1, 1); // Set to original scale
            }
            else if (direction.x < 0)
            {
                // Face left
                agentTransform.localScale = new Vector3(-1, 1, 1); // Flip horizontally
            }
        }
    }
}
