using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static class Evt
{
    public static readonly Evt GameStarted = new Evt();
    public static readonly Evt GamePaused = new Evt();
    public static readonly Evt GameEnded = new Evt();
}

public class Evt
{
    public event Action Action = delegate { };

    public void Invoke()
    {
        Action.Invoke(enemy);
    }

    public void UpsertListener(Action<Enemy> listener)
    {
        Action -= listener;
        Action += listener;
    }

    public void RemoveListener(Action<Enemy> listener)
    {
        Action -= listener;
    }
}
