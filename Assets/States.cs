class DogFollowPlayerState : State
{
    public override void Enter()
    {
        owner.GetComponent<FollowPlayerController>().enabled = true;
    }

    public override void Think()
    {
        
    }

    public override void Exit()
    {
        owner.GetComponent<FollowPlayerController>().enabled = false;
    }
}

class DogChaseBallState : State
{
    public override void Enter()
    {
        owner.GetComponent<FollowBallController>().enabled = true;
    }

    public override void Think()
    {
        
    }

    public override void Exit()
    {
        owner.GetComponent<FollowBallController>().enabled = false;
    }
}