using UnityEngine;

public class NotifyFloatingTextAnimationFinished : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FloatingText floatingText = animator.GetComponent<FloatingText>();

        if (floatingText == null)
        {
            return;
        }

        floatingText.Hide();
    }
}
