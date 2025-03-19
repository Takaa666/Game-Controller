using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Custom/Movement")]
    [Description("Moves the agent towards the target per frame without pathfinding, plays the walking animation, and flips the sprite based on direction.")]
    public class MoveTowards : ActionTask<Transform>
    {
        [RequiredField]
        public BBParameter<GameObject> target;
        public BBParameter<float> speed = 2;
        public BBParameter<float> stopDistance = 0.1f;
        public BBParameter<string> walkAnimationName = "IsWalking"; // Nama parameter animasi berjalan
        public bool waitActionFinish;

        private Animator animator;
        private Transform agentTransform;

        protected override void OnExecute()
        {
            animator = agent.GetComponent<Animator>();
            agentTransform = agent;

            if (animator != null)
            {
                animator.SetBool(walkAnimationName.value, false);
            }
        }

        protected override void OnUpdate()
        {
            float distance = (agent.position - target.value.transform.position).magnitude;

            if (distance <= stopDistance.value)
            {
                if (animator != null)
                {
                    animator.SetBool(walkAnimationName.value, false);
                }
                EndAction();
                return;
            }

            agent.position = Vector3.MoveTowards(agent.position, target.value.transform.position, speed.value * Time.deltaTime);

            if (animator != null)
            {
                animator.SetBool(walkAnimationName.value, true);
            }

            FlipSprite();

            if (!waitActionFinish)
            {
                EndAction();
            }
        }

        private void FlipSprite()
        {
            Vector3 direction = (target.value.transform.position - agent.position).normalized;

            if (direction.x > 0)
            {
                agentTransform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.x < 0)
            {
                agentTransform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
