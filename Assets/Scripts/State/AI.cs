public class AI
{
    public enum ai_states
    {
        idle = 0,
        patrolling,
        hunting,
        searching,
        chasing,
        attacking,
    }

    public ai_states CurrentState = ai_states.idle;
}