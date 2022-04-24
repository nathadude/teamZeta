public class Timer
{
    private float duration;
    private float alarm;

    public Timer(float durationNum, float alarmNum = 0)
    {
        duration = durationNum;
        alarm = alarmNum;
    }

    public void Reset()
    {
        alarm = duration;
    }

    public void Tick(float deltaTime)
    {
        alarm -= deltaTime;

        if (alarm < 0)
        {
            alarm = 0;
        }
    }

    public bool RunOnAlarm(float deltaTime)
    {
        if (alarm > 0)
        {
            Tick(deltaTime);

            if (alarm == 0)
            {
                return true;
            }
        }

        return false;
    }

    public bool RunOnAlarmRepeat(float deltaTime)
    {
        if (alarm == 0) alarm = duration;

        return RunOnAlarm(deltaTime);
    }

    public void SetDuration(float num)
    {
        duration = num;
    }

    public void SetAlarm(float num)
    {
        alarm = num;
    }
}