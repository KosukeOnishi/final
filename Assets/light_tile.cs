using System;
using GameCanvas;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace GameLightTile
{
  public class LightTile
  {
    public int length = 190;
    public int x;
    public int y;
    public bool isTurnedOn;
    public bool initial;

    public LightTile(int x, int y, bool isTurnedOn)
    {
      this.x = x;
      this.y = y;
      this.isTurnedOn = isTurnedOn;
      this.initial = isTurnedOn;
    }

    public void Reset()
    {
      isTurnedOn = initial;
    }

    public void OnPressed()
    {
      isTurnedOn = !isTurnedOn;
    }
  }
}
