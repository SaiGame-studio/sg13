public abstract class TxtUpdate : TextAbstact
{
    protected virtual void FixedUpdate()
    {
        this.ShowingText();
    }

    protected abstract void ShowingText();
}
