using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions
{
    [Category("GameObject")]
    [Description("A combination of line of sight and view angle check for 2D")]
    public class CanSeeTarget2D : ConditionTask<Transform>
    {

        [RequiredField]
        public BBParameter<GameObject> target;
        [Tooltip("Distance within which to look out for.")]
        public BBParameter<float> maxDistance = 50;
        [Tooltip("A layer mask to use for line of sight check.")]
        public BBParameter<LayerMask> layerMask = (LayerMask)(-1);
        [Tooltip("Distance within which the target can be seen (or rather sensed) regardless of view angle.")]
        public BBParameter<float> awarnessDistance = 0f;
        [SliderField(1, 180)]
        public BBParameter<float> viewAngle = 70f;
        public Vector2 offset;

        private RaycastHit2D hit;

        protected override string info
        {
            get { return "Can See " + target; }
        }

        protected override bool OnCheck()
        {
            var t = target.value.transform;

            if (!t.gameObject.activeInHierarchy)
            {
                return false;
            }

            Vector2 agentPosition = agent.position + (Vector3)offset;
            Vector2 targetPosition = t.position + (Vector3)offset;

            // Awarness distance check
            if (Vector2.Distance(agentPosition, targetPosition) <= awarnessDistance.value)
            {
                hit = Physics2D.Linecast(agentPosition, targetPosition, layerMask.value);
                if (hit.collider != null && hit.collider != t.GetComponent<Collider2D>())
                {
                    return false;
                }
                return true;
            }

            // Max distance check
            if (Vector2.Distance(agentPosition, targetPosition) > maxDistance.value)
            {
                return false;
            }

            // View angle check
            Vector2 directionToTarget = targetPosition - agentPosition;
            float angle = Vector2.Angle(directionToTarget, agent.right); // agent.right is the forward direction in 2D
            if (angle > viewAngle.value)
            {
                return false;
            }

            // Line of sight check
            hit = Physics2D.Linecast(agentPosition, targetPosition, layerMask.value);
            if (hit.collider != null && hit.collider != t.GetComponent<Collider2D>())
            {
                return false;
            }

            return true;
        }

        public override void OnDrawGizmosSelected()
        {
            if (agent != null)
            {
                Vector2 agentPosition = agent.position + (Vector3)offset;

                Gizmos.DrawLine(agent.position, agentPosition);
                Gizmos.DrawLine(agentPosition, agentPosition + (Vector2)(agent.right * maxDistance.value));
                Gizmos.DrawWireSphere(agentPosition + (Vector2)(agent.right * maxDistance.value), 0.1f);
                Gizmos.DrawWireSphere(agentPosition, awarnessDistance.value);

                // Draw view angle as a line
                float angleLeft = viewAngle.value / 2;
                float angleRight = -viewAngle.value / 2;

                Vector2 leftDir = Quaternion.Euler(0, 0, angleLeft) * agent.right;
                Vector2 rightDir = Quaternion.Euler(0, 0, angleRight) * agent.right;

                Gizmos.DrawLine(agentPosition, agentPosition + leftDir * maxDistance.value);
                Gizmos.DrawLine(agentPosition, agentPosition + rightDir * maxDistance.value);
            }
        }
    }
}
