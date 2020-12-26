using System;
using GameCanvas;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace GameLightTile
{
  public class LightTile
  {
    public int length = 160;
    public int x;
    public int y;
    public bool isTurnedOn;

    public LightTile(int x, int y, bool isTurnedOn)
    {
      this.x = x;
      this.y = y;
      this.isTurnedOn = isTurnedOn;
    }

    public void OnPressed()
    {
      isTurnedOn = !isTurnedOn;
    }
  }
}
