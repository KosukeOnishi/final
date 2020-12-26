using System;

namespace GameTimer
{
  public class Timer
  {
    public int current = 0;
    private int _frames = 0;
    public bool isTimerOn = false;
    // public int Current
    // {
    //   get { return _current; }
    //   set { _current = value; }
    // }

    public void Start()
    {
      isTimerOn  = true;
    }

    public void Stop()
    {
      isTimerOn = false;
    }

    public void Reset()
    {
      isTimerOn = false;
      current = 0;
      _frames = 0;
    }

    public void Update()
    {
      if (isTimerOn)
      {
        _frames ++;
        if (_frames % 60 == 0)
        {
          current ++;
          _frames = 0;
        }
      }
    }
  }
}
